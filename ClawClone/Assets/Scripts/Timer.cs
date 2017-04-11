using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    private float _timeLeft = 5.0f;
    private Text _text;

    void Start()
    {
        _text = GetComponent<Text>();
    }

    void Update()
    {
        _timeLeft -= Time.deltaTime;
        _text.text = Mathf.Round(_timeLeft).ToString();
        if (_timeLeft < 0)
        {
            GameObject.Find("Player").GetComponent<Player>().ChangeCurrentSkill(ExtraSkill.Skill.None);
            Destroy(gameObject);
        }
    }

    public void reset()
    {
        _timeLeft = 5.0f;
    }
}
