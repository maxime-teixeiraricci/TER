using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateTeam : MonoBehaviour
{
    public GameObject window;
    public Dropdown teamDropDown;
    List<string> dropOption = new List<string>();
    public InputField mainInputField;

    public void NameInput()
    {
        window.SetActive(true);
    }

    public void disableWindow()
    {
        window.SetActive(false);
    }

    public void validateName()
    {
        string teamName = mainInputField.text;
        dropOption.Add(teamName);
        teamDropDown.AddOptions(dropOption);
        window.SetActive(false);
    }
}


