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
            print("TestENDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDD");
            GameObject slider = GameObject.Find("Slider");
            slider.SetActive(false);
            print("after slider");
         /*   GameObject minimap = GameObject.Find("Minimap Image");
            minimap.GetComponent<Image>().enabled = false;
            //minimap.SetActive(false);
            print("after minimpa");
            GameObject stats = GameObject.FindGameObjectWithTag("HUDstats");
            stats.GetComponent<Image>().enabled = false;
            print("after HUD");
            GameObject msgb = GameObject.Find("MessageButton");
            msgb.GetComponent<Image>().enabled = false;
            msgb.GetComponent<Text>().enabled = false;
            print("after MsgButton");
            GameObject msgbs = GameObject.Find("StatsButton");
            msgbs.GetComponent<Image>().enabled = false;
            msgbs.GetComponent<Text>().enabled = false;
            print("after StatsButton");
            GameObject scorebar = GameObject.Find("ScoreBar");
            scorebar.GetComponent<Image>().enabled = false;
            scorebar.GetComponent<Text>().enabled = false;
            print("after ScoreBar");*/
            GameObject.Find("TextWinnerTeam").GetComponent<Text>().text = "Winner team : " + winner;
            print("after Winnerteam");
            GameObject.Find("TextGameOver").GetComponent<Text>().text = "Game Over ! ";
            print("after TextGO"); 
            Time.timeScale = 0;
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
