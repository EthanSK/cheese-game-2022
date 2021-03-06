using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ETGgames.Extensions;


public class EnemyMovement : MonoBehaviour
{
    public bool IsTouchingJumpable => _rigidbody.IsTouchingLayers(_whatIsJumpable);


    [SerializeField] private float _speed;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _gravity;
    [SerializeField] private float _jumpOutOfStuckForce;

    [SerializeField] private float _speedConsideredIdle;
    [SerializeField] private LayerMask _whatIsJumpable; //what is the enemy allowed to jump off.



    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {



        _rigidbody.AddRelativeForce(new Vector2(_speed, 0f), ForceMode2D.Force);

        LocalGravity();

        Vector2 vel = _rigidbody.velocity;
        if (Mathf.Abs(vel.x) > _maxSpeed)
            _rigidbody.velocity = vel.WithX(Mathf.Clamp(vel.x, -_maxSpeed, _maxSpeed));

        JumpIfNeeded();




    }

    private void LocalGravity()
    {
        var closestPoint = GetComponentInParent<Cheese>().GetComponent<Collider2D>().ClosestPoint(transform.position);
        var gravityVec = _gravity * ((Vector2)transform.position - closestPoint);

        _rigidbody.AddForce(gravityVec, ForceMode2D.Force);
    }

    private void JumpIfNeeded()
    {
        if (
            Mathf.Abs(_rigidbody.velocity.x) < _speedConsideredIdle &&
            IsTouchingJumpable
        )
        {
            _rigidbody.AddRelativeForce(new Vector2(0f, _jumpOutOfStuckForce), ForceMode2D.Impulse);
        }
    }
}
