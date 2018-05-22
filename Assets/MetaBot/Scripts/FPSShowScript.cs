using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSShowScript : MonoBehaviour {

    public float _fps;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update ()
    {
        float d = Time.deltaTime;
        float fps = 0;
        
        if (d > 0f)
        {
            fps = 1.0f / d;
        }
        _fps = fps;
        if (fps < 30f)
        {
            GetComponent<Text>().color = Color.red;
        }
        else if (fps < 60f)
        {
            GetComponent<Text>().color = Color.yellow;
        }
        else
        {
            GetComponent<Text>().color = Color.green;
        }
        GetComponent<Text>().text = "" + fps;
    }
}
