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

    public Text timeLimit;
    public Text timedisplay;

    public Text ressourceGoal;
    public Text ressourceDisplay;
    public int scorewinner;
    public GameObject Score;

    Animator anim;
    public GameObject textWinnerTeam;
    public int ressourceLimit;
    public float timeLimitSeconds;
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
        ressourceLimit = GameObject.FindObjectOfType<GameManager>().ressourceLimit;
        timeLimitSeconds = GameObject.FindObjectOfType<GameManager>().timeLimit;
        if (_gamename.Equals("RessourceRace"))
        {
            int mins = (int)(timeLimitSeconds / 60);
            int secs = (int)(timeLimitSeconds % 60);
            timeLimit.text = ("" + mins).PadLeft(2, '0') + ":" + ("" + secs).PadLeft(2, '0');
            timedisplay.transform.localScale = new Vector3(1, 1, 1);
            ressourceDisplay.transform.localScale = new Vector3(1, 1, 1);
            ressourceGoal.text = ressourceLimit + "";
        }
        else
        {
            timeLimit.text = "";
        }
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
            textWinnerTeam.GetComponent<Text>().text = "Winner " + " : " + winnername;
            Score.GetComponent<Text>().text = "Score : " + scorewinner;
            print("after Winnerteam");
            GameObject[] units = GameObject.FindGameObjectsWithTag("Unit");
            foreach (GameObject u in units)
            {
                Destroy(u);
            }
            print("DEBUG END RESSOURCERACE apres destrcution unités");
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
            int nbRessources = -1;
            GameObject[] units = GameObject.FindGameObjectsWithTag("Unit");
            foreach (GameObject u in units)
            {
                if (u.GetComponent<Stats>()._unitType.Equals("Base"))
                {
                    if (u.GetComponent<Inventory>()._actualSize > nbRessources)
                    {
                        print("WinnerUpdated");
                        nbRessources = u.GetComponent<Inventory>()._actualSize;
                        winnername = GameObject.Find("GameManager").GetComponent<TeamManager>()._teams[u.GetComponent<Stats>()._teamIndex]._name;
                        winner = u.GetComponent<Stats>()._teamIndex;
                    }
                }
            }

            if (timer.GetComponent<TimerScriptHUD>().timePassed > timeLimitSeconds || nbRessources >= ressourceLimit)
            {
                print("Dans IF Test Reussi");
                scorewinner = nbRessources;
            }

            return (timer.GetComponent<TimerScriptHUD>().timePassed > timeLimitSeconds || nbRessources >= ressourceLimit);
        };
    }

    // Update is called once per frame
    void Update()
    {
        if (_gamename != GameObject.FindObjectOfType<GameManager>()._gameName)
        {
            _gamename = GameObject.FindObjectOfType<GameManager>()._gameName;

            if (_gamename.Equals("RessourceRace"))
            {
                int mins = (int)(timeLimitSeconds / 60);
                int secs = (int)(timeLimitSeconds % 60);
                timeLimit.text = ("" + mins).PadLeft(2, '0') + ":" + ("" + secs).PadLeft(2, '0');
                timedisplay.transform.localScale = new Vector3(1, 1, 1);
                ressourceDisplay.transform.localScale = new Vector3(1, 1, 1);
                ressourceGoal.text = ressourceLimit + "";
            }
            else
            {
                timeLimit.text = "";
            }
        }

            if (_tests[_gamename]())
        {
            print("Dans IF ");
            _ends[_gamename]();
        }
    }
}
