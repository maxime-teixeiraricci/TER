using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTeams : MonoBehaviour {

	// Use this for initialization
	void Start () {
        TeamsPerformance p = new TeamsPerformance();
        p.WriteStats(new string[] { "DefaultTeam", "EquipeTest A", "testTeamOpponent", "equipetriche" }, "DefaultTeam", 4);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
