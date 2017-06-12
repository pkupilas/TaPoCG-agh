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
    private Sprite _pawSlot;

    // Use this for initialization
    void Start () {
        _emptySlot = Resources.Load<Sprite>("empty_slot");
        _runSlot = Resources.Load<Sprite>("run_slot");
        _jumpSlot = Resources.Load<Sprite>("jump_slot");
        _gunSlot = Resources.Load<Sprite>("gun_slot");
        _pawSlot = Resources.Load<Sprite>("paw_slot");
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

    public void ChangeToGun()
    {
        _weaponSlot.sprite = _gunSlot;
    }

    public void ChangeToPaw()
    {
        _weaponSlot.sprite = _pawSlot;
    }
}
