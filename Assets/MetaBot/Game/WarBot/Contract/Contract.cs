using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Contract : MonoBehaviour {

    public delegate bool Listener();
    public Dictionary<string, Listener> _percepts = new Dictionary<string, Listener>();

    public abstract bool isDone();
    public virtual void InitPercept()
    {

    }

}
