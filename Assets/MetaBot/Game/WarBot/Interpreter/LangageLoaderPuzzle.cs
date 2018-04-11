using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class LangageLoaderPuzzle
{
    public List<Langage> langues;
    string language;

    public LangageLoaderPuzzle(string lang)
    {
        language = lang;
        langues = new List<Langage>();
        langues.Add(readFile(language));
    }

    public bool checkFile(string Language)
    {
        if (!Directory.Exists(Constants.langDirectory))
        {
            Directory.CreateDirectory(Constants.langDirectory);
        }

        foreach (string file in Directory.GetFiles(Constants.langDirectory))
        {
            //  print("File : " + file);
            if (file.Contains(Language))
            {
                return true;
            }
        }
        return false;
    }


    public Langage readFile(string Language)
    {
        Langage l_lang = new Langage();
        l_lang.langue = Language;
        l_lang.trads = new List<Traduction>();


        string[] lines = System.IO.File.ReadAllLines(Constants.langDirectory + Language + ".txt");

        //Debug.Log(Constants.langDirectory + Language + ".txt");

        foreach (string line in lines)
        {
            if (!(line.Contains("#")))
            {
                string[] tmp = line.Split('=');
                Traduction l_tmp = new Traduction();
                l_tmp.cle = tmp[0].Replace("\t", "");
                l_tmp.valeur = tmp[1];
                l_lang.trads.Add(l_tmp);
            }
        }

        return l_lang;
    }

}
