using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPlayer : MonoBehaviour
{
    public Animator anim;
    private int animationIndex = 0;
    private float timeSinceLastClick = 0f;
    public bool isAnimating = false;
    [SerializeField] public int damage;
    private movement classMovement;
    void Awake()
    {
        anim = GetComponent<Animator>();
        classMovement = GetComponent<movement>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            classMovement.slashCollider.gameObject.SetActive(true);
            timeSinceLastClick = 0f; 
            animationIndex++; 
            anim.SetInteger("CountAttack", animationIndex); 
            isAnimating = true; 
            anim.SetBool("isAttack", true);
        }

        if (isAnimating)
        {
            timeSinceLastClick += Time.deltaTime;

            if (timeSinceLastClick >= 0.3f)
            {
                classMovement.slashCollider.gameObject.SetActive(false);
                animationIndex = 0;
                anim.SetInteger("CountAttack", animationIndex);
                anim.SetBool("isAttack", false); 
                isAnimating = false;
            }
        }
    }
}
