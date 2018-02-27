using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class createPuzzle : MonoBehaviour {

    public GameObject puzzle;
    public GameObject puzzle2;

    public void OnMouseDown()
    {
        GameObject puzzleClone = (GameObject)Instantiate(puzzle, GameObject.Find("Editeur").transform);
    }
}
