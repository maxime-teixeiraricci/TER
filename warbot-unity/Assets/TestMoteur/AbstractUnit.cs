using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class AbstractUnit : MonoBehaviour
{
    public List<NavUtilities.Areas> allowedArea = new List<NavUtilities.Areas>(){
        NavUtilities.Areas.WALKABLE, NavUtilities.Areas.JUMP }; // Les zones que l'agent est autorisé à traversé.

    public int health { get; set; }

    public void addAllowedArea(NavUtilities.Areas area)
    {
        allowedArea.Add(area);
    }
}
