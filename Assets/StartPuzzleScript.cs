using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPuzzleScript : MonoBehaviour
{
    public GameObject ifPuzzle;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        UpdateCondPuzzle();

    }

    void UpdateCondPuzzle()
    {
        ifPuzzle = null;
        foreach (GameObject puzzle in GameObject.FindGameObjectsWithTag("IfPuzzle"))
        {
            if (GetComponent<ManageDragAndDrop>().getGridPosition() + new Vector2(0,-1) == puzzle.GetComponent<ManageDragAndDrop>().getGridPosition() &&
                puzzle.GetComponent<IfPuzzleScript>().validPlace == "true")
            {
                ifPuzzle = puzzle;
                break;
            }
        }
    }

}
