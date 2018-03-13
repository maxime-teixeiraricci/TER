using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUnitScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (GetComponent<Stats>()._health < 0)
        {
            foreach (GameObject unit in GameObject.FindGameObjectsWithTag("Unit"))
            {
                if (unit.GetComponent<Stats>()._teamIndex == GetComponent<Stats>()._teamIndex)
                {
                    unit.GetComponent<Stats>()._health = -10000;
                }
            }
        }
	}
}
