using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleButtonScript : MonoBehaviour
{
    public Text _text;
    public string label;
    public string value;

	// Use this for initialization
	void Start () {

    }

    // Update is called once per frame
    void Update ()
    {
        string traduction = value;
        if (GameObject.Find("GameManager"))
        {
            GameObject.Find("GameManager").GetComponent<Traducteur>().setTextOriginal(value);
            traduction = GameObject.Find("GameManager").GetComponent<Traducteur>().traduction;
        }
        label = traduction.Replace("PERCEPT_", "").Replace("ACTN_", "").Replace("_", " ");
        _text.text = label;
        //label = value.Replace("PERCEPT_", "").Replace("ACTN_", "").Replace("_", " ");
        //_text.text = label;
	}
}
