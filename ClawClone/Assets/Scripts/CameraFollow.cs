using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    [SerializeField]
    private Vector2 _max;

    [SerializeField]
    private Vector2 _min;

    private Transform target;

    // Use this for initialization
    void Start () {
        target = GameObject.Find("Player").transform;
	}
	
	// Update is called once per frame
	void LateUpdate () {
        transform.position = new Vector3(Mathf.Clamp(target.position.x, _min.x, _max.x), Mathf.Clamp(target.position.y, _min.y, _max.y), transform.position.z);
	}
}
