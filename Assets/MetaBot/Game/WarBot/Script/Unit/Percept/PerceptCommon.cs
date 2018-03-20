using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;


public class PerceptCommon : Percept
{
    [ExecuteInEditMode()]
    void Start()
    {
        InitPercept();
    }

    
    public override void InitPercept()
    {
        _percepts["PERCEPT_LIFE_MAX"] = delegate () { return GetComponent<Stats>()._maxHealth == GetComponent<Stats>()._health; };
        _percepts["PERCEPT_BLOCKED"] = delegate () { return GetComponent<Stats>()._isBlocked; };
        _percepts["PERCEPT_LIFE_NOT_MAX"] = delegate () { return GetComponent<Stats>()._maxHealth != GetComponent<Stats>()._health; };
        _percepts["PERCEPT_BAG_FULL"] = delegate () { return GetComponent<Inventory>()._maxSize == GetComponent<Inventory>()._actualSize; };
        _percepts["PERCEPT_BAG_NOT_FULL"] = delegate () { return !_percepts["PERCEPT_BAG_FULL"](); };
        _percepts["PERCEPT_BAG_EMPTY"] = delegate () { return GetComponent<Inventory>()._actualSize == 0; };
        _percepts["PERCEPT_BAG_10"] = delegate () { return GetComponent<Inventory>()._actualSize >= 10; };
        _percepts["PERCEPT_BAG_25"] = delegate () { return GetComponent<Inventory>()._actualSize >= 25; };
        _percepts["PERCEPT_BAG_NOT_EMPTY"] = delegate () { return !_percepts["PERCEPT_BAG_EMPTY"](); };
        _percepts["PERCPET_CAN_GIVE"] = delegate () 
        {
            return (GetComponent<Stats>()._target != null) && (Vector3.Distance(GetComponent<Stats>()._target.transform.position, transform.position) < 1.5f);
        };
        _percepts["PERCEPT_IS_RELOADED"] = delegate () { return GetComponent<Stats>()._reloadTime <= 0; };
        _percepts["PERCEPT_IS_NOT_RELOADED"] = delegate () { return GetComponent<Stats>()._reloadTime > 0; };
        _percepts["PERCEPT_BASE_NEAR_ALLY"] = delegate ()
        {
            GetComponent<Stats>()._target = null;
            foreach (GameObject gO in GetComponent<Sight>()._listOfCollision)
            {
                if (gO&&gO.GetComponent<Stats>() != null && gO.GetComponent<Stats>()._unitType == "Base" && 
                Vector3.Distance(transform.position, gO.transform.position) < 7f && gO.GetComponent<Stats>()._teamIndex == GetComponent<Stats>()._teamIndex)
                {
                    GetComponent<Stats>()._target = gO;
                    return true;
                }
            }
            return false;
        };
       
    }

    
}
