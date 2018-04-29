using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChangeLanguage : MonoBehaviour {


    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {

    }



    public void ChangementLangue(string newLangage)
    {
        GameObject.Find("GameManager").GetComponent<LangageLoader>().changeLanguage(newLangage);
        GameObject.Find("GameManager").GetComponent<Traducteur>().langue = newLangage;
        

        string[] lines = System.IO.File.ReadAllLines(Application.streamingAssetsPath+"/properties.yml");
        int cpt = 0;
        foreach (string line in lines)
        {
            if (line.Contains("Language"))
            {
                string[] tmp = line.Split('=');
                tmp[1] = newLangage;
                lines[cpt] = tmp[0] + "=" + tmp[1];
                break;
            }
            cpt++;
        }
        System.IO.File.WriteAllLines(Application.streamingAssetsPath+"/properties.yml", lines);

    }

}
