using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityTER.Interpreter;

public class TeamManager : MonoBehaviour
{
    public List<Team> _teams = new List<Team>();


    public List<Instruction> getUnitsBevahiours(int teamIndex, string unitType)
    {
        string gamePath = "./teams/" + GetComponent<GameManager>()._gameName + "/";
        XMLWarbotInterpreter interpreter = new XMLWarbotInterpreter();
        return interpreter.xmlToUnitBehavior(_teams[teamIndex]._name, gamePath, unitType);
    }
}
