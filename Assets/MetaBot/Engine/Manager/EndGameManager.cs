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
    public GameObject GO;
    public GameObject textWinnerTeam;
    public GameObject button1;
    public GameObject button2;
    public GameObject fond;
    public GameObject canvashud;

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
            textWinnerTeam.GetComponent<Text>().text = "Winner team : " + winner;
            print("after Winnerteam");
            GO.GetComponent<Text>().text = "Game is Over !";
            canvashud.SetActive(false);
            button1.SetActive(true);
            button2.SetActive(true);
            fond.SetActive(true);
            
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
