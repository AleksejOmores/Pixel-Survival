using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Image health;
    private float damage;
    public Animator anim;
    public GameObject enemy;
    void Start()
    {
        health = GetComponent<Image>();
    }
    public void TakeDamage(float damage)
    {
        health.fillAmount -= damage;
        Debug.Log(damage);
        Debug.Log(health.fillAmount);

        if (health.fillAmount <= 0)
        {
            anim.SetBool("StopAttack", true);
            anim.SetBool("isDead", true);
            gameObject.GetComponent<movement>().enabled = false;
            gameObject.GetComponent<Rigidbody2D>().freezeRotation = true;
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
            Invoke("ExitGame", 5f);
        }
    }

    void ExitGame()
    {
        UnityEditor.EditorApplication.isPlaying = false;

        Application.Quit();
    }
}
