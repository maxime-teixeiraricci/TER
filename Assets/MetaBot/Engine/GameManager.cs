using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Text;


public class GameManager : MonoBehaviour
{
    [Header("Game Settings")]
    public string _gameName;
    public string gamepath;
    public int _minNumberOfTeam;
    public int _maxNumberOfTeam;

    [Header("Units")]
    public List<GameObject> _listUnitGameObject;

    [Header("Debug")]
    public List<string> fileUnit = new List<string>();

    static bool created = false;

    void Awake()
    {
        if (!created)
        {
            DontDestroyOnLoad(this.gameObject);
            created = true;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void Start()
    {
        gamepath = "./teams/" + _gameName + "/";
        if (!Directory.Exists(gamepath))
        {
            //if it doesn't, create it
            Directory.CreateDirectory(gamepath);

        }
    } 






    public void SaveGameFile()
    {
        string path = "Assets/MetaBot/Game/WarBot/" + _gameName + ".gameset";

        //Write some text to the test.txt file
        StreamWriter writer = new StreamWriter(path, false);



        foreach (GameObject unit in _listUnitGameObject)
        {
            writer.WriteLine("<");
            writer.WriteLine(unit.GetComponent<Stats>()._unitType);

            // Recuperer les percepts
            Percept unitPercepts = unit.GetComponent<Percept>();
            unitPercepts.InitPercept();
            foreach (string s in unitPercepts._percepts.Keys)
            {
                if (s.Contains("PERCEPT"))
                {
                    writer.WriteLine("[PERCEPTS]" + s);
                }
                if(s.Contains("MESSAGE"))
                {
                    writer.WriteLine("[MESSAGE]ACTN_" + s.Replace("PERCEPT_", ""));
                }
            }

            // Recuperer les actions
            Action unitAction = unit.GetComponent<Action>();
            unitAction.InitAction();
            foreach (string s in unitAction._actions.Keys) {  writer.WriteLine("[ACTIONS]"+s); }

            // Recuperer les actions
            ActionNonTerminal unitActionNonTerminal = unit.GetComponent<ActionNonTerminal>();
            unitActionNonTerminal.InitActionNonTerminal();
            foreach (string s in unitActionNonTerminal._actionsNT.Keys)
            {
                if (!s.Contains("ACTN_MESSAGE_")) writer.WriteLine("[ANT]"+s);
            }
            
            writer.WriteLine(">");
        }

        writer.Close();

        print("Done !");
    }

}
