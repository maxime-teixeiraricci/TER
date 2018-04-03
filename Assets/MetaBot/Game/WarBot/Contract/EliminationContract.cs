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

    public override void InitPercept()
    {
        base.InitPercept();
        _percepts["CONTRACT_ELIMINATION"] = delegate () { return GetComponent<Stats>()._contract.GetType() == gameObject.GetType(); };
        _percepts["CONTRACT_ELIMINATION_TARGET_NEAR"] = delegate () {
            Brain brain = GetComponent<Brain>();
            Sight sight = brain.GetComponent<Sight>();
            List<GameObject> _listOfUnitColl = new List<GameObject>();
            foreach (GameObject gO in sight._listOfCollision)
            {
                EliminationContract contract = (EliminationContract)GetComponent<Stats>()._contract;
                if (gO && contract._target == gO)
                {
                    if (gO.GetComponent<Stats>() && gO.GetComponent<Stats>()._teamIndex != GetComponent<Stats>()._teamIndex)
                    {
                        GetComponent<Stats>()._target = gO;
                        GetComponent<Stats>()._heading = getAngle(gO);
                        return true;
                    }
                }
            }
            return false;
        };
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
