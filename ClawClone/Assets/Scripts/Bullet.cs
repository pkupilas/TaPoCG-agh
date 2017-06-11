using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    [SerializeField]
    private float _moveSpeed = 0.5f;
    [SerializeField]
    private int _timeToDestroy = 1;

	void Update () {
        transform.Translate(Vector3.left * Time.deltaTime * _moveSpeed);
        Destroy(gameObject, _timeToDestroy);
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.GetComponent<Enemy>() != null)
        {
            other.gameObject.GetComponent<Enemy>().TakeDamage(GetComponentInParent<Weapon>().Damage);
        } else if (other.gameObject.GetComponent<Box>() != null)
        {
            other.gameObject.GetComponent<Box>().TakeHit();
        }
        else if (other.gameObject.layer != LayerMask.NameToLayer("Jumpable"))
        {
            return;
        }
        Destroy(gameObject);
    }
}
