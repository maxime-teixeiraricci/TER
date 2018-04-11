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
    public bool isValid;
    public ManageDragAndDrop manager;
    Image image;
    public Color defaultColor;
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
        image.color = (isValid) ? validColor : defaultColor;
    }

    public void UpdatePuzzle()
    {
        puzzleCondObject = null;
        puzzleActionObject = null;
        puzzleIfObject = null;
        manager = GetComponent<ManageDragAndDrop>();
        UpdateIfPuzzle();
        updateCondPuzzle();
        updateActPuzzle();

        if (puzzleCondObject)
        {
            puzzleCondObject.GetComponent<PuzzleScript>().isValid = true;
            puzzleCondObject.GetComponent<PuzzleScript>().UpdateNextPuzzle();
        }
        if (puzzleActionObject)
        {
            puzzleActionObject.GetComponent<PuzzleScript>().isValid = true;
            puzzleActionObject.GetComponent<PuzzleScript>().UpdateNextPuzzle();
        }
        if (puzzleIfObject)
        {
            puzzleIfObject.GetComponent<IfPuzzleScript>().isValid = true;
            puzzleIfObject.GetComponent<IfPuzzleScript>().UpdatePuzzle();
        }
}

    void updateCondPuzzle()
    {
        foreach (GameObject puzzle in GameObject.FindGameObjectsWithTag("Puzzle"))
        {
            Vector2 currentGridPos = manager.getGridPosition();
            Vector2 puzzleGridPos = puzzle.GetComponent<ManageDragAndDrop>().getGridPosition();
            PuzzleScript.Type typePuzzle = puzzle.GetComponent<PuzzleScript>().type;


            if (currentGridPos + new Vector2(1,0) == puzzleGridPos && typePuzzle == PuzzleScript.Type.CONDITION)
            {
                puzzleCondObject = puzzle;
                break;
            }
        }
    }

    void updateActPuzzle()
    {
        foreach (GameObject puzzle in GameObject.FindGameObjectsWithTag("Puzzle"))
        {
            Vector2 currentGridPos = manager.getGridPosition();
            Vector2 puzzleGridPos = puzzle.GetComponent<ManageDragAndDrop>().getGridPosition();
            PuzzleScript.Type typePuzzle = puzzle.GetComponent<PuzzleScript>().type;
            
            if (currentGridPos + new Vector2(1, -1) == puzzleGridPos && (
                typePuzzle == PuzzleScript.Type.ACTION || typePuzzle == PuzzleScript.Type.ACTION_NON_TERMINAL || typePuzzle == PuzzleScript.Type.MESSAGE))
            {
                puzzleActionObject = puzzle;
                break;
            }
        }
    }

    void UpdateIfPuzzle()
    {
        foreach (GameObject puzzle in GameObject.FindGameObjectsWithTag("IfPuzzle"))
        {
            Vector2 currentGridPos = manager.getGridPosition();
            Vector2 puzzleGridPos = puzzle.GetComponent<ManageDragAndDrop>().getGridPosition();
            if (manager.getGridPosition() + new Vector2(0, -2) == puzzle.GetComponent<ManageDragAndDrop>().getGridPosition())
            {
                puzzleIfObject = puzzle;
                break;
            }
        }
    }

    public Instruction createInstruction()
    {
        List<string> percepts = new List<string>();
        List<MessageStruct> ms = new List<MessageStruct>();
        string action ="";

        GameObject condObjectCurrent = puzzleCondObject;
        GameObject actionObjectCurrent = puzzleActionObject;
        while (condObjectCurrent != null)
        {
            percepts.Add(condObjectCurrent.GetComponent<PuzzleScript>()._value);
            condObjectCurrent = condObjectCurrent.GetComponent<PuzzleScript>().nextPuzzle;
        }
        while (actionObjectCurrent != null)
        {
            if (actionObjectCurrent.GetComponent<PuzzleScript>().type == PuzzleScript.Type.MESSAGE)
            {
                MessageStruct message = new MessageStruct(actionObjectCurrent.GetComponent<PuzzleScript>()._value, actionObjectCurrent.GetComponent<PuzzleScript>().messageDropDown.captionText.text);
                ms.Add(message);
            }
            else if (actionObjectCurrent.GetComponent<PuzzleScript>().type == PuzzleScript.Type.ACTION_NON_TERMINAL)
            {
                MessageStruct message = new MessageStruct(actionObjectCurrent.GetComponent<PuzzleScript>()._value, "NONE");
                ms.Add(message);
            }
            else
            {
                action = actionObjectCurrent.GetComponent<PuzzleScript>()._value;
            }
            actionObjectCurrent = actionObjectCurrent.GetComponent<PuzzleScript>().nextPuzzle;



        }
        
        return new Instruction(percepts.ToArray(), ms.ToArray(), action);
    }

    private void OnMouseOver()
    {
        if ( Input.GetMouseButtonDown(1) && GameObject.Find("Undo"))
        {   
            gameObject.SetActive(false);
            createPuzzle.listPieces.Remove(gameObject);
            createPuzzle.listPieces.Add(gameObject);
            createPuzzle.cptObjects = createPuzzle.listPieces.Count;
        } 
    }
}
