using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionCommon : Action
{

    void Start()
    {
        InitAction();
    }

    public override void InitAction()
    {
        _actions["ACTION_IDLE"] = delegate () { };
        _actions["ACTION_GIVE_RESSOURCE"] = delegate ()
        {
            GameObject target = GetComponent<Stats>().GetTarget();
            if (target != null && GetComponent<Percept>()._percepts.ContainsKey("PERCEPT_CAN_GIVE" )&& GetComponent<Percept>()._percepts["PERCEPT_CAN_GIVE"]())
            {
                Objet item = GetComponent<Inventory>().find("Ressource");
                print(item);
                if (GetComponent<Inventory>()._objets.ContainsKey(item) && GetComponent<Inventory>()._objets[item] != 0 && !target.GetComponent<Inventory>().isFull())
                {
                    target.GetComponent<Inventory>().add(item);
                        GetComponent<Inventory>().pop(item);
                }
            }
        };
        _actions["ACTION_HEAL"] = delegate () { GetComponent<Inventory>().use("Ressource"); };
    }


}