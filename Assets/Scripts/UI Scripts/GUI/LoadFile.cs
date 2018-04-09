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
    GameObject startPuzzle;
    NewFile clear;

    // Use this for initialization
    void Start()
    {
        startPuzzle = GameObject.FindGameObjectWithTag("StartPuzzle");
        Updating();
    }

    // Update is called once per frame
    void Update()
    {

    }

    /*
    public void createBehaviorFromXML()
    {

        int placeIf = 75;
        int nbrIf = 1;
        clear = new NewFile();
        clear.clearEditor();        // On clear l'editeur avant de charger un nouveau comportement

        string teamName = team.captionText.text;
        string unitName = unit.captionText.text;
        string path = Constants.teamsDirectory + Constants.gameModeWarBot;
        XMLWarbotInterpreter interpreter = new XMLWarbotInterpreter();

        Dictionary<string, List<Instruction>> behavior = interpreter.xmlToBehavior(teamName, path);

        if (behavior.ContainsKey(unitName))
        {
            List<Instruction> behaviorList = behavior[unitName];



            foreach (Instruction instruction in behaviorList)
            {
                int nbrCond = 1;
                // Création du bloc IF
                GameObject puzzleIf = Instantiate(ifPuzzle, GameObject.Find("Editeur").transform);
                puzzleIf.GetComponent<RectTransform>().anchoredPosition = new Vector3(startPuzzle.GetComponent<RectTransform>().anchoredPosition.x, startPuzzle.GetComponent<RectTransform>().anchoredPosition.y - placeIf, startPuzzle.GetComponent<RectTransform>().anchoredPosition.z);
                puzzleIf.GetComponent<ManageDragAndDrop>().posGridX = startPuzzle.GetComponent<ManageDragAndDrop>().posGridX;
                puzzleIf.GetComponent<ManageDragAndDrop>().posGridY = startPuzzle.GetComponent<ManageDragAndDrop>().posGridY - nbrIf;

                placeIf += (int)puzzleIf.GetComponent<RectTransform>().rect.height;
                nbrIf += 2;

                int placeCond = (int)puzzleCond.GetComponent<RectTransform>().rect.height;
                // Pour chaque percept associé au IF
                for (int i = 0; i < instruction._listeStringPerceptsVoulus.Length; i++)
                {
                    GameObject puzzleCond = (GameObject)Instantiate(condPuzzle, GameObject.Find("Editeur").transform);
                    puzzleCond.GetComponent<RectTransform>().position = new Vector3(puzzleIf.GetComponent<RectTransform>().position.x + placeCond, puzzleIf.GetComponent<RectTransform>().position.y, puzzleIf.GetComponent<RectTransform>().position.z);
                    puzzleCond.GetComponent<ManageDragAndDrop>().posGridX = puzzleIf.GetComponent<ManageDragAndDrop>().posGridX + nbrCond;
                    puzzleCond.GetComponent<ManageDragAndDrop>().posGridY = puzzleIf.GetComponent<ManageDragAndDrop>().posGridY;
                    puzzleCond.GetComponent<CondPuzzleScript>().condName = instruction._listeStringPerceptsVoulus[i];
                    placeCond += 150;
                    nbrCond += 2;
                }

                int placeAction = 75;

                GameObject puzzleAction = (GameObject)Instantiate(actionPuzzle, GameObject.Find("Editeur").transform);
                puzzleAction.GetComponent<RectTransform>().position = new Vector3(puzzleIf.GetComponent<RectTransform>().position.x + placeAction, puzzleIf.GetComponent<RectTransform>().position.y - 75, puzzleIf.GetComponent<RectTransform>().position.z);
                puzzleAction.GetComponent<ManageDragAndDrop>().posGridX = puzzleIf.GetComponent<ManageDragAndDrop>().posGridX + 1;
                puzzleAction.GetComponent<ManageDragAndDrop>().posGridY = puzzleIf.GetComponent<ManageDragAndDrop>().posGridY - 1;
                puzzleAction.GetComponent<ActionPuzzleScript>().actionName = instruction._stringAction;
            }
        }
        else
        {
            print("Aucun comportement existant !");
        }
    }*/

    public void createBehaviorFromXML()
    {
        clear = new NewFile();
        clear.clearEditor();
        string teamName = team.captionText.text;
        string unitName = unit.captionText.text;
        string path = Constants.teamsDirectory + Constants.gameModeWarBot;
        Transform editeurTransform = GameObject.Find("Editeur").transform;
        XMLWarbotInterpreter interpreter = new XMLWarbotInterpreter();

        Dictionary<string, List<Instruction>> behavior = interpreter.xmlToBehavior(teamName, path);
        Vector2 delta = new Vector2(0, -1);
        if (behavior[unitName].Count != 0)
        {
            GameObject currentIf = GameObject.Find("StartPuzzle");
            GameObject currentPercept = GameObject.Find("StartPuzzle");
            GameObject currentAction = GameObject.Find("StartPuzzle");
            foreach (Instruction I in behavior[unitName])
            {
                GameObject _ifPuzzle = Instantiate(ifPuzzle, editeurTransform);
                _ifPuzzle.GetComponent<ManageDragAndDrop>().setGridPosition(currentIf.GetComponent<ManageDragAndDrop>().getGridPosition() + delta);
                currentIf = _ifPuzzle;

                // INSTRUCTION : public string[] _listeStringPerceptsVoulus; MessageStruct[] _stringActionsNonTerminales; public string _stringAction;
                delta = new Vector2(1, 0);
                currentPercept = currentIf;


                foreach (string s in I._listeStringPerceptsVoulus)
                {
                    GameObject _condPuzzle = Instantiate(condPuzzle, editeurTransform);
                    

                    _condPuzzle.GetComponent<ManageDragAndDrop>().setGridPosition(currentPercept.GetComponent<ManageDragAndDrop>().getGridPosition() + delta);
                    currentPercept = _condPuzzle;
                    _condPuzzle.GetComponent<CondPuzzleScript>()._value = s;
                    if (s.Contains("NOT_"))
                    {
                        _condPuzzle.GetComponent<CondPuzzleScript>().NegationBoutton();
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

                        if (s._intitule.Contains("ACTN"))
                        {
                            _messPuzzle = Instantiate(antPuzzle, editeurTransform);
                        }
                        if (s._intitule.Contains("ACTN_MESSAGE_"))
                        {
                            _messPuzzle = Instantiate(messagePuzzle, editeurTransform);
                        }
                        print(s._intitule); 
                        if (_messPuzzle != null)
                        {
                            _messPuzzle.GetComponent<ManageDragAndDrop>().setGridPosition(currentAction.GetComponent<ManageDragAndDrop>().getGridPosition() + delta);
                            currentAction = _messPuzzle;
                            _messPuzzle.GetComponent<CondPuzzleScript>()._value = s._intitule;
                            /*
                            _messPuzzle.GetComponent<CondPuzzleScript>().messageDropDown.value
                            */
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
                    _actPuzzle.GetComponent<CondPuzzleScript>()._value = I._stringAction;
                }
                delta = new Vector2(0, -2);
            }
        }
    }


    public void Updating()
    {
        string gamePath = "./teams/TestBot/";
        List<string> teams = new List<string>();
        string[] fileEntries = Directory.GetFiles(gamePath);
        foreach (string s in fileEntries)
        {
            print(s);
            string team = s.Replace(gamePath,"");
            if (team.Contains(".wbt"))
            {
                team = team.Replace(".wbt", "");
                teams.Add(team);
            }
        }
        team.ClearOptions();
        team.AddOptions(teams);

    }

 
}



