using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IfPuzzleScript : MonoBehaviour
{
    public GameObject puzzleCondObject;
    public GameObject puzzleActionObject;

    public ManageDragAndDrop manager;

    public bool debugInstruction;
	// Use this for initialization
	void Start ()
    {
        manager = GetComponent<ManageDragAndDrop>();

    }
	
	// Update is called once per frame
	void Update ()
    {
        updateCondPuzzle();
        updateActionPuzzle();
        
        
        if (debugInstruction)
        {
            debugInstruction = false;
            Instruction I = createInstruction();
            print(I.toString());
        }
    }

    void updateCondPuzzle()
    {
        puzzleCondObject = null;
        foreach (GameObject puzzle in GameObject.FindGameObjectsWithTag("CondPuzzle"))
        {
            if (manager.posGridX + 1 == puzzle.GetComponent<ManageDragAndDrop>().posGridX && manager.posGridY == puzzle.GetComponent<ManageDragAndDrop>().posGridY)
            {
                puzzleCondObject = puzzle;
                break;
            }
        }
    }

    void updateActionPuzzle()
    {
        puzzleActionObject = null;
        foreach (GameObject puzzle in GameObject.FindGameObjectsWithTag("ActionPuzzle"))
        {
            if (manager.posGridX + 1 == puzzle.GetComponent<ManageDragAndDrop>().posGridX && manager.posGridY - 1 == puzzle.GetComponent<ManageDragAndDrop>().posGridY)
            {
                puzzleActionObject = puzzle;
                break;
            }
        }
    }

    /*
      behavior = new List<Instruction>(){
            new Instruction(new string[] { "PERCEPT_LIFE_NOT_MAX","PERCEPT_BAG_NOT_EMPTY"}, "ACTION_HEAL"),
            new Instruction(new string[] { "PERCEPT_BAG_25"}, "ACTION_CREATE_HEAVY"),
        new Instruction(new string[] { "PERCEPT_BAG_10"}, "ACTION_CREATE_LIGHT")};
        */

    public Instruction createInstruction()
    {
        List<string> percepts = new List<string>();
        string action = puzzleActionObject.GetComponent<ActionPuzzleScript>().actionName;
        GameObject condObjectCurrent = puzzleCondObject;
        while (condObjectCurrent != null)
        {
            percepts.Add(condObjectCurrent.GetComponent<CondPuzzleScript>().condName);
            condObjectCurrent = condObjectCurrent.GetComponent<CondPuzzleScript>().nextCondPuzzle;
        }
        print(percepts.Count);
        print(action);
        return new Instruction(percepts.ToArray(), action);
    }
}
