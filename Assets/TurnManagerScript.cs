using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManagerScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        bool nextTurn = true;

		foreach (GameObject unit in GameObject.FindGameObjectsWithTag("Unit"))
        {
            if (unit.GetComponent<Brain>())
            {
                if (!unit.GetComponent<Brain>().turnEnd) { nextTurn = false; }
            }
        }

        if (nextTurn)
        {
            foreach (GameObject unit in GameObject.FindGameObjectsWithTag("Unit"))
            {
                if (unit.GetComponent<Brain>())
                {
                    unit.GetComponent<Brain>().turnEnd = false; 
                }
            }
        }

	}





}
