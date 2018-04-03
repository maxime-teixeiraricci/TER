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

 




    public int getAngle(GameObject _gameObject)
    {
        Vector3 vect = _gameObject.transform.position - transform.position;
        Vector3 projVect = Vector3.ProjectOnPlane(vect, Vector3.up);

        if (projVect.z > 0)
        {
            return (int)(360 - Vector3.Angle(projVect, new Vector3(1, 0, 0)));
        }
        return (int)(Vector3.Angle(projVect, new Vector3(1, 0, 0)));

    }
}
