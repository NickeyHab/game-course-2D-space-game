using Unity.Mathematics;
using UnityEngine;
public class EnemyWeapon : MonoBehaviour
{
    [SerializeField] private bool hasTarget = false;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform weapon;
    [SerializeField] private float fireSpeed = 1f;
    [SerializeField] private float fireDelay = 1f;
    private Transform target;
    private bool isActive = true;

    private void Start()
    {
        if (hasTarget)
            target = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void Update()
    {
        if (!isActive) return;
        if (target)
        {
            Vector2 direction = (weapon.position - target.position);
            weapon.rotation = Quaternion.FromToRotation(Vector3.down, direction);
        }
    }
    private void OnBecameVisible()
    {
        InvokeRepeating("Shoot", fireDelay, fireSpeed);
    }

    private void OnBecameInvisible()
    {
        CancelInvoke(nameof(Shoot));
        Destroy(gameObject);
    }
    private void Shoot()
    {
        if (!isActive) return;
        Instantiate(projectilePrefab, weapon.position, weapon.rotation);
    }
    private void OnDeath()
    {
        isActive = false;
    }
}
