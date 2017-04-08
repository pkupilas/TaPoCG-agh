using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    private float timeLeft = 5.0f;
    private Text text;

    void Start()
    {
        text = GetComponent<Text>();
    }

    void Update()
    {
        timeLeft -= Time.deltaTime;
        text.text = Mathf.Round(timeLeft).ToString();
        if (timeLeft < 0)
        {
            GameObject.Find("Player").GetComponent<Player>().changeCurrentSkill(ExtraSkill.Skill.None);
            Destroy(gameObject);
        }
    }

    public void reset()
    {
        timeLeft = 5.0f;
    }
}
