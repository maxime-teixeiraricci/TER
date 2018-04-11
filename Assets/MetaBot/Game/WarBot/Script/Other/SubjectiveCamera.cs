using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubjectiveCamera : MonoBehaviour {
    Vector3 backPosition;
    Quaternion backRotation;
    public GameObject unit;
    public GameObject minimap;
    bool fps;
    public GameObject _hudTextUnit;
    public GameObject _hudStatsUnit;

    // Use this for initialization
    void Start () {
        backPosition = transform.position;
        backRotation = transform.rotation;
        
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
	void Update ()
    {
        _hudStatsUnit.SetActive(unit != null);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits;
        hits = Physics.RaycastAll(ray);
        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.tag == "Unit" && !hit.collider.isTrigger)
            {
                _hudTextUnit.transform.position = Camera.main.WorldToScreenPoint(hit.transform.position);
                _hudTextUnit.GetComponent<Text>().text = hit.transform.name;
                unit = hit.transform.gameObject;
                if (Input.GetMouseButtonDown(0))
                {
                    fps = true;
                    minimap.gameObject.SetActive(true);
                }
                break;
            }
            else
            {
                _hudTextUnit.GetComponent<Text>().text = "";
                unit = null;
            }
           
        }
        if (Input.GetMouseButtonDown(1) && unit != null)
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
