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
	void Update () {
        

    }

    public void UpdateOptions()
    {
        GetComponent<Dropdown>().ClearOptions();
        GameObject.Find("GameSetting").GetComponent<GameSettingsScript>().Updating();
        GetComponent<Dropdown>().AddOptions(GameObject.Find("GameSetting").GetComponent<GameSettingsScript>().teams);
    }
}
