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
        print("AGGRESSIVE");
        _percepts["PERCEPT_IS_RELOADED"] = delegate () { return GetComponent<Stats>()._reloadTime <= 0; };
        _percepts["PERCEPT_IS_NOT_RELOADED"] = delegate () { return GetComponent<Stats>()._reloadTime > 0; };
        _percepts["PERCEPT_BLOCKED"] = delegate () { return GetComponent<MovableCharacter>().isBlocked(); };

    }


}
