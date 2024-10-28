using System;
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
    private Knockback knockback;
    private Flash flash;
    private EnemySpawner enemySpawner;
    private void Awake()
    {
        flash = GetComponent<Flash>();
        knockback = GetComponent<Knockback>();
        enemySpawner = FindObjectOfType<EnemySpawner>();
    }
    public void TakeDamage(float damage)
    {
        health.fillAmount -= damage;
        knockback.GetKnockedBack(EnemyMoveSoul.Instance.transform, 10f);
        StartCoroutine(flash.FlashPlayerRoutine());

        enemySpawner?.NotifyHealthObserver((float)Math.Round(health.fillAmount * 100));
    }

    public void SetCurrentHealth(float healthSize)
    {
        health.fillAmount = healthSize;
    }
    public void Die()
    {
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
