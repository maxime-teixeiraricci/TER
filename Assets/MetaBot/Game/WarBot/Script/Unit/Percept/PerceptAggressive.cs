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
        
        _percepts["PERCEPT_IS_RELOADED"] = delegate () { return GetComponent<Stats>()._reloadTime <= 0; };
        _percepts["PERCEPT_IS_NOT_RELOADED"] = delegate () { return GetComponent<Stats>()._reloadTime > 0; };

    }


}
