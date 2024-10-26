using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float health;
    private float currentHealth;
    public GameObject enemy;

    private void Awake()
    {
        enemy = GetComponent<GameObject>();
        currentHealth = health;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
    }
}
