using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameViewer : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
        int nbPlayer = GameObject.Find("GameManager").GetComponent<TeamManager>()._teams.Count;
        foreach (GameObject unit in GameObject.FindGameObjectsWithTag("Unit"))
        {
            if ( !(unit.GetComponent<Stats>()._teamIndex < nbPlayer) ) { Destroy(unit); }
        }	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
