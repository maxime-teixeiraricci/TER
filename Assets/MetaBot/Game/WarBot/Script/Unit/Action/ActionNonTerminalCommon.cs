using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionNonTerminalCommon : ActionNonTerminal
{

    void Start()
    {
        InitActionNonTerminal();
    }

    public override void InitActionNonTerminal()
    {
        _actionsNT["MESSAGE_HELP"] = delegate ()
        {
            Message message = new Message(gameObject, "MESSAGE_HELP");
            GetComponent<MessageManager>().Send(message, _messageDestinataire);
        };
        _actionsNT["MESSAGE_POSITION"] = delegate ()
        {
            Message message = new Message(gameObject, "MESSAGE_POSITION");
            GetComponent<MessageManager>().Send(message, _messageDestinataire);
        };
        _actionsNT["MESSAGE_ATTACK"] = delegate ()
        {
            Message message = new Message(gameObject, "MESSAGE_ATTACK", GetComponent<Stats>()._target);
            GetComponent<MessageManager>().Send(message, _messageDestinataire);
            print("Sender : " + gameObject +"  Target : " + message._contenu);
        };


        _actionsNT["ACTN_HEADING_SOUTH"] = delegate ()
        {
            GetComponent<Stats>()._heading = 270;
        };
        _actionsNT["ACTN_HEADING_NORTH"] = delegate ()
        {
            GetComponent<Stats>()._heading = 90;
        };
        _actionsNT["ACTN_HEADING_EAST"] = delegate ()
        {
            GetComponent<Stats>()._heading = 0;
        };
        _actionsNT["ACTN_HEADING_WEST"] = delegate ()
        {
            GetComponent<Stats>()._heading = 180;
        };

        _actionsNT["ACTN_TURN_AROUND"] = delegate ()
        {
            GetComponent<Stats>()._heading += 180;
        };
        _actionsNT["ACTN_ADD_ELIMINATION_CONTRACT"] = delegate ()
        {
            EliminationContract newContract = new EliminationContract((GameObject)GetComponent<Percept>()._tmpMessage._contenu);
            GetComponent<Stats>()._contract = newContract;
        };



    }
}
