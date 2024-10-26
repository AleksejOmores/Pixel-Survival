using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public Transform playerPosition;
    [SerializeField] private int speed;
    private Animator anim;
    private Rigidbody2D rb;
    private Vector2 moveEnemy;
    private SpriteRenderer sR;
    private GameObject player;
    [SerializeField] private float damage;
    private void Awake()
    {
        player = GameObject.Find("Player");
        GameObject character = GameObject.Find("Player");
        playerPosition = character.transform;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sR = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        TurnEnemyMove();
        float distancePlayer = Vector2.Distance(this.transform.position, playerPosition.position);
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
            player.GetComponent<PlayerHealth>().TakeDamage(damage);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player.GetComponent<PlayerHealth>().TakeDamage(damage);
        }
    }
}
