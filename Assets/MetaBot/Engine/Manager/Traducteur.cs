using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traducteur : MonoBehaviour {

    public string langue;
    public string textOriginal;
    public string traduction;

    public void setTextOriginal(string s)
    {
        textOriginal = s;
        Traduction();
    }

    public void Traduction()
    {
        foreach (Langage l in GameObject.Find("GameManager").GetComponent<LangageLoader>().langues)
        {
            if (l.langue.Equals(langue))
            {
                foreach (Traduction t in l.trads)
                {
                    if (textOriginal.Equals(t.cle))
                    {
                        traduction = t.valeur;
                        return;
                    }
                }
            }
        }

        traduction = textOriginal;
    }

    // Use this for initialization
    void Start () {
        langue = GameObject.Find("GameManager").GetComponent<LangageLoader>().language;
        Traduction();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
