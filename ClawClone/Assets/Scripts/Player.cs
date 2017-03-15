using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{

    private Vector2 _move;
    private float _move_acceleration = 10f;
    private float _jump_acceleration = 300f;
    private Rigidbody2D _rigidbody;


	// Use this for initialization
	private void Start ()
	{
	    _rigidbody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	private void Update ()
	{
	    var h = CrossPlatformInputManager.GetAxis("Horizontal");
	    var v = CrossPlatformInputManager.GetAxis("Vertical");

        _move = (Vector2.right*h).normalized;
        _rigidbody.AddForce(_move * _move_acceleration, ForceMode2D.Force);
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        var jump = CrossPlatformInputManager.GetButtonDown("Space");
        if (jump && other.gameObject.CompareTag("Ground"))
        {
            _rigidbody.AddForce(Vector2.up * _jump_acceleration, ForceMode2D.Force);
        }
    }
}
