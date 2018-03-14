using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class createPuzzle2 : MonoBehaviour
{

    public GameObject puzzle;
    public GameObject puzzle2;

    public void OnMouseDown()
    {
        GameObject puzzleClone = (GameObject)Instantiate(puzzle2, GameObject.Find("Editeur").transform);
    }
}
