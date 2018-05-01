using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubjectiveCamera : MonoBehaviour {
    Vector3 backPosition;//sauvegarde de la position de la caméra de base
    Quaternion backRotation;//sauvegarde de la rotation de la caméra de base
    public GameObject unit;//unit a laquelle on est accroché
    public GameObject minimap;//minimap a activer en mode fps
    public bool fps;//gestion de la caméra FPS
    public bool stuck;//gestion de la caméra libre
    public GameObject _hudTextUnit;//Text panneau stat de l'unité
    public GameObject select;//fait apparaitre l'unit sur la minimap en caméra fps
    public GameObject _hudStatsUnit;//Panneau stat de l'unité

    public float speed = 1f;
    public bool ReverseDrag = true;
    public GameObject mainCam;

    private Vector3 _DragOrigin;
    private Vector3 _Move;

    public float speedH = 2.0f;
    public float speedV = 2.0f;

    Collider terrain;//Collider du terrain pour limiter la caméra libre

    private float yaw = 0.0f;
    private float pitch = 0.0f;
    private CursorLockMode back;//permet de lock le curseur en caméra fps

    //Quitter le mode 1ere personne
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

    //Quitter le mode Caméra Libre
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

    //Rentrer en mode Caméra Libre
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
        terrain = GameObject.FindGameObjectWithTag("Ground").GetComponent<Collider>();
    }

    //Permet de tester si la souris est en dehors de l'écran
    bool isMouseOffScreen()
    {
        return (Input.mousePosition.x <= 2 || Input.mousePosition.y <= 2 || Input.mousePosition.x >= Screen.width - 2 || Input.mousePosition.y >= Screen.height - 2);
    }


	// Update is called once per frame
	void Update ()
    {
        //Vérifie si on a cliqué sur une unité afin de passer en caméra FPS
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


        //traitement de la caméra fps
        if (fps)
        {
            Camera.main.transform.position = new Vector3(unit.gameObject.transform.position.x, unit.gameObject.transform.position.y + 3, unit.gameObject.transform.position.z+1);
            select.transform.position = new Vector3(unit.transform.position.x, unit.transform.position.y, unit.transform.position.z);

            yaw += speedH * Input.GetAxis("Mouse X");
            pitch -= speedV * Input.GetAxis("Mouse Y");
            transform.eulerAngles = new Vector3(+25.0f, yaw, 0.0f);
        }

        //traitement de la caméra libre
        if (stuck)
        {
            if (Input.GetAxis("Mouse ScrollWheel") < 0f) // arriere
            {
                GameObject ground = GameObject.FindGameObjectWithTag("Ground");
                Vector3 dist = Camera.main.transform.position - ground.transform.position;
                if (!(dist.magnitude > backPosition.y + backPosition.y/2))
                {
                    Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y - 10 * Input.GetAxis("Mouse ScrollWheel"), Camera.main.transform.position.z);
                }
            }
            else if (Input.GetAxis("Mouse ScrollWheel") > 0f) // avant
            {
                        float dist = Camera.main.transform.position.y - terrain.bounds.size.y;
                        if (!(dist < 1))
                        {
                            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y - 10 * Input.GetAxis("Mouse ScrollWheel"), Camera.main.transform.position.z);
                        }
            }
            

            if (Input.mousePosition.x <= 2 && Camera.main.transform.position.x > backPosition.x - terrain.bounds.size.x / 4)
                Camera.main.transform.position = new Vector3(Camera.main.transform.position.x - speed, Camera.main.transform.position.y, Camera.main.transform.position.z);
            if (Input.mousePosition.y <= 2 && Camera.main.transform.position.z > backPosition.z - terrain.bounds.size.z / 4)
                Camera.main.transform.position = new Vector3(Camera.main.transform.position.x , Camera.main.transform.position.y , Camera.main.transform.position.z - speed);

            if (Input.mousePosition.x >= Screen.width - 2 && Camera.main.transform.position.x < backPosition.x + terrain.bounds.size.x / 4)
                Camera.main.transform.position = new Vector3(Camera.main.transform.position.x + speed, Camera.main.transform.position.y, Camera.main.transform.position.z);
            if (Input.mousePosition.y >= Screen.height - 2 && Camera.main.transform.position.z < backPosition.z + terrain.bounds.size.z / 4)
                Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y , Camera.main.transform.position.z + speed);
        }
    }

}
