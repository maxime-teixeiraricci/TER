using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
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
        string path = Application.dataPath + "/StreamingAssets/teams/TestBot/";
        foreach (string file in Directory.GetFiles(path))
        {
            //Debug.Log("VALUE FILE = " + file);
            string res = file.Replace(path, "");
            //Debug.Log("VALUE RES =  " + file.Replace(path, ""));
            //Debug.Log("VALUE TEAMNAME = " + teamName);
            if (res == teamName + ".wbt")
            {
                errorText.SetActive(true);
                Text error = errorText.GetComponentInChildren<Text>();
                error.text = "L'équipe existe déjà !";
                return;
            }
        }

        List<int> listInt = new List<int>();
        for(int i = 0; i < teamName.Length; i++)
        {
            listInt.Add(System.Convert.ToInt32(teamName[i]));
        }

        for(int i = 0; i < listInt.Count; i++)
        {
            int result = listInt[i];
            if((result > 90 && result < 97) || (result < 65 && result > 57) || result > 122 )
            {
                //Debug.Log("REASON ERROR = " + result);
                errorText.SetActive(true);
                Text error = errorText.GetComponentInChildren<Text>();
                error.text = "Nom invalide ! (a-zA-Z0-9)";
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


