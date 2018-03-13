using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    private Vector3 currentTarget;
    public float _keySpeed;
    public float _speed;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        Vector3 res = Vector3.zero;
        GameObject[] units = GameObject.FindGameObjectsWithTag("Unit");

        foreach (GameObject go in units)
        {
            res += go.transform.position;
        }
        res *= 1f / (units.Length + 1);
        currentTarget += (res - currentTarget) * Time.deltaTime * _speed;
        transform.LookAt(currentTarget);


        if (Input.GetKey(KeyCode.UpArrow)) { transform.position += new Vector3(1, 0, 0) * Time.deltaTime * _keySpeed; }
        if (Input.GetKey(KeyCode.DownArrow)) { transform.position += new Vector3(-1, 0, 0) * Time.deltaTime * _keySpeed; }
        if (Input.GetKey(KeyCode.LeftArrow)) { transform.position += new Vector3(0, 0, -1) * Time.deltaTime * _keySpeed; }
        if (Input.GetKey(KeyCode.RightArrow)) { transform.position += new Vector3(0, 0, 1) * Time.deltaTime * _keySpeed; }
    }
}
