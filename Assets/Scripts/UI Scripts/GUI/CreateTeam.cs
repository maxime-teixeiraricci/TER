using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
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

    // Affiche la fenêtre pop-up pour créer une nouvelle équipe
    public void NameInput()
    {
        window.SetActive(true);
    }

    // Masque la fenêtre pop-up
    public void disableWindow()
    {
        window.SetActive(false);
    }

    // Vérifie le nom d'équipe entré par l'utilisateur, pour empêcher l'utilisation de caractères spéciaux
    // Caractères acceptés : a-zA-Z0-9
    public void validateName()
    {
        string teamName = mainInputField.text;
        string path = Application.streamingAssetsPath + "/teams/TestBot/";


        List<int> listInt = new List<int>();
        for (int i = 0; i < teamName.Length; i++)
        {
            listInt.Add(Convert.ToInt32(teamName[i]));
        }

        for (int i = 0; i < listInt.Count; i++)
        {
            int result = listInt[i];
            if ((result > 90 && result < 97) || (result < 65 && result > 57) || (result < 48 && result > 32) || result > 122)
            {
                
                errorText.SetActive(true);
                Text error = errorText.GetComponentInChildren<Text>();
                error.text = "Nom invalide ! (a-zA-Z0-9)";
                return;
            }
            if (listInt[0] == 32) //&& result == 32)
            {
                errorText.SetActive(true);
                Text errorSpace = errorText.GetComponentInChildren<Text>();
                errorSpace.text = "Nom invalide ! (a-zA-Z0-9)";
                return;

            }
        }

        if(listInt.Count == 0)
        {
            errorText.SetActive(true);
            Text errorSpace = errorText.GetComponentInChildren<Text>();
            errorSpace.text = "Nom invalide ! (a-zA-Z0-9)";
            return;
        }

        foreach (string file in Directory.GetFiles(path))
        {
            string res = file.Replace(path, "");
            if (res == teamName + ".wbt")
            {
                errorText.SetActive(true);
                Text error = errorText.GetComponentInChildren<Text>();
                error.text = "L'équipe existe déjà !";
                return;
            }
        }

        if (!System.IO.File.Exists(Application.streamingAssetsPath + "/ELO/" + teamName + ".elo"))
        {
            File.Create(Application.streamingAssetsPath + "/ELO/" + teamName + ".elo").Dispose();
            File.WriteAllLines(Application.streamingAssetsPath + "/ELO/" + teamName + ".elo", new string[] { 1000 + "" });
        }


        dropOption.Add(teamName);
        teamDropDown.AddOptions(dropOption);
        XMLWarbotInterpreter interpreter = new XMLWarbotInterpreter();
        interpreter.generateEmptyFile(teamName, path);
        dropOption.Clear();
        errorText.SetActive(false);
        window.SetActive(false);
    }
}


