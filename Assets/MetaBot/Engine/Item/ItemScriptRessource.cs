using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScriptRessource : ItemScript
{
    public override void use(GameObject unit)
    {
        unit.GetComponent<Stats>().AddHealth(20);
    }
}
