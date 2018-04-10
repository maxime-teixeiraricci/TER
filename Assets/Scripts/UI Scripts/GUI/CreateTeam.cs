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
    XMLWarbotInterpreter interpreter;

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
        string path = Constants.teamsDirectory + Constants.gameModeWarBot;
        dropOption.Add(teamName);
        teamDropDown.AddOptions(dropOption);
        interpreter = new XMLWarbotInterpreter();
        interpreter.generateEmptyFile(teamName, path);

        window.SetActive(false);
    }
}


