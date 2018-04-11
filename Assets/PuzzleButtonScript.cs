using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleButtonScript : MonoBehaviour
{
    public Text _text;
    public string label;
    public string value;
    public string langue;
    public string l;
	// Use this for initialization
	void Start () {
        string[] lines = System.IO.File.ReadAllLines("properties.yml");
        foreach (string line in lines)
        {
            if (line.Contains("Language"))
            {
                string[] tmp = line.Split('=');
                langue = tmp[1];
                break;
            }
        }
    }

    // Update is called once per frame
    void Update ()
    {
        Traducteur l = new Traducteur(langue, value);
        string traduction = l.Traduction();
        label = traduction.Replace("PERCEPT_", "").Replace("ACTN_", "").Replace("_", " ");
        _text.text = label;
        //label = value.Replace("PERCEPT_", "").Replace("ACTN_", "").Replace("_", " ");
        //_text.text = label;
	}
}
