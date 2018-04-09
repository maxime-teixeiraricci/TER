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
        if (Physics.Raycast(ray, out hit, Mathf.Infinity) && hit.collider.name.Contains("War"))
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
           /* Quaternion  q = unit.gameObject.transform.rotation;
            q.SetEulerRotation(new Vector3(unit.gameObject.transform.rotation.x, unit.gameObject.transform.rotation.y, unit.gameObject.transform.rotation.z));*/
            Camera.main.transform.SetPositionAndRotation(new Vector3(unit.gameObject.transform.position.x + 2, unit.gameObject.transform.position.y + 2, unit.gameObject.transform.position.z), unit.gameObject.transform.rotation);
            /*Vector3 rotation = transform.eulerAngles;

            rotation.x += Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
            transform.eulerAngles = rotation;*/
        }
    }
}
