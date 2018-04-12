using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class CreateTeam : MonoBehaviour
{
    public GameObject window;
    public Dropdown teamDropDown;
    List<string> dropOption = new List<string>();
    public InputField mainInputField;
    public GameObject errorText;

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
        foreach (string file in Directory.GetFiles(path))
        {
            Debug.Log("VALUE FILE = " + file);
            string res = file.Replace(path + "\\", "");
            Debug.Log("VALUE RES =  " + file.Replace(path + "\\", ""));
            Debug.Log("VALUE TEAMNAME = " + teamName);
            if (res == teamName + ".wbt")
            {

                errorText.SetActive(true);
                return;
            }
        }
        dropOption.Add(teamName);
        teamDropDown.AddOptions(dropOption);
        XMLWarbotInterpreter interpreter = new XMLWarbotInterpreter();
        interpreter.generateEmptyFile(teamName, path);
        errorText.SetActive(false);
        window.SetActive(false);
    }
}


