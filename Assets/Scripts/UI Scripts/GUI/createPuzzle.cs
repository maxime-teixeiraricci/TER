using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class createPuzzle : MonoBehaviour {

    public GameObject puzzle;
    public string _label;
    public Vector3 positionInitial;
    public static ArrayList listPieces = new ArrayList();
    public static ArrayList recoverList = new ArrayList();
    public static int cptObjects;
    public static int cptUndo;
    public int cptIfPuzzle = 1;
    int countID = 0;
    public ArrayList pieceIf = new ArrayList();
    public static GameObject pieceToUndo;
    public static GameObject pieceToRedo;

    private void Update()
    {
        
    }

    public int numberIfPuzzle()
    {
        pieceIf = new ArrayList();
        foreach (GameObject puzzle in GameObject.FindGameObjectsWithTag("IfPuzzle"))
        {
            if (!pieceIf.Contains(puzzle))
            {
                pieceIf.Add(puzzle);
            }
            // ifPuzzleCreated = pieceIf.Count;
            // Debug.Log("VAL LIST UPDATE = " + ifPuzzleCreated);
        }
        return pieceIf.Count;
    }

    public void create()
    {
        GameObject puzzleClone = (GameObject)Instantiate(puzzle, GameObject.Find("Editeur").transform);
        bool negAction = GameObject.Find("NegationAction").GetComponent<Toggle>().isOn;
        bool negCondition = GameObject.Find("NegationCondition").GetComponent<Toggle>().isOn;
        if (puzzleClone.GetComponent<ActionPuzzleScript>())
        {
            if (negAction)
            {
                //Debug.Log("Label ACTION = " + _label);
                puzzleClone.GetComponent<ActionPuzzleScript>().actionName = "Not" + _label;
            }
            else
            {
                puzzleClone.GetComponent<ActionPuzzleScript>().actionName = _label;
            }
            
        }
        else if (puzzleClone.GetComponent<CondPuzzleScript>())
        {
            if (negCondition)
            {
                //Debug.Log("Label ACTION = " + _label);
                puzzleClone.GetComponent<CondPuzzleScript>().condName = "Not" + _label;
            }
            else
            {
                puzzleClone.GetComponent<CondPuzzleScript>().condName = _label;
            }
        }

        else if (puzzleClone.GetComponent<IfPuzzleScript>())
        {
            puzzleClone.GetComponent<IfPuzzleScript>().ID = countID + 1;            // Attribute the ID of the piece equal to the number of IfPuzzle pieces
            countID++;
        }

        puzzleClone.GetComponent<RectTransform>().anchoredPosition = positionInitial;
        listPieces.Add(puzzleClone);
        cptObjects = listPieces.Count;
        
        //Debug.Log("Nombre piece IF a la création " + cptIfPuzzle);

    }


    public void Undo()
    {
        //Debug.Log("Valeur liste entrée undo = " + ifPuzzleCreated);
        pieceToUndo = (GameObject)listPieces[cptObjects - 1];
        if (pieceToUndo.tag == "IfPuzzle")
        {
            Debug.Log("Suppression piece IF n°" + cptIfPuzzle);
            cptIfPuzzle = numberIfPuzzle() - 1;
            Debug.Log("Il reste " + cptIfPuzzle + " pièces IF");
        }
        recoverList.Add(pieceToUndo);
        pieceToUndo.SetActive(false);
        cptObjects--;
        cptUndo = recoverList.Count;
    }

    public void Redo()
    {
        //Debug.Log("Valeur liste entrée redo = " + ifPuzzleCreated);
        GameObject pieceToRedo = (GameObject)recoverList[cptUndo - 1];
        if (pieceToRedo.tag == "IfPuzzle")
        {
            Debug.Log("Recréation piece IF " + cptIfPuzzle);
            cptIfPuzzle = numberIfPuzzle() + 1;
            Debug.Log("Il y a maintenant " + cptIfPuzzle + " pièces IF");
        }
        pieceToRedo.SetActive(true);
        recoverList.RemoveAt(cptUndo - 1);
        cptUndo = recoverList.Count;
        cptObjects++;
    }

}
