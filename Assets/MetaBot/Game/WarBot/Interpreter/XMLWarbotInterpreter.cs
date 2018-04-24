using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using System.Xml;
using System.IO;



public class XMLWarbotInterpreter : XMLInterpreter
{
    public XMLWarbotInterpreter()
    {
    }

    /**
        * Genere un fichier XML contenant uniquement le nom de l'equipe
        */
    public override void generateEmptyFile(string teamName, string path)
    {
        // Necessaire pour le fonctionnement des Nodes
        XmlDocument l_doc = new XmlDocument();

        // Creation du noeud de base
        XmlNode l_root = l_doc.CreateElement(Constants.nodeContainer);
        l_doc.AppendChild(l_root);

        // Rajout du nom de l'equipe dans le fichier vide
        XmlNode l_node = l_doc.CreateElement(Constants.nodeTeam);
        l_node.InnerText = teamName;
        l_root.AppendChild(l_node);

        //sauvegarde du fichier xml
        l_doc.Save(path + "/" + teamName + Constants.xmlExtension);
    }

    /**
        * Retourne une instruction correspondants au noeud actuellement traité
        * Une instruction est une liste de condition, une liste d'action non terminale/messages , ainsi que l'action temrinale
        */
    public override Instruction whichInstruction(string unitName, XmlNode ins)
    {
        //if (ins.Name.Equals(typeof(Task).Name))
        //{//cas d'un If"
        List<string> l_conditions = new List<string>();
        List<MessageStruct> l_MsgStruct = new List<MessageStruct>();
        List<string> l_actions = new List<string>();

        foreach (XmlNode x in ins.ChildNodes)
        {

            if (x.Name.Contains("parameters"))
            {
                foreach (XmlNode c in x)
                    l_conditions.Add(c.Name);
            }

            if (x.Name.Contains("message"))
            {
                foreach (XmlNode c in x)
                {
                    l_MsgStruct.Add(new MessageStruct(c.Name, c.FirstChild.Name));
                   // Debug.Log("c" + c.Name + " " + c.FirstChild.Name);
                }
            }

            if (x.Name.Contains("actions"))
            {
                foreach (XmlNode c in x)
                    l_actions.Add(c.Name);
            }
        }
        Instruction t;

        /*
        if (l_conditions.Count > 0)
        {
            string[] cond = new string[l_conditions.Count];
            for (int i = 0; i < l_conditions.Count; i++)
            {
                cond[i] = l_conditions[i];
            }

            if (l_MsgStruct.Count > 0)
            {
                MessageStruct[] msgstruct = new MessageStruct[l_MsgStruct.Count];
                for (int i = 0; i < l_MsgStruct.Count; i++)
                {
                    msgstruct[i] = l_MsgStruct[i];
                }

                t = new Instruction(cond, msgstruct, l_actions[0]);
                Debug.Log("A1");
            }
            else
            {
                t = new Instruction(cond, l_actions[0]);
                Debug.Log("A2");
            }
        }
        else
        {
            t = new Instruction(l_actions[0]);
            Debug.Log("A3");
        }

        Debug.Log(" T : " + t.ToString());
        return t;*/
        if (l_actions.Count == 0)
        {
            return new Instruction(l_conditions.ToArray(), l_MsgStruct.ToArray());
        }
        return new Instruction(l_conditions.ToArray(), l_MsgStruct.ToArray(), l_actions[0]);

    }


    /**
        * Renvoie le nom du noeud correspondant a l'equipe recherché , un string vide sinon
        */
    public override string whichFileName(string teamName, string path)
    {
        string l_fileName = "";
        foreach (string file in Directory.GetFiles(path))
        {
            if (file.Contains(Constants.xmlExtension) && !file.Contains(".meta"))
            {
                using (var stream = new FileStream(file, FileMode.Open))
                {
                    if (stream.CanRead)
                    {
                        Load(stream);
                        XmlNode team = SelectSingleNode("//" + Constants.nodeTeam);
                        if (team.InnerText != null && team.InnerText.Equals(teamName))
                            l_fileName = file;

                    }
                }

                if (!l_fileName.Equals("")) // if we found the right file
                    break; // stop looking for other files
            }
        }

        return l_fileName;
    }

