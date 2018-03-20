using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewFile : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void clearEditor()
    {
        foreach(GameObject puzzle in GameObject.FindGameObjectsWithTag("IfPuzzle"))
        {
            Destroy(puzzle);
        }

        foreach (GameObject puzzle in GameObject.FindGameObjectsWithTag("ActionPuzzle"))
        {
            Destroy(puzzle);
        }

        foreach (GameObject puzzle in GameObject.FindGameObjectsWithTag("CondPuzzle"))
        {
            Destroy(puzzle);
        }
    }
}
