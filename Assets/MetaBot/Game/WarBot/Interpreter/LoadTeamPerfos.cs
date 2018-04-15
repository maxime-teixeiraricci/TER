using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class LoadTeamPerfos : MonoBehaviour
{
    public Dropdown d;
    // Use this for initialization
    void Start()
    {
        loadDropdown();
    }

    public void loadDropdown()
    {
        d.ClearOptions();
        List<string> customNames = new List<string>();
        foreach (string file in Directory.GetFiles(Application.streamingAssetsPath + "/Stats/"))
        {
            if (file.Contains(".stat") && !file.Contains(".meta"))
            {
                string[] tmp = file.Split('/');
                customNames.Add(tmp[tmp.Length-1].Replace(".stat", "").Replace(" ", "_"));
            }
        }

        d.AddOptions(customNames);

    }
} 
