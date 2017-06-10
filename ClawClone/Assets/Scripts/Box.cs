using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    [SerializeField]
    private int _hitsToDestroy = 1;

    private Animator _anim;

    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    public void TakeHit()
    {
        _hitsToDestroy -= 1;
        if (_hitsToDestroy == 0)
        {
            _anim.SetTrigger("Destroy");
        }
    }

    // Used in BoxDestroy animation as animation event
    private void DestroyBox()
    {
        Destroy(gameObject);
    }
}
