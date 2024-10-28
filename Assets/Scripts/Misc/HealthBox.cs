using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBox : MonoBehaviour
{
    [SerializeField] private int health = 5;

    public void TakeDamage()
    {
        health -= 1;

        if (health <= 0)
            Destroy(gameObject);
    }
}
