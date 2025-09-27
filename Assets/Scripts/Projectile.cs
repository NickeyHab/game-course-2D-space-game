using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 8f;
    [SerializeField] private float _damage = 1f;
    public float damage { get; private set; }
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = speed * transform.up;
        damage = _damage;
        Invoke("DestroySelf", 5);
    }

    private void OnBecameInvisible()
    {
        DestroySelf();
    }
    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}
