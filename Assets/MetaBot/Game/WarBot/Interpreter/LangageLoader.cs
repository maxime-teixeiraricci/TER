using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LangageLoader {
    public string Language;


	public LangageLoader(string l){
        Language = l;
	}

    public bool checkFile()
    {
        bool flag = false;

        if (!Directory.Exists(Constants.langDirectory))
        {
            Directory.CreateDirectory(Constants.langDirectory);
            System.Console.WriteLine("Dossier existe pas");
        }

        foreach (string file in Directory.GetFiles(Constants.langDirectory))
        {
            System.Console.WriteLine("File : " + file);
            if (file.Contains(Language))
            {
                flag = true;
                break;
            }
        }
        System.Console.WriteLine(" flag = "+flag);
        return flag;
    }

    public Dictionary<string,string> readFile()
    {
        Dictionary<string, string> result = new Dictionary<string, string>();

        if (!checkFile())
            return null;

        string[] lines = System.IO.File.ReadAllLines(Constants.langDirectory + Language + ".txt");
        foreach (string line in lines)
        {
            if (!(line[0] == '#'))
            {
                string [] tmp = line.Split(':');
                result.Add(tmp[0], tmp[1]);
            }
        }

        return result;
    }
}
