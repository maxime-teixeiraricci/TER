using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveFile : MonoBehaviour {

    List<Instruction> listBehavior = new List<Instruction>();
    createPuzzle editor;
    ManageDragAndDrop manager;
    GameObject initialIf;
    GameObject currentIf;
    GameObject nextIf;
    GameObject numberIf;
    public Dropdown team;
    public Dropdown unit;
    public ArrayList pieceIf = new ArrayList();
    List<GameObject> listPieces = new List<GameObject>();
   // public ArrayList listPieces = new ArrayList();

    int numberIfPuzzle;

	// Use this for initialization
	void Start () {
        manager = GetComponent<ManageDragAndDrop>();
        editor = GetComponent<createPuzzle>();
        numberIfPuzzle = 0;
    }
	

    // Compte le nombre de pièces IF présentes dans l'éditeur
    public int countIfPuzzle()
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

    public void createBehavior()
    {
        GameObject startPuzzle = GameObject.FindGameObjectWithTag("StartPuzzle");
        GameObject ifpuzzle = startPuzzle.GetComponent<StartPuzzleScript>().ifPuzzle;
        while (ifpuzzle != null && ifpuzzle.activeSelf == true)
        {
            // Crée l'instruction (conditions + une/des actions) correspondate au If courant
            listBehavior.Add(ifpuzzle.GetComponent<IfPuzzleScript>().createInstruction());
            ifpuzzle = ifpuzzle.GetComponent<IfPuzzleScript>().puzzleIfObject;
        }
        createXML();
        Debug.Log("Saving file done !");
        listBehavior.RemoveRange(0, listBehavior.Count);
    }

    public void createXML()
    {
        //recuperation de l'unité actuellement traitée et de l'equipe
        string teamName = team.captionText.text;
        string unitName = unit.captionText.text;
        string path = Application.dataPath + "/StreamingAssets/" + Constants.teamsDirectory + Constants.gameModeWarBot;
        if(teamName != "")
        {
            XMLWarbotInterpreter interpreter = new XMLWarbotInterpreter();
            interpreter.behaviorToXml(teamName, path, unitName, listBehavior);
        }
        

    }
}
