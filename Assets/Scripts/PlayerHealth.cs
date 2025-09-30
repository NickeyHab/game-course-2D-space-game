using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float maxHealth = 5f;
    private float health;
    private ParticleSystem particles;
    void Start()
    {
        health = maxHealth;
        particles = GetComponentInChildren<ParticleSystem>();
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
        particles.transform.parent = null;
        particles.Play();
        Destroy(particles.gameObject, particles.main.duration + particles.main.startLifetime.constantMax);

        BroadcastMessage("OnDeath");
        GameManager.Instance.RestartOnDeath();

        Destroy(gameObject);
    }
}
