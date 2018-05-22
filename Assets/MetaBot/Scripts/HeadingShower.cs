using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeadingShower : MonoBehaviour
{
    public float angle;
    public float distance;
    public GameObject center;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        HeadingPosition();
    }

    void HeadingPosition()
    {
        float _angle = 360 - angle;
        float X = distance * Mathf.Cos(_angle * Mathf.Deg2Rad) + center.GetComponent<RectTransform>().anchoredPosition.x;
        float Y = distance * Mathf.Sin(_angle * Mathf.Deg2Rad) + center.GetComponent<RectTransform>().anchoredPosition.y;

        GetComponent<RectTransform>().anchoredPosition = new Vector2(X, Y);
    }
}
