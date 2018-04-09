using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LangageLoader : MonoBehaviour {
    public List<Langage> langues;


    public bool checkFile(string Language)
    {
        if (!Directory.Exists(Constants.langDirectory))
        {
            Directory.CreateDirectory(Constants.langDirectory);
           print("Dossier existe pas");
        }

        foreach (string file in Directory.GetFiles(Constants.langDirectory))
        {
           print("File : " + file);
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
        Dictionary<string, string> result = new Dictionary<string, string>();

        if (!checkFile(Language))
            return new Langage();

        string[] lines = System.IO.File.ReadAllLines(Constants.langDirectory + Language + ".txt");
        foreach (string line in lines)
        {
            if (!(line.Contains("#")))
            {
                string [] tmp = line.Split(':');
                Traduction l_tmp = new Traduction();

                l_tmp.cle = tmp[0].Replace("\t","") ;
                l_lang.trads.Add(l_tmp);
            }
        }

        return l_lang;
    }

    void Start()
    {
        langues = new List<Langage>();
        langues.Add(readFile("francais"));
    }

}
[System.Serializable]
public struct Langage
{
    public string langue;
    public List<Traduction> trads;
}

[System.Serializable]
public struct Traduction
{
    public string cle;
    public string valeur;
}

