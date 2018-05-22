using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeamNameDisplayHUD : MonoBehaviour
{
    public int indexTeam;
	// Use this for initialization
	void Start ()
    {
        TeamManager tm = GameObject.Find("GameManager").GetComponent<TeamManager>();
        if (indexTeam >= tm._teams.Count)
        {
            Destroy(gameObject);
        }
        else
        {
            GetComponent<Text>().text = tm._teams[indexTeam]._name;
            GetComponent<Text>().color = tm._teams[indexTeam]._color;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
