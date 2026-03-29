using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerEntity : MonoBehaviour
{
    private MovementComponent _movementComponent;
    private WeaponComponent _weaponComponent;
    private StatComponent _statComponent;
    private CircleCollider2D _circleCollider;

    [SerializeField] FloatingJoystick joystick;
    [SerializeField] Rigidbody2D rb;

    [SerializeField] float maxHealth;
    [SerializeField] float pickupRange;
    [SerializeField] float currentHealth;

    [SerializeField] List<Weapon> weapons;

    private List<Enemy> _nearbyEnemies;

    private void Awake()
    {
        _nearbyEnemies = new List<Enemy>();
        _movementComponent = new MovementComponent(rb, transform);
        _weaponComponent = new WeaponComponent(weapons);
        _statComponent = new StatComponent(maxHealth, currentHealth, pickupRange);
        _circleCollider = gameObject.AddComponent<CircleCollider2D>();
        _circleCollider.isTrigger = true;
        _circleCollider.radius = 5f;
    }

    private void FixedUpdate()
    {
        _nearbyEnemies.RemoveAll(e => e == null);
        _movementComponent.Move();
        _movementComponent.FlipSprite();
        _movementComponent.SetInput(joystick.Direction);
        if(_nearbyEnemies != null && _nearbyEnemies.Count > 0) WeaponManager.Instance.Tick(_nearbyEnemies);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            _nearbyEnemies.Add(enemy);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (_nearbyEnemies.Contains(enemy))
        {
            _nearbyEnemies.Remove(enemy);
        }
    }
}
