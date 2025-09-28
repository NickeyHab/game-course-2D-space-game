using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float maxHealth = 5f;
    private float health;
    private ParticleSystem particleSystem;
    void Start()
    {
        health = maxHealth;
        particleSystem = GetComponentInChildren<ParticleSystem>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy Projectile")
        {
            Projectile projectile = other.gameObject.GetComponent<Projectile>();
            health -= projectile.damage;
            if (health <= 0)
            {
                Death();
            }
            Destroy(other.gameObject);
        }
    }
    private void Death()
    {
        particleSystem.transform.parent = null;
        particleSystem.Play();
        BroadcastMessage("OnDeath");
        Destroy(particleSystem.gameObject, particleSystem.main.duration + particleSystem.main.startLifetime.constantMax);
        Destroy(gameObject);
    }
}
