using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActionNonTerminal : MonoBehaviour
{
    public delegate void ActNT(string dest);
    public Dictionary<string, ActNT> _actionsNT = new Dictionary<string, ActNT>();
    public abstract void InitActionNonTerminal();
}
