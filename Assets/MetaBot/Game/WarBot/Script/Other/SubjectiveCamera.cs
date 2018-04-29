using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubjectiveCamera : MonoBehaviour {
    Vector3 backPosition;
    Quaternion backRotation;
    public GameObject unit;
    public GameObject minimap;
    public bool fps;
    public bool stuck;
    public GameObject _hudTextUnit;
    public GameObject select;
    public GameObject _hudStatsUnit;
    public float speed = 20.0f;
    public bool ReverseDrag = true;
    public GameObject mainCam;
    public GameObject terrain;

    private Vector3 _DragOrigin;
    private Vector3 _Move;

    public float speedH = 2.0f;
    public float speedV = 2.0f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;
    private CursorLockMode back;

    public bool removeFPS()
    {
        if (fps)
        {
            transform.SetPositionAndRotation(backPosition, backRotation);
            fps = false;
            removeStuck();
            Cursor.lockState = back;
            minimap.gameObject.SetActive(false);
            unit = null;
            mainCam.GetComponent<FollowCamera>().enabled = true;
            select.GetComponent<SpriteRenderer>().color = new Color(select.GetComponent<SpriteRenderer>().color.r, select.GetComponent<SpriteRenderer>().color.g, select.GetComponent<SpriteRenderer>().color.b, 0);
            return true;
        }

        return false;
    }

    public bool removeStuck()
    {
        if (stuck)
        {
            if (fps)
            {
                removeFPS();
            }
            transform.SetPositionAndRotation(backPosition, backRotation);
            stuck = false;
            mainCam.GetComponent<FollowCamera>().enabled = true;

            return true;
        }

        return false;
    }

    public bool goStuck()
    {
        if (!stuck && !fps)
        {
            if (mainCam.GetComponent<FollowCamera>().enabled)
                mainCam.GetComponent<FollowCamera>().enabled = false;
            stuck = true;
            fps = false;
            Camera.main.transform.rotation = Quaternion.Euler(80, 0, 0);
            return true;
        }

        return false;
    }

    // Use this for initialization
    void Start () {
        backPosition = transform.position;
        backRotation = transform.rotation;
        stuck = false;
        back = Cursor.lockState;
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

                if (Input.GetMouseButtonDown(0) && !stuck)
                {
                        Cursor.lockState = CursorLockMode.Confined;
                        fps = true;
                        unit = hit.transform.gameObject;
                        minimap.gameObject.SetActive(true);
                        mainCam.GetComponent<FollowCamera>().enabled = false;
                        _hudTextUnit.GetComponent<Text>().text = "";
                        Camera.main.transform.position = (new Vector3(unit.gameObject.transform.position.x, unit.gameObject.transform.position.y + 3, unit.gameObject.transform.position.z+1));
                    select.GetComponent<SpriteRenderer>().color = new Color(select.GetComponent<SpriteRenderer>().color.r, select.GetComponent<SpriteRenderer>().color.g, select.GetComponent<SpriteRenderer>().color.b, 1);
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
            Camera.main.transform.position = new Vector3(unit.gameObject.transform.position.x, unit.gameObject.transform.position.y + 3, unit.gameObject.transform.position.z+1);
            select.transform.position = new Vector3(unit.transform.position.x, unit.transform.position.y, unit.transform.position.z);

            yaw += speedH * Input.GetAxis("Mouse X");
            pitch -= speedV * Input.GetAxis("Mouse Y");
            transform.eulerAngles = new Vector3(+25.0f, yaw, 0.0f);
        }

        if (stuck)
        {
            if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backward
            {
                GameObject ground = GameObject.FindGameObjectWithTag("Ground");
                //Vector3 dist = new Vector3(ground.transform.position.x - Camera.main.transform.position.x, ground.transform.position.y - Camera.main.transform.position.y + 10 * Input.GetAxis("Mouse ScrollWheel"), ground.transform.position.z - Camera.main.transform.position.z);
                Vector3 dist = Camera.main.transform.position - ground.transform.position;
                if (!(dist.magnitude > 150))
                {
                    Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y - 10 * Input.GetAxis("Mouse ScrollWheel"), Camera.main.transform.position.z);
                }
            }
            else if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
            {
                GameObject ground = GameObject.FindGameObjectWithTag("Ground");
                //Vector3 dist = new Vector3(ground.transform.position.x - Camera.main.transform.position.x, ground.transform.position.y - Camera.main.transform.position.y + 10 * Input.GetAxis("Mouse ScrollWheel"), ground.transform.position.z - Camera.main.transform.position.z);
                Ray ray2 = Camera.main.ViewportPointToRay(Camera.main.transform.forward);
                RaycastHit[] hits2;
                hits2 = Physics.RaycastAll(ray2);
                foreach (RaycastHit hit2 in hits2)
                {
                    if (hit2.collider.tag == "Ground")
                    {
                        float dist = Vector3.Distance(Camera.main.transform.forward,hit2.transform.position);
                        if (!(dist < 10))
                        {
                            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y - 10 * Input.GetAxis("Mouse ScrollWheel"), Camera.main.transform.position.z);
                        }
                        break;
                    }
                }
            }
            /*  else
              {
                  if (Input.GetAxis("Mouse ScrollWheel") > 0)//scroll arriere
                      Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y - 10 * Input.GetAxis("Mouse ScrollWheel"), Camera.main.transform.position.z);
              }

          /  Ray ray2 = Camera.main.ScreenPointToRay(new Vector3(Screen.height / 2, Screen.width / 2, 0));
              RaycastHit[] hits2;
              hits2 = Physics.RaycastAll(ray2);
              foreach (RaycastHit hit in hits2)
              {
                  if (hit.collider.tag == "Ground")
                  {
                      dist = Camera.main.transform.position - hit.transform.position;
                      if (!(dist.magnitude < 1))
                      {
                          Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y - 10 * Input.GetAxis("Mouse ScrollWheel"), Camera.main.transform.position.z);
                      }
                      else
                      {
                          if (Input.GetAxis("Mouse ScrollWheel") < 0)//scroll arriere
                              Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y - 10 * Input.GetAxis("Mouse ScrollWheel"), Camera.main.transform.position.z);
                      }

                      break;
                  }
              }*/


            /*   if (Input.mousePosition.x <= 2 && Camera.main.transform.position.x > backPosition.x - terrain.GetComponent<Renderer>().bounds.size.x /4)
                   Camera.main.transform.Translate(new Vector3(-speed * Time.deltaTime, 0));
               if (Input.mousePosition.y <= 2 && Camera.main.transform.position.z > backPosition.y - terrain.GetComponent<Renderer>().bounds.size.y/4)
                   Camera.main.transform.Translate(new Vector3(0, -speed * Time.deltaTime));

               if (Input.mousePosition.x >= Screen.width - 2 && Camera.main.transform.position.x < backPosition.x + terrain.GetComponent<Renderer>().bounds.size.x/4)
                   Camera.main.transform.Translate(new Vector3(speed * Time.deltaTime, 0));
               if (Input.mousePosition.y >= Screen.height - 2 && Camera.main.transform.position.z < backPosition.y + terrain.GetComponent<Renderer>().bounds.size.y/4)
                   Camera.main.transform.Translate(new Vector3(0, speed * Time.deltaTime)); */

            /*
          if (Input.mousePosition.x <= 2 && Camera.main.transform.position.x > backPosition.x - terrain.GetComponent<Renderer>().bounds.size.x /4)
               Camera.main.transform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0));
           if (Input.mousePosition.y <= 2 && Camera.main.transform.position.y > backPosition.y - terrain.GetComponent<Renderer>().bounds.size.y/4)
               Camera.main.transform.Translate(new Vector3(0, -speed * Time.deltaTime,0));

           if (Input.mousePosition.x >= Screen.width - 2 && Camera.main.transform.position.x < backPosition.x + terrain.GetComponent<Renderer>().bounds.size.x/4)
               Camera.main.transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
           if (Input.mousePosition.y >= Screen.height - 2 && Camera.main.transform.position.y < backPosition.z + terrain.GetComponent<Renderer>().bounds.size.y/4)
               Camera.main.transform.Translate(new Vector3(0, speed * Time.deltaTime,0)); /* */

            if (Input.mousePosition.x <= 2 && Camera.main.transform.position.x > backPosition.x - terrain.GetComponent<Renderer>().bounds.size.x / 4)
                Camera.main.transform.position = new Vector3(Camera.main.transform.position.x - speed * Time.deltaTime, Camera.main.transform.position.y, Camera.main.transform.position.z);
            if (Input.mousePosition.y <= 2 && Camera.main.transform.position.z > backPosition.z - terrain.GetComponent<Renderer>().bounds.size.z / 4)
                Camera.main.transform.position = new Vector3(Camera.main.transform.position.x , Camera.main.transform.position.y , Camera.main.transform.position.z - speed * Time.deltaTime);

            if (Input.mousePosition.x >= Screen.width - 2 && Camera.main.transform.position.x < backPosition.x + terrain.GetComponent<Renderer>().bounds.size.x / 4)
                Camera.main.transform.position = new Vector3(Camera.main.transform.position.x + speed * Time.deltaTime, Camera.main.transform.position.y, Camera.main.transform.position.z);
            if (Input.mousePosition.y >= Screen.height - 2 && Camera.main.transform.position.z < backPosition.z + terrain.GetComponent<Renderer>().bounds.size.z / 4)
                Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y , Camera.main.transform.position.z + speed * Time.deltaTime);
        }
        //Camera.main.transform.rotation.Set(0, 0, 0, unit.transform.rotation.w);
        //Camera.main.transform.position = (new Vector3(unit.gameObject.transform.position.x, unit.gameObject.transform.position.y + 35, unit.gameObject.transform.position.z));
    }

}
