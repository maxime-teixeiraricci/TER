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
                GameObject puzzleIf = (GameObject)Instantiate(ifPuzzle, GameObject.Find("Editeur").transform);
                puzzleIf.GetComponent<RectTransform>().position = new Vector3(startPuzzle.GetComponent<RectTransform>().position.x, startPuzzle.GetComponent<RectTransform>().position.y - placeIf, startPuzzle.GetComponent<RectTransform>().position.z);
                puzzleIf.GetComponent<ManageDragAndDrop>().posGridX = startPuzzle.GetComponent<ManageDragAndDrop>().posGridX;
                puzzleIf.GetComponent<ManageDragAndDrop>().posGridY = startPuzzle.GetComponent<ManageDragAndDrop>().posGridY - nbrIf;

                placeIf += 150;
                nbrIf += 2;

                int placeCond = 75;
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



