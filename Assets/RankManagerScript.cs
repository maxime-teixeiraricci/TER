using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankManagerScript : MonoBehaviour
{
    public List<int> _rank;
    public delegate void Rank(int i);
    public Dictionary<string, Rank> _ranks = new Dictionary<string, Rank>();


    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
