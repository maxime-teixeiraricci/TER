using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traducteur{
    public string langue;
    public string textOriginal;
    public string traduction;

    public Traducteur(string l,string original)
    {
        langue = l;
        textOriginal = original;
        traduction = textOriginal;
    }

    public string Traduction()
    {
        string result;
        LangageLoaderPuzzle llp = new LangageLoaderPuzzle(langue);
        foreach(Langage l in llp.langues)
        {
            if (l.langue == langue)
            {
                foreach(Traduction t in l.trads)
                {
                    if (textOriginal.Equals(t.cle))
                    {
                        traduction = t.valeur;
                        break;
                    }
                }
            }
        }

        result = traduction;
        return result;
    }
}
