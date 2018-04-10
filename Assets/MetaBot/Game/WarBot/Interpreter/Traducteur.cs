using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traducteur : MonoBehaviour {
    public string langue;
    public string textOriginal;
    public string traduction;

    public void Traduction()
    {
        LangageLoader ll = GameObject.Find("GameManager").GetComponent<LangageLoader>();
        foreach(Langage l in ll.langues)
        {
            if (l.langue == langue)
            {
                foreach(Traduction t in l.trads)
                {
                    if (textOriginal == t.cle)
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
        
    }
	
	// Update is called once per frame
	void Update () {
        Traduction();
    }
}
