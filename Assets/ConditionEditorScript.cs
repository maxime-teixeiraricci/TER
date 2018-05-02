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
    public GameObject _labelANTButtonObject;


    public List<GameObject> _buttonCreated;

    public Dropdown _dropDown;
    public Vector2 deltaVect;

	// Use this for initialization
    public void Start()
    {
        UpdateControl();
    }

    public void UpdateButton()
    {
        ClearButton();
        UpdateCondition();
        UpdateAction();
        GameObject.Find("StartPuzzle").GetComponent<StartPuzzleScript>().UpdateAllValidPuzzles();
    }

    public void UpdateCondition()
    {
        ClearButton();

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
            button.GetComponent<RectTransform>().anchoredPosition = new Vector2(button.GetComponent<RectTransform>().anchoredPosition.x, GameObject.Find("ListPuzzle").GetComponent<RectTransform>().rect.height / 2 - button.GetComponent<RectTransform>().rect.height);
            mov += new Vector2(0, -button.GetComponent<RectTransform>().rect.height) + deltaVect;
        }

    }

    public void UpdateANT()
    {
        ClearButton();

        UnitPerceptAction upa = GameObject.Find("EditorManager").GetComponent<EditorManagerScript>().find(_dropDown.captionText.text);
        Vector2 mov = Vector2.zero;
        foreach (string s in upa.actionsNonTerminal)
        {
            print(s);
            GameObject button = Instantiate(_labelANTButtonObject, GameObject.Find("ListPuzzle").transform);
            button.transform.parent = GameObject.Find("ListPuzzle").transform;
            _buttonCreated.Add(button);
            button.GetComponent<RectTransform>().anchoredPosition += mov;
            mov += new Vector2(0, -button.GetComponent<RectTransform>().rect.height) + deltaVect;

            button.GetComponent<PuzzleButtonScript>().value = s;
            button.GetComponent<createPuzzle>()._label = s;
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
