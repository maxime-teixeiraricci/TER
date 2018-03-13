using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class PerceptAggressive : PerceptCommon
{

    void Start()
    {
        InitPercept();
    }


    public new void InitPercept()
    {
        base.InitPercept();
        _percepts["PERCEPT_FOOD"] = delegate ()
        {
            GetComponent<Stats>()._target = null;
            foreach (GameObject gO in GetComponent<Sight>()._listOfCollision)
            {
                if (gO && gO.tag == "Item")
                {
                    GetComponent<Stats>()._target = gO;
                    return true;
                }
            }
            return false;
        };
        _percepts["PERCEPT_FOOD_NEAR"] = delegate ()
        {
            return (_percepts["PERCEPT_FOOD"]()) && (Vector3.Distance(GetComponent<Stats>()._target.transform.position, transform.position) < 4f);
        };
        _percepts["PERCEPT_ENEMY"] = delegate ()
        {

            Brain brain = GetComponent<Brain>();
            Sight sight = brain.GetComponent<Sight>();
            List<GameObject> _listOfUnitColl = new List<GameObject>();
            GameObject _gameObject = GetComponent<Stats>()._target;
            int angleEnemy;


            foreach (GameObject gO in sight._listOfCollision)
            {
                if (gO && !_gameObject)
                {
                    if (gO.GetComponent<Stats>() && gO.GetComponent<Stats>()._teamIndex != GetComponent<Stats>()._teamIndex)
                    {
                        _gameObject = gO;
                        angleEnemy = getAngle(_gameObject);
                        GetComponent<Stats>()._heading = getAngle(_gameObject);
                        return true;
                    }
                }
            }
            return false;
        };
    
    _percepts["PERCEPT_NOT_ENEMY"] = delegate ()
        {
           if (GetComponent<Stats>()._target != null && GetComponent<Stats>()._target.GetComponent<Stats>())
            {
                return GetComponent<Stats>()._target.GetComponent<Stats>()._teamIndex == GetComponent<Stats>()._teamIndex;
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
