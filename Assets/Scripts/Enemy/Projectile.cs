using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    internal class Projectile : MonoBehaviour
    {
        [SerializeField] private float speed;
        private float damage;
        private Rigidbody2D rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        public void SetDamage(float damageAmount)
        {
            damage = damageAmount;
        }

        public void Initialize(Vector2 direction)
        {
            rb.velocity = direction * speed; 
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                collision.gameObject.GetComponent<PlayerHealth>()?.TakeDamage(damage);
                Destroy(this.gameObject);
            }
            if (collision.gameObject.CompareTag("Wall"))
            {
                Destroy(this.gameObject);
            }
        }
    }
}
