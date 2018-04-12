using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipeTeamSetup : MonoBehaviour {

	// Use this for initialization
	void Start () {
        UpdateOptions();
    }
	
	// Update is called once per frame
	void Update ()
    {
        

    }

    public void UpdateOptions()
    {
        GetComponent<Dropdown>().ClearOptions();
        GameObject.Find("GameSetting").GetComponent<GameSettingsScript>().Updating();
        List<string> nameTeams = GameObject.Find("GameSetting").GetComponent<GameSettingsScript>().teams;
        List<string> customNames = new List<string>();
        for(int i = 0; i < nameTeams.Count; i++)
        {
            string tmp = nameTeams[i].Replace(" ", "_");
            customNames.Add(tmp.Replace(".wbt", ""));
        }
        GetComponent<Dropdown>().AddOptions(customNames);
    }
}
