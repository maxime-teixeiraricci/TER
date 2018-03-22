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
    public string validPlace = "false";
    Image image;
    Color defaultColor;
    Color validColor = new Color(49.0F / 255, 204.0F / 255.0F, 1);

    // Use this for initialization
    void Start ()
    {
        
        manager = GetComponent<ManageDragAndDrop>();
        _labelText.GetComponent<Text>().text = condName;
        image = GetComponent<Image>();
        defaultColor = image.color;
    }

    // Update is called once per frame
    void Update ()
    {
        UpdateIfPuzzle();
        UpdateCondPuzzle();
    }

    void UpdateIfPuzzle()
    {
        image.color = defaultColor;
        validPlace = "false";

        foreach (GameObject puzzle in GameObject.FindGameObjectsWithTag("IfPuzzle"))
        {
            if (manager.posGridX - 1 == puzzle.GetComponent<ManageDragAndDrop>().posGridX
                && manager.posGridY == puzzle.GetComponent<ManageDragAndDrop>().posGridY && puzzle.GetComponent<IfPuzzleScript>().validPlace == "true")
            {
                image.color = validColor;
                validPlace = "true";
                break;
            }
        }
    }

    void UpdateCondPuzzle()
    {
        nextCondPuzzle = null;

        foreach (GameObject puzzle in GameObject.FindGameObjectsWithTag("CondPuzzle"))
        {
            if (manager.posGridX + 2 == puzzle.GetComponent<ManageDragAndDrop>().posGridX && manager.posGridY == puzzle.GetComponent<ManageDragAndDrop>().posGridY && puzzle.GetComponent<CondPuzzleScript>().validPlace == "true")
            {
                nextCondPuzzle = puzzle;
                break;
            }

            
        }
        foreach (GameObject puzzle in GameObject.FindGameObjectsWithTag("CondPuzzle"))
        {
            if (manager.posGridX - 2 == puzzle.GetComponent<ManageDragAndDrop>().posGridX && manager.posGridY == puzzle.GetComponent<ManageDragAndDrop>().posGridY && puzzle.GetComponent<CondPuzzleScript>().validPlace == "true")
            {
                image.color = validColor;
                validPlace = "true";
                break;
            }
        }
    }
}
