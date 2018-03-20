using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConditionEditorScript : MonoBehaviour
{
    public GameObject _labelActionbuttonObject;
    public GameObject _labelConditionbuttonObject;

    public Transform _conditionParent;
    public Transform _actionParent;

    public List<GameObject> _buttonConditionCreated;
    public List<GameObject> _buttonActionCreated;

    public Dropdown _dropDown;
    public Vector2 deltaVect;
    public bool test;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (test)
        {
            test = !test;
            UpdateCondition();
            UpdateAction();
        }
    }

    void UpdateCondition()
    {
        ClearButton();
        UnitDropDownScript dropDown = GameObject.Find("UnitDropdown").GetComponent<UnitDropDownScript>();
        UnitPerceptAction upa = GameObject.Find("EditorManager").GetComponent<EditorManagerScript>().find(_dropDown.captionText.text);
        Vector2 mov = Vector2.zero;
        foreach (string s in upa.percepts)
        {
            GameObject button = Instantiate(_labelConditionbuttonObject, _conditionParent);
            button.GetComponent<RectTransform>().anchoredPosition += mov;
            mov += deltaVect;

            button.GetComponent<Text>().text = s.Replace('_', ' ');
            button.GetComponent<createPuzzle>()._label = s;

        }

    }

    void UpdateAction()
    {
        ClearButton();
        UnitDropDownScript dropDown = GetComponent<UnitDropDownScript>();
        UnitPerceptAction upa = GetComponent<EditorManagerScript>().find(_dropDown.captionText.text);
        Vector2 mov = Vector2.zero;
        foreach (string s in upa.actions)
        {
            GameObject button = Instantiate(_labelActionbuttonObject, _actionParent);
            button.GetComponent<RectTransform>().anchoredPosition += mov;
            mov += deltaVect;

            button.GetComponent<Text>().text = s.Replace('_', ' ');
            button.GetComponent<createPuzzle>()._label = s;

        }

    }

    void ClearButton()
    {
        foreach (GameObject go in _buttonActionCreated)
        {
            Destroy(go);
        }
        foreach (GameObject go in _buttonConditionCreated)
        {
            Destroy(go);
        }
        _buttonActionCreated = new List<GameObject>();
        _buttonConditionCreated = new List<GameObject>();
    }
}
