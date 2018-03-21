using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamManager : MonoBehaviour
{
    public List<Team> _teams = new List<Team>();

    public void Start()
    {
        DontDestroyOnLoad(this);
    }

    public List<Instruction> getUnitsBevahiours(int teamIndex, string unitType)
    {
        string gamePath = "./teams/" + GetComponent<GameManager>()._gameName + "/";
        XMLWarbotInterpreter interpreter = new XMLWarbotInterpreter();
        return interpreter.xmlToUnitBehavior(_teams[teamIndex]._name, gamePath, unitType);
    }
}
