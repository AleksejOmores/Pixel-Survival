using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public Transform playerPosition;
    public static EnemyMove Instance;
    [SerializeField] private int speed;
    private Animator anim;
    private Rigidbody2D rb;
    private Vector2 moveEnemy;
    private SpriteRenderer sR;
    private GameObject player;
    private Knockback knockback;
    [SerializeField] private float damage; 
    private bool isAttackBlock = false;
    private EnemySpawner enemySpawner;
    private bool hasNotified = false;
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
        if (distancePlayer <= 6.1f && !hasNotified)
        {
            enemySpawner?.NotifyObserver("Враг приближается к игроку");
            hasNotified = true;
        }
        else if (distancePlayer > 6f)
        {
            hasNotified = false;
        }
        if (distancePlayer <= 2f)
        {
            Invoke("StartAttack", 0.5f);
        }
        else
        {
            anim.SetBool("isAttack", false);
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

    void StartAttack()
    {
        anim.SetBool("isAttack", true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Barrier"))
        {
            isAttackBlock = true;
            StartCoroutine(RepeatAttackOnBlock(collision.gameObject));
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Barrier"))
        {
            isAttackBlock = false;
        }
    }

    private IEnumerator RepeatAttackOnBlock(GameObject block)
    {
        while (isAttackBlock)
        {
            block.GetComponent<HealthBox>()?.TakeDamage();
            Invoke("StartAttack", 0.5f);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
