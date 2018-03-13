using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Percept : MonoBehaviour
{
    public delegate bool Listener();
    public Dictionary<string, Listener> _percepts = new Dictionary<string, Listener>();

}
