using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item {

    public Transform BulletSpawnPoint;
    public Transform bullet;

	public void Shoot()
    {
        Instantiate(bullet, BulletSpawnPoint, false);
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        if (player != null)
        {
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
            player.SetWeapon(this);
        }
    }
}
