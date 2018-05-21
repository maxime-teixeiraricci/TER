using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleScript : MonoBehaviour
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
    public bool isLock;
    public bool neg;
    public Color defaultColor;
    public Color validColor;
    public Color currentColor;
    public Dropdown messageDropDown;
    public Vector2 deltaPosNextObject;
    GameObject gamemanager;

    // Use this for initialization
    void Start ()
    {
       
        NormalizedLabel();
        //defaultColor = image.color;
        if (messageDropDown)
        {
            DropDownUpdate();
        }

        gamemanager = GameObject.Find("GameManager");
    }

    public void LockUpdate(bool value)
    {
        isLock = value;
        if (nextPuzzle)
        {
            nextPuzzle.GetComponent<PuzzleScript>().LockUpdate(value);
            deltaPosNextObject = nextPuzzle.GetComponent<RectTransform>().anchoredPosition - GetComponent<RectTransform>().anchoredPosition;
            nextPuzzle.GetComponent<ManageDragAndDrop>().UpdateGridPosition();
        }
    }



    public void DropDownUpdate()
    {
        messageDropDown.ClearOptions();
        List<string> options = new List<string>();
        options.Add("Target");
        foreach (UnitPerceptAction upa in GameObject.Find("EditorManager").GetComponent<EditorManagerScript>()._unitBehaviour)
        options.Add(upa.unit);
        options.Add("All");
        messageDropDown.AddOptions(options);
    }

    public void NormalizedLabel()
    {
        string affiche = _value;
        affiche = affiche.Replace("NOT_", "FALSE_");
        //if (gamemanager)
        //{
        GameObject.Find("GameManager").GetComponent<Traducteur>().setTextOriginal(affiche);
            affiche = GameObject.Find("GameManager").GetComponent<Traducteur>().traduction;
        //}
        if (type == Type.CONDITION)
        {
            _label.GetComponent<Text>().text = affiche.Replace("PERCEPT_", "").Replace("_", " ") + "?";
        }
        if (type == Type.ACTION)
        {
            _label.GetComponent<Text>().text = affiche.Replace("ACTION_", "").Replace("_", " ");
        }
        if (type == Type.MESSAGE)
        {
            string verb = "SEND";
            if (gamemanager)
            {
                gamemanager.GetComponent<Traducteur>().setTextOriginal(verb);
                verb = gamemanager.GetComponent<Traducteur>().traduction;
            }
                _label.GetComponent<Text>().text = verb +" \"" + affiche.Replace("ACTN_MESSAGE_", "").Replace("PERCEPT_", "").Replace("_", " ") + "\"";
        }
        if (type == Type.ACTION_NON_TERMINAL)
        {
          //  _label.GetComponent<Text>().text = _value.Replace("MESSAGE_", "").Replace("ACTN_", "").Replace("_", " ");
            _label.GetComponent<Text>().text = affiche.Replace("MESSAGE_", "").Replace("ACTN_", "").Replace("_", " ");
        }
        if (neg)
        {
         /*   string not = "Not";
            if (GameObject.Find("GameManager"))
            {
                GameObject.Find("GameManager").GetComponent<Traducteur>().setTextOriginal(not);
                not = GameObject.Find("GameManager").GetComponent<Traducteur>().traduction;
            }
            _label.GetComponent<Text>().text = not + " " + _label.GetComponent<Text>().text.Replace("NOT_","");*/
            _label.GetComponent<Text>().color = new Color(1, 0.6f, 0.6f);
        }
        else
        {
            _label.GetComponent<Text>().color = Color.white;
        }

    }

    // Update is called once per frame
    void Update ()
    {
        //isValid = false;
        //UpdateIfPuzzle();
       // UpdateNextPuzzle();
        // isValid = IsValid();
        currentColor = (isValid) ? validColor : defaultColor;
        GetComponent<Image>().color = currentColor;
        if (isLock)
        {
            LockTransition();
        }
    }

 


    void UpdateIfPuzzle()
    {
        
        validPlace = "false";

        foreach (GameObject puzzle in GameObject.FindGameObjectsWithTag("IfPuzzle"))
        {
            if (manager.posGridX - 1 == puzzle.GetComponent<ManageDragAndDrop>().posGridX
                && manager.posGridY == puzzle.GetComponent<ManageDragAndDrop>().posGridY && puzzle.GetComponent<IfPuzzleScript>().validPlace == "true")
            {
                GetComponent<Image>().color = validColor;
                validPlace = "true";
                break;
            }
        }
    }


    public void NegationBoutton()
    {
        neg = !neg;
        if (neg)
        {

            _value = "NOT_"+ _value.Replace("NOT_", "");
        }
        else
        {
            _value = _value.Replace("NOT_", "");
        }
        NormalizedLabel();
    }

    public void UpdateNextPuzzle()
    {
        nextPuzzle = null;
        if (type == Type.CONDITION)
        {
            UpdateNextCondPuzzle();
        }
        else if (type == Type.ACTION_NON_TERMINAL || type == Type.MESSAGE)
        {
            UpdateNextActionsPuzzle();
        }

        if (nextPuzzle)
        {
            nextPuzzle.GetComponent<PuzzleScript>().isValid = true;
            nextPuzzle.GetComponent<PuzzleScript>().UpdateNextPuzzle();
        }
      
    }

    void UpdateNextCondPuzzle()
    {
        
        foreach (GameObject puzzle in GameObject.FindGameObjectsWithTag("Puzzle"))
        {

            manager = GetComponent<ManageDragAndDrop>();
            Vector2 currentGridPos = manager.getGridPosition();
            Vector2 puzzleGridPos = puzzle.GetComponent<ManageDragAndDrop>().getGridPosition();
            Type typePuzzle = puzzle.GetComponent<PuzzleScript>().type;
            

            if (currentGridPos + new Vector2(2, 0) == puzzleGridPos && typePuzzle == Type.CONDITION)
            {
                nextPuzzle = puzzle;
                break;
            }
        }
    }

    public void LockTransition()
    {
        if (nextPuzzle && nextPuzzle.GetComponent<PuzzleScript>().isLock)
        {
            nextPuzzle.GetComponent<RectTransform>().anchoredPosition = deltaPosNextObject + GetComponent<RectTransform>().anchoredPosition;
            nextPuzzle.GetComponent<ManageDragAndDrop>().UpdateGridPosition();
        }
    }

    void UpdateNextActionsPuzzle()
    {
        foreach (GameObject puzzle in GameObject.FindGameObjectsWithTag("Puzzle"))
        {
            Vector2 currentGridPos = GetComponent<ManageDragAndDrop>().getGridPosition();
            Vector2 puzzleGridPos = puzzle.GetComponent<ManageDragAndDrop>().getGridPosition();
            Type typePuzzle = puzzle.GetComponent<PuzzleScript>().type;
            

            if (currentGridPos + new Vector2(2, 0) == puzzleGridPos && 
                ((typePuzzle == Type.ACTION) || (typePuzzle == Type.ACTION_NON_TERMINAL)|| (typePuzzle == Type.MESSAGE)))
            {
                nextPuzzle = puzzle;
                break;
            }
        }
    }

