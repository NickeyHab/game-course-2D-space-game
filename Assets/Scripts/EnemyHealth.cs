using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float maxHealth = 5f;
    private float health;
    private Animator animator;
    private ParticleSystem particles;

    void Start()
    {
        health = maxHealth;
        animator = GetComponent<Animator>();
        particles = GetComponentInChildren<ParticleSystem>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Projectile")
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
        if (gameObject.CompareTag("Boss"))
        {
            animator.SetTrigger("Death");
            particles.Play();
            BroadcastMessage("OnDeath");
        }
        else
        {
            particles.transform.parent = null;
            particles.Play();
            BroadcastMessage("OnDeath");
            Destroy(particles.gameObject, particles.main.duration + particles.main.startLifetime.constantMax);
            DestroySelf();
        }
    }
    private void DestroySelf()
    {
        Destroy(gameObject, 0.1f);
    }
}
