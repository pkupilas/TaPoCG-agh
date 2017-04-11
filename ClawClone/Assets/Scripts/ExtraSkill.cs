using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraSkill : Item {

    public enum Skill { DoubleJump, Run, None};

    [SerializeField]
    private Skill _skill;

    protected override void OnCollisionEnter2D(Collision2D other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        if (player != null)
        {
            base.OnCollisionEnter2D(other);
            player.ChangeCurrentSkill(_skill);
        }
    }
}
