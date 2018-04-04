using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConditionEditorScript : MonoBehaviour
{
    public GameObject _labelActionButtonObject;
    public GameObject _labelConditionButtonObject;
    public GameObject _labelMessageButtonObject;
    public GameObject _labelControlButtonObject;


    public List<GameObject> _buttonCreated;

    public Dropdown _dropDown;
    public Vector2 deltaVect;

	// Use this for initialization


    public void UpdateButton()
    {
        ClearButton();
        UpdateCondition();
        UpdateAction();
    }

    public void UpdateCondition()
    {
        ClearButton();
        UnitDropDownScript dropDown = GameObject.Find("UnitDropdown").GetComponent<UnitDropDownScript>();
        UnitPerceptAction upa = GameObject.Find("EditorManager").GetComponent<EditorManagerScript>().find(_dropDown.captionText.text);
        Vector2 mov = Vector2.zero;
        foreach (string s in upa.percepts)
        {
            GameObject button = Instantiate(_labelConditionButtonObject, GameObject.Find("ListPuzzle").transform);
            button.transform.parent = GameObject.Find("ListPuzzle").transform;
            _buttonCreated.Add(button);
            button.GetComponent<RectTransform>().anchoredPosition += mov;
            mov += new Vector2(0, -button.GetComponent<RectTransform>().rect.height)+ deltaVect;

            button.GetComponent<PuzzleButtonScript>().value = s;
            button.GetComponent<createPuzzle>()._label = s;
        }

    }

    public void UpdateAction()
    {
        ClearButton();
        UnitDropDownScript dropDown = GameObject.Find("UnitDropdown").GetComponent<UnitDropDownScript>();
        UnitPerceptAction upa = GameObject.Find("EditorManager").GetComponent<EditorManagerScript>().find(_dropDown.captionText.text);
        Vector2 mov = Vector2.zero;
        foreach (string s in upa.actions)
        {
            GameObject button = Instantiate(_labelActionButtonObject, GameObject.Find("ListPuzzle").transform);
            button.transform.parent = GameObject.Find("ListPuzzle").transform;
            _buttonCreated.Add(button);
            button.GetComponent<RectTransform>().anchoredPosition += mov;
            mov += new Vector2(0, -button.GetComponent<RectTransform>().rect.height) + deltaVect;

            button.GetComponent<PuzzleButtonScript>().value = s;
            button.GetComponent<createPuzzle>()._label = s;
        }

    }

    public void UpdateMessage()
    {
        ClearButton();
        UnitDropDownScript dropDown = GameObject.Find("UnitDropdown").GetComponent<UnitDropDownScript>();
        UnitPerceptAction upa = GameObject.Find("EditorManager").GetComponent<EditorManagerScript>().find(_dropDown.captionText.text);
        Vector2 mov = Vector2.zero;
        foreach (string s in upa.message)
        {
            GameObject button = Instantiate(_labelMessageButtonObject, GameObject.Find("ListPuzzle").transform);
            button.transform.parent = GameObject.Find("ListPuzzle").transform;
            _buttonCreated.Add(button);
            button.GetComponent<RectTransform>().anchoredPosition += mov;
            mov += new Vector2(0, -button.GetComponent<RectTransform>().rect.height) + deltaVect;

            button.GetComponent<PuzzleButtonScript>().value = s;
            button.GetComponent<createPuzzle>()._label = s;
        }

    }

    public void UpdateControl()
    {
        ClearButton();
        Vector2 mov = Vector2.zero;
        foreach (string s in GameObject.Find("EditorManager").GetComponent<EditorManagerScript>()._controlePuzzle)
        {
            GameObject button = Instantiate(_labelControlButtonObject, GameObject.Find("ListPuzzle").transform);
            button.transform.parent = GameObject.Find("ListPuzzle").transform;
            _buttonCreated.Add(button);
            button.GetComponent<RectTransform>().anchoredPosition += mov;
            mov += new Vector2(0, -button.GetComponent<RectTransform>().rect.height) + deltaVect;
        }

    }
    /*
    void UpdateAction()
    {
        UnitDropDownScript dropDown = GetComponent<UnitDropDownScript>();
        UnitPerceptAction upa = GetComponent<EditorManagerScript>().find(_dropDown.captionText.text);
        Vector2 mov = Vector2.zero;
        foreach (string s in upa.actions)
        {
            GameObject button = Instantiate(_labelActionbuttonObject, _actionParent);
            _buttonActionCreated.Add(button);
            button.GetComponent<RectTransform>().anchoredPosition += mov;
            mov += deltaVect;

            button.GetComponent<Text>().text = s.Replace('_', ' ');
            button.GetComponent<createPuzzle>()._label = s;

        }

    }*/

    void ClearButton()
    {
        foreach (GameObject go in _buttonCreated)
        {
            Destroy(go);
        }
        _buttonCreated = new List<GameObject>();
    }
}
