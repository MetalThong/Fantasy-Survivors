using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementComponent
{
    private Vector2 _moveValue;
    private float _moveSpeed = 3f;
    private Rigidbody2D _rb;
    private Transform _transform;
    public MovementComponent(Rigidbody2D rb, Transform transform)
    {
        _rb = rb;
        _transform = transform;
    }

    public void Move()
    {
        Vector2 newPos = (Vector2)_rb.position + _moveValue * Time.deltaTime * _moveSpeed;
        _rb.MovePosition(newPos);
    }

    public void FlipSprite()
    {
        Vector3 left = new Vector3(-1, 1, 1);
        Vector3 right = new Vector3(1, 1, 1);
        if (_moveValue.x > 0)
        {
            _transform.localScale = right;
        }
        else if (_moveValue.x < 0)
        {
            _transform.localScale = left;
        }
    }

    public void SetInput(Vector2 input)
    {
        _moveValue = input;
    }

    public void IncreaseMoveSpeed(float value)
    {
        _moveSpeed += value;
    }
}
