using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamPlayManagerScript : MonoBehaviour
{
    public int teamIndex;
    public int id = 0;
    // Use this for initialization
    void Start ()
    {
        UpdateUnit();
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void UpdateUnit()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            Stats statsChild = child.GetComponent<Stats>();
            statsChild._teamIndex = teamIndex;
            child.name = "[" + teamIndex + "]" + statsChild._unitType + " #" + id;
            id++;

        }
    }
}
