using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    private Animator _animator;
    private Rigidbody2D _rigidbody;

    [SerializeField] private float _moveAcceleration = 50f;
    [SerializeField] private float _distance = 4f;                  // Value how far enemy can walk
    private float _lastIdlePosition;
    private Vector2 _direction = Vector2.right;

    // Use this for initialization
    void Start ()
	{
	    _animator = GetComponent<Animator>();
	    _rigidbody = GetComponent<Rigidbody2D>();
	    _lastIdlePosition = transform.position.x;
	}
	
	// Update is called once per frame
	void Update ()
	{
	    Move();
	}

    private void Move()
    {
        // Change direction of moving after distance
        if (Mathf.Abs(transform.position.x - _lastIdlePosition) > _distance)
        {
            _animator.SetBool("isWalking", false);
            _lastIdlePosition = transform.position.x;
            _rigidbody.velocity = Vector2.zero;
            RotateEnemy();
            ChangeDirectionVector();
        }

        // Set velocity when walking animation is enabled
        if(_animator.GetBool("isWalking"))
        {
            _rigidbody.velocity = _direction * Time.deltaTime * _moveAcceleration;
        }
    }

    // Used in EnemyIdle animation as animation event 
    private void SetIsWalkingBool()
    {
        _animator.SetBool("isWalking", true);
    }

    private void RotateEnemy()
    {
        transform.eulerAngles = _direction.x < 0 ? new Vector2(0, 0) : new Vector2(0, 180);
    }

    private void ChangeDirectionVector()
    {
        _direction = _direction == Vector2.left ? Vector2.right : Vector2.left;
    }
}
