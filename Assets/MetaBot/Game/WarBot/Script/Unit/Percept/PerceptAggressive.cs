using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


public class PerceptAggressive : PerceptUnit
{

    void Start()
    {
        InitPercept();
    }


    public override void InitPercept()
    {
        base.InitPercept();
        _percepts["PERCEPT_IS_RELOADED"] = delegate () { return GetComponent<Shooter>().reloadTick <= 0; };
        _percepts["PERCEPT_BLOCKED"] = delegate () { return GetComponent<MovableCharacter>().isBlocked(); };

    }


}
