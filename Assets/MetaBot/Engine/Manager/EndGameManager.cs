using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGameManager : MonoBehaviour {

    public delegate bool Tests();
    public delegate void Ends();
    public Dictionary<string, Tests> _tests = new Dictionary<string, Tests>();
    public Dictionary<string, Ends> _ends = new Dictionary<string, Ends>();
    public string _gamename;
    public int winner;

    // Use this for initialization
    void Start () {
        InitTests();
        InitEnds();
        winner = -1;
        _gamename = GameObject.FindObjectOfType<GameManager>()._gameName;
	}

    public void InitEnds()
    {
        _ends["TestBot"] = delegate ()
        {
            Time.timeScale = 0;
            GameObject.Find("Slider").SetActive(false);
            print("after slider");
            if (GameObject.Find("Minimap Image").active == true)
                GameObject.Find("Minimap Image").SetActive(false);
            print("after minimpa");
            if (GameObject.Find("HUDStats").active == true)
                GameObject.FindGameObjectWithTag("HUDstats").SetActive(false);
            print("after HUD");
            if (GameObject.Find("MessageButton").active == true)
                GameObject.Find("MessageButton").SetActive(false);
            print("after MsgButton");
            if (GameObject.Find("StatsButton").active == true)
                GameObject.Find("StatsButton").SetActive(false);
            print("after StatsButton");
            if (GameObject.Find("ScoreBar").active == true)
                GameObject.Find("ScoreBar").SetActive(false);
            print("after ScoreBar");
            GameObject.Find("TextWinnerTeam").GetComponent<Text>().text = "Winner team : " + winner;
            print("after Winnerteam");
            GameObject.Find("TextWinnerTeam").SetActive(true);
            print("after winnerteam2");
            GameObject.Find("TextGameOver").SetActive(true);
            print("after TextGO");
        };

        _ends["RessourceRace"] = delegate ()
        {

        };
    }

    public void InitTests()
    {
        _tests["TestBot"] = delegate ()
        {
            List<int> teams = new List<int>();
            GameObject[] units = GameObject.FindGameObjectsWithTag("Unit");
            foreach (GameObject u in units)
            {
                if (u.GetComponent<Stats>()._unitType.Equals("Base"))
                {
                    if (!teams.Contains(u.GetComponent<Stats>()._teamIndex))
                        teams.Add(u.GetComponent<Stats>()._teamIndex);
                }
            }

            if (teams.Count == 1)
                winner = teams[0];
            print("teams count : " + teams.Count);
            return teams.Count <= 1;
        };

        _tests["RessourceRace"] = delegate ()
        {
            return false;
        };
    }

    // Update is called once per frame
    void Update () {
        if (_tests[_gamename]())
            _ends[_gamename]();
	}
}
