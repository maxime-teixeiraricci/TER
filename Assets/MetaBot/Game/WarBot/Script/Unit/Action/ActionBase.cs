using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionBase : ActionCommon
{

    void Start()
    {
        InitAction();
    }

    public override void InitAction()
    {
        base.InitAction(); // IMPORTANT : Permet de recuperer les percepts de la classe mere
        _actions["ACTION_CREATE_LIGHT"] = delegate () {
            Objet o = GetComponent<Inventory>().find("Ressource");
            if (GetComponent<Inventory>()._objets[o] >= 10)
            {
                GetComponent<Inventory>()._objets[o] -= 10;
                GetComponent<CreatorUnit>().Create("Light");
            }
        };
        _actions["ACTION_CREATE_HEAVY"] = delegate () {
            print("OK");
            Objet o = GetComponent<Inventory>().find("Ressource");
            if (GetComponent<Inventory>()._objets[o] >= 25)
            {
                
                GetComponent<Inventory>()._objets[o] -= 25;
                GetComponent<CreatorUnit>().Create("Heavy");
            }
        };

        _actions["ACTION_CREATE_EXPLORER"] = delegate () {
            Objet o = GetComponent<Inventory>().find("Ressource");
            if (GetComponent<Inventory>()._objets[o] >= 5)
            {
                GetComponent<Inventory>()._objets[o] -= 5;
                GetComponent<CreatorUnit>().Create("Explorer");
            }
        };

        _actions["ACTION_CREATE_LIFE_LIGHT"] = delegate ()
        {
            GetComponent<Stats>().AddHealth(-200);
            GetComponent<CreatorUnit>().Create("Light");
        };
        _actions["ACTION_CREATE_LIFE_HEAVY"] = delegate () 
        {
            GetComponent<Stats>().AddHealth(-500);
            GetComponent<CreatorUnit>().Create("Heavy");
        };

        _actions["ACTION_CREATE_LIFE_EXPLORER"] = delegate () {
            GetComponent<Stats>().AddHealth(-100);
            GetComponent<CreatorUnit>().Create("Explorer");
        };
    }
}
