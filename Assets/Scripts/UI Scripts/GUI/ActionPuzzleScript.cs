using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionPuzzleScript : MonoBehaviour {
    public string actionName;
    public Text _labelText;
    ManageDragAndDrop manager;
    public string validPlace = "false";
    Image image;
    Color defaultColor;
    Color validColor = new Color(1, 105.0F / 255.0F, 121.0F / 255);

    private void Start()
    {
        manager = GetComponent<ManageDragAndDrop>();
        image = GetComponent<Image>();
        defaultColor = image.color;
    }

    void Update()
    {
        _labelText.GetComponent<Text>().text = actionName;

        image.color = defaultColor;
        validPlace = "false";

        foreach (GameObject puzzle in GameObject.FindGameObjectsWithTag("IfPuzzle"))
        {
            if (manager.posGridX - 1 == puzzle.GetComponent<ManageDragAndDrop>().posGridX
                && manager.posGridY + 1 == puzzle.GetComponent<ManageDragAndDrop>().posGridY)
            {
                image.color = validColor;
                validPlace = "true";
                break;
            }
        }

    }
}
