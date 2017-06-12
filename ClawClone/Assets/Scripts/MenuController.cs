using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

    [SerializeField]
    private Button _playButton;
    [SerializeField]
    private Button _exitButton;

    void Start () {
        _playButton = _playButton.GetComponent<Button>();
        _exitButton = _exitButton.GetComponent<Button>();
    }
	
	public void ExitPress()
    {
        Application.Quit();
    }

    public void PlayPress()
    {
        SceneManager.LoadScene("NewMapLevel1");
    }
}
