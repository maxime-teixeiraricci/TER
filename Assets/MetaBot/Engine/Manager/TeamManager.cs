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
        
        string gamePath = Application.streamingAssetsPath + "/teams/" + GetComponent<GameManager>()._gameName + "/";
        XMLWarbotInterpreter interpreter = new XMLWarbotInterpreter();
        if (_teams[teamIndex]._unitsBehaviour.ContainsKey(unitType))
        {
            return _teams[teamIndex]._unitsBehaviour[unitType];
        }
        return new List<Instruction>();
    }

}
