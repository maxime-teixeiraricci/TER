using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class LoadFile : MonoBehaviour
{
    public Dropdown team;
    public Dropdown unit;
    public GameObject ifPuzzle;
    public GameObject condPuzzle;
    public GameObject actionPuzzle;
    public GameObject messagePuzzle;
    public GameObject antPuzzle;
    public Vector3 positionInitial;
    public Scrollbar scrollEdit;
    public Scrollbar scrollPieces;
    GameObject startPuzzle;
    NewFile clear;
   
    // Lit un fichier .xml, et crée le comportement correspondant
    public void createBehaviorFromXML()
    {
        clear = new NewFile();
        clear.clearEditor();
        string teamName = team.captionText.text;
        string unitName = unit.captionText.text;
        string path = Application.dataPath + "/StreamingAssets/" + Constants.teamsDirectory + Constants.gameModeWarBot;
        Transform editeurTransform = GameObject.Find("Editeur").transform;
        XMLWarbotInterpreter interpreter = new XMLWarbotInterpreter();
        Dictionary<string, List<Instruction>> behavior = interpreter.xmlToBehavior(teamName, path);
        Vector2 delta = new Vector2(0, -1);
        if (behavior.ContainsKey(unitName) && behavior[unitName].Count != 0)
        {
            GameObject.Find("StartPuzzle").GetComponent<ManageDragAndDrop>().UpdateGridPosition();
            GameObject.Find("StartPuzzle").GetComponent<StartPuzzleScript>().UpdateAllValidPuzzles();
            float widthPuzzle = GameObject.Find("StartPuzzle").GetComponent<ManageDragAndDrop>().sizePuzzleX;
            float heightPuzzle = GameObject.Find("StartPuzzle").GetComponent<ManageDragAndDrop>().sizePuzzleY;
            float newX = GameObject.Find("StartPuzzle").GetComponent<ManageDragAndDrop>().posGridX * widthPuzzle;
            float newY = GameObject.Find("StartPuzzle").GetComponent<ManageDragAndDrop>().posGridY * heightPuzzle;
            Vector3 newPos = new Vector3(newX, newY, 10);
            GameObject.Find("StartPuzzle").GetComponent<RectTransform>().localPosition = newPos;
            GameObject currentIf = GameObject.Find("StartPuzzle");
            GameObject currentPercept = GameObject.Find("StartPuzzle");
            GameObject currentAction = GameObject.Find("StartPuzzle");

            foreach (Instruction I in behavior[unitName])
            {
                GameObject _ifPuzzle = Instantiate(ifPuzzle, editeurTransform);
                _ifPuzzle.GetComponent<ManageDragAndDrop>().setGridPosition(currentIf.GetComponent<ManageDragAndDrop>().getGridPosition() + delta);
                _ifPuzzle.GetComponent<ManageDragAndDrop>().UpdateGridPosition();
                GameObject.Find("StartPuzzle").GetComponent<StartPuzzleScript>().UpdateAllValidPuzzles();
                widthPuzzle = _ifPuzzle.GetComponent<ManageDragAndDrop>().sizePuzzleX;
                heightPuzzle = _ifPuzzle.GetComponent<ManageDragAndDrop>().sizePuzzleY;
                newX = _ifPuzzle.GetComponent<ManageDragAndDrop>().posGridX * widthPuzzle;
                newY = _ifPuzzle.GetComponent<ManageDragAndDrop>().posGridY * heightPuzzle;
                newPos = new Vector3(newX, newY, 10);

                _ifPuzzle.GetComponent<RectTransform>().localPosition = newPos;
                currentIf = _ifPuzzle;
                delta = new Vector2(1, 0);
                currentPercept = currentIf;

                foreach (string s in I._listeStringPerceptsVoulus)
                {
                    GameObject _condPuzzle = Instantiate(condPuzzle, editeurTransform);
                    _condPuzzle.GetComponent<ManageDragAndDrop>().setGridPosition(currentPercept.GetComponent<ManageDragAndDrop>().getGridPosition() + delta);
                    currentPercept = _condPuzzle;
                    _condPuzzle.GetComponent<PuzzleScript>()._value = s;
                    if (s.Contains("NOT_"))
                    {
                        _condPuzzle.GetComponent<PuzzleScript>().NegationBoutton();
                    }
                    delta = new Vector2(2, 0);
                }
                delta = new Vector2(1, -1);
                currentAction = currentIf;
                if (I._stringActionsNonTerminales.Length != 0)
                {
                    foreach (MessageStruct s in I._stringActionsNonTerminales)
                    {
                        GameObject _messPuzzle = null;
                        if (s._intitule.Contains("ACTN")) { _messPuzzle = Instantiate(antPuzzle, editeurTransform); }
                        if (s._intitule.Contains("ACTN_MESSAGE_")) { _messPuzzle = Instantiate(messagePuzzle, editeurTransform); }
                        //print(s._intitule); 
                        if (_messPuzzle != null)
                        {
                            _messPuzzle.GetComponent<ManageDragAndDrop>().setGridPosition(currentAction.GetComponent<ManageDragAndDrop>().getGridPosition() + delta);
                            currentAction = _messPuzzle;
                            _messPuzzle.GetComponent<PuzzleScript>()._value = s._intitule;
                            //print("A Dest : " + s._destinataire);
                            if (_messPuzzle.GetComponent<PuzzleScript>().messageDropDown)
                            {
                                _messPuzzle.GetComponent<PuzzleScript>().DropDownUpdate();
                                for (int i = 0; i < _messPuzzle.GetComponent<PuzzleScript>().messageDropDown.options.Count; i++)
                                {
                                   // print("B " + _messPuzzle.GetComponent<PuzzleScript>().messageDropDown.options[i].text + " >< " + s._destinataire);
                                    if (_messPuzzle.GetComponent<PuzzleScript>().messageDropDown.options[i].text == s._destinataire)
                                    {
                                        //print("B Dest : " + s._destinataire);
                                        _messPuzzle.GetComponent<PuzzleScript>().messageDropDown.value = i;
                                        _messPuzzle.GetComponent<PuzzleScript>().messageDropDown.Select();
                                        _messPuzzle.GetComponent<PuzzleScript>().messageDropDown.RefreshShownValue();
                                        break;
                                    }
                                }
                            }
                            delta = new Vector2(2, 0);
                        }
                    }
                }
                else
                {
                    delta = new Vector2(1, -1);
                }
                if (I._stringAction != "")
                {
                    GameObject _actPuzzle = Instantiate(actionPuzzle, editeurTransform);
                    _actPuzzle.GetComponent<ManageDragAndDrop>().setGridPosition(currentAction.GetComponent<ManageDragAndDrop>().getGridPosition() + delta);
                    _actPuzzle.GetComponent<PuzzleScript>()._value = I._stringAction;
                }
                delta = new Vector2(0, -2);
            }
        }
        GameObject.Find("StartPuzzle").GetComponent<StartPuzzleScript>().UpdateAllValidPuzzles();
        ResetScrollBarEditorPosition();
    }
}