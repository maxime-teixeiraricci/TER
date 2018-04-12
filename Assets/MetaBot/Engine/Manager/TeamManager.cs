using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamManager : MonoBehaviour
{
    public List<Team> _teams = new List<Team>();

    public void Start()
    {
        DontDestroyOnLoad(this);
    }

    public List<Instruction> getUnitsBevahiours(int teamIndex, string unitType)
    {
        
        string gamePath = Application.streamingAssetsPath + "/teams/" + GetComponent<GameManager>()._gameName + "/";
        XMLWarbotInterpreter interpreter = new XMLWarbotInterpreter();
        if (_teams[teamIndex]._unitsBehaviour.ContainsKey(unitType))
        {
            return _teams[teamIndex]._unitsBehaviour[unitType];
        }
        return new List<Instruction>();
    }

    public bool endGameTestFunc()
    {
        List<int> teams = new List<int>();
        GameObject[] units = GameObject.FindGameObjectsWithTag("Unit");
        foreach(GameObject u in units)
        {
            if (u.GetComponent<Stats>()._unitType.Equals("Base"))
            {
                if (!teams.Contains(u.GetComponent<Stats>()._teamIndex))
                    teams.Add(u.GetComponent<Stats>()._teamIndex);
            }
        }

        return teams.Count<=1;
    }

    public void endGame()
    {
        bool test = endGameTestFunc();
        if (test)
        {
            Time.timeScale = 0;
            GameObject.FindGameObjectWithTag("FinishText").SetActive(true);
            GameObject.FindGameObjectWithTag("FpsCamera").SetActive(false);
        }
    }
}
