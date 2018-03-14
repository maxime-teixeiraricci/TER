using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreHUD : MonoBehaviour
{
    public Image _Score1;
    public Image _Score2;
    public Image _Score3;
    public Image _Score4;
    

	// Update is called once per frame
	void Update ()
    {
        _Score1.color = GameObject.Find("GameManager").GetComponent<TeamManager>()._teams[0]._color;
        _Score2.color = GameObject.Find("GameManager").GetComponent<TeamManager>()._teams[1]._color;
        _Score3.color = GameObject.Find("GameManager").GetComponent<TeamManager>()._teams[2]._color;
        _Score4.color = GameObject.Find("GameManager").GetComponent<TeamManager>()._teams[3]._color;
        Dictionary<int, int> _score = new Dictionary<int, int>();
        _score[0] = 0; _score[1] = 0; _score[2] = 0; _score[3] = 0;
        foreach (GameObject unit in GameObject.FindGameObjectsWithTag("Unit"))
        {
            int teamUnit = unit.GetComponent<Stats>()._teamIndex;
            int scoreUnit = unit.GetComponent<Stats>()._health;
            if (_score.ContainsKey(teamUnit))
            {
                _score[teamUnit] += scoreUnit;
            }
            else
            {
                _score[teamUnit] = scoreUnit;
            }
        }
        float total = _score[0] + _score[1] + _score[2] + _score[3];
        _Score1.fillAmount = _score[0] / total;
        _Score2.fillAmount = (_score[0] + _score[1]) / total;
        _Score3.fillAmount = (_score[0] + _score[1] + _score[2]) / total;
        _Score4.fillAmount = 1;

    }
}
