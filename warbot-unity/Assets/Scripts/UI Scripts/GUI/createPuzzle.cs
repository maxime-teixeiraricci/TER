using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class createPuzzle : MonoBehaviour {

    public GameObject puzzle;
    public GameObject puzzle2;
    public ArrayList listPieces = new ArrayList();
    public ArrayList recoverList = new ArrayList();
    public static int cptObjects;
    public int sizeRecoverList;

    public void OnMouseDown()
    {
        GameObject puzzleClone = (GameObject)Instantiate(puzzle, GameObject.Find("Editeur").transform);
        listPieces.Add(puzzleClone);
        cptObjects = listPieces.Count;
        Debug.Log("TAILLE LISTE ONCLICK DOOOOOOWN = " + cptObjects);
    }

    public void Undo()
    {
        GameObject pieceToUndo = (GameObject)listPieces[cptObjects - 1];
        recoverList.Add(pieceToUndo);
        sizeRecoverList = recoverList.Count;
        Destroy(pieceToUndo);
        listPieces.Remove(listPieces[cptObjects - 1]);
        cptObjects = listPieces.Count;
    }

}
