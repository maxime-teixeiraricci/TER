using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterMovingScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        GetComponent<MeshRenderer>().material.mainTextureOffset = new Vector2(Time.time * -0.2f, Time.time * -0.3f);
	}
}
