using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Action : MonoBehaviour
{
    public delegate void Act();
    public Dictionary<string, Act> _actions = new Dictionary<string, Act>();
    public abstract void InitAction();
}
