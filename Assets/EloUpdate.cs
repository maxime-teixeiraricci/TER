﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EloUpdate : MonoBehaviour {
    public GameObject TeamName1, TeamName2, TeamName3, TeamName4;
    public GameObject TeamElo1, TeamElo2, TeamElo3, TeamElo4;
    string oldName1, oldName2, oldName3, oldName4;
    TeamsPerformance t;
    // Use this for initialization
    void Start () {
        oldName1 = TeamName1.GetComponent<Text>().text;
        oldName2 = TeamName2.GetComponent<Text>().text;
        oldName3 = TeamName3.GetComponent<Text>().text;
        oldName4 = TeamName4.GetComponent<Text>().text;
        t = new TeamsPerformance();
        TeamElo1.GetComponent<Text>().text = ""+t.GetTeamElo(oldName1);
        TeamElo2.GetComponent<Text>().text = "" + t.GetTeamElo(oldName2);
        TeamElo3.GetComponent<Text>().text = "" + t.GetTeamElo(oldName3);
        TeamElo4.GetComponent<Text>().text = "" + t.GetTeamElo(oldName4);
    }
	
	// Update is called once per frame
	void Update () {
		if (!TeamName1.GetComponent<Text>().text.Equals(oldName1))
        {
            oldName1 = TeamName1.GetComponent<Text>().text;
            TeamElo1.GetComponent<Text>().text = "Elo : AAAH" + t.GetTeamElo(oldName1);
        }

        if (!TeamName2.GetComponent<Text>().text.Equals(oldName2))
        {
            oldName2 = TeamName2.GetComponent<Text>().text;
            TeamElo2.GetComponent<Text>().text = "Elo : AAAH" + t.GetTeamElo(oldName2);
        }

        if (!TeamName3.GetComponent<Text>().text.Equals(oldName3))
        {
            oldName3 = TeamName3.GetComponent<Text>().text;
            TeamElo3.GetComponent<Text>().text = "Elo : AAAH" + t.GetTeamElo(oldName3);
        }

        if (!TeamName4.GetComponent<Text>().text.Equals(oldName4))
        {
            oldName4 = TeamName4.GetComponent<Text>().text;
            TeamElo4.GetComponent<Text>().text = "Elo : AAAH" + t.GetTeamElo(oldName4);
        }
    }
}
