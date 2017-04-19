using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public static SoundManager instance;

    [SerializeField]
    private AudioClip _collectSound;
    [SerializeField]
    private AudioClip _hpUpSound;
    [SerializeField]
    private AudioClip _skillSound;

    private AudioSource _source;

    void Awake()
    {
        if (instance != null)
        {
            Debug.Log("There is a Sound Manager on the scene!");
        } else
        {
            instance = this;
        }
    }

    void Start()
    {
        _source = GetComponent<AudioSource>();
    }

    public void PlayCollect()
    {
        _source.clip = _collectSound;
        _source.Play();
    }

    public void PlayHpUp()
    {
        _source.clip = _hpUpSound;
        _source.Play();
    }

    public void PlaySkill()
    {
        _source.clip = _skillSound;
        _source.Play();
    }
}
