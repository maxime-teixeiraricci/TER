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
    public string _wincondition;
    public int winner;
    public string winnername;
    public GameObject timer;

    public Text timeLimit;
    public Text timedisplay;

    public Text ressourceGoal;
    public Text ressourceDisplay;
    public int scorewinner;
    public GameObject Score;
    GameObject gm;
    Animator anim;
    public GameObject textWinnerTeam;
    public int ressourceLimit;
    public float timeLimitSeconds;
    public bool written;
    public bool equals;

    // Use this for initialization
    void Start () {
        gm = GameObject.Find("GameManager");
        anim = GetComponent<Animator>();
        InitTests();
        InitEnds();
        winner = -1;
        equals = false;
        _gamename = gm.GetComponent<GameManager>()._gameName;
        _wincondition = gm.GetComponent<GameManager>().wincondition;
        written = false;
        winnername = "";
        ressourceLimit = gm.GetComponent<GameManager>().ressourceLimit;
        timeLimitSeconds = gm.GetComponent<GameManager>().timeLimit;
        if (_wincondition.Equals("RessourceRace"))
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
            t.langue = gm.GetComponent<LangageLoader>().language;
            t.setTextOriginal("Winner");
            trad = t.traduction;
            textWinnerTeam.GetComponent<Text>().text = trad + " : " + winnername;
            print("after Winnerteam");
           /* GameObject[] units = GameObject.FindGameObjectsWithTag("Unit");
            foreach (GameObject u in units)
            {
                Destroy(u);
            }*/
            anim.SetTrigger("GameOver");
            print("after TextGO");
            if (!written)
            {
                TeamsPerformance p = new TeamsPerformance();
                int size = gm.GetComponent<TeamManager>()._teams.Count;
                string[] teams = new string[size];
                int cpt = 0;
                foreach (Team t2 in gm.GetComponent<TeamManager>()._teams)
                {
                    teams[cpt] = t2._name;
                    cpt++;
                }
                p.WriteStats(teams, teams[winner], size);
                if (teams.Length == 2)
                {
                    p.ComputeELO(teams, teams[winner]);
                }

                written = true;
            }
            Time.timeScale = 0;
        };

        _ends["RessourceRace"] = delegate ()
        {
            string trad = "Winner";
            Traducteur t = new Traducteur();

            if (equals || scorewinner == -1)
                trad = "Egalite";

            t.langue = gm.GetComponent<LangageLoader>().language;
            t.setTextOriginal(trad);
            trad = t.traduction;
            if (equals || scorewinner == -1)
                textWinnerTeam.GetComponent<Text>().text = trad +" ! ";
            else
                textWinnerTeam.GetComponent<Text>().text = trad + " : " + winnername;

            Score.GetComponent<Text>().text = "Score : " + scorewinner;
            print("after Winnerteam");
        /*    GameObject[] units = GameObject.FindGameObjectsWithTag("Unit");
            foreach (GameObject u in units)
            {
                Destroy(u);
            }
            print("DEBUG END RESSOURCERACE apres destrcution unités");*/
            anim.SetTrigger("GameOver");
            print("after TextGO");
            if (!written && !equals && scorewinner != -1)
            {
                TeamsPerformance p = new TeamsPerformance();
                int size =gm.GetComponent<TeamManager>()._teams.Count;
                string[] teams = new string[size];
                int cpt = 0;
                foreach (Team t2 in gm.GetComponent<TeamManager>()._teams)
                {
                    teams[cpt] = t2._name;
                    cpt++;
                }
                p.WriteStats(teams, teams[winner], size);
                print("apres write stats ");
                if (teams.Length == 2  && !equals && scorewinner != -1)
                {
                    print("ComputeElo ");
                    p.ComputeELO(teams, teams[winner]);
                }
                written = true;
            }
            Time.timeScale = 0;
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
                winnername = gm.GetComponent<TeamManager>()._teams[winner]._name;
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

                    if (u.GetComponent<Inventory>()._actualSize == nbRessources && !gm.GetComponent<TeamManager>()._teams[u.GetComponent<Stats>()._teamIndex]._name.Equals(winnername))
                    {
                        equals = true;
                    }
                    if (u.GetComponent<Inventory>()._actualSize > nbRessources)
                    {
                        print("WinnerUpdated");
                        winnername = gm.GetComponent<TeamManager>()._teams[u.GetComponent<Stats>()._teamIndex]._name;
                        winner = u.GetComponent<Stats>()._teamIndex;
                        nbRessources = u.GetComponent<Inventory>()._actualSize;
                        equals = false;
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
        if (_wincondition != gm.GetComponent<GameManager>().wincondition)
        {
            _wincondition = gm.GetComponent<GameManager>().wincondition;

            if (_wincondition.Equals("RessourceRace"))
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

            if (_tests[_wincondition]())
        {
            print("Dans IF ");
            _ends[_wincondition]();
        }
    }
}
