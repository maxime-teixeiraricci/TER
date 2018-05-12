using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRessource : MonoBehaviour
{
    public float _rayon;
    public GameObject _ressource;
    public float _timer;
    public int nbItem;
    public int nbItemMax;

    private float t;

	// Use this for initialization
	void Start ()
    {
        //nbItemMax = 40;
        nbItemMax = GameObject.Find("GameManager").GetComponent<GameManager>()._maxResources;
    }
	
	// Update is called once per frame
	void Update ()
    {
        t -= Time.deltaTime;
        nbItem = GameObject.FindGameObjectsWithTag("Item").Length;
        if (t <= 0 && nbItem < nbItemMax)
        {
            t = _timer * Random.Range(0.85f, 1.15f);
            GameObject g = Instantiate(_ressource);
            float r = Random.Range(0f, _rayon);
            float a = Random.Range(0f, 2 * Mathf.PI);
            g.transform.position = transform.position + (new Vector3(Mathf.Cos(a), 0, Mathf.Sin(a))) * r;
        }
        
	}
}
