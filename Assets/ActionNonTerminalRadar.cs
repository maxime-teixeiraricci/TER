using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionNonTerminalRadar : ActionNonTerminal
{

    
    void Start()
    {
        
        InitActionNonTerminal();
    }
    

    public override void InitActionNonTerminal()
    {
        

        _actionsNT["ACTN_TURN_LEFT_30"] = delegate ()
        {
            GetComponent<Stats>().SetHeading(GetComponent<Stats>().GetHeading() + 30);

        };
        _actionsNT["ACTN_TURN_RIGHT_30"] = delegate ()
        {
            GetComponent<Stats>().SetHeading(GetComponent<Stats>().GetHeading() - 30);
        };
    }
}