/*
    bool IsValid()
    {
        if (beforePuzzle != null && beforePuzzle.GetComponent<IfPuzzleScript>()) { return true; }
        if (beforePuzzle != null && beforePuzzle.GetComponent<PuzzleScript>() && beforePuzzle.GetComponent<PuzzleScript>().isValid)
        {
            if (type == Type.CONDITION)
            {
                return (beforePuzzle.GetComponent<IfPuzzleScript>() != null) || (beforePuzzle.GetComponent<PuzzleScript>().type == Type.CONDITION);
            }
            if (type == Type.ACTION || type == Type.ACTION_NON_TERMINAL || type == Type.MESSAGE)
            {
                Type t = beforePuzzle.GetComponent<PuzzleScript>().type;
                return (beforePuzzle.GetComponent<IfPuzzleScript>() != null) || (t == Type.ACTION_NON_TERMINAL) || (t == Type.MESSAGE);
            }
        }
        return false;
    }*/


    private void OnMouseOver()
    {
        if ( Input.GetMouseButtonDown(1) && GameObject.Find("Undo"))
        {   
            gameObject.SetActive(false);
            createPuzzle.listPieces.Remove(gameObject);
            createPuzzle.listPieces.Add(gameObject);
            createPuzzle.cptObjects = createPuzzle.listPieces.Count;
        } 
    }
}
