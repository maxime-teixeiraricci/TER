using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class LangageLoader : MonoBehaviour {
    public List<Langage> langues;//langues avec leur traduction
    public GameObject conteneur;//objet auquel est rattaché le script
    public string language;//langue actuelle

    //Verifie si le fichier existe
    public bool checkFile(string Language)
    {
        if (!Directory.Exists(Application.streamingAssetsPath + Constants.langDirectory))
        {
            Directory.CreateDirectory(Application.streamingAssetsPath + Constants.langDirectory);
        }

        foreach (string file in Directory.GetFiles(Application.dataPath + "/StreamingAssets/"+Constants.langDirectory))
        {
            if (file.Contains(Language))
            {
                return true;
            }
        }
        return false;
    }

    //applique la traduction a l'objet rattaché
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

    //permet de changer la langue actuelle du programme
    public void changeLanguage(string s)
    {
        language = s;
        langues.Clear();
        langues.Add(readFile(s));
    }

    //recupere les traductions de la langue actuelle
    public Langage readFile(string Language)
    {
        Langage l_lang = new Langage();
        l_lang.langue = Language;
        l_lang.trads = new List<Traduction>();

        if (!checkFile(Language))
            return new Langage();
            
        string[] lines = System.IO.File.ReadAllLines(Application.streamingAssetsPath + Constants.langDirectory + Language + ".txt");

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
        string[] lines = System.IO.File.ReadAllLines(Application.streamingAssetsPath+"/properties.yml");
        foreach (string line in lines)
        {
            if (line.Contains("Language"))
            {
                string[] tmp = line.Split('=');
                language = tmp[1];
                break;
            }
        }

        langues = new List<Langage>();
        langues.Add(readFile(language));
        if (conteneur != null)
            applyTradToscene(language);
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

