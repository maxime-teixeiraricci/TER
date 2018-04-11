using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeleteTeam : MonoBehaviour
{

    public GameObject Window;
    public GameObject windowDelete;
    public Dropdown dropdown;
    Text windowText;
    public string initialText = "Voulez vous supprimer ";


    public void DisableWindow()
    {
        
        Window.SetActive(false);
    }

    public void Delete()
    {
        Window.SetActive(true);
        windowText = windowDelete.GetComponentInChildren<Text>();
        windowText.text = initialText;
        string nameTeam = dropdown.captionText.text;
        windowText.text = windowText.text + nameTeam + " ?";
    }
}
