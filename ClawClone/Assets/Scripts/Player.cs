using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Utilities;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private float _moveAcceleration = StandardValues.PlayerMoveAcceleration;
    private float _jumpAcceleration = StandardValues.PlayerJumpAcceleration;
    private float _climbAcceleration = StandardValues.PlayerClimbAcceleration;
    private float _health = StandardValues.PlayerMaxHealth;
    private ExtraSkill.Skill _skill = ExtraSkill.Skill.None;

    private int _points;
    private float _horizontalMoveInput;
    private float _verticalMoveInput;

    private bool _jumpPressed;
    private bool _canRejump;
    private bool _grounded;
    private bool _onLadder;
    public bool IsDead { get; private set; }

    private Rigidbody2D _rigidbody;
    private Animator _anim;
    private HealthBar _healthBar;
    private Vector2 _targetDistance;

    [SerializeField]
    private Text _pointsLabel;
    [SerializeField]
    private SkillPanel _skillPanel;
    [SerializeField]
    private Transform _groundChecker;
    [SerializeField]
    private Transform _respawnPoint;

    
    private void Start ()
	{
        _rigidbody = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _healthBar = GetComponent<HealthBar>();

        _healthBar.UpdateHealthBar(StandardValues.PlayerMaxHealth, 1);
        _pointsLabel.text = "0";
    }

    private void FixedUpdate()
    {
        if (_targetDistance != Vector2.zero)
        {
            RotateIfChangeDirection();
            transform.Translate(new Vector3(_targetDistance.x, 0, 0), Space.World);
        }
    }

    private void Update()
    {
        _grounded = Physics2D.Linecast(transform.position, _groundChecker.position, 1 << LayerMask.NameToLayer("Ground"));
        _horizontalMoveInput = CrossPlatformInputManager.GetAxis("Horizontal");
        _verticalMoveInput = CrossPlatformInputManager.GetAxis("Vertical");
        _jumpPressed = CrossPlatformInputManager.GetButtonDown("Space");
        _targetDistance = (IsDead) ? Vector2.zero : Vector2.right * Time.deltaTime * _moveAcceleration * _horizontalMoveInput;

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
                transform.Translate(new Vector3(_targetDistance.x, 0, 0), Space.World);
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0);
                _rigidbody.velocity += Vector2.up * _jumpAcceleration;
            }
            else if (_rigidbody.velocity.y < 0)
            {
                _anim.SetBool("isFalling", true);
            }
            else
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

    public void ChangePlayerHealth(float value)
    {
        _health += value;
        if (_health <= 0)
        {
            _health = 0;
            _healthBar.UpdateHealthBar(0, 0);
            if (!IsDead)
            {
                IsDead = true;
                _anim.SetTrigger("isDead");
            }
        }
        else if(_health > StandardValues.PlayerMaxHealth)
        {
            _health = StandardValues.PlayerMaxHealth;
        }
        else
        {
            _healthBar.UpdateHealthBar(_health, _health / StandardValues.PlayerMaxHealth);
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
            _moveAcceleration = 2*StandardValues.PlayerMoveAcceleration;
            _skillPanel.ChangeSkill(skill);
        }
        else
        {
            _moveAcceleration = StandardValues.PlayerMoveAcceleration;
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

    private void Dead()
    {
        _anim.SetTrigger("isIdle");
        transform.position = _respawnPoint.position;
        _health = StandardValues.PlayerMaxHealth;
        _healthBar.UpdateHealthBar(_health, 1);
        IsDead = false;
    }
}
