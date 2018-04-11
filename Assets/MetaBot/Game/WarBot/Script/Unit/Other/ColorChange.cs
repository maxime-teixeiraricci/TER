using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour
{
    public MeshRenderer[] _materials;
	// Use this for initialization
	void Start ()
    {
        ChangeColor();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ChangeColor()
    {
        if (GameObject.Find("GameManager"))
        {
            foreach (MeshRenderer mesh in _materials)
            {
                foreach (Material mat in mesh.materials)
                {
                    if (GetComponent<Stats>()._teamIndex < GameObject.Find("GameManager").GetComponent<TeamManager>()._teams.Count)
                    {
                        mat.color = GameObject.Find("GameManager").GetComponent<TeamManager>()._teams[GetComponent<Stats>()._teamIndex]._color;
                    }
                }
            }
        }
    }
}
