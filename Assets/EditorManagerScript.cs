using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using UnityEngine.Serialization;

public class EditorManagerScript : MonoBehaviour
{
    public List<string> _controlePuzzle;
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
        string path = Application.streamingAssetsPath+"/WarBot/" + gameName+".gameset";
        StreamReader reader = new StreamReader(path);

        while (!reader.EndOfStream)
        {
            string s = reader.ReadLine();

            if (s.Equals("<"))
            {
                UnitPerceptAction unitBehav = new UnitPerceptAction();
                unitBehav.percepts = new List<string>();
                unitBehav.actions = new List<string>();
                unitBehav.actionsNonTerminal = new List<string>();
                unitBehav.message = new List<string>();

                unitBehav.unit = reader.ReadLine();
                while (!s.Equals(">"))
                {
                    s = reader.ReadLine();
                    if (s.Contains("[MESSAGE]"))
                    {
                        unitBehav.message.Add(s.Replace("[MESSAGE]", ""));
                    }
                    else if (s.Contains("[PERCEPTS]"))
                    {
                        unitBehav.percepts.Add(s.Replace("[PERCEPTS]", ""));
                    }
                    else if (s.Contains("[ACTIONS]"))
                    {
                        unitBehav.actions.Add(s.Replace("[ACTIONS]", ""));
                    }
                    else if (s.Contains("[ANT]"))
                    {
                        unitBehav.actionsNonTerminal.Add(s.Replace("[ANT]", ""));
                    }
                }
                /*
                string action = reader.ReadLine();
                
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
                }*/
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
    public List<string> message ;
    public List<string> actionsNonTerminal;

}
