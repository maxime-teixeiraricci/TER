using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeamMenuHUD : MonoBehaviour
{
    public Color _teamColor;
    public int _idPlayer;
    public Text _powerScoreText;
    public Text _statsText;
    public Image[] _colorImage;
    public Dropdown _teamDropDown;

    public string _teamName;

    public void Start()
    {
        if (GameObject.Find("GameManager").GetComponent<TeamManager>()._teams.Count != 0)
        {
            string team = GameObject.Find("GameManager").GetComponent<TeamManager>()._teams[_idPlayer]._name;
            for (int i = 0; i < _teamDropDown.options.Count; i ++)
            {
                if (_teamDropDown.options[i].text.Equals(team))
                {
                    _teamDropDown.value = i;
                }
            }
        }
        else
        {
            _teamName = _teamDropDown.captionText.text;
        }
        
        foreach (Image image in _colorImage)
        {
            image.color = _teamColor;
        }
        Change();
    }
    


	public void Change()
    {
        _teamName = _teamDropDown.captionText.text;
        _powerScoreText.text = TeamsPerformance.GetTeamElo(_teamName).ToString();
        _powerScoreText.color = ColorElo(TeamsPerformance.GetTeamElo(_teamName));



    }


    Color ColorElo(int elo)
    {
        if (elo < 500) return Color.red;
        if (elo < 900) return Color.Lerp(Color.red, Color.yellow, (elo - 500) / 400.0f);
        if (elo < 1000) return Color.Lerp(Color.yellow, Color.white, (elo - 900) / 100.0f);
        if (elo < 1250) return Color.Lerp(Color.white, Color.cyan, (elo - 1000) / 250.0f);
        if (elo < 1750) return Color.Lerp(Color.cyan, Color.blue, (elo - 1250) / 500.0f);
        return Color.blue;
    }
}
