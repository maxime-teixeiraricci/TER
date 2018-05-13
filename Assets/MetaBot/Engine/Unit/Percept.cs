using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Percept : MonoBehaviour
{
    public delegate bool Listener();
    public Dictionary<string, Listener> _percepts = new Dictionary<string, Listener>();

    public Dictionary<string, Listener> _messagePercepts = new Dictionary<string, Listener>();
    
    public float angleToMessage;

    public virtual void InitPercept()
    {
        // Generation des percepts liées aux messages.
        MessageManager mm = GetComponent<MessageManager>();
        mm.Init();
        foreach (string messageType in mm._messageType)
        {
            _percepts["PERCEPT_"+messageType] = delegate () 
            {
                GetComponent<ActionNonTerminal>()._tmpMessage = mm.ContainsType(messageType);
                return GetComponent<ActionNonTerminal>()._tmpMessage != null;
            };
        }
    }
}
