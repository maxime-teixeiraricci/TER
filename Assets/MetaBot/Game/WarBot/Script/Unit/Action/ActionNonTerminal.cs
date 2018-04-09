using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActionNonTerminal : MonoBehaviour
{
    public delegate void ActNonTerm();
    public Dictionary<string, ActNonTerm> _actionsNT = new Dictionary<string, ActNonTerm>();
    public abstract void InitActionNonTerminal();
    public string _messageDestinataire;
    public Message _tmpMessage;
}
