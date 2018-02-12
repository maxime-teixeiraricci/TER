using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetRandom : MonoBehaviour
{
    private float timer;
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        timer += Time.deltaTime;
        Debug.DrawRay(transform.position, new Vector3(0, -50, 0));
        if (timer > 6)
        {
            timer -= 6;
            transform.position = new Vector3(Random.Range(-15f, 15f), 5, Random.Range(-15f, 15f));
        }
	}
}
