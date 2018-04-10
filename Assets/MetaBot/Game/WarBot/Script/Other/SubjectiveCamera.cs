using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubjectiveCamera : MonoBehaviour {
    Vector3 backPosition;
    Quaternion backRotation;
    GameObject unit;
    public GameObject minimap;
    bool fps;

    // Use this for initialization
    void Start () {
        backPosition = transform.position;
        backRotation = transform.rotation;
        unit = GetClickedGameUnit();
        
    }

    GameObject GetClickedGameUnit()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity) && hit.collider.tag == "Unit" )
            return hit.transform.gameObject;
        else
            return null;
    }


	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
           unit = GetClickedGameUnit();
            if (unit != null)
            {
                fps = true;
                minimap.gameObject.SetActive(true);
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (fps)
            {
                transform.SetPositionAndRotation(backPosition, backRotation);
                fps = false;
                minimap.gameObject.SetActive(false);
                unit = null;
            }
        }

        if (fps)
        {
              Camera.main.transform.SetPositionAndRotation(new Vector3(unit.gameObject.transform.position.x , unit.gameObject.transform.position.y + 2, unit.gameObject.transform.position.z), unit.gameObject.transform.rotation);
            Camera.main.transform.eulerAngles += new Vector3(0,90,0);
        }
    }
}
