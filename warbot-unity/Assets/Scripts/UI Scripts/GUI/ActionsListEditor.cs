using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionsListEditor : MonoBehaviour
{
    public GameObject[] _listActionTextObject;

	// Use this for initialization
	void Start ()
    {
        _listActionTextObject = GameObject.FindGameObjectsWithTag("ActionEditor");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
