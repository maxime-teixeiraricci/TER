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
    public float speed = 5.0f;
    public float rotationX;
    public float rotationY;
    Color back;

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

    bool isMouseOffScreen()
    {
        return (Input.mousePosition.x <= 2 || Input.mousePosition.y <= 2 || Input.mousePosition.x >= Screen.width - 2 || Input.mousePosition.y >= Screen.height - 2);
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
                if (Input.GetMouseButtonDown(0))
                {
                    unit = hit.transform.gameObject;
                    fps = true;
                    minimap.gameObject.SetActive(true);
                    GameObject.Find("Main Camera").GetComponent<FollowCamera>().enabled = false;
                    //      Camera.main.transform.rotation = unit.gameObject.transform.rotation;
                    //      Camera.main.transform.eulerAngles += new Vector3(0, 90, 0);

                    Renderer rs = unit.GetComponentInChildren<Renderer>();
                    Material m = rs.material;
                    back = m.color;
                    m.color = Color.white;

                }
                break;
            }
            else
            {
                    _hudTextUnit.GetComponent<Text>().text = "";
            }
        }

        if (fps)
        {
            if (_hudTextUnit.GetComponent<Text>().text != "")
                _hudTextUnit.GetComponent<Text>().text = ""; 

            if (Input.GetMouseButtonDown(1) && unit != null)
            {
                transform.SetPositionAndRotation(backPosition, backRotation);
                fps = false;
                minimap.gameObject.SetActive(false);
                Renderer rs = unit.GetComponentInChildren<Renderer>();
                Material m = rs.material;
                m.color = back;
                unit = null;
                GameObject.Find("Main Camera").GetComponent<FollowCamera>().enabled = true;
            }


            if (!isMouseOffScreen())
            {
                Camera.main.transform.RotateAround(Camera.main.transform.position, new Vector3(0,Input.GetAxis("Mouse X")), speed * Time.deltaTime);
               // Camera.main.transform.RotateAround(Camera.main.transform.position, new Vector3(Input.GetAxis("Mouse Y"),0), speed * Time.deltaTime);
                
            }

        }
        Camera.main.transform.position = (new Vector3(unit.gameObject.transform.position.x, unit.gameObject.transform.position.y + 35, unit.gameObject.transform.position.z));
    }

}
