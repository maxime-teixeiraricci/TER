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
    LoadFile loadfile;
    public string initialText = "Voulez vous supprimer ";

    // Affiche la fenêtre pop-up de confirmation de suppression d'équipe
    public void DisplayWindow()
    {
        Traducteur t = new Traducteur();
        t.langue = GameObject.Find("GameManager").GetComponent<LangageLoader>().language;
        t.setTextOriginal(initialText);
        initialText = t.traduction;
        Window.SetActive(true);
        windowText = windowDelete.GetComponentInChildren<Text>();
        windowText.text = initialText;
        nameTeam = dropdown.captionText.text;
        windowText.text = windowText.text + nameTeam + " ?";
    }

    // Masque la fenêtre pop-up
    public void DisableWindow()
    {
        Window.SetActive(false);
    }

    // Supprime l'équipe selectionnée de la liste des équipes, ainsi que les fichiers .wbt et .meta associés
    public void Delete()
    {
        nameTeam = dropdown.captionText.text;
        string path = Application.streamingAssetsPath + "/teams/TestBot/"; //Application.dataPath + "/StreamingAssets/teams/TestBot/";
        string pathStats = Application.streamingAssetsPath + "/Stats/";
        foreach (string file in Directory.GetFiles(path))
        {
            string res = file.Replace(path, "");
            if (res == nameTeam + ".wbt")
            
            {
                string fileMeta = file + ".meta";
                File.Delete(file);
                File.Delete(fileMeta);
                break;
            }
        }

        foreach (string file in Directory.GetFiles(pathStats))
        {
            string res = file.Replace(pathStats , "");
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

    // Met à jour la liste des teams existantes 
    public void Updating()
    {
        string gamePath = Application.streamingAssetsPath + "/teams/TestBot/";  //Application.dataPath + "/StreamingAssets/teams/TestBot/";
        List<string> teams = new List<string>();
        string[] fileEntries = Directory.GetFiles(gamePath);
        foreach (string s in fileEntries)
        {
            string team = s.Replace(gamePath, "");
            if (team.Contains(".wbt") && !team.Contains(".meta"))
            {
                team = team.Replace(".wbt", "");
                teams.Add(team);
            }
        }
        dropdown.ClearOptions();
        dropdown.AddOptions(teams);
        loadfile = GameObject.Find("LoadFile").GetComponent<LoadFile>();
        loadfile.createBehaviorFromXML();
    }
}
