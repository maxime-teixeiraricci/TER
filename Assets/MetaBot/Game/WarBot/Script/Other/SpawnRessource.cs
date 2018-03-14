using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRessource : MonoBehaviour
{
    public float _rayon;
    public GameObject _ressource;
    public float _timer;

    private float t;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        t -= Time.deltaTime;
        if (t <= 0)
        {
            t = _timer * Random.Range(0.85f, 1.15f);
            GameObject g = Instantiate(_ressource);
            float r = Random.Range(0f, _rayon);
            float a = Random.Range(0f, 2 * Mathf.PI);
            g.transform.position = transform.position + (new Vector3(Mathf.Cos(a), 0, Mathf.Sin(a))) * r;
        }
        
	}
}
