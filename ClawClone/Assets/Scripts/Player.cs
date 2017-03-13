using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{

    private Vector2 _move;
    private float _move_power = 5;
    private Rigidbody2D _rigidbody;

	// Use this for initialization
	void Start ()
	{
	    _rigidbody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update ()
	{
	    var h = CrossPlatformInputManager.GetAxis("Horizontal");
	    var v = CrossPlatformInputManager.GetAxis("Vertical");

        _move = (Vector2.right*h).normalized;
        _rigidbody.AddForce(_move * _move_power);
        var jump = CrossPlatformInputManager.GetButton("Space");
        Debug.Log("Colliding.");
        if (jump)
        {
            _rigidbody.AddForce(new Vector2(0, 1) * 20);
        }
        //Debug.Log("Horizontal: " + h + "Vertical: " + v + "Move: " + _move + "Space: " + jump);
    }

    void OnCollisionEnter2D(Collision2D other)
    {


    }

}
