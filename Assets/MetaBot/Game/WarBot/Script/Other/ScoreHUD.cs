using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreHUD : MonoBehaviour
{
    public Image[] _scores;
   



    // Update is called once per frame
    void Update ()
    {
        Dictionary<int, int> _score = new Dictionary<int, int>();
        
        for (int i = 0; i < GameObject.Find("GameManager").GetComponent<TeamManager>()._teams.Count; i ++)
        {
            _scores[i].color = GameObject.Find("GameManager").GetComponent<TeamManager>()._teams[i]._color;
            _scores[i].color = new Color(_scores[i].color.r, _scores[i].color.g, _scores[i].color.b, 1);
            _score[i] = 0;
        }

        float total = 0;
        foreach (GameObject unit in GameObject.FindGameObjectsWithTag("Unit"))
        {
            int teamUnit = unit.GetComponent<Stats>()._teamIndex;
            int scoreUnit = unit.GetComponent<Stats>().GetHealth();
            if (_score.ContainsKey(teamUnit))
            {
                _score[teamUnit] += scoreUnit;
            }
            else
            {
                _score[teamUnit] = scoreUnit;
            }
            total += scoreUnit;
        }

        float currentValue = 0;
        for (int i = 0; i < GameObject.Find("GameManager").GetComponent<TeamManager>()._teams.Count; i++)
        {
            currentValue += _score[i];
            _scores[i].fillAmount = currentValue / total;
        }


    }
}
