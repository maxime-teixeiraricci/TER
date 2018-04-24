using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System.Xml;

public class DeleteTeam : MonoBehaviour
{

    public GameObject Window;
    public GameObject windowDelete;
    public Dropdown dropdown;
    Text windowText;
    string nameTeam;
    public string initialText = "Voulez vous supprimer ";


    public void DisplayWindow()
    {
        Window.SetActive(true);
        windowText = windowDelete.GetComponentInChildren<Text>();
        windowText.text = initialText;
        nameTeam = dropdown.captionText.text;
        windowText.text = windowText.text + nameTeam + " ?";
    }

    public void DisableWindow()
    {
        Window.SetActive(false);
    }

    public void Delete()
    {
        nameTeam = dropdown.captionText.text;
        string path = Application.streamingAssetsPath + "/teams/TestBot/"; //Application.dataPath + "/StreamingAssets/teams/TestBot/";
        string pathStats = Application.streamingAssetsPath + "/Stats/";
        foreach (string file in Directory.GetFiles(path))
        {
            string res = file.Replace(path, "");
            Debug.Log("RES = " + res);
            Debug.Log("nameTeam = " + nameTeam);
            if (res == nameTeam + ".wbt")
            
            {
                Debug.Log("FILE QUI VA ETRE DELETE = " + file);
                string fileMeta = file + ".meta";
                File.Delete(file);
                File.Delete(fileMeta);
                break;
            }
        }

        foreach (string file in Directory.GetFiles(pathStats))
        {
            string res = file.Replace(pathStats , "");
            //Debug.Log("NAME FILE = " + res);
            if (res == nameTeam + ".stat")
            {
                string fileMeta = file + ".meta";
                File.Delete(file);
                File.Delete(fileMeta);
                break;
            }

        }

            Updating();
        Window.SetActive(false);
    }

    public void Updating()
    {

        string gamePath = Application.streamingAssetsPath + "/teams/TestBot/";  //Application.dataPath + "/StreamingAssets/teams/TestBot/";
        //Debug.Log(Application.dataPath);
        List<string> teams = new List<string>();
        string[] fileEntries = Directory.GetFiles(gamePath);
        foreach (string s in fileEntries)
        {
            //print(s);
            string team = s.Replace(gamePath, "");
            if (team.Contains(".wbt") && !team.Contains(".meta"))
            {
                team = team.Replace(".wbt", "");
                teams.Add(team);
            }
        }
        dropdown.ClearOptions();
        dropdown.AddOptions(teams);
    }
}
