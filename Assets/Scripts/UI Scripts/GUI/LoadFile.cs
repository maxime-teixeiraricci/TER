using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class LoadFile : MonoBehaviour
{

    public Dropdown team;
    public Dropdown unit;
    createPuzzle creation;
    public GameObject ifPuzzle;
    public GameObject condPuzzle;
    public GameObject actionPuzzle;
    public Vector3 positionInitial;
    GameObject startPuzzle;
    ManageDragAndDrop manager;
    NewFile clear;

    // Use this for initialization
    void Start()
    {
        startPuzzle = GameObject.FindGameObjectWithTag("StartPuzzle");
        manager = GetComponent<ManageDragAndDrop>();
        //clear = GetComponent<NewFile>();

    }

    // Update is called once per frame
    void Update()
    {

    }
    
    
    public void createBehaviorFromXML()
    {

        int placeIf = 75;
        int nbrIf = 1;
        clear = new NewFile();
        clear.clearEditor();        // On clear l'editeur avant de charger un nouveau comportement
        /*
        string patternPercepts = "percept.*";             // Regular expression for the percepts
        Regex rgxPercept = new Regex(patternPercepts, RegexOptions.IgnoreCase);

        string patternActions = "action.*";             // Regular expression for the actions
        Regex rgxAction = new Regex(patternActions, RegexOptions.IgnoreCase);
        */

        string teamName = team.captionText.text;
        string unitName = unit.captionText.text;
        string path = Constants.teamsDirectory + Constants.gameModeWarBot;
        XMLWarbotInterpreter interpreter = new XMLWarbotInterpreter();

        Dictionary<string, List<Instruction>> behavior = interpreter.xmlToBehavior(teamName, path);

        List<string> list_conditions = new List<string>();
        List<string> list_actions = new List<string>();
        List<Instruction> behaviorList = behavior[unitName];


        foreach(Instruction instruction in behaviorList)
        {
            int nbrCond = 1;
            // Création du bloc IF
            GameObject puzzleIf = (GameObject)Instantiate(ifPuzzle, GameObject.Find("Editeur").transform);
            //Debug.Log("PosX Start = " + startPuzzle.GetComponent<ManageDragAndDrop>().posGridX);
            //Debug.Log("PosY Start = " + startPuzzle.GetComponent<ManageDragAndDrop>().posGridY);

            puzzleIf.GetComponent<RectTransform>().position = new Vector3(startPuzzle.GetComponent<RectTransform>().position.x, startPuzzle.GetComponent<RectTransform>().position.y - placeIf, startPuzzle.GetComponent<RectTransform>().position.z);
            puzzleIf.GetComponent<ManageDragAndDrop>().posGridX = startPuzzle.GetComponent<ManageDragAndDrop>().posGridX;
            puzzleIf.GetComponent<ManageDragAndDrop>().posGridY = startPuzzle.GetComponent<ManageDragAndDrop>().posGridY - nbrIf;
            //puzzleIf.GetComponent<ManageDragAndDrop>().posGridY = startPuzzle.GetComponent<ManageDragAndDrop>().posGridY + nbrIf;
            //puzzleIf.GetComponent<RectTransform>().anchoredPosition = positionInitial;
            placeIf += 150;
            nbrIf += 2;

            int placeCond = 75;
            // Pour chaque percept associé au IF
            for (int i = 0; i < instruction._listeStringPerceptsVoulus.Length; i++)
            {
                
                // On vérifie qu'il s'agisse bien d'un percept
                //Match matchPercepts = rgxPercept.Match(instruction._listeStringPerceptsVoulus[i]);
                //Debug.Log("result test = " + matchPercepts.Success);
                //if (matchPercepts.Success)              // Test if the name of the string is a percept
               // {
                GameObject puzzleCond = (GameObject)Instantiate(condPuzzle, GameObject.Find("Editeur").transform);
                puzzleCond.GetComponent<RectTransform>().position = new Vector3(puzzleIf.GetComponent<RectTransform>().position.x + placeCond, puzzleIf.GetComponent<RectTransform>().position.y, puzzleIf.GetComponent<RectTransform>().position.z);
                puzzleCond.GetComponent<ManageDragAndDrop>().posGridX = puzzleIf.GetComponent<ManageDragAndDrop>().posGridX + nbrCond;
                puzzleCond.GetComponent<ManageDragAndDrop>().posGridY = puzzleIf.GetComponent<ManageDragAndDrop>().posGridY;
                puzzleCond.GetComponent<CondPuzzleScript>().condName = instruction._listeStringPerceptsVoulus[i];
                placeCond += 150;
                nbrCond += 2;
               // }
                //Debug.Log("liste des percepts : " + instruction._listeStringPerceptsVoulus[i]);
            }

            // Match matchActions = rgxAction.Match(instruction._stringAction);
            //if (matchActions.Success)       // Test if the name of the string is an action
            //{
            int placeAction = 75;

            GameObject puzzleAction = (GameObject)Instantiate(actionPuzzle, GameObject.Find("Editeur").transform);
            puzzleAction.GetComponent<RectTransform>().position = new Vector3(puzzleIf.GetComponent<RectTransform>().position.x + placeAction, puzzleIf.GetComponent<RectTransform>().position.y - 75, puzzleIf.GetComponent<RectTransform>().position.z);
            puzzleAction.GetComponent<ManageDragAndDrop>().posGridX = puzzleIf.GetComponent<ManageDragAndDrop>().posGridX + 1;
            puzzleAction.GetComponent<ManageDragAndDrop>().posGridY = puzzleIf.GetComponent<ManageDragAndDrop>().posGridY - 1;
            puzzleAction.GetComponent<ActionPuzzleScript>().actionName = instruction._stringAction;
            //placeAction -= 150;
            //}
            //Debug.Log("Liste d'instru = " + instruction);
            //creation.create();
            
        }
    }
    
}



