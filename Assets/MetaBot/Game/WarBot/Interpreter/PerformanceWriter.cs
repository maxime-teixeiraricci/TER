using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class PerformanceWriter : MonoBehaviour {

    public Dropdown change;
	// Use this for initialization

	void Start () {
        ReadStats();
    }

    public void ReadStats()
    {
        GameObject.Find("StatText").GetComponent<Text>().text = "";
        string team = change.captionText.text;
        team = team.Replace("_", " ");
        TeamsPerformance t = new TeamsPerformance();
        KeyValuePair<string, List<Matchup>> stats = TeamsPerformance.getStats(team);

        foreach (Matchup m in stats.Value)
        {
            string back = GameObject.Find("StatText").GetComponent<Text>().text;
            GameObject.Find("StatText").GetComponent<Text>().text = back + '\n' + m.opponent + '\n' + "Wins " + m.victoryCount + " / Loses " + (m.totalMatchCount - m.victoryCount) + '\n';
        }
    }
}
