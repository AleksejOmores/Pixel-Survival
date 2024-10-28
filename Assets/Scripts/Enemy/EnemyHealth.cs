using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float health;
    public Animator anim;
    private Rigidbody2D rb;
    public int damage = 25;
    private Knockback knockback;
    private Flash flash;
    [SerializeField] private GameObject woodPrefab;
    private EnemySpawner enemySpawner;

    private void Awake()
    {
        flash = GetComponent<Flash>();
        knockback = GetComponent<Knockback>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void SetEnemySpawner(EnemySpawner spawner)
    {
        enemySpawner = spawner;
    }

    public void TakeDamage()
    {
        health -= damage;
        knockback.GetKnockedBack(movement.Instance.transform, 15f);
        StartCoroutine(flash.FlashEnemyRoutine());
    }

    public void Die()
    {
        if (health <= 0)
        {
            float randomWood = Random.Range(1, 4);

            for (float wood = randomWood; wood < 4; wood++)
            {
                Instantiate(woodPrefab, woodPrefab.transform.position, Quaternion.identity);
            }

            anim.SetBool("isDead", true);
            rb.velocity = Vector2.zero;
            gameObject.GetComponent<EnemyMove>().enabled = false;
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            Invoke("DestroyObject", 3f);
        }
    }
    void DestroyObject()
    {
        enemySpawner?.RemoveEnemy(this.gameObject);
        Destroy(this.gameObject);
    }
}
