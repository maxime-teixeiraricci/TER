using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class NewFile : MonoBehaviour {

    GameObject editeur;

    // Supprime toutes les pièces présentes sur l'éditeur
    public void clearEditor()
    {
        foreach (GameObject puzzle in GameObject.FindGameObjectsWithTag("IfPuzzle"))
        {
            Destroy(puzzle);  
        }

        foreach (GameObject puzzle in GameObject.FindGameObjectsWithTag("Puzzle"))
        {
            Destroy(puzzle); 
        }
    }  
}