    /**
        * Vas chercher un fichier XML et remplit un comportement 
        **/
    public override Dictionary<string, List<Instruction>> xmlToBehavior(string teamName, string path)
    {
        // Try to find an already existing file with this team name
        string l_fileName = whichFileName(teamName, path);

        // If no file has been found, create a new one with the given team name
        if (l_fileName.Equals(""))
            l_fileName = teamName.Replace(".wbt", "") + Constants.xmlExtension;

        Dictionary<string, List<Instruction>> behavior = new Dictionary<string, List<Instruction>>();

        string pathtmp = path;
        l_fileName = path +"/" +  l_fileName.Replace(path, "");
        using (var stream = new FileStream(l_fileName, FileMode.Open))
        {
            Load(stream);
            XmlNodeList l_units = GetElementsByTagName(Constants.nodeUnit);
            stream.Close();

            for (int i = 0; i < l_units.Count; i++)
            {
                string l_unitName = l_units.Item(i).Attributes.Item(0).Value;
                behavior.Add(l_unitName, xmlToUnitBehavior(teamName, path, l_unitName));
            }
        }

        return behavior;
    }

    /**
        * -> Moteur
        * Sous fonction qui va chercher le comportement d'une unite
        **/
    public List<Instruction> xmlToUnitBehavior(string teamName, string path, string unitName)
    {
        // Try to find an already existing file with this team name
        string l_fileName = whichFileName(teamName, path);

        // If no file has been found, create a new one with the given team name
        if (l_fileName.Equals(""))
            l_fileName = teamName;

        List<Instruction> l_behavior = new List<Instruction>();
        l_fileName = teamName.Replace(".wbt", "") + Constants.xmlExtension;
        l_fileName = path + "/" + l_fileName.Replace(path, "");

        using (FileStream stream = new FileStream(l_fileName, FileMode.Open))
        {
            Load(stream);
            // select the node containing the unit name
            XmlNode l_unitBehavior = SelectSingleNode("//" + Constants.nodeUnit + "[@" + Constants.attributeName + "='" + unitName + "']");
            if (l_unitBehavior != null && l_unitBehavior.HasChildNodes)
            {
                foreach (XmlNode ins in l_unitBehavior.ChildNodes)
                {
                    System.Console.WriteLine(ins.Name);
                    l_behavior.Add(whichInstruction(unitName, ins));
                    //System.Console.WriteLine(l_behavior[l_behavior.Count - 1].ToString());
                }

            }

            stream.Close();
        }
        return l_behavior;
    }

    /**
        * Vas creer un fichier XML correspondant au comportement passé en parametre
        **/
    public override void behaviorToXml(string teamName, string path, string unitName, List<Instruction> behavior)
    {
        // Try to find an already existing file with this team name
        System.Console.WriteLine(path);
        string l_fileName = whichFileName(teamName, path);
        // If no file has been found, create a new one with the given team name
        if (l_fileName.Equals(""))
        {
            l_fileName = teamName + Constants.xmlExtension;
            generateEmptyFile(teamName, path);
        }

        l_fileName = whichFileName(teamName, path);
        // Load the file
        //Load(path + "/" + fileName);
        Load(l_fileName);

        // Get all nodes named "unit" if null , create it
        XmlNode l_node = SelectSingleNode("//" + Constants.nodeUnit + "[@" + Constants.attributeName + "='" + unitName + "']");
        if (l_node == null)
            l_node = CreateElement(Constants.nodeUnit);

        //erase the old behavior to add the new one
        l_node.RemoveAll();

        XmlAttribute l_name = CreateAttribute(Constants.attributeName);
        l_name.Value = unitName;
        l_node.Attributes.Append(l_name);

        foreach (Instruction i in behavior)
            l_node.AppendChild(ImportNode(i.xmlStructure(), true));

        //Debug.Log(node.OuterXml);
        XmlNode l_embbed = FirstChild;
        l_embbed.AppendChild(l_node);

        //Save(path + "/" + fileName);
        Save(l_fileName);
    }

    /**
        * Recupere la liste des equipes pour l'editeur
        **/
    public override List<string> allTeamsInXmlFiles(string path)
    {
        List<string> l_teams = new List<string>();

        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);

        foreach (string file in Directory.GetFiles(path))
        {
            if (file.Contains(Constants.xmlExtension))
            {
                using (var stream = new FileStream(file, FileMode.Open))
                {
                    if (stream.CanRead)
                    {
                        Load(stream);
                        XmlNode l_team = SelectSingleNode("//" + Constants.nodeTeam);
                        if (l_team.InnerText != null)
                            l_teams.Add(l_team.InnerText);
                    }
                }
            }
        }

        return l_teams;
    }

}

