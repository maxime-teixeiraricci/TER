using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SightVisionScript : MonoBehaviour
{
    /* Ce script permet d'afficher le champs de vision des unites */

    public GameObject target;
    public GameObject maskL;
    public GameObject maskR;
    public float angleR;
    public float angleL;
    public bool done;

	// Use this for initialization
	void Start ()
    {
        
    }
	
	// Update is called once per frame
	void Update ()
    {
		if (GameObject.Find("GameManager") && !done && target.GetComponent<Stats>()._teamIndex < GameObject.Find("GameManager").GetComponent<TeamManager>()._teams.Count)
        {
            float angle = target.GetComponent<Sight>()._angle;
            float distance = target.GetComponent<Sight>()._distance;
            if (angle > 180) { Destroy(maskL); Destroy(maskR); }
            GetComponent<SpriteRenderer>().color = GameObject.Find("GameManager").GetComponent<TeamManager>()._teams[target.GetComponent<Stats>()._teamIndex]._color;
            GetComponent<SpriteRenderer>().color *= new Color(1, 1, 1, 0.25f);
            
            angleR = -angle / 2;
            angleL = angle / 2;
            maskL.transform.localEulerAngles = new Vector3(maskL.transform.localEulerAngles.x, maskL.transform.localEulerAngles.y, angleL);
            maskR.transform.localEulerAngles = new Vector3(maskR.transform.localEulerAngles.x, maskR.transform.localEulerAngles.y, angleR);
            transform.localScale = new Vector3(distance, distance, 1);
            done = true;
        }
       /* if (maskR.transform.eulerAngles.z != angleR || maskL.transform.eulerAngles.z != angleL)
        {
            maskL.transform.localEulerAngles = new Vector3(maskL.transform.localEulerAngles.x, maskL.transform.localEulerAngles.y, angleL);
            maskR.transform.localEulerAngles = new Vector3(maskR.transform.localEulerAngles.x, maskR.transform.localEulerAngles.y, angleR);
        }*/
	}
}
