using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActionNonTerminal : MonoBehaviour
{
    public delegate void ActNT();
    public Dictionary<string, ActNT> _actions = new Dictionary<string, ActNT>();
    public abstract void InitActionNonTerminal();
}
