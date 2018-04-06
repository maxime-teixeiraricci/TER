using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

class TestInterpreter 
{
    static void test(string[] args) {
        string teamName = "TestInterpret";
        string unitName = "Light";


        XMLWarbotInterpreter interpreter = new XMLWarbotInterpreter();
        List<Instruction> behavior = new List<Instruction>();

        // INSTRUCTION DE EAT DES LIGHTS
        string[] percepts = new string[] { "PERCEPT_FOOD_INVENTORY", "PERCEPT_LIFE_NOT_MAX" };
        MessageStruct[] messages = new MessageStruct[] { new MessageStruct("MESSAGE_HELP","ALL")};
        string action = "ACTION_EAT";

        string[] percepts2 = new string[] { };
        string action2 = "ACTION_MOVE";

        Instruction i = new Instruction(percepts,messages,action);
        Instruction i2 = new Instruction(action2);
        behavior.Add(i);
        behavior.Add(i2);

        interpreter.behaviorToXml(teamName, Constants.teamsDirectory, unitName, behavior);
        System.Console.WriteLine("Ecriture fichier XML termine.");
        System.Console.ReadLine();

        /* */

        List<Instruction> behavior2 = new List<Instruction>();
        behavior2 = interpreter.xmlToUnitBehavior(teamName, Constants.teamsDirectory,unitName);
        System.Console.WriteLine("count : " + behavior2.Count);
        for (int cpt = 0; cpt < behavior2.Count; cpt++)
        {
            System.Console.WriteLine(behavior2[cpt].getStringAction());
            foreach (string s in behavior2[cpt]._listeStringPerceptsVoulus)
                System.Console.WriteLine(s);
            foreach (MessageStruct s in behavior2[cpt]._stringActionsNonTerminales)
                System.Console.WriteLine(s._intitule + " - > " + s._destinataire);
        }

        System.Console.WriteLine("Construction du comportement depuis fichier XML termine.");
        System.Console.ReadLine();

        /* */


    }


}