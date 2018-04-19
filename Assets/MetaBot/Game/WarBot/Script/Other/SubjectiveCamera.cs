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
    bool stuck;
    public GameObject _hudTextUnit;
    public GameObject _hudStatsUnit;
    public float speed = 5.0f;
    public float DragSpeed = 20f;
    public bool ReverseDrag = true;

    private Vector3 _DragOrigin;
    private Vector3 _Move;
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

                    if (fps)
                    {
                        Renderer rs = unit.GetComponentInChildren<Renderer>();
                        Material m = rs.material;
                        m.color = back;

                        unit = hit.transform.gameObject;

                        Renderer rs2 = unit.GetComponentInChildren<Renderer>();
                        Material m2 = rs2.material;
                        back = m2.color;
                        m2.color = Color.white;
                        Camera.main.transform.position = (new Vector3(unit.gameObject.transform.position.x, unit.gameObject.transform.position.y + 35, unit.gameObject.transform.position.z - 18));
                        stuck = true;
                    }
                    else
                    {
                        fps = true;
                        stuck = true;
                        unit = hit.transform.gameObject;
                        minimap.gameObject.SetActive(true);
                        GameObject.Find("Main Camera").GetComponent<FollowCamera>().enabled = false;
                        //      Camera.main.transform.rotation = unit.gameObject.transform.rotation;
                        //      Camera.main.transform.eulerAngles += new Vector3(0, 90, 0);
                        _hudTextUnit.GetComponent<Text>().text = "";
                        Renderer rs = unit.GetComponentInChildren<Renderer>();
                        Material m = rs.material;
                        back = m.color;
                        m.color = Color.white;
                        Camera.main.transform.position = (new Vector3(unit.gameObject.transform.position.x, unit.gameObject.transform.position.y + 35, unit.gameObject.transform.position.z - 18));
                    }
                    break;
                }
            }
            else
            {
                    _hudTextUnit.GetComponent<Text>().text = "";
            }
        }

        if (fps)
        {
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
            if (!stuck)
            {
                if (Input.mousePosition.x <= 2)
                    Camera.main.transform.Translate(new Vector3(-DragSpeed * Time.deltaTime, 0));
                if (Input.mousePosition.y <= 2)
                    Camera.main.transform.Translate(new Vector3(0, -DragSpeed * Time.deltaTime));

                if (Input.mousePosition.x >= Screen.width - 2)
                    Camera.main.transform.Translate(new Vector3(DragSpeed * Time.deltaTime, 0));
                if (Input.mousePosition.y >= Screen.height - 2)
                    Camera.main.transform.Translate(new Vector3(0, DragSpeed * Time.deltaTime));

                if (Input.GetMouseButtonDown(2))
                    stuck = true;
            }
            else
            {
                Camera.main.transform.position = (new Vector3(unit.gameObject.transform.position.x, unit.gameObject.transform.position.y + 35, unit.gameObject.transform.position.z-18));
                if (Input.GetMouseButtonDown(2))
                    stuck = false;
            }


            /* if (Input.GetButtonDown("y"))
             {
                 Camera.main.transform.position = (new Vector3(unit.gameObject.transform.position.x, unit.gameObject.transform.position.y + 35, unit.gameObject.transform.position.z));
                 return;
             }*/


            /*           if (Input.GetMouseButtonDown(2))
                       {
                           _DragOrigin = Input.mousePosition;
                           //return;
                       }

                       if (!Input.GetMouseButton(2)) return;

                       if (ReverseDrag)
                       {
                           Vector3 pos = Camera.main.ScreenToViewportPoint(new Vector3(0,0,0) - _DragOrigin);
                           _Move = new Vector3(pos.x * DragSpeed, 0, pos.y * DragSpeed);
                       }
                      else
                       {
                           Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - _DragOrigin);
                           _Move = new Vector3(pos.x * -DragSpeed, 0, pos.y * -DragSpeed);
                       }

                       Camera.main.transform.Translate(_Move, Space.World);
                       //Camera.main.transform.RotateAround(Camera.main.transform.position, new Vector3(0,Input.GetAxis("Mouse X")), speed * Time.deltaTime);
                       // Camera.main.transform.RotateAround(Camera.main.transform.position, new Vector3(Input.GetAxis("Mouse Y"),0), speed * Time.deltaTime);      */
        if (Input.GetAxis("Mouse ScrollWheel") != 0f) // forward
        {
                Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y - 10 * Input.GetAxis("Mouse ScrollWheel"), Camera.main.transform.position.z);
        }

        }
    

        //Camera.main.transform.rotation.Set(0, 0, 0, unit.transform.rotation.w);
        //Camera.main.transform.position = (new Vector3(unit.gameObject.transform.position.x, unit.gameObject.transform.position.y + 35, unit.gameObject.transform.position.z));
    }

}
