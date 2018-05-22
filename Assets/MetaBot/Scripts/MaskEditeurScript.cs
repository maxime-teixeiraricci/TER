using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskEditeurScript : MonoBehaviour
{
    public bool _over;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnMouseOver()
    {
        _over = true;
    }

    void OnMouseExit()
    {
        _over = false;
    }
}
