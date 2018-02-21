using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PanelCompare : MonoBehaviour {

    RectTransform rt;

    // Use this for initialization
    void Start () {
        //rt = GetComponent<RectTransform>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void comparePanels()
    {
        /*Vector3[] v = new Vector3[4];
        rt.GetWorldCorners(v);
        for (var i = 0; i < 4; i++)
        {
            Debug.Log("World Corner " + i + " : " + v[i]);
        }*/

        //float newWidth = Screen.width / 2;
        
        
        //this.transform.localScale = new Vector3(194.353f, -56.91212f, 0);
        //this.transform.position.x = new Vector3(0,0);
        //Panel.transform.scale = new Vector3(x, y, z);
        //Debug.Log("x = " + this.transform.position.x);
        //Debug.Log("y = " + this.transform.position.y);
    }
}
