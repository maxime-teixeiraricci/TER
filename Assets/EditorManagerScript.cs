using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using UnityEngine.Serialization;

public class EditorManagerScript : MonoBehaviour
{

    public List<UnitPerceptAction> _unitBehaviour = new List<UnitPerceptAction>();

	// Use this for initialization
	void Awake ()
    {
        ReadGameFile();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ReadGameFile()
    {
        string gameName = "TestBot";
        string path = "./Assets/MetaBot/Game/WarBot/"+gameName+".gameset";
        StreamReader reader = new StreamReader(path);

        while (!reader.EndOfStream)
        {
            string currentString = reader.ReadLine();

            if (currentString.Equals("<"))
            {
                UnitPerceptAction unitBehav = new UnitPerceptAction();
                unitBehav.percepts = new List<string>();
                unitBehav.actions = new List<string>();
                unitBehav.actionsNonTerminal = new List<string>();

                unitBehav.unit = reader.ReadLine();
                 
                string action = reader.ReadLine();
                string s = reader.ReadLine();
                while (  s != "[ACTIONS]" && !reader.EndOfStream)
                {
                    unitBehav.percepts.Add(s);
                    s = reader.ReadLine();
                }

                s = reader.ReadLine();
                while (s != "[ACTIONS NON TERMINAL]" && !reader.EndOfStream)
                {
                    unitBehav.actions.Add(s);
                    s = reader.ReadLine();
                }

                s = reader.ReadLine();
                while (s != ">" && !reader.EndOfStream)
                {
                    unitBehav.actionsNonTerminal.Add(s);
                    s = reader.ReadLine();
                }
                _unitBehaviour.Add(unitBehav);
            }
        }
        reader.Close();
    }

    public UnitPerceptAction find(string unit)
    {
        foreach (UnitPerceptAction u in _unitBehaviour)
        {
            if (u.unit == unit) { return u; }
        }
        return new UnitPerceptAction();
    }

}

[System.Serializable]
public struct UnitPerceptAction
{
    public string unit;
    public List<string> percepts;
    public List<string> actions;
    public List<string> actionsNonTerminal;
}
