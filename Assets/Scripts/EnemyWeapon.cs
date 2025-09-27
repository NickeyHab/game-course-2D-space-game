using UnityEngine;
public class EnemyWeapon : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform weapon;
    [SerializeField] private float fireSpeed = 1f;
    [SerializeField] private float fireDelay = 1f;
    private Renderer renderer;

    private void Start()
    {
        renderer = GetComponent<Renderer>();
    }
    private void OnBecameVisible()
    {
        InvokeRepeating("Shoot", fireDelay, fireSpeed);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
    private void Shoot()
    {
        if (!renderer.isVisible) return;
        Instantiate(projectilePrefab, weapon.position, weapon.rotation);
    }
}
