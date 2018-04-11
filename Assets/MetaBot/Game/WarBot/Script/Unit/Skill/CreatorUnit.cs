using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatorUnit : MonoBehaviour
{
    public Transform[] _spawnPoint;
    public GameObject[] _unitsToCreate;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Create(string name)
    {
        GameObject target = null;
        foreach (GameObject _unit in _unitsToCreate)
        {
            if (_unit.GetComponent<Stats>()._unitType.Equals(name))
            {
                target = _unit;
            }
        }
        GameObject unit = Instantiate(target,transform.parent);
        transform.parent.GetComponent<TeamPlayManagerScript>().UpdateUnit();
        unit.transform.position = _spawnPoint[Random.Range(0, _spawnPoint.Length - 1)].position;
        unit.GetComponent<Stats>()._teamIndex = GetComponent<Stats>()._teamIndex;
    }
}
