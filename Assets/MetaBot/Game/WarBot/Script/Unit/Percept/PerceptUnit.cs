using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerceptUnit : PerceptCommon
{

    void Start()
    {
        InitPercept();
    }


    public override void InitPercept()
    {
        base.InitPercept();
        _percepts["PERCEPT_BLOCKED"] = delegate () { return GetComponent<MovableCharacter>().isBlocked(); };

        _percepts["PERCEPT_BASE_NEAR_ALLY"] = delegate ()
        {
            GetComponent<Stats>().SetTarget(null);
            foreach (GameObject gO in GetComponent<Sight>()._listOfCollision)
            {
                if (gO && gO.GetComponent<Stats>() != null && gO.GetComponent<Stats>()._unitType == "Base" &&
                Vector3.Distance(transform.position, gO.transform.position) < 8f && gO.GetComponent<Stats>()._teamIndex == GetComponent<Stats>()._teamIndex)
                {
                    GetComponent<Stats>().SetTarget(gO);
                    return true;
                }
            }
            return false;
        };
        /** PERCEPT **/
        _percepts["PERCEPT_CAN_GIVE"] = delegate ()
        {
            GetComponent<Stats>().SetTarget(null);
            foreach (GameObject gO in GetComponent<Sight>()._listOfCollision)
            {
                if (gO && gO.GetComponent<Stats>() != null &&
                Vector3.Distance(transform.position, gO.transform.position) < 8f && gO.GetComponent<Stats>()._teamIndex == GetComponent<Stats>()._teamIndex)
                {
                    GetComponent<Stats>().SetTarget(gO);
                    return true;
                }
            }
            return false;
        };
        _percepts["PERCEPT_CONTRACT_ELIMINATION"] = delegate () { return GetComponent<Stats>().HaveContrat() && GetComponent<Stats>().GetContract().type == "Elimination"; };
        _percepts["PERCEPT_CONTRACT_ELIMINATION_TARGET_NEAR"] = delegate () {
            Brain brain = GetComponent<Brain>();
            Sight sight = brain.GetComponent<Sight>();
            List<GameObject> _listOfUnitColl = new List<GameObject>();
            if (!GetComponent<Stats>().HaveContrat() || (GetComponent<Stats>().HaveContrat() && GetComponent<Stats>().GetContract().type != "Elimination")) { return false; }
            EliminationContract contract = (EliminationContract)GetComponent<Stats>().GetContract();

            foreach (GameObject gO in sight._listOfCollision)
            {
                if (gO && contract._target == gO)
                {
                    GetComponent<Stats>().SetTarget(gO);
                    GetComponent<Stats>().SetHeading(getAngle(gO));
                    return true;
                }
            }
            return false;
        };

        _percepts["PERCEPT_FOOD_NEAR"] = delegate ()
        {
            return (_percepts["PERCEPT_FOOD"]()) && (Vector3.Distance(GetComponent<Stats>().GetTarget().transform.position, transform.position) < 4f);
        };

        _percepts["PERCEPT_CONTRACT"] = delegate () { return GetComponent<Stats>().HaveContrat(); };
        _percepts["PERCEPT_FOOD"] = delegate ()
        {
            GetComponent<Stats>().SetTarget(null);
            foreach (GameObject gO in GetComponent<Sight>()._listOfCollision)
            {
                if (gO && gO.tag == "Item")
                {
                    GetComponent<Stats>().SetTarget(gO);
                    return true;
                }
            }
            return false;
        };
    }


}