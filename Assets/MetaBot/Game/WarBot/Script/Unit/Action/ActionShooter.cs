using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionShooter : ActionUnit
{

    void Start()
    {
        InitAction();
    }

    public override void InitAction()
    {
        base.InitAction(); // IMPORTANT : Permet de recuperer les percepts de la classe mere
        _actions["ACTION_FIRE"] = delegate () {
            if (GetComponent<Shooter>().reloadTick <= 0)
            {
                GetComponent<Shooter>().Shoot();
            }
        };
        _actions["ACTION_RELOAD"] = delegate () { GetComponent<Shooter>().reloadTick--; };
    }
}