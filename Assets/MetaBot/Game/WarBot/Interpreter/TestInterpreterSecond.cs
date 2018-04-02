
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

using System.IO;

//[assembly: AssemblyVersionAttribute("1.0")]
public class TestInterpreterSecond : MonoBehaviour
{
    
    public List<Instruction> _instruction = new List<Instruction>();


    void Start()
    {
        string teamName = "blabla";
        string unitName = "Unit";
        string gameName = "TestBot";


        XMLWarbotInterpreter interpreter = new XMLWarbotInterpreter();
        List<Instruction> behavior = new List<Instruction>();

        // INSTRUCTION DE EAT DES LIGHTS
        string[] percepts1 = new string[] { "PERCEPT_FOOD_INVENTORY", "PERCEPT_LIFE_NOT_MAX" };
        string action1 = "ACTION_EAT";

        string[] percepts2 = new string[] {};
        string action2 = "ACTION_MOVE";


        Instruction i1 = new Instruction(percepts1, action1);
        Instruction i2 = new Instruction(percepts2, action2);
        behavior.Add(i1);
        behavior.Add(i2);

        string _teamPath = Constants.teamsDirectory + gameName + "/" + teamName + "/";

        if (!Directory.Exists(_teamPath))
        {
            //if it doesn't, create it
            Directory.CreateDirectory(_teamPath);

        }

        //check if directory doesn't exit
        if (!Directory.Exists(_teamPath))
        {
            //if it doesn't, create it
            Directory.CreateDirectory(_teamPath);

        }

        interpreter.behaviorToXml(teamName, _teamPath, unitName, behavior);
        print("Ecriture fichier XML termine.");
        System.Console.ReadLine();

        /* */

        List<Instruction> behavior2 = new List<Instruction>();
        behavior2 = interpreter.xmlToUnitBehavior(teamName, _teamPath, unitName);
        _instruction = behavior2;
        
        print("Construction du comportement depuis fichier XML termine.");
        System.Console.ReadLine();

        /* */


    }

    void Update()
    {
        foreach (Instruction i in _instruction)
        {
            print(i);
        }
    }

}
