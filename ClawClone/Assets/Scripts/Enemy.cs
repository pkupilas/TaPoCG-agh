using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    private Animator _animator;
    private Rigidbody2D _rigidbody;

    [SerializeField] private float _move_acceleration = 4f;
    [SerializeField] private float _distance = 3f;
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
	    CheckIfMove();
	}

    private void CheckIfMove()
    {

        if (transform.position.x-(_lastIdlePosition+_distance)>Mathf.Epsilon)
        {
            _animator.SetBool("isWalking", false);
            _lastIdlePosition = transform.position.x;
            transform.eulerAngles = new Vector2(0, 180);
            _rigidbody.velocity = Vector2.zero;
            _direction = Vector2.left;
        }
        else
        {
            _animator.SetBool("isWalking", true);
            _rigidbody.velocity += _direction * Time.deltaTime * _move_acceleration;
        }
        //if (_animator.GetBool("isWalking"))
        //{
        //    _rigidbody.velocity += _direction * Time.deltaTime * _move_acceleration;
        //}
    }
}
