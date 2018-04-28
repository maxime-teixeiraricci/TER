using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Traducteur : MonoBehaviour {
    string basictext;
    public string langue;
    public string textOriginal;
    public string traduction;
    public GameObject component;

    public void setTextOriginal(string s)
    {
        textOriginal = s;
        Traduction();
    }

    public void applyTradToscene()
    {
        component.gameObject.GetComponent<Text>().text = traduction;
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
        textOriginal = component.gameObject.GetComponent<Text>().text;
        basictext = textOriginal;
        Traduction();
        applyTradToscene();
	}
	
	// Update is called once per frame
	void Update () {
        if (component.gameObject.GetComponent<Text>().text != basictext)
        {
            basictext = component.gameObject.GetComponent<Text>().text;
            textOriginal = component.gameObject.GetComponent<Text>().text;
            Traduction();
            applyTradToscene();
        }
	}
}
