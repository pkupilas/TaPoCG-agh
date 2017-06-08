using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _patrolWaypoints;         // Waypoints between which platform can move {PointA, PointB}
    private GameObject _waypointOfInterest;
    private Vector2 _direction = Vector2.right;
    private float _moveAcceleration = 30f;

    private Rigidbody2D _rigidbody;

	// Use this for initialization
	void Start ()
	{
	    _rigidbody = GetComponent<Rigidbody2D>();
        _waypointOfInterest = _patrolWaypoints[1];
    }
	
	// Update is called once per frame
	void FixedUpdate ()
	{
	    Move();
	}

    private void Move()
    {
        // Change direction of moving after reaching waypoint of interest
        if (Mathf.Abs(transform.position.x - _waypointOfInterest.transform.position.x) < 0.1)
        {
            Turn();
        }
        
        _rigidbody.velocity = _direction * Time.deltaTime * _moveAcceleration;
    }

    private void Turn()
    {
        _rigidbody.velocity = Vector2.zero;
        _waypointOfInterest = _waypointOfInterest == _patrolWaypoints[0] ? _patrolWaypoints[1] : _patrolWaypoints[0];
        ChangeDirectionVector();
    }

    private void ChangeDirectionVector()
    {
        _direction = _direction == Vector2.left ? Vector2.right : Vector2.left;
    }
}
