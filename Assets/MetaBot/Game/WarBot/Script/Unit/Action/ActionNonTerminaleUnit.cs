using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionNonTerminaleUnit : ActionNonTerminalCommon
{


    void Start()
    {
        InitActionNonTerminal();
    }

    public override void InitActionNonTerminal()
    {
        base.InitActionNonTerminal();

        _actionsNT["ACTN_HEADING_SOUTH"] = delegate ()
        {
            GetComponent<Stats>().SetHeading(270);
        };
        _actionsNT["ACTN_HEADING_NORTH"] = delegate ()
        {
            GetComponent<Stats>().SetHeading(90);
        };
        _actionsNT["ACTN_HEADING_EAST"] = delegate ()
        {
            GetComponent<Stats>().SetHeading(0);
        };
        _actionsNT["ACTN_HEADING_WEST"] = delegate ()
        {
            GetComponent<Stats>().SetHeading(180);
        };

        _actionsNT["ACTN_HEADING_RANDOM"] = delegate ()
        {
            GetComponent<Stats>().SetHeading(Random.Range(0f,360f));
        };

        _actionsNT["ACTN_TURN_AROUND"] = delegate ()
        {
            GetComponent<Stats>().SetHeading(180 + GetComponent<Stats>().GetHeading());
        };
        _actionsNT["ACTN_TOWARD_MESSAGE_SENDER"] = delegate ()
        {
            print("ACTN_TOWARD_MESSAGE_SENDER " + _tmpMessage);
            if (_tmpMessage != null)
            {

                GetComponent<Stats>().SetHeading(_tmpMessage.heading);
            }
        };
        _actionsNT["ACTN_ADD_ELIMINATION_CONTRACT"] = delegate ()
        {
            EliminationContract newContract = new EliminationContract((GameObject)_tmpMessage._contenu);
            GetComponent<Stats>().AddContract(newContract);
        };
    }
}

