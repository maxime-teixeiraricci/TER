using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class createPuzzle : MonoBehaviour {

    public GameObject puzzle;
    public GameObject puzzle2;
    public static ArrayList listPieces = new ArrayList();
    public static ArrayList recoverList = new ArrayList();
    public static int cptObjects;
    public static int cptUndo;
    public static GameObject pieceToUndo;
    public static GameObject pieceToRedo;

    public void OnMouseDown()
    {
        GameObject puzzleClone = (GameObject)Instantiate(puzzle, GameObject.Find("Editeur").transform);
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
