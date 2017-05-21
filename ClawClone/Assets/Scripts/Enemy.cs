using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody2D _rigidbody;
    private Player _player;

    private GameObject _waypointOfInterest;
    private Vector2 _direction = Vector2.right;
    private bool _isPlayerSpottedOnBack;
    private bool _isPlayerSpottedOnFront;

    [SerializeField] private GameObject[] _patrolWaypoints;         // Waypoints between which enemy can walk {PointA, PointB}
    [SerializeField] private GameObject[] _spottingPoints;          // Back and Front vision {BackVision, FrontVision}
    [SerializeField] private float _damage = -10f;
    [SerializeField] private float _moveAcceleration = 50f;

    // Use this for initialization
    void Start ()
	{
	    _animator = GetComponent<Animator>();
	    _rigidbody = GetComponent<Rigidbody2D>();
	    _player = FindObjectOfType<Player>();
	    _waypointOfInterest = _patrolWaypoints[1];
	}
	
	// Update is called once per frame
	void Update ()
    {
        LookForPlayer();
        
        if (_isPlayerSpottedOnBack || _isPlayerSpottedOnFront)
        {
            if (_isPlayerSpottedOnBack)
            {
                Turn();
            }

	        Attack();
	    }
	    else
	    {
            Move();
        }
	}

    private void LookForPlayer()
    {
        _isPlayerSpottedOnBack = Physics2D.Linecast(transform.position, _spottingPoints[0].transform.position,
            1 << LayerMask.NameToLayer("Player"));

        _isPlayerSpottedOnFront = Physics2D.Linecast(transform.position, _spottingPoints[1].transform.position,
            1 << LayerMask.NameToLayer("Player"));
    }

    private void Attack()
    {
        if (!_player.IsDead)
        {
            _animator.SetBool("isWalking", false);
            _animator.SetBool("isAttacking", true);
        }
    }

    private void Move()
    {
        // Change direction of moving after reaching waypoint of interest
        if (Mathf.Abs(transform.position.x - _waypointOfInterest.transform.position.x) < 0.1)
        {
            _animator.SetBool("isWalking", false);
            Turn();
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

    // Used in EnemyAttack animation as animation event 
    private void UnsetIsAttackingBool()
    {
        _animator.SetBool("isAttacking", false);
    }

    // Used in EnemyAttack animation as animation event 
    private void DealDamage()
    {
        _player.ChangePlayerHealth(_damage);
    }

    private void Turn()
    {
        _rigidbody.velocity = Vector2.zero;
        _waypointOfInterest = _waypointOfInterest == _patrolWaypoints[0] ? _patrolWaypoints[1] : _patrolWaypoints[0];
        RotateEnemy();
        ChangeDirectionVector();
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
