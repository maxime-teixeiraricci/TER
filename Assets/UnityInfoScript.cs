using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnityInfoScript : MonoBehaviour
{
    public string _name;
    public GameObject _textRender;
    public GameObject _target;
    public Vector2 _deltaScreen;


    // Update is called once per frame
    void Update()
    {
        RaycastUpdate();
        UpdateCamera();
    }

    void RaycastUpdate()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider != null && hit.transform.tag == "Unit")
            {
                _name = hit.transform.name;
                _target = hit.transform.gameObject;

            }

        }
        else
        {
            _name = "";
        }
    }



    void UpdateCamera()
    {
        Vector3 screenLocation = Camera.main.WorldToScreenPoint(_target.transform.position);
        _textRender.transform.position = screenLocation + (Vector3)_deltaScreen;
        _textRender.GetComponent<Text>().text = _name;
    }
}
