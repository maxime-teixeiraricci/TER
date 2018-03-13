using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CondPuzzleScript : MonoBehaviour
{
    public GameObject nextCondPuzzle;
    public string condName;
    ManageDragAndDrop manager;
    public Text _labelText;

    // Use this for initialization
    void Start ()
    {
        manager = GetComponent<ManageDragAndDrop>();
        _labelText.GetComponent<Text>().text = condName;
    }

    // Update is called once per frame
    void Update ()
    {
        nextCondPuzzle = null;
        foreach (GameObject puzzle in GameObject.FindGameObjectsWithTag("CondPuzzle"))
        {
            if (manager.posGridX + 2 == puzzle.GetComponent<ManageDragAndDrop>().posGridX && manager.posGridY == puzzle.GetComponent<ManageDragAndDrop>().posGridY)
            {
                nextCondPuzzle = puzzle;
                break;
            }
        }
    }
}
