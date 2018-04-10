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
        UpdateAllValidPuzzles();

    }

    void UpdateCondPuzzle()
    {
        ifPuzzle = null;
        foreach (GameObject puzzle in GameObject.FindGameObjectsWithTag("IfPuzzle"))
        {
            if (GetComponent<ManageDragAndDrop>().getGridPosition() + new Vector2(0,-1) == puzzle.GetComponent<ManageDragAndDrop>().getGridPosition())
            {
                ifPuzzle = puzzle;
                break;
            }
        }
    }

    void UpdateAllValidPuzzles()
    {
        // Initialize all at false 
        foreach (GameObject puzzle in GameObject.FindGameObjectsWithTag("Puzzle"))
        {
            puzzle.GetComponent<PuzzleScript>().isValid = false;
        }
        foreach (GameObject puzzle in GameObject.FindGameObjectsWithTag("IfPuzzle"))
        {
            puzzle.GetComponent<IfPuzzleScript>().isValid = false;
        }

        if (ifPuzzle)
        {
            ifPuzzle.GetComponent<IfPuzzleScript>().isValid = true;
            ifPuzzle.GetComponent<IfPuzzleScript>().UpdatePuzzle();
        }

    }

}
