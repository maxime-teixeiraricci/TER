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
        GameObject puzzleClone = (GameObject)Instantiate(puzzle, GameObject.Find("MaskEditeur").transform);
        if (puzzleClone.GetComponent<PuzzleScript>())
        {
            puzzleClone.GetComponent<PuzzleScript>()._value = _label;
        }
        puzzleClone.GetComponent<RectTransform>().anchoredPosition = new Vector3(positionInitial.x, positionInitial.y,15);
        puzzleClone.transform.parent = GameObject.Find("Editeur").transform;
        puzzleClone.GetComponent<RectTransform>().localPosition = new Vector3(puzzleClone.GetComponent<RectTransform>().localPosition.x, puzzleClone.GetComponent<RectTransform>().localPosition.y, 10);
        listPieces.Add(puzzleClone);
        cptObjects = listPieces.Count;
        
        //Debug.Log("Nombre piece IF a la création " + cptIfPuzzle);

    }


    public void Undo()
    {
        if( cptObjects > 0 )
        {
            pieceToUndo = (GameObject)listPieces[cptObjects - 1];
            if (pieceToUndo.tag == "IfPuzzle")
            {
                Debug.Log("Suppression piece IF n°" + cptIfPuzzle);
                cptIfPuzzle = numberIfPuzzle() - 1;
                Debug.Log("Il reste " + cptIfPuzzle + " pièces IF");
            }
            if(pieceToUndo.activeSelf == true)
            {
                recoverList.Add(pieceToUndo);
                pieceToUndo.SetActive(false);
                cptObjects--;
            }

            else if(pieceToUndo.activeSelf == false)
            {
                pieceToUndo.SetActive(true);
                GameObject tmp = pieceToUndo;
                listPieces.Remove(pieceToUndo);
                listPieces.Insert(0,pieceToUndo);
            }

            cptUndo = recoverList.Count;
        }
    }

    public void Redo()
    {
        if( recoverList.Count > 0 )
        {
            GameObject pieceToRedo = (GameObject)recoverList[cptUndo - 1];
            if (pieceToRedo.tag == "IfPuzzle")
            {
                Debug.Log("Recréation piece IF " + cptIfPuzzle);
                cptIfPuzzle = numberIfPuzzle() + 1;
                Debug.Log("Il y a maintenant " + cptIfPuzzle + " pièces IF");
            }

            pieceToRedo.SetActive(true);
            recoverList.RemoveAt(cptUndo - 1);
            cptObjects++;

            cptUndo = recoverList.Count;
            
        }
    }

}
