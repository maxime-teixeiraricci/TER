using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class NewFile : MonoBehaviour {

    GameObject editeur;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void clearEditor()
    {
        /*
        GameObject[] listEdit;
        GameObject[] listEdit2;
        GameObject[] listEdit3;
        listEdit = GameObject.FindGameObjectsWithTag("IfPuzzle");
        listEdit2 = GameObject.FindGameObjectsWithTag("ActionPuzzle");
        listEdit3 = GameObject.FindGameObjectsWithTag("CondPuzzle");
        if (listEdit.Length > 0 || listEdit2.Length > 0 || listEdit3.Length > 0)*/
        //{
            foreach (GameObject puzzle in GameObject.FindGameObjectsWithTag("IfPuzzle"))
            {

                    Destroy(puzzle);
               
            }

            foreach (GameObject puzzle in GameObject.FindGameObjectsWithTag("ActionPuzzle"))
            {
            
                    Destroy(puzzle);
             
            }

            foreach (GameObject puzzle in GameObject.FindGameObjectsWithTag("CondPuzzle"))
            {
                
                    Destroy(puzzle);
                
            }
        }
    
}
