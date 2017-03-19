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

	// Use this for initialization
	private void Start ()
	{
        _rigidbody = GetComponent<Rigidbody2D>();
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

        //check if player is on ground and if space has been pressed
        if (_grounded && _isJumping)
        {
            _grounded = false;
            _rigidbody.velocity += Vector2.up * _jump_acceleration;
        }
    }

    //rotate player to the moving direction
    private void RotateIfChangeDirection()
    {
        transform.eulerAngles = _targetDistance.x < 0 ? new Vector2(0, 0) : new Vector2(0, 180);
    }
}
