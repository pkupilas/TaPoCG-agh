using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : Item {

    public enum Skill { DoubleJump, Run, None };

    [SerializeField]
    private int hp;

    protected override void OnCollisionEnter2D(Collision2D other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        if (player != null)
        {
            base.OnCollisionEnter2D(other);
            player.TakeDamage(hp);
        }
    }
}
