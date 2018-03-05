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
        //GameObject puzzleCloneRecover = puzzleClone.AddComponent<GameObject>();
        listPieces.Add(puzzleClone);
        //recoverList.Add(puzzleCloneRecover);
        cptObjects = listPieces.Count;
    }

    public void OnMouseOver()
    {
        if (Input.GetKeyDown("delete"))
        {
            Undo();
        }
    }

    public void Undo()
    {

        pieceToUndo = (GameObject)listPieces[cptObjects - 1];
        recoverList.Add(pieceToUndo);
        pieceToUndo.SetActive(false);
        
        //listPieces.Remove(listPieces[cptObjects - 1]);
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
        //GameObject pieceToRedo = (GameObject)recoverList[cptUndo - 1];
        //Debug.Log("PIECE TO REDOOOOO = " + pieceToRedo);
        //listPieces.Add(pieceToRedo);
        //GameObject redoPiece = (GameObject)Instantiate(puzzle, new Vector3((float)recoverList[cptUndo - 3],
                                                                          // (float)recoverList[cptUndo - 2],
                                                                          // (float)recoverList[cptUndo - 1]), 
                                                                          // Quaternion.identity);
        //recoverList.RemoveAt(cptUndo - 1);
        //recoverList.RemoveAt(cptUndo - 1);
        //recoverList.RemoveAt(cptUndo - 1);
    }

}
