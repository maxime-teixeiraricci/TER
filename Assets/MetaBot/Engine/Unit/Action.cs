using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Action : MonoBehaviour
{
    /*
     * Cette classe abstraite permet de créer de nouvelle action.
     * Pour cela, créer une classe qui dérive de celle-ci.
     */



    public delegate void Act();
    public Dictionary<string, Act> _actions = new Dictionary<string, Act>();
    public abstract void InitAction(); // 
}
