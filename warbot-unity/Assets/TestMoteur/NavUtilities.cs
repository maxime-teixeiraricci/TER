using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavUtilities
{
    public enum Areas { WALKABLE = 0, NOT_WALKABLE = 1, JUMP = 2, WATER = 3 };
    



	public int allowedArea(Areas[] listAreasAllowed)
    {
        int res = 0;
        foreach(Areas area in listAreasAllowed)
        {
            res += (int)Mathf.Pow(2, (int)area);
        }
        return res;
    } 
}
