using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class GameSettingsScript : MonoBehaviour
{
    public string gamePath;
    public int nbPlayers;
    public Dropdown nbPlayersDropdown;
    public GameObject[] _teamsHUD;
    public List<string> teams = new List<string>();

    // Use this for initialization
    void Start ()
    {
        //Updating();
    }
	
	// Update is called once per frame
	void Update ()
    {

        nbPlayers = int.Parse(nbPlayersDropdown.captionText.text);
        for (int i = 0; i < _teamsHUD.Length; i ++)
        {
            if (i < nbPlayers) { _teamsHUD[i].SetActive(true); }
            else { _teamsHUD[i].SetActive(false); }
        }

        
    }

    string stringDifference(string s, string z)
    {
        string res = "";
        for (int i = 0; i < s.Length; i++)
        {
            if (i >= z.Length)
            {
                res = res + s[i];
            }
        }
        return res;
    }

    public void Updating()
    {
        gamePath = Application.streamingAssetsPath + "/teams/TestBot/";
        //gamePath = Application.streamingAssetsPath+"/teams/"+GameObject.Find("GameManager").GetComponent<GameManager>()._gameName+ "/";
        teams = new List<string>();
        string[] fileEntries = Directory.GetFiles(gamePath);
        foreach (string s in fileEntries)
        {
            string team = stringDifference(s, gamePath);
            if (team.Contains(".wbt") && !team.Contains(".meta"))
            {
                teams.Add(team);
            }
        }

   
    }
}
