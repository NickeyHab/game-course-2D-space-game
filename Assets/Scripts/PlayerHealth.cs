using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float maxHealth = 5f;
    private float health;
    void Start()
    {
        health = maxHealth;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy Projectile")
        {
            Projectile projectile = other.gameObject.GetComponent<Projectile>();
            health -= projectile.damage;
            if (health <= 0)
            {
                Destroy(gameObject);
            }
            Destroy(other.gameObject);
        }
    }
}
