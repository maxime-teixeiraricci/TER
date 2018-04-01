using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Expression : MonoBehaviour
{
    public delegate void Execution();
    public Dictionary<string, Execution> _actions = new Dictionary<string, Execution>();
    public abstract void InitExpression();
}
