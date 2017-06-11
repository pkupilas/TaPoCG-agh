using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillPanel : MonoBehaviour {

    [SerializeField]
    private Image _skillSlot;
    [SerializeField]
    private Image _weaponSlot;

    private Sprite _emptySlot;
    private Sprite _runSlot;
    private Sprite _jumpSlot;
    private Sprite _gunSlot;

    // Use this for initialization
    void Start () {
        _emptySlot = Resources.Load<Sprite>("empty_slot");
        _runSlot = Resources.Load<Sprite>("run_slot");
        _jumpSlot = Resources.Load<Sprite>("jump_slot");
        _gunSlot = Resources.Load<Sprite>("gun_slot");
    }
	
    public void ChangeSkill(ExtraSkill.Skill skill)
    {
        if (skill.Equals(ExtraSkill.Skill.DoubleJump))
        {
            _skillSlot.sprite = _jumpSlot;
        }
        else if (skill.Equals(ExtraSkill.Skill.Run))
        {
            _skillSlot.sprite = _runSlot;
        }
        else
        {
            _skillSlot.sprite = _emptySlot;
        }

    }

    public void ChangeWeapon()
    {
        _weaponSlot.sprite = _gunSlot;
    }
}
