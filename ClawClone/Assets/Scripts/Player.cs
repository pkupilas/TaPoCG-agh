using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    public Transform groundChecker;

    private Vector2 _targetDistance;                    // way to go by player
    private float _move_acceleration = 15f;
    private float _jump_acceleration = 6f;
    private Rigidbody2D _rigidbody;
    private float _horizontalMoveInput;
    private bool _isJumping;
    private bool _grounded;

    private float health = 100;
    private float maxHealth = 100;

    private Animator _anim;
    private HealthBar healthBar;

    // Use this for initialization
    private void Start ()
	{
        _rigidbody = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        healthBar = GetComponent<HealthBar>();

        healthBar.UpdateHealthBar(100, 1);
    }

    private void Update()
    {
        //Check if player is staying on ground
        _grounded = Physics2D.Linecast(transform.position, groundChecker.position, 1 << LayerMask.NameToLayer("Ground"));
        _targetDistance = Vector2.zero;
        _horizontalMoveInput = CrossPlatformInputManager.GetAxis("Horizontal");
        _isJumping = CrossPlatformInputManager.GetButtonDown("Space");

        //init way to go
        _targetDistance = Vector2.right * Time.deltaTime * _move_acceleration * _horizontalMoveInput;

        if (_targetDistance != Vector2.zero)
        {
            RotateIfChangeDirection();
            _rigidbody.velocity += _targetDistance;
        }

        // if arrow pressed - walk animation
        if (_horizontalMoveInput != 0)
        {
            _anim.SetBool("isWalking", true);
            _anim.SetFloat("inputX", CrossPlatformInputManager.GetAxisRaw("Horizontal"));
        }
        else
        {
            _anim.SetBool("isWalking", false);
        }

        //check if player is on ground and if space has been pressed
        if (_grounded && _isJumping)
        {
            _grounded = false;
            _rigidbody.velocity += Vector2.up * _jump_acceleration;
        }

        // for testing
        if(CrossPlatformInputManager.GetButtonDown("Fire1"))
        {
            TakeDamage(-10);
        }
    }

    //rotate player to the moving direction
    private void RotateIfChangeDirection()
    {
        transform.eulerAngles = _targetDistance.x < 0 ? new Vector2(0, 0) : new Vector2(0, 180);
    }

    private void TakeDamage(float value)
    {
        health += value;
        if (health <= 0)
        {
            Debug.Log("Dead");
            health = 0;
            healthBar.UpdateHealthBar(0, 0);
        }
        else
        {
            healthBar.UpdateHealthBar(health, health / maxHealth);
        }
    }
}
