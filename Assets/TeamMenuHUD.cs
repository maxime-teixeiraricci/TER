using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeamMenuHUD : MonoBehaviour
{
    public Color _teamColor;

    public Text _powerScoreText;
    public Text _statsText;
    public Image[] _colorImage;
    public Dropdown _teamDropDown;

    public string _teamName;

    public void Start()
    {
        _teamName = _teamDropDown.captionText.text;
        foreach (Image image in _colorImage)
        {
            image.color = _teamColor;
        }
    }
    


	public void Change()
    {
        _teamName = _teamDropDown.captionText.text;
        _powerScoreText.text = TeamsPerformance.GetTeamElo(_teamName).ToString();
        
       
        
    }
}
