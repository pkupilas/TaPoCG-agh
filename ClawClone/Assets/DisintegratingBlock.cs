using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisintegratingBlock : MonoBehaviour
{

    private Animator _animator;

	// Use this for initialization
	void Start ()
	{
	    _animator = GetComponent<Animator>();
	}
	
    private void OnCollisionEnter2D(Collision2D other)
    {
        var player = other.gameObject.GetComponent<Player>();
        if (player != null)
        {
            _animator.SetTrigger("isDisintegrating");
        }
    }

    // Used in Disintegrate animation as animation event 
    private void DestroyBlock()
    {
        Destroy(gameObject.transform.parent.gameObject);
    }
}
