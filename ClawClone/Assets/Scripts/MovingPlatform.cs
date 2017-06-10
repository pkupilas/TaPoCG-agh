using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    private enum Orientation
    {
        Horizontal,
        Vertical
    }
    [SerializeField] private Orientation _platformOrientation;
    [SerializeField] private GameObject[] _patrolWaypoints;         // Waypoints between which platform can move {PointA, PointB}
    private GameObject _waypointOfInterest;
    private Vector2 _direction;
    private float _moveAcceleration = 30f;
    private Rigidbody2D _rigidbody;

	// Use this for initialization
	void Start ()
	{
	    _rigidbody = GetComponent<Rigidbody2D>();
        _waypointOfInterest = _patrolWaypoints[1];
        _direction = _platformOrientation == Orientation.Horizontal ? Vector2.right : Vector2.down;
    }
	
	// Update is called once per frame
	void FixedUpdate ()
	{
	    Move();
	}

    private void Move()
    {
        var minimalDistance = 0.1;
        // Change direction of moving after reaching waypoint of interest
        switch (_platformOrientation)
        {
            case Orientation.Horizontal:
                
                if (Mathf.Abs(transform.position.x - _waypointOfInterest.transform.position.x) < minimalDistance)
                {
                    Turn();
                }
                break;
            case Orientation.Vertical:
                if (Mathf.Abs(transform.position.y - _waypointOfInterest.transform.position.y) < minimalDistance)
                {
                    Turn();
                }
                break;
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
        switch (_platformOrientation)
        {
            case Orientation.Horizontal:
                _direction = _direction == Vector2.left ? Vector2.right : Vector2.left;
                break;
            case Orientation.Vertical:
                _direction = _direction == Vector2.up ? Vector2.down : Vector2.up;
                break;
        }
    }
}
