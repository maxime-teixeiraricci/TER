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
        _actionsNT["ACTN_MESSAGE_HELP"] = delegate ()
        {
            Message message = new Message(gameObject, "MESSAGE_HELP");
            GetComponent<MessageManager>().Send(message, _messageDestinataire);
            print("SEND HELP !");
        };
        _actionsNT["ACTN_MESSAGE_POSITION"] = delegate ()
        {
            Message message = new Message(gameObject, "MESSAGE_POSITION");
            GetComponent<MessageManager>().Send(message, _messageDestinataire);
            print("SEND POSITION !");
        };
        _actionsNT["ACTN_MESSAGE_ATTACK"] = delegate ()
        {
            Message message = new Message(gameObject, "MESSAGE_ATTACK", GetComponent<Stats>()._target);
            GetComponent<MessageManager>().Send(message, _messageDestinataire);
            print("SEND ATTACK !");
        };
    }
}
