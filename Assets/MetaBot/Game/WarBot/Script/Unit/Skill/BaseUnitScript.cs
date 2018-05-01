using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUnitScript : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        // Permet de créer les unités au début de la partie en fonction du nombre choisi dans le menu.
        GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();

        foreach (KeyValuePair<string,int> unit in gm._gameSettings._initStartUnit)
        {
            for (int i = 0; i < unit.Value; i++ )
            {
                GetComponent<CreatorUnit>().Create(unit.Key);
            }
        }
    }

    
    void OnDestroy()
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
