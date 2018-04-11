using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleButtonScript : MonoBehaviour
{
    public Text _text;
    public string label;
    public string value;
    public string l;
	// Use this for initialization
	void Start () {
        
    }

    // Update is called once per frame
    void Update ()
    {
        label = value.Replace("PERCEPT_", "").Replace("ACTN_", "").Replace("_", " ");
        _text.text = label;
	}
}
