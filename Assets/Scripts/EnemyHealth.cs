using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float maxHealth = 5f;
    private float health;
    private Animator animator;
    private ParticleSystem particleSystem;

    void Start()
    {
        health = maxHealth;
        animator = GetComponent<Animator>();
        particleSystem = GetComponentInChildren<ParticleSystem>();
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
            particleSystem.Play();
            BroadcastMessage("OnDeath");
        }
        else
        {
            particleSystem.transform.parent = null;
            particleSystem.Play();
            BroadcastMessage("OnDeath");
            Destroy(particleSystem.gameObject, particleSystem.main.duration + particleSystem.main.startLifetime.constantMax);
            DestroySelf();
        }
    }
    private void DestroySelf()
    {
        Destroy(gameObject, 0.1f);
    }
}
