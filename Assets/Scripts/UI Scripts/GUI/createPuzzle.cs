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
    public static GameObject pieceToUndo;
    public static GameObject pieceToRedo;

    public void create()
    {
        GameObject puzzleClone = (GameObject)Instantiate(puzzle, GameObject.Find("Editeur").transform);
        if (puzzleClone.GetComponent<ActionPuzzleScript>())
        {
            Debug.Log("Label ACTION = " + _label);
            puzzleClone.GetComponent<ActionPuzzleScript>().actionName = _label;
        }
        else if (puzzleClone.GetComponent<CondPuzzleScript>())
        {
            puzzleClone.GetComponent<CondPuzzleScript>()._labelText.text = _label;
        }

        puzzleClone.GetComponent<RectTransform>().anchoredPosition = positionInitial;
        listPieces.Add(puzzleClone);
        cptObjects = listPieces.Count;
    }


    public void Undo()
    {

        pieceToUndo = (GameObject)listPieces[cptObjects - 1];
        recoverList.Add(pieceToUndo);
        pieceToUndo.SetActive(false);
        cptObjects--;
        cptUndo = recoverList.Count;
    }

    public void Redo()
    {
        GameObject pieceToRedo = (GameObject)recoverList[cptUndo - 1];
        pieceToRedo.SetActive(true);
        recoverList.RemoveAt(cptUndo - 1);
        cptUndo = recoverList.Count;
        cptObjects++;
    }

}
