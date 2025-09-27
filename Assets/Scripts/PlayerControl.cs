using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerControl : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 5f;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] Transform Weapon0;
    [SerializeField] float fireRate = 0.2f;
    private Rigidbody2D _rigidbody;

    private float xMin = -8.15f;
    private float xMax = 8.15f;
    private float yMin = -4.35f;
    private float yMax = 4.35f;

    private InputSystem_Actions controls;
    private Vector2 _movementInput;
    private bool isShooting = false;
    private float nextFireTime = 0f;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        controls = new InputSystem_Actions();
    }
    private void OnEnable()
    {
        controls.Player.Shoot.Enable();
        controls.Player.Shoot.performed += ctx => isShooting = true;
        controls.Player.Shoot.canceled += ctx => isShooting = false;
    }

    private void OnDisable()
    {
        controls.Player.Shoot.Disable();
    }

    private void Update()
    {
        if (isShooting && Time.time >= nextFireTime)
        {
            FireProjectile();
            nextFireTime = Time.time + fireRate;
        }
    }
    private void FixedUpdate()
    {
        _rigidbody.linearVelocity = _movementInput * _movementSpeed;

        Vector2 clampedPos = _rigidbody.position;
        clampedPos.x = Mathf.Clamp(clampedPos.x, xMin, xMax);
        clampedPos.y = Mathf.Clamp(clampedPos.y, yMin, yMax);

        if (Mathf.Approximately(clampedPos.x, xMin) && _movementInput.x < 0 ||
            Mathf.Approximately(clampedPos.x, xMax) && _movementInput.x > 0)
        {
            _rigidbody.linearVelocity = new Vector2(0, _rigidbody.linearVelocity.y);
        }
        if (Mathf.Approximately(clampedPos.y, yMin) && _movementInput.y < 0 ||
            Mathf.Approximately(clampedPos.y, yMax) && _movementInput.y > 0)
        {
            _rigidbody.linearVelocity = new Vector2(_rigidbody.linearVelocity.x, 0);
        }
        _rigidbody.position = clampedPos;
    }

    private void OnMove(InputValue inputValue)
    {
        _movementInput = inputValue.Get<Vector2>();
    }

    void FireProjectile()
    {
        Instantiate(projectilePrefab, Weapon0.position, Weapon0.rotation);
    }
}
