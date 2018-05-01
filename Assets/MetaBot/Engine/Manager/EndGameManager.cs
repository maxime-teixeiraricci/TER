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
    public string winnername;
    public GameObject timer;
    Animator anim;
    public GameObject textWinnerTeam;
    public int ressourceLimit;
    public int timeLimitSeconds;
    public bool written;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        InitTests();
        InitEnds();
        winner = -1;
        _gamename = GameObject.FindObjectOfType<GameManager>()._gameName;
        written = false;
        winnername = "";
	}

    public void InitEnds()
    {
        _ends["TestBot"] = delegate ()
        {
            Traducteur t = new Traducteur();
            string trad = "Winner";
            t.langue = GameObject.FindObjectOfType<GameManager>().GetComponent<LangageLoader>().language;
            t.setTextOriginal("Winner");
            trad = t.traduction;
            textWinnerTeam.GetComponent<Text>().text = trad + " : " + winnername;
            print("after Winnerteam");
            GameObject[] units = GameObject.FindGameObjectsWithTag("Unit");
            foreach (GameObject u in units)
            {
                Destroy(u);
            }
            anim.SetTrigger("GameOver");
            print("after TextGO");
            if (!written)
            {
                TeamsPerformance p = new TeamsPerformance();
                int size = GameObject.FindObjectOfType<GameManager>().GetComponent<TeamManager>()._teams.Count;
                string[] teams = new string[size];
                int cpt = 0;
                foreach (Team t2 in GameObject.FindObjectOfType<GameManager>().GetComponent<TeamManager>()._teams)
                {
                    teams[cpt] = t2._name;
                    cpt++;
                }
                p.WriteStats(teams, teams[winner], size);
                written = true;
            }
        };

        _ends["RessourceRace"] = delegate ()
        {
            Traducteur t = new Traducteur();
            string trad = "Winner";
            t.langue = GameObject.FindObjectOfType<GameManager>().GetComponent<LangageLoader>().language;
            t.setTextOriginal("Winner");
            trad = t.traduction;
            textWinnerTeam.GetComponent<Text>().text = trad + " : " + winnername;
            print("after Winnerteam");
            GameObject[] units = GameObject.FindGameObjectsWithTag("Unit");
            foreach (GameObject u in units)
            {
                Destroy(u);
            }
            anim.SetTrigger("GameOver");
            print("after TextGO");
            if (!written)
            {
                TeamsPerformance p = new TeamsPerformance();
                int size = GameObject.FindObjectOfType<GameManager>().GetComponent<TeamManager>()._teams.Count;
                string[] teams = new string[size];
                int cpt = 0;
                foreach (Team t2 in GameObject.FindObjectOfType<GameManager>().GetComponent<TeamManager>()._teams)
                {
                    teams[cpt] = t2._name;
                    cpt++;
                }
                p.WriteStats(teams, teams[winner], size);
                written = true;
            }
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
            {
                winner = teams[0];
                winnername = GameObject.Find("GameManager").GetComponent<TeamManager>()._teams[winner]._name;
            }

            return teams.Count <= 1;
        };

        _tests["RessourceRace"] = delegate ()
        {
            int nbRessources = 0;
            List<int> teams = new List<int>();
            GameObject[] units = GameObject.FindGameObjectsWithTag("Unit");
            foreach (GameObject u in units)
            {
                if (u.GetComponent<Stats>()._unitType.Equals("Base"))
                {
                    if (!teams.Contains(u.GetComponent<Stats>()._teamIndex))
                        teams.Add(u.GetComponent<Stats>()._teamIndex);

                    if (u.GetComponent<Inventory>()._actualSize > nbRessources)
                    {
                        nbRessources = u.GetComponent<Inventory>()._actualSize;
                        winnername = GameObject.Find("GameManager").GetComponent<TeamManager>()._teams[u.GetComponent<Stats>()._teamIndex]._name;
                        winner = u.GetComponent<Stats>()._teamIndex;
                    }
                }
            }

            if (teams.Count == 1)
            {
                winnername = GameObject.Find("GameManager").GetComponent<TeamManager>()._teams[teams[0]]._name;
                winner = teams[0];
            }

            return (timer.GetComponent<TimerScriptHUD>().timePassed >= timeLimitSeconds || teams.Count <= 1 || nbRessources >= ressourceLimit);
        };
    }

    // Update is called once per frame
    void Update () {
        if (_tests[_gamename]())
            _ends[_gamename]();
	}
}
