using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Traducteur : MonoBehaviour {
    public string langue;//langue actuelle
    public string textOriginal;//texte a traduire
    public string traduction;//traduction recuperee
    public GameObject component;//Objet a traduire

    private GameObject gameManager;

    public void setTextOriginal(string s)
    {
        textOriginal = s;
        Traduction();
    }

    //Recupere la nouvelle traduction
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
        gameManager = GameObject.Find("GameManager");
        if (!gameManager.GetComponent<LangageLoader>()) return;

            langue = gameManager.GetComponent<LangageLoader>().language;
            if (component)
                textOriginal = component.gameObject.GetComponent<Text>().text;
            Traduction();
        if (component)
            component.gameObject.GetComponent<Text>().text = traduction;
    }
	
	// Update is called once per frame
	void Update () {
        //Si la langue est changée en runtime, mettre a jour le texte
            if (gameManager.GetComponent<LangageLoader>().language != langue)
            {
            langue = gameManager.GetComponent<LangageLoader>().language;
            Traduction();
            if (component)
                component.gameObject.GetComponent<Text>().text = traduction;
            }
	}
}
