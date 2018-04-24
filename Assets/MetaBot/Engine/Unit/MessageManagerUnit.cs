using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageManagerUnit : MessageManager
{
    
    public override void Init() { _messageType = new string[] { "MESSAGE_HELP", "MESSAGE_POSITION", "MESSAGE_ATTACK", "MESSAGE_POSITION_RESOURCE" }; }
}
