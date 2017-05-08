﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Transform groundChecker;

    private Vector2 _targetDistance;                    // way to go by player
    private float _moveAcceleration = 15f;
    private float _jumpAcceleration = 6f;
    private float _climbAcceleration = 2f;
    private Rigidbody2D _rigidbody;
    private float _horizontalMoveInput;
    private float _verticalMoveInput;
    private bool _jumpPressed;
    private bool _canRejump = false;
    private bool _grounded;
    private bool _onLadder = false;

    private float _health = 100;
    private float _maxHealth = 100;

    private int _points = 0;
    private ExtraSkill.Skill _skill = ExtraSkill.Skill.None;

    private Animator _anim;
    private HealthBar _healthBar;
    [SerializeField]
    private Text _pointsLabel;
    [SerializeField]
    private SkillPanel _skillPanel;

    // Use this for initialization
    private void Start ()
	{
        _rigidbody = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _healthBar = GetComponent<HealthBar>();

        _healthBar.UpdateHealthBar(100, 1);
        _pointsLabel.text = "0";
    }

    private void Update()
    {
        //Check if player is staying on ground
        _grounded = Physics2D.Linecast(transform.position, groundChecker.position, 1 << LayerMask.NameToLayer("Ground"));
        _targetDistance = Vector2.zero;
        _horizontalMoveInput = CrossPlatformInputManager.GetAxis("Horizontal");
        _verticalMoveInput = CrossPlatformInputManager.GetAxis("Vertical");
        _jumpPressed = CrossPlatformInputManager.GetButtonDown("Space");

        //init way to go
        _targetDistance = Vector2.right * Time.deltaTime * _moveAcceleration * _horizontalMoveInput;

        if (_targetDistance != Vector2.zero)
        {
            RotateIfChangeDirection();
            _rigidbody.velocity += _targetDistance;
        }

        // if arrow pressed - walk animation
        if (!_onLadder)
        {
            _rigidbody.gravityScale = 1;
            if (_horizontalMoveInput != 0)
            {
                _anim.SetBool("isWalking", true);
                _anim.SetFloat("inputX", CrossPlatformInputManager.GetAxisRaw("Horizontal"));
            }
            else
            {
                _anim.SetBool("isWalking", false);
            }

        }
        else
        {
            _rigidbody.gravityScale = 0;
            if (_verticalMoveInput != 0)
            {
                _rigidbody.velocity = new Vector2(0, _climbAcceleration * _verticalMoveInput);
            }
        }
        
        //check if player is on ground and if space has been pressed
        if (_grounded)
        {
            _anim.SetBool("isFalling", false);
            _anim.SetBool("isJumping", false);
            if (_jumpPressed)
            {
                _grounded = false;
                _anim.SetBool("isJumping", true);
                _canRejump = _skill.Equals(ExtraSkill.Skill.DoubleJump);
                _rigidbody.velocity += Vector2.up * _jumpAcceleration;
            }
            else if (_anim.GetBool("isJumping"))
            {
                _anim.SetBool("isJumping", false);
            }
        }
        else if (!_grounded)
        {
            if (_jumpPressed && _canRejump)
            {
                _canRejump = false;
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0);
                _rigidbody.velocity += Vector2.up * _jumpAcceleration;
            }
            else if (_rigidbody.velocity.y < 0)
            {
                _anim.SetBool("isFalling", true);
            } else
            {
                _anim.SetBool("isFalling", false);
            }

        }
    }

    //rotate player to the moving direction
    private void RotateIfChangeDirection()
    {
        transform.eulerAngles = _targetDistance.x < 0 ? new Vector2(0, 0) : new Vector2(0, 180);
    }

    public void TakeDamage(float value)
    {
        _health += value;
        if (_health <= 0)
        {
            Debug.Log("Dead");
            _health = 0;
            _healthBar.UpdateHealthBar(0, 0);
        }
        else if(_health > _maxHealth)
        {
            _health = _maxHealth;
        }
        else
        {
            _healthBar.UpdateHealthBar(_health, _health / _maxHealth);
        }
    }

    public void GainPoints(int amount)
    {
        _points += amount;
        _pointsLabel.text = _points.ToString();
    }

    public void ChangeCurrentSkill(ExtraSkill.Skill skill)
    {
        _skill = skill;

        if (_skill.Equals(ExtraSkill.Skill.Run))
        {
            _moveAcceleration = 30f;
            _skillPanel.ChangeSkill(skill);
        }
        else
        {
            _moveAcceleration = 15f;
            _skillPanel.ChangeSkill(skill);
        }

        var timer = FindObjectOfType<Timer>();
        if (timer != null)
        {
            timer.GetComponent<Timer>().reset();
        } 
        else
        {
            Instantiate(Resources.Load("Timer"), GameObject.Find("SkillPanel").transform);
        }
    }

    public void ChangeOnLadder(bool onLadder)
    {
        _onLadder = onLadder;
    }
}
