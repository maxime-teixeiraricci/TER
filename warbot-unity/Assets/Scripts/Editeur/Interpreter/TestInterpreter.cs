using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using WarBotEngine.Editeur;

[assembly: AssemblyVersionAttribute("1.0")]
namespace Assets.Scripts.Editeur.Interpreter
{
    class TestInterpreter : MonoBehaviour
    {
        static void Main(string[] args) {
            string teamName = "DoudouLaMalice";
            string unitName = "WarExplorer";
            XMLWarbotInterpreter interpreter = new XMLWarbotInterpreter();

               List<Instruction> behavior = new List<Instruction>();
               // Ecriture d'un fichier xml

               Condition bag = new Condition("Empty",true);
               Condition near = new Condition("NearEnemies", false);
               Action idle = new Action("Idle");
               Action walk = new Action("Walk");

            Condition testOr1 = new Condition("Reloaded", false);
            Condition testOr2 = new Condition("NearAllieeBases", false);

            Or o = new Or(null,false);
            o.Add(testOr1);
            o.Add(testOr2);


            If i = new If();
               i.addCondition(bag);
               i.addCondition(o);
               i.addAction(idle);
               i.addElseActions(walk);

               Task t = new Task();
               t.addCondition(bag);
               t.addAction(walk);
           
               //behavior.Add(i);
               behavior.Add(t);
               behavior.Add(new Action("HalfTurn"));

             // Partie 1 , ecriture du fichier

               
               interpreter.behaviorToXml(teamName, Constants.teamsDirectory, unitName, behavior);
              // System.Console.WriteLine("fini");
               /* */

               
           // partie 2 , lecture du fichier

            
            Dictionary<string, List<Instruction>> behavior2 = new Dictionary<string, List<Instruction>>();
            behavior2 = interpreter.xmlToBehavior(teamName, Constants.teamsDirectory);
            System.Console.WriteLine(behavior2.Count);
            for (int cpt = 0; cpt < behavior2["WarExplorer"].Count; cpt++)
            {
                System.Console.WriteLine(behavior2["WarExplorer"][cpt].ToString());
            }
            /* */

            System.Console.WriteLine("fini");
            System.Console.ReadLine();
        }

        void Update()
        {

        }

    }


}
