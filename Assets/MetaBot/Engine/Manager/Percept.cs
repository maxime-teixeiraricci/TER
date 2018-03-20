using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Percept : MonoBehaviour
{
    public delegate bool Listener();
    public Dictionary<string, Listener> _percepts = new Dictionary<string, Listener>();
    public abstract void InitPercept();
}
