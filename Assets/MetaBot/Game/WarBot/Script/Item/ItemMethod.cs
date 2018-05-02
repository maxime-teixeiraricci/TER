using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMethod : MonoBehaviour
{
    public delegate void Effect(GameObject unit);

    // Liste des déclarations des actions. (ACTION_...)

    public Dictionary<string, Effect> _effects = new Dictionary<string, Effect>();

    void Start()
    {
        _effects["Ressource"] = delegate (GameObject unit) { unit.GetComponent<Stats>().AddHealth(20); };
    }
}
