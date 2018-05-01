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


    //Genere un fichier XML contenant uniquement le nom de l'equipe
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

    //Recupere l'instruction du noeud actuellement traité
    public override Instruction whichInstruction(string unitName, XmlNode ins)
    {
        List<string> l_conditions = new List<string>();
        List<MessageStruct> l_MsgStruct = new List<MessageStruct>();
        List<string> l_actions = new List<string>();

        foreach (XmlNode x in ins.ChildNodes)
        {

            if (x.Name.Contains("parameters"))//Partie condition de l'instruction
            {
                foreach (XmlNode c in x)
                    l_conditions.Add(c.Name);
            }

            if (x.Name.Contains("message"))//Partie action non temrinale de l'instruction
            {
                foreach (XmlNode c in x)
                {
                    l_MsgStruct.Add(new MessageStruct(c.Name, c.FirstChild.Name));
                }
            }

            if (x.Name.Contains("actions"))//Partie action terminale de l'instruction
            {
                foreach (XmlNode c in x)
                    l_actions.Add(c.Name);
            }
        }
        if (l_actions.Count == 0)
        {
            return new Instruction(l_conditions.ToArray(), l_MsgStruct.ToArray());
        }
        return new Instruction(l_conditions.ToArray(), l_MsgStruct.ToArray(), l_actions[0]);

    }


    //recupere le nom du fichier correspondant a l'equipe
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

                if (!l_fileName.Equals("")) 
                    break;
            }
        }

        return l_fileName;
    }

    //Creer un comportement a partir d'un fichier xml
    public override Dictionary<string, List<Instruction>> xmlToBehavior(string teamName, string path)
    {
        string l_fileName = whichFileName(teamName, path);

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

    //Recupere le comportement d'une unité dans un fichier xml d'une equipe
    public List<Instruction> xmlToUnitBehavior(string teamName, string path, string unitName)
    {
        string l_fileName = whichFileName(teamName, path);

        if (l_fileName.Equals(""))
            l_fileName = teamName;

        List<Instruction> l_behavior = new List<Instruction>();
        l_fileName = teamName.Replace(".wbt", "") + Constants.xmlExtension;
        l_fileName = path + "/" + l_fileName.Replace(path, "");

        using (FileStream stream = new FileStream(l_fileName, FileMode.Open))
        {
            Load(stream);
            XmlNode l_unitBehavior = SelectSingleNode("//" + Constants.nodeUnit + "[@" + Constants.attributeName + "='" + unitName + "']");
            if (l_unitBehavior != null && l_unitBehavior.HasChildNodes)
            {
                foreach (XmlNode ins in l_unitBehavior.ChildNodes)
                {
                    l_behavior.Add(whichInstruction(unitName, ins));
                }

            }

            stream.Close();
        }
        return l_behavior;
    }

       //Cree un fichier XML d'equipe a partir d'un comportement et d'un nom d'equipe
    public override void behaviorToXml(string teamName, string path, string unitName, List<Instruction> behavior)
    {
        System.Console.WriteLine(path);
        string l_fileName = whichFileName(teamName, path);

        if (l_fileName.Equals(""))
        {
            l_fileName = teamName + Constants.xmlExtension;
            generateEmptyFile(teamName, path);
        }

        l_fileName = whichFileName(teamName, path);
        Load(l_fileName);

        XmlNode l_node = SelectSingleNode("//" + Constants.nodeUnit + "[@" + Constants.attributeName + "='" + unitName + "']");
        if (l_node == null)
            l_node = CreateElement(Constants.nodeUnit);

        l_node.RemoveAll();

        XmlAttribute l_name = CreateAttribute(Constants.attributeName);
        l_name.Value = unitName;
        l_node.Attributes.Append(l_name);

        foreach (Instruction i in behavior)
            l_node.AppendChild(ImportNode(i.xmlStructure(), true));

        XmlNode l_embbed = FirstChild;
        l_embbed.AppendChild(l_node);

        Save(l_fileName);
    }

    //Recupere la liste des equipes dans le dossier
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

