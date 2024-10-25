using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    private Vector2 move;
    private Rigidbody2D rb;
    public Animator anim;
    public SpriteRenderer sR;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    void Start()
    {
        sR = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        PlayerInput();
    }
    private void FixedUpdate()
    {
        Movement();
        PlayerFacingDirection();
    }
    void Movement()
    {
        move.x = Input.GetAxis("Horizontal");
        move.y = Input.GetAxis("Vertical");
        move.Normalize();
        if (move != Vector2.zero)
        {
            anim.SetBool("isMove", true);

            rb.MovePosition(rb.position + move.normalized * (moveSpeed * Time.fixedDeltaTime));
        }
        else
        {
            anim.SetBool("isMove", false);

            rb.velocity = Vector2.zero;
        }
    }
    void PlayerInput()
    {
        anim.SetFloat("moveX", move.x);
        anim.SetFloat("moveY", move.y);
    }
    void PlayerFacingDirection()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);

        if (mousePos.x < playerScreenPoint.x)
            sR.flipX = true;
        else
            sR.flipX = false;
    }
}
