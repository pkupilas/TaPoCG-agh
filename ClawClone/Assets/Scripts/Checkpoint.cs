using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

    private Sprite _visited;
    private bool _isVisited = false;

    void Start()
    {
        _visited = Resources.Load<Sprite>("checkpoint_saved");
    }

    public void Visit()
    {
        if (!_isVisited)
        {
            transform.Find("CpState").GetComponent<SpriteRenderer>().sprite = _visited;
            _isVisited = true;
        }
    }
}
