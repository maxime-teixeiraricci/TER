using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

[assembly: AssemblyVersionAttribute("1.0")]
namespace UnityTER.Interpreter
{ 
    class TestInterpreter 
    {
        static void Main(string[] args) {
           string teamName = "TestInterpret";
           string unitName = "Light";


           XMLWarbotInterpreter interpreter = new XMLWarbotInterpreter();
           List<Instruction> behavior = new List<Instruction>();

            // INSTRUCTION DE EAT DES LIGHTS
            string[] percepts = new string[] { "PERCEPT_FOOD_INVENTORY", "PERCEPT_LIFE_NOT_MAX" };
            string action = "ACTION_EAT";


            Instruction i = new Instruction(percepts, action);
            behavior.Add(i);

            interpreter.behaviorToXml(teamName, Constants.teamsDirectory, unitName, behavior);
            System.Console.WriteLine("Ecriture fichier XML termine.");
            System.Console.ReadLine();

            /* */

            List<Instruction> behavior2 = new List<Instruction>();
            behavior2 = interpreter.xmlToUnitBehavior(teamName, Interpreter.Constants.teamsDirectory,unitName);
            System.Console.WriteLine("count : " + behavior2.Count);
            for (int cpt = 0; cpt < behavior2.Count; cpt++)
            {
                System.Console.WriteLine(behavior2[cpt].getStringAction());
                foreach (string s in behavior2[cpt]._listeStringPerceptsVoulus)
                    System.Console.WriteLine(s);
            }

            System.Console.WriteLine("Construction du comportement depuis fichier XML termine.");
            System.Console.ReadLine();

            /* */


        }


    }


}
