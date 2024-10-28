using Assets.Scripts.Enemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveSoul : MonoBehaviour
{
    public Transform playerPosition;
    public static EnemyMoveSoul Instance;
    [SerializeField] private int speed;
    private Animator anim;
    private Rigidbody2D rb;
    private Vector2 moveEnemy;
    private SpriteRenderer sR;
    private GameObject player;
    private Knockback knockback;
    private EnemySpawner enemySpawner;
    private bool hasNotified = false;
    [SerializeField] private GameObject projectilePrefab; 
    [SerializeField] private float attackRange = 7f;
    [SerializeField] private float attackCooldown = 1.5f;
    private bool isAttacking = false;
    private Coroutine attackCoroutine;
    private Vector2 targetDirection;
    private void Awake()
    {
        Instance = this;
        knockback = GetComponent<Knockback>();
        player = GameObject.Find("Player");
        GameObject character = GameObject.Find("Player");
        playerPosition = character.transform;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sR = GetComponent<SpriteRenderer>();
        enemySpawner = FindObjectOfType<EnemySpawner>();
    }

    void Update()
    {
        TurnEnemyMove();

        float distancePlayer = Vector2.Distance(this.transform.position, playerPosition.position);
        if (distancePlayer <= 10f && !hasNotified)
        {
            enemySpawner?.NotifyObserver("Враг c дальней атакой приближается к игроку");
            hasNotified = true;
        }
        else if (distancePlayer > 10f)
        {
            hasNotified = false;
        }

        if (distancePlayer <= attackRange && !isAttacking)
        {
            attackCoroutine = StartCoroutine(PerformAttack());
        }
    }

    private void FixedUpdate()
    {
        if (knockback.gettingKnockedBack) return;

        EnemyMovement();
    }

    void EnemyMovement()
    {
        Vector2 playerPos = (playerPosition.position - transform.position).normalized;
        moveEnemy = playerPos;
        rb.velocity = playerPos * speed;

    }

    void TurnEnemyMove()
    {
        if (moveEnemy.x < moveEnemy.y)
            sR.flipX = true;
        else
            sR.flipX = false;

        if (gameObject.CompareTag("flipXEnemy"))
        {
            if (moveEnemy.x < moveEnemy.y)
                sR.flipX = false;
            else
                sR.flipX = true;
        }
    }
    private IEnumerator PerformAttack()
    {
        isAttacking = true;
        rb.velocity = Vector2.zero; 
        anim.SetBool("isAttack", true);

        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);

        // Execute ranged attack
        StartRangedAttack();

        anim.SetBool("isAttack", false);
        yield return new WaitForSeconds(attackCooldown);

        isAttacking = false;
    }
    void StartRangedAttack()
    {
        anim.SetBool("isAttack", true);
        Vector2 playerPositionAtAttack = playerPosition.position;
        Vector2 targetDirection = (playerPositionAtAttack - (Vector2)transform.position).normalized;
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        projectile.GetComponent<Projectile>().Initialize(targetDirection);
        projectile.GetComponent<Projectile>().SetDamage(20);
    }
}
