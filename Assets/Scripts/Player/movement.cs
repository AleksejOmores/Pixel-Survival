using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class movement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    public static movement Instance { get; private set; }
    private Vector2 move;
    private Rigidbody2D rb;
    public Animator anim;
    public SpriteRenderer sR;
    [SerializeField] private int damage;
    [SerializeField] public Transform slashCollider;
    private Knockback knockback;
    private BlockManager blockManager;
    private void Awake()
    {
        blockManager = GetComponent<BlockManager>();
        Instance = this;
        knockback = GetComponent<Knockback>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sR = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        PlayerInput();
        PlayerFacingDirection();
    }
    private void FixedUpdate()
    {
        if (!knockback.gettingKnockedBack)

        Movement();
    }
    void Movement()
    {
        move.Normalize();
        anim.SetBool("isMove", move != Vector2.zero);
        rb.velocity = move * moveSpeed;
    }
    void PlayerInput()
    {
        move.x = Input.GetAxis("Horizontal");
        move.y = Input.GetAxis("Vertical");
        anim.SetFloat("moveX", move.x);
        anim.SetFloat("moveY", move.y);
    }
    void PlayerFacingDirection()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        sR.flipX = mousePos.x < transform.position.x;
        slashCollider.localRotation = Quaternion.Euler(0, sR.flipX ? 180 : 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("wood"))
        {
            Destroy(collision.gameObject);
            blockManager.UpWood(1);
        }
    }
}
