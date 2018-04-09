using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameViewer : MonoBehaviour
{
    /* Classe permettant de mettre en place les unités selon le nombre d'équipe selectionné.
     */
	// Use this for initialization
	void Start ()
    {
        if (GameObject.Find("GameManager")) // Si on trouve l'objet GameManager
        {
            int nbPlayer = GameObject.Find("GameManager").GetComponent<TeamManager>()._teams.Count;
            foreach (GameObject unit in GameObject.FindGameObjectsWithTag("Unit"))
            {
                if (!(unit.GetComponent<Stats>()._teamIndex < nbPlayer)) { Destroy(unit); }
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
