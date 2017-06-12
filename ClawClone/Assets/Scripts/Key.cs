using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Key : MonoBehaviour
{

    [SerializeField] private Image[] _keyHoles;
    public static int keyCounter = 0;

    private void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.GetComponent<Player>();
        if (player != null)
        {
            FillOneOfKeyHoles();
            Destroy(gameObject);
        }
    }

    private void FillOneOfKeyHoles()
    {
        _keyHoles[keyCounter].color = Color.white;
        keyCounter++;
    }
}
