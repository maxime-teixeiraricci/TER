using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CondPuzzleScript : MonoBehaviour
{

    public enum Type {ACTION, CONDITION, ACTION_NON_TERMINAL, MESSAGE};

    public Type type;
    public GameObject nextPuzzle;
    public GameObject ifPuzzle;
    public GameObject beforePuzzle;
    public string _value;
    ManageDragAndDrop manager;
    public Text _label;
    public string validPlace = "false";
    public bool isValid;
    public bool neg;
    Image image;
    public Color defaultColor;
    public Color validColor;
    public Dropdown messageDropDown;
    

    // Use this for initialization
    void Start ()
    {
        
        manager = GetComponent<ManageDragAndDrop>();
        NormalizedLabel();
        image = GetComponent<Image>();
        //defaultColor = image.color;
        if (messageDropDown)
        {
            messageDropDown.ClearOptions();
            List<string> options = new List<string>();
            options.Add("Target");
            foreach (UnitPerceptAction upa in GameObject.Find("EditorManager").GetComponent<EditorManagerScript>()._unitBehaviour)
                options.Add(upa.unit);
            options.Add("All");
            messageDropDown.AddOptions(options);

        }
    }

    public void NormalizedLabel()
    {
        string questionString = (_value.Contains("PERCEPT_")) ? "?" : "";
        if (_value.Contains("PERCEPT_"))
        {
            _label.GetComponent<Text>().text = _value.Replace("PERCEPT_", "").Replace("_", " ") + "?";
        }
        if (_value.Contains("ACTION_"))
        {
            _label.GetComponent<Text>().text = _value.Replace("ACTION_", "").Replace("_", " ");
        }
        if (_value.Contains("MESSAGE_"))
        {
            _label.GetComponent<Text>().text = "SEND \"" + _value.Replace("MESSAGE_", "").Replace("_", " ") + "\"";
        }

    }

    // Update is called once per frame
    void Update ()
    {
        UpdateIfPuzzle();
        UpdateCondPuzzle();
        isValid = IsValid();
        image.color = (isValid) ? validColor:defaultColor ;
    }

 


    void UpdateIfPuzzle()
    {
        
        validPlace = "false";

        foreach (GameObject puzzle in GameObject.FindGameObjectsWithTag("IfPuzzle"))
        {
            if (manager.posGridX - 1 == puzzle.GetComponent<ManageDragAndDrop>().posGridX
                && manager.posGridY == puzzle.GetComponent<ManageDragAndDrop>().posGridY && puzzle.GetComponent<IfPuzzleScript>().validPlace == "true")
            {
                image.color = validColor;
                validPlace = "true";
                break;
            }
        }
    }


    public void NegationBoutton()
    {
        neg = !neg;
        if (!neg)
        {
            _value = _value.Replace("NOT_", "");
            _label.GetComponent<Text>().color = Color.white;
        }
        else
        {
            _value = "NOT_" + _value.Replace("NOT_", "");
            _label.GetComponent<Text>().color = new Color(0.95f, 0.5f, 0.5f);
        }
        _label.GetComponent<Text>().text = _value.Replace("PERCEPT_", "").Replace("_", " ") + "?";
    }

    void UpdateCondPuzzle()
    {
        nextPuzzle = null;

        foreach (GameObject puzzle in GameObject.FindGameObjectsWithTag("Puzzle"))
        {

            if (manager.posGridX + 2 == puzzle.GetComponent<ManageDragAndDrop>().posGridX && 
                manager.posGridY == puzzle.GetComponent<ManageDragAndDrop>().posGridY)
            {
                nextPuzzle = puzzle;
                puzzle.GetComponent<CondPuzzleScript>().beforePuzzle = gameObject;

                puzzle.GetComponent<CondPuzzleScript>().validPlace = "true";
                image.color = validColor;
                break;
            }

            
        }
      
    }

    bool IsValid()
    {
        if (beforePuzzle != null && beforePuzzle.GetComponent<IfPuzzleScript>()) { return true; }
        if (beforePuzzle != null && beforePuzzle.GetComponent<CondPuzzleScript>() && beforePuzzle.GetComponent<CondPuzzleScript>().isValid)
        {
            if (type == Type.CONDITION)
            {
                return (beforePuzzle.GetComponent<IfPuzzleScript>() != null) || (beforePuzzle.GetComponent<CondPuzzleScript>().type == Type.CONDITION);
            }
            if (type == Type.ACTION || type == Type.ACTION_NON_TERMINAL)
            {
                return (beforePuzzle.GetComponent<IfPuzzleScript>() != null) || (beforePuzzle.GetComponent<CondPuzzleScript>().type == Type.ACTION_NON_TERMINAL);
            }
        }
        return false;
    }
}
