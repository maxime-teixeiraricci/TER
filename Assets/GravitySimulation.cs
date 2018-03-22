using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitySimulation : MonoBehaviour {

    public float _speed;
    public Vector3 _gravityVect;
    public float _groundDist;

	// Use this for initialization
	void Start () {
		
	}
	
    //position += _speed/seconde dans la direction _gravityVect
	// Update is called once per frame
	void Update () {
        //S'il n'y a rien quelque chose en dessous
        if (!Physics.Raycast(transform.position, _gravityVect.normalized, _groundDist))
        {
            transform.position += Time.deltaTime * _speed * _gravityVect;
        }
	}
}
