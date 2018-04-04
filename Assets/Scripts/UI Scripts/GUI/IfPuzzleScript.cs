using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IfPuzzleScript : MonoBehaviour
{
    public GameObject puzzleCondObject;
    public GameObject puzzleActionObject;
    public GameObject puzzleIfObject;
    public String validPlace = "false";
    public ManageDragAndDrop manager;
    Image image;
    Color defaultColor;
    public Color validColor = new Color(158.0F / 255, 1, 79.0F / 255);
    public int ID;

    public bool debugInstruction;
	// Use this for initialization
	void Start ()
    {
        manager = GetComponent<ManageDragAndDrop>();
        image = GetComponent<Image>();
        defaultColor = image.color;

    }
	
	// Update is called once per frame
	void Update ()
    {
        UpdateIfPuzzle();
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
        foreach (GameObject puzzle in GameObject.FindGameObjectsWithTag("Puzzle"))
        {
            puzzle.GetComponent<CondPuzzleScript>().beforePuzzle = null;
            if (manager.posGridX + 1 == puzzle.GetComponent<ManageDragAndDrop>().posGridX && manager.posGridY == puzzle.GetComponent<ManageDragAndDrop>().posGridY &&
                puzzle.GetComponent<CondPuzzleScript>().type == CondPuzzleScript.Type.CONDITION)
            {
                puzzleCondObject = puzzle;

                puzzle.GetComponent<CondPuzzleScript>().beforePuzzle = gameObject;
            }
        }
    }

    void updateActionPuzzle()
    {
        puzzleActionObject = null;
        foreach (GameObject puzzle in GameObject.FindGameObjectsWithTag("Puzzle"))
        {
            if (manager.posGridX + 1 == puzzle.GetComponent<ManageDragAndDrop>().posGridX && manager.posGridY - 1 == puzzle.GetComponent<ManageDragAndDrop>().posGridY &&
                (puzzle.GetComponent<CondPuzzleScript>().type == CondPuzzleScript.Type.ACTION || puzzle.GetComponent<CondPuzzleScript>().type == CondPuzzleScript.Type.ACTION_NON_TERMINAL))
            {
                puzzleActionObject = puzzle;
                puzzle.GetComponent<CondPuzzleScript>().beforePuzzle = gameObject;
                break;
            }
        }
    }

  

    void UpdateIfPuzzle()
    {
        image.color = defaultColor;
        validPlace = "false";
        puzzleIfObject = null;

        GameObject startPuzzle = GameObject.FindGameObjectWithTag("StartPuzzle");
        foreach (GameObject puzzle in GameObject.FindGameObjectsWithTag("IfPuzzle"))
        {
            if(manager.posGridX == puzzle.GetComponent<ManageDragAndDrop>().posGridX && manager.posGridY - 2 == puzzle.GetComponent<ManageDragAndDrop>().posGridY)
            {
                puzzleIfObject = puzzle;
                break;
            }
        }

        foreach (GameObject puzzle in GameObject.FindGameObjectsWithTag("IfPuzzle"))
        {
            if (manager.posGridX == startPuzzle.GetComponent<ManageDragAndDrop>().posGridX && manager.posGridY + 1 == startPuzzle.GetComponent<ManageDragAndDrop>().posGridY ||
            (manager.posGridX == puzzle.GetComponent<ManageDragAndDrop>().posGridX && manager.posGridY + 2 == puzzle.GetComponent<ManageDragAndDrop>().posGridY && puzzle.GetComponent<IfPuzzleScript>().validPlace == "true"))
            {
                image.color = validColor;
                validPlace = "true";
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
        List<MessageStruct> ms = new List<MessageStruct>();
        string action = puzzleActionObject.GetComponent<CondPuzzleScript>()._value;

        GameObject condObjectCurrent = puzzleCondObject;
        GameObject actionObjectCurrent = puzzleActionObject;
        while (condObjectCurrent != null)
        {
            percepts.Add(condObjectCurrent.GetComponent<CondPuzzleScript>()._value);
            condObjectCurrent = condObjectCurrent.GetComponent<CondPuzzleScript>().nextPuzzle;
        }
        while (actionObjectCurrent != null)
        {
            if (actionObjectCurrent.GetComponent<CondPuzzleScript>().type == CondPuzzleScript.Type.ACTION_NON_TERMINAL)
            {
                MessageStruct message = new MessageStruct(actionObjectCurrent.GetComponent<CondPuzzleScript>()._value, actionObjectCurrent.GetComponent<CondPuzzleScript>().messageDropDown.captionText.text);
                ms.Add(message);
            }
            else
            {
                action = actionObjectCurrent.GetComponent<CondPuzzleScript>()._value;
            }
            actionObjectCurrent = actionObjectCurrent.GetComponent<CondPuzzleScript>().nextPuzzle;



        }
        return new Instruction(percepts.ToArray(), ms.ToArray(), action);
    }
}
