using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LangageLoader {
    public string Language;
    public string path;

	public LangageLoader(string l){
        Language = l;
        path = Constants.langDirectory;
	}

    public bool checkFile()
    {
        bool flag = false;

        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);

        foreach (string file in Directory.GetFiles(path))
        {
            System.Console.WriteLine(file);
            if (file.Equals(Language + ".txt"))
            {
                flag = true;
                break;
            }
        }

        return flag;
    }

    public Dictionary<string,string> readFile()
    {
        Dictionary<string, string> result = new Dictionary<string, string>();

        if (!checkFile())
            return null;

        string[] lines = System.IO.File.ReadAllLines(path + Language + ".txt");
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
