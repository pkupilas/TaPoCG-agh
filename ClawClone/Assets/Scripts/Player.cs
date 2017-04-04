using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Transform groundChecker;

    private Vector2 _targetDistance;                    // way to go by player
    private float _move_acceleration = 15f;
    private float _jump_acceleration = 6f;
    private Rigidbody2D _rigidbody;
    private float _horizontalMoveInput;
    private bool _jumpPressed;
    private bool _canRejump;
    private bool _grounded;

    private float health = 100;
    private float maxHealth = 100;

    private int _points = 0;
    private ExtraSkill.Skill _skill;

    private Animator _anim;
    private HealthBar healthBar;
    [SerializeField]
    private Text pointsLabel;

    // Use this for initialization
    private void Start ()
	{
        _rigidbody = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        healthBar = GetComponent<HealthBar>();

        healthBar.UpdateHealthBar(100, 1);
        pointsLabel.text = "0";
    }

    private void Update()
    {
        //Check if player is staying on ground
        _grounded = Physics2D.Linecast(transform.position, groundChecker.position, 1 << LayerMask.NameToLayer("Ground"));
        _targetDistance = Vector2.zero;
        _horizontalMoveInput = CrossPlatformInputManager.GetAxis("Horizontal");
        _jumpPressed = CrossPlatformInputManager.GetButtonDown("Space");

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
        if (_grounded && _jumpPressed)
        {
            _grounded = false;
            _canRejump = _skill.Equals(ExtraSkill.Skill.DoubleJump);
            _rigidbody.velocity += Vector2.up * _jump_acceleration;
        }
        else if (!_grounded && _canRejump && _jumpPressed)
        {
            _canRejump = false;
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0);
            _rigidbody.velocity += Vector2.up * _jump_acceleration;
        }

        // for testing
        if (CrossPlatformInputManager.GetButtonDown("Fire1"))
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

    public void gainPoints(int amount)
    {
        _points += amount;
        pointsLabel.text = _points.ToString();
    }

    public void changeCurrentSkill(ExtraSkill.Skill skill)
    {
        _skill = skill;

        if (_skill.Equals(ExtraSkill.Skill.Run))
        {
            _move_acceleration = 30f;
        }
        else
        {
            _move_acceleration = 15f;
        }
    }
}
