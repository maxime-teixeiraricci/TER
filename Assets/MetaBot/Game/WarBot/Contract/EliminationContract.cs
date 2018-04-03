using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliminationContract : Contract
{
    public GameObject _target;

    public EliminationContract(GameObject target)
    {
        _target = target;
    }

    public override bool isDone()
    {
        GameObject[] listeUnits = GameObject.FindGameObjectsWithTag("Unit");
        foreach(GameObject unit in listeUnits){
            if (unit.Equals(_target))
            {
                return false;
            }
        }
        return true;
    }
}
