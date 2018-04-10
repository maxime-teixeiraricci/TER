using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class LangageLoader : MonoBehaviour {
    public List<Langage> langues;
    public GameObject conteneur;

    public bool checkFile(string Language)
    {
        if (!Directory.Exists(Constants.langDirectory))
        {
            Directory.CreateDirectory(Constants.langDirectory);
           print("Dossier existe pas");
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

    public void applyTradToscene(string Language)
    {
            Langage l = langues.Find(i => i.langue.Equals(Language));
            Traduction t = l.trads.Find(k => k.cle.Equals(conteneur.GetComponent<Text>().text));
            if (t.cle != null)
            {
                print(t.cle + " traduit par " + t.valeur);
                conteneur.gameObject.GetComponent<Text>().text = t.valeur;
            }
    }

    public Langage readFile(string Language)
    {
        Langage l_lang = new Langage();
        l_lang.langue = Language;
        l_lang.trads = new List<Traduction>();

        if (!checkFile(Language))
            return new Langage();
            
        string[] lines = System.IO.File.ReadAllLines(Constants.langDirectory + Language + ".txt");

        //Debug.Log(Constants.langDirectory + Language + ".txt");

        foreach (string line in lines)
        {
            if (!(line.Contains("#")))
            {
                string [] tmp = line.Split('=');
                Traduction l_tmp = new Traduction();
                l_tmp.cle = tmp[0].Replace("\t","") ;
                l_tmp.valeur = tmp[1];
                l_lang.trads.Add(l_tmp);
            }
        }

        return l_lang;
    }

    void Start()
    {
        langues = new List<Langage>();
        langues.Add(readFile("english"));
        applyTradToscene("english");
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

