using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;

public class StatsLoader : MonoBehaviour
{

    public Dropdown unit;

    public void Start()
    {
        ReadStats();
    }

    public void ReadStats()
    {
        Text statText = GameObject.Find("PropertiesValues").GetComponentInChildren<Text>();
        Image imageUnit = GameObject.Find("ImageUnit").GetComponent<Image>();
        Image heavySprite = GameObject.Find("HeavySprite").GetComponent<Image>();
        Image baseSprite = GameObject.Find("BaseSprite").GetComponent<Image>();
        Image lightSprite = GameObject.Find("LightSprite").GetComponent<Image>();
        Image explorerSprite = GameObject.Find("ExplorerSprite").GetComponent<Image>();

        string unitName = unit.captionText.text;
        int statBase = 4;
        int statExplo = 5;
        int statHeavy = 6;
        string fileName = "/properties.yml";
        TextReader reader;
        reader = new StreamReader(Application.streamingAssetsPath + fileName);
        string line;
        while (true)
        {
            // lecture de la ligne
            line = reader.ReadLine();

            // si la ligne est vide on arrête
            if (line == null) break;

            // Lecture des statistiques de la base
            else if (unitName == "Base" && line.Contains("WarBase"))
            {
                readStatsFile(unitName, reader, statBase);
                imageUnit.sprite = baseSprite.sprite;
                imageUnit.color = new Color(imageUnit.color.r, imageUnit.color.g, imageUnit.color.b, 255);
                /*
                for(int i = 0; i < statBase; i++)
                {
                    line = reader.ReadLine();
                    // on affiche la ligne
                    //Debug.Log("VALUE LINE = " + line);
                    if (i == 0)
                    {
                        string oldTexte = statText.text;
                        //Debug.Log("VALEUR OLDTEEEEEEEEEEEEXTEEEEEE ====== " + oldTexte);
                        //statText.text.Replace(oldTexte, line);
                        statText.text = line;
                    }
                    else
                    {
                        string oldText = statText.text;
                        string newText = oldText + line;
                        Debug.Log("VALEUR OLDTEEEEEEEEEEEEXT ====== " + oldText);
                        Debug.Log("VALEUR NEEEEEEEEEW ====== " + newText);
                        statText.text = newText;
                    }
                }*/
            }

            // Lecture des statistiques des explorateurs

            else if (unitName == "Explorer" && line.Contains("WarExplorer"))
            {
                readStatsFile(unitName, reader, statExplo);
                imageUnit.sprite = explorerSprite.sprite;
                imageUnit.color = new Color(imageUnit.color.r, imageUnit.color.g, imageUnit.color.b, 255);
            }

            else if (unitName == "Heavy" && line.Contains("WarHeavy"))
            {
                readStatsFile(unitName, reader, statHeavy);
                imageUnit.sprite = heavySprite.sprite;
                imageUnit.color = new Color(imageUnit.color.r, imageUnit.color.g, imageUnit.color.b, 255);
            }

            else if (unitName == "Light")
            {
                Text t0 = GameObject.Find("Text1").GetComponent<Text>();
                t0.text = "No data available";
                Text t1 = GameObject.Find("Text2").GetComponent<Text>();
                t1.text = "No data available";
                Text t2 = GameObject.Find("Text3").GetComponent<Text>();
                t2.text = "No data available";
                Text t3 = GameObject.Find("Text4").GetComponent<Text>();
                t3.text = "No data available";
                Text t4 = GameObject.Find("Text5").GetComponent<Text>();
                t4.text = "No data available";
                Text t5 = GameObject.Find("Text6").GetComponent<Text>();
                t5.text = "No data available";
                imageUnit.sprite = lightSprite.sprite;
                imageUnit.color = new Color(imageUnit.color.r, imageUnit.color.g, imageUnit.color.b, 255);
            }


        }
        reader.Close();
    }


    void readStatsFile(string unitName, TextReader reader, int nbrStats)
    {
        string langage = "";
        string[] lines = File.ReadAllLines(Application.streamingAssetsPath + "/properties.yml");
        foreach (string line1 in lines)
        {
            if (line1.Contains("Language"))
            {
                string[] tmp = line1.Split('=');
                langage = tmp[1];
                break;
            }
        }

        string line;

        for (int i = 0; i < nbrStats; i++)
        {
            line = reader.ReadLine();
            line = line.Replace("   ", "").Replace(" ", "");
            string[] splited = line.Split(':');
            string trad = splited[0];
            if (GameObject.Find("GameManager"))
            {
                GameObject.Find("GameManager").GetComponent<Traducteur>().setTextOriginal(splited[0]);
                trad = GameObject.Find("GameManager").GetComponent<Traducteur>().traduction;
            }
            line = "    " + trad + ": " + splited[1];
            switch (i)
            {
                case 0:
                    Text t0 = GameObject.Find("Text1").GetComponent<Text>();
                    t0.text = line;
                    break;
                case 1:
                    Text t1 = GameObject.Find("Text2").GetComponent<Text>();
                    t1.text = line;
                    break;
                case 2:
                    Text t2 = GameObject.Find("Text3").GetComponent<Text>();
                    t2.text = line;
                    break;
                case 3:
                    Text t3 = GameObject.Find("Text4").GetComponent<Text>();
                    t3.text = line;
                    Text t3_1 = GameObject.Find("Text5").GetComponent<Text>();
                    t3_1.text = "";
                    Text t3_2 = GameObject.Find("Text6").GetComponent<Text>();
                    t3_2.text = "";
                    break;
                case 4:
                    Text t4 = GameObject.Find("Text5").GetComponent<Text>();
                    t4.text = line;
                    break;
                case 5:
                    Text t5 = GameObject.Find("Text6").GetComponent<Text>();
                    t5.text = line;
                    break;
            }
            //statText.text = line;
            /*if (i == 0)
            {
                string oldText = statText.text;
                line = line.Replace("   ", "").Replace(" ","");
                string[] splited = line.Split(':');
                string trad = splited[0];
                if (GameObject.Find("GameManager"))
                {
                    GameObject.Find("GameManager").GetComponent<Traducteur>().setTextOriginal(splited[0]);
                    trad = GameObject.Find("GameManager").GetComponent<Traducteur>().traduction;
                }
                line = "    "+trad + ": " + splited[1];
                statText.text = line;
            }
            else
            {
                string oldText = statText.text;
                line = line.Replace("   ", "").Replace(" ","") ;
                string[] splited = line.Split(':');
                string trad = splited[0];
                if (GameObject.Find("GameManager"))
                {
                    GameObject.Find("GameManager").GetComponent<Traducteur>().setTextOriginal(splited[0]);
                    trad = GameObject.Find("GameManager").GetComponent<Traducteur>().traduction;
                }
                line = "    "+trad + ": " + splited[1];
                string newText = oldText + line;
                statText.text = newText;
                statText.resizeTextForBestFit = true;
            }*/
        }
    }
}