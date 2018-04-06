using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLangageLoader  {

    // Use this for initialization
    static void Main(string[] args)
    {
        string language = "francais";
        LangageLoader l = new LangageLoader(language);
        System.Console.WriteLine(Constants.langDirectory + l.Language + ".txt");
        if (l.checkFile())
            System.Console.WriteLine("Fichier Ok ! ");

        Dictionary<string, string> r = l.readFile();
        if (r == null)
        {
            System.Console.WriteLine("Le dictionnaire est null : le fichier de langue utilisé n'existe pas ou est corrompu ! ");
        }
        else
        {
            foreach (string s in r.Keys)
            {
                System.Console.WriteLine("Dictionnaire a l'index : " + s + "traduit par : " + r[s]);
            }
        }

        System.Console.ReadLine();
    }
}
