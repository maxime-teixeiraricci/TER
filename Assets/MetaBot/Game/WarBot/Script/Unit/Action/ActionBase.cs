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
            print("OK");
            Objet o = GetComponent<Inventory>().find("Ressource");
            
            GetComponent<Inventory>()._objets[o] -= 10;
            GetComponent<CreatorUnit>().Create("Light");
        };
        _actions["ACTION_CREATE_HEAVY"] = delegate () {
            print("OK");
            Objet o = GetComponent<Inventory>().find("Ressource");
            GetComponent<Inventory>()._objets[o] -= 25;
            GetComponent<CreatorUnit>().Create("Heavy");
        };

        _actions["ACTION_CREATE_EXPLORER"] = delegate () {
            print("OK");
            Objet o = GetComponent<Inventory>().find("Ressource");
            GetComponent<Inventory>()._objets[o] -= 10;
            GetComponent<CreatorUnit>().Create("Explorer");
        };
    }
}
