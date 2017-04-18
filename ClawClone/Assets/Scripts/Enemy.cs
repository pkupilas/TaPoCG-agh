using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private bool _isPlayerSpottedOnBack = false;
    private bool _isPlayerSpottedOnFront = false;
    private Animator _animator;
    private Rigidbody2D _rigidbody;

    [SerializeField] private GameObject[] _spottingPoints;
    [SerializeField] private float _damage = -10f;
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
        Debug.DrawLine(transform.position, _spottingPoints[0].transform.position, Color.red);
        Debug.DrawLine(transform.position, _spottingPoints[1].transform.position, Color.red);
        /*SPOTTING PLAYER:
         * - chceck distance between sprites
         * - use fancy stuff -> Linecast checking
         */
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

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            var player = other.gameObject.GetComponent<Player>();
            //player.TakeDamage(_damage);
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
        _animator.SetBool("isWalking", false);
        _animator.SetBool("isAttacking", true);
    }

    private void Move()
    {
        // Change direction of moving after distance
        if (Mathf.Abs(transform.position.x - _lastIdlePosition) > _distance)
        {
            _animator.SetBool("isWalking", false);
            _lastIdlePosition = transform.position.x;
            _rigidbody.velocity = Vector2.zero;
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

    private void Turn()
    {
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
