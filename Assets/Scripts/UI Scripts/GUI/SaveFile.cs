using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveFile : MonoBehaviour {

    List<Instruction> listBehavior = new List<Instruction>();
    createPuzzle editor;
    ManageDragAndDrop manager;
    GameObject initialIf;
    GameObject currentIf;
    GameObject nextIf;
    GameObject numberIf;
    public ArrayList pieceIf = new ArrayList();
    //List<GameObject> listPieces = new List<GameObject>();
    public ArrayList listPieces = new ArrayList();

    int numberIfPuzzle;

	// Use this for initialization
	void Start () {
        manager = GetComponent<ManageDragAndDrop>();
        editor = GetComponent<createPuzzle>();
        numberIfPuzzle = 0;
    }
	
	// Update is called once per frame
	void Update () {
		
	}


    // Comtpe le nombre de pièces IF présentes dans l'éditeur
    /*
    public int countIfPuzzle()
    {
        pieceIf = new ArrayList();
        foreach (GameObject puzzle in GameObject.FindGameObjectsWithTag("IfPuzzle"))
        {
            if (!pieceIf.Contains(puzzle))
            {
                pieceIf.Add(puzzle);
            }
            // ifPuzzleCreated = pieceIf.Count;
            Debug.Log("VAL COUNT IF = " + pieceIf.Count);
        }
        return pieceIf.Count;
    }
    */

    //Permet de récupérer une pièce présente sur la grille, à l'aide de ses coordonnées X et Y relatives aux cases
    /*
    public GameObject getPiece(int posGridX, int posGridY)
    {
        foreach (GameObject puzzle in GameObject.FindGameObjectsWithTag("IfPuzzle"))
        {
            if (puzzle.GetComponent<ManageDragAndDrop>().posGridX == posGridX && puzzle.GetComponent<ManageDragAndDrop>().posGridY == posGridY)
            {
                return puzzle;
            }

        }
        return null;
    }
    */

    /*
    public void createBehavior()
    {
        
        numberIfPuzzle = countIfPuzzle();        // Le nombre de pièces IF présentes sur le board
        int initialX = -4;
        int initialY = 2;
        foreach (GameObject puzzle in GameObject.FindGameObjectsWithTag("IfPuzzle"))
        {
            if(puzzle.GetComponent<ManageDragAndDrop>().posGridX == initialX && puzzle.GetComponent<ManageDragAndDrop>().posGridY == initialY)
            {
                initialIf = puzzle;
                listPieces.Add(initialIf);
                currentIf = initialIf;
                break;
            }
        }
        while (listPieces.Count != numberIfPuzzle)
        {
            nextIf = getPiece(initialX, initialY - 2);
            listPieces.Add(nextIf);
            initialX = nextIf.GetComponent<ManageDragAndDrop>().posGridX;
            initialY = nextIf.GetComponent<ManageDragAndDrop>().posGridY;
            currentIf = nextIf;
            
        }

        for(int i = 0; i < listPieces.Count; i++)
        {
            Debug.Log("LAST LOOP : " + listPieces[i].GetComponent<IfPuzzleScript>().createInstruction());
            listBehavior.Add(listPieces[i].GetComponent<IfPuzzleScript>().createInstruction());
        }
    }
    */

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
    }
}
