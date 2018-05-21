using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPuzzleScript : MonoBehaviour
{
    public GameObject ifPuzzle;
	// Use this for initialization
	void Start () {
       UpdateCondPuzzle();
       UpdateAllValidPuzzles();
    }
	
	// Update is called once per frame
	void Update () {
        if (!ifPuzzle || ifPuzzle.GetComponent<IfPuzzleScript>().isValid == false) // Pour eviter de recalculer a chaque fois
        {
            UpdateCondPuzzle();
            UpdateAllValidPuzzles();
        }

    }

    void UpdateCondPuzzle()
    {
        foreach (GameObject puzzle in GameObject.FindGameObjectsWithTag("IfPuzzle"))
        {
            if (GetComponent<ManageDragAndDrop>().getGridPosition() + new Vector2(0,-1) == puzzle.GetComponent<ManageDragAndDrop>().getGridPosition())
            {

                ifPuzzle = puzzle;
                break;
            }
        }
    }

    public void UpdateAllValidPuzzles()
    {
        // Initialize all at false 
        ifPuzzle = null;
        UpdateCondPuzzle();
        foreach (GameObject puzzle in GameObject.FindGameObjectsWithTag("Puzzle"))
        {
            puzzle.GetComponent<PuzzleScript>().isValid = false;
        }
        foreach (GameObject puzzle in GameObject.FindGameObjectsWithTag("IfPuzzle"))
        {
            puzzle.GetComponent<IfPuzzleScript>().isValid = false;
        }

        if (ifPuzzle && ifPuzzle.GetComponent<IfPuzzleScript>().isValid == false)
        {
            ifPuzzle.GetComponent<IfPuzzleScript>().isValid = true;
            ifPuzzle.GetComponent<IfPuzzleScript>().UpdatePuzzle();
        }

    }

}
