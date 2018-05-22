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

    // Comptabilise le nombre total de pièces IF présentes sur l'éditeur, et le retourne
    public int numberIfPuzzle()
    {
        pieceIf = new ArrayList();
        foreach (GameObject puzzle in GameObject.FindGameObjectsWithTag("IfPuzzle"))
        {
            if (!pieceIf.Contains(puzzle))
            {
                pieceIf.Add(puzzle);
            }
        }
        return pieceIf.Count;
    }

    // Crée une pièce de puzzle, avec le label approprié
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
    }

    // Lorsqu'une pièce de puzzle est supprimée, on l'ajoute à une liste, pour en garder une trace
    public void Undo()
    {
        if( cptObjects > 0 )
        {
            pieceToUndo = (GameObject)listPieces[cptObjects - 1];
            // Si la pièce supprimée est une pièce IF, on oublie pas de mettre à jour le compteur de pièces IF
            if (pieceToUndo.tag == "IfPuzzle")
            {
                cptIfPuzzle = numberIfPuzzle() - 1;
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

    // Ré instantie la dernière pièce de la liste recoverList
    public void Redo()
    {
        if( recoverList.Count > 0 )
        {
            GameObject pieceToRedo = (GameObject)recoverList[cptUndo - 1];
            if (pieceToRedo.tag == "IfPuzzle")
            {
                cptIfPuzzle = numberIfPuzzle() + 1;
            }

            pieceToRedo.SetActive(true);
            recoverList.RemoveAt(cptUndo - 1);
            cptObjects++;

            cptUndo = recoverList.Count;
            
        }
    }

}
