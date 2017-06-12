using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    [SerializeField] private const int _keyCountToOpen = 4;
    private int _keyCount = 0;
    private Animator _animator;

    // Use this for initialization
    void Start ()
    {
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        _keyCount = Key.keyCounter;
    }
	
	// Update is called once per frame
	void OnTriggerEnter2D (Collider2D other)
	{
	    var player = other.gameObject.GetComponent<Player>();
	    if (player != null && CheckDoorStatus())
	    {
	        OpenDoor();
            SceneManager.LoadScene("MainMenu");
	    }
	}

    private bool CheckDoorStatus()
    {
        return _keyCountToOpen == _keyCount;
    }

    private void OpenDoor()
    {
        _animator.SetBool("isOpening", true);
    }

    public void AddKeys(int numberOfKeys)
    {
        _keyCount += numberOfKeys;
    }
}
