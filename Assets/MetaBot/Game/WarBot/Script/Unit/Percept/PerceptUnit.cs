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

    }


}