using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScriptHUD : MonoBehaviour
{
    public float timePassed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        timePassed += Time.deltaTime;
        int mins = (int)(timePassed / 60);
        int secs = (int)(timePassed % 60);
        GetComponent<Text>().text = ("" + mins).PadLeft(2, '0') + ":" + ("" + secs).PadLeft(2, '0');

    }
}
