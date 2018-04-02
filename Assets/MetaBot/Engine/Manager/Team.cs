using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable()]
public class Team 
{
    public string _name;
    public Color _color;
    public Dictionary<string, List<Instruction>> _unitsBehaviour;
    public List<Instruction> explorerTest;

}
