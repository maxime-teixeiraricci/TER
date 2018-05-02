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


    // Methode créant un Comportement ( liste d'Instructions )
    /*
    public void createBehavior()
    {
        listPieces.RemoveRange(0, listPieces.Count);
        numberIfPuzzle = countIfPuzzle();                                                   // Le nombre de pièces IF présentes sur le board
        GameObject startPuzzle = GameObject.FindGameObjectWithTag("StartPuzzle");           // La pièce de puzzle de départ
        int initialX = startPuzzle.GetComponent<ManageDragAndDrop>().posGridX;              // Ses coordonnées
        int initialY = startPuzzle.GetComponent<ManageDragAndDrop>().posGridY;
        string[] I;
        foreach (GameObject puzzle in GameObject.FindGameObjectsWithTag("IfPuzzle"))
        {
            if(puzzle.GetComponent<ManageDragAndDrop>().posGridX == initialX && puzzle.GetComponent<ManageDragAndDrop>().posGridY + 1 == initialY)
            {
                initialIf = puzzle;
                listPieces.Add(initialIf);
                currentIf = initialIf;
                break;
            }
        }
        while (listPieces.Count != numberIfPuzzle)
        {
            nextIf = currentIf.GetComponent<IfPuzzleScript>().puzzleIfObject;
            listPieces.Add(nextIf);
            initialX = nextIf.GetComponent<ManageDragAndDrop>().posGridX;
            initialY = nextIf.GetComponent<ManageDragAndDrop>().posGridY;
            currentIf = nextIf;
            
        }

        for(int i = 0; i < listPieces.Count; i++)
        {
            listBehavior.Add(listPieces[i].GetComponent<IfPuzzleScript>().createInstruction());
        }

        for(int i = 0; i < listPieces.Count; i++)
        {
            I = listBehavior[i]._listeStringPerceptsVoulus;
            
            for (int j = 0; j < I.Length; j++)
            {
                string h = I[j];
            }
        }
        createXML();
        Debug.Log("here");
        listBehavior.RemoveRange(0, listBehavior.Count);
    }
    */
    public void createBehavior()
    {
        GameObject startPuzzle = GameObject.FindGameObjectWithTag("StartPuzzle");
        GameObject ifpuzzle = startPuzzle.GetComponent<StartPuzzleScript>().ifPuzzle;
        while (ifpuzzle != null && ifpuzzle.activeSelf == true)
        {
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

        XMLWarbotInterpreter interpreter = new XMLWarbotInterpreter();
        interpreter.behaviorToXml(teamName, path, unitName, listBehavior);

    }

    /*
    public void createBehavior()
    {
        List<GameObject> ifPuzzleList = new List<GameObject>();
        // Instruction instruction = manager.GetComponent
        foreach (GameObject puzzle in GameObject.FindGameObjectsWithTag("IfPuzzle"))
        {
            listPieces.Add(puzzle.GetComponent<IfPuzzleScript>().ID);
            ifPuzzleList.Add(puzzle);
            //Instruction instruction = puzzle.GetComponent<IfPuzzleScript>().createInstruction();
            //listBehavior.Add(instruction);
        }
        listPieces.Sort();

        for (int i = 0; i < listPieces.Count; i++)
        {
            for (int j = 0; j < ifPuzzleList.Count; j++)
            {
                if (ifPuzzleList[j].GetComponent<IfPuzzleScript>().ID.Equals(listPieces[i]))
                {
                    listBehavior.Add(ifPuzzleList[j].GetComponent<IfPuzzleScript>().createInstruction());
                }
            }
        }
    }*/
}
