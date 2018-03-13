using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CondPuzzleScript : MonoBehaviour
{
    public GameObject nextCondPuzzle;
    public string condName;
    public ManageDragAndDrop manager;
    // Use this for initialization
    void Start ()
    {
        manager = GetComponent<ManageDragAndDrop>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        nextCondPuzzle = null;
        foreach (GameObject puzzle in GameObject.FindGameObjectsWithTag("CondPuzzle"))
        {
            if (manager.posGridX + 1 == puzzle.GetComponent<ManageDragAndDrop>().posGridX && manager.posGridY == puzzle.GetComponent<ManageDragAndDrop>().posGridY)
            {
                nextCondPuzzle = puzzle;
                break;
            }
        }
    }
}
