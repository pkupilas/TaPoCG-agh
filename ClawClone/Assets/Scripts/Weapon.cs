using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public float Damage;

    public Transform bullet;

	public void Shoot()
    {
        Instantiate(bullet, transform, false);
    }
}
