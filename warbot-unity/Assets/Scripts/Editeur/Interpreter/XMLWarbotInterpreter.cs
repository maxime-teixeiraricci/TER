using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.IO;

namespace WarBotEngine.Editeur
{
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
            // Creating new xml document
            XmlDocument l_doc = new XmlDocument();

            // Creating root node
            XmlNode l_root = l_doc.CreateElement(Constants.nodeContainer);
            l_doc.AppendChild(l_root);

            // Appending team name
            XmlNode l_node = l_doc.CreateElement(Constants.nodeTeam);
            l_node.InnerText = teamName;
            l_root.AppendChild(l_node);

            l_doc.Save(path + "/" + teamName + Constants.xmlExtension);
        }

        /**
         * Retourne les conditions correspondants aux noeuds
         */
        public override Instruction whichInstruction(string unitName, XmlNode ins)
        {
            if (ins.Name.Equals(typeof(Task).Name))
            {//cas d'un "If"
               // System.Console.WriteLine("Task");
                List<Condition> l_conditions = new List<Condition>();
                XmlNode l_cond = ins.FirstChild;
                if (l_cond != null)
                {
                    foreach (XmlNode c in l_cond)
                    {
                      //  System.Console.WriteLine("dans le task : condition");
                        if (c.Name.Equals(typeof(Or).Name))
                        {
                            l_conditions.Add((Or)whichInstruction(unitName, c));
                            //  System.Console.WriteLine("Cas d'un or ! ");
                        }
                        else
                            l_conditions.Add((Condition)whichInstruction(unitName, c));
                    }
                }
                List<Instruction> l_actions = new List<Instruction>();
                XmlNode l_act = ins.LastChild;
                //System.Console.WriteLine("l_act : ");
                if (l_act != null)
                {
                    foreach (XmlNode a in l_act)
                    {
                        // System.Console.WriteLine("a : " + a.ToString());
                        l_actions.Add((Instruction)whichInstruction(unitName, a));
                    }
                }

                return new Task(l_conditions, l_actions);
            }
            else if (ins.Name.Equals(typeof(If).Name) && ins.ChildNodes.Count == 3)
            {//Cas d'un "If/then/Else"
              //  System.Console.WriteLine("if");
                List<Condition> l_conditions = new List<Condition>();
                XmlNode l_cond = ins.FirstChild;
                if (l_cond != null)
                {
                    foreach (XmlNode c in l_cond)
                    {
                        //System.Console.WriteLine("dans le if : condition");
                        if (c.Name.Equals(typeof(Or).Name))
                        {
                            //  System.Console.WriteLine("Cas d'un or ! ");
                            Or o = new Or(null, false);
                            List<Condition> l_conditions2 = new List<Condition>();
                            XmlNode l_cond2 = c.FirstChild;
                            if (l_cond2 != null)
                            {
                                foreach (XmlNode c2 in l_cond2)
                                {
                                    //   System.Console.WriteLine("dans le or : condition");
                                    l_conditions2.Add((Condition)whichInstruction(unitName, c2));
                                }
                            }
                            Or tmp = new Or(null, false);
                            foreach (Condition c3 in l_conditions2)
                            {
                                tmp.Add(c3);
                            }

                            l_conditions.Add(tmp);
                        }
                        else
                            l_conditions.Add((Condition)whichInstruction(unitName, c));
                    }
                }
                List<Instruction> l_actions = new List<Instruction>();
                XmlNode l_act = ins.ChildNodes[1];
                if (l_act != null)
                {
                    foreach (XmlNode a in l_act)
                        l_actions.Add((Instruction)whichInstruction(unitName, a));
                }
                List<Instruction> l_elseActions = new List<Instruction>();
                XmlNode l_elseAct = ins.ChildNodes[2];
                if (l_elseAct != null)
                {
                    foreach (XmlNode a in l_elseAct)
                        l_elseActions.Add((Instruction)whichInstruction(unitName, a));
                }

                return new If(l_conditions, l_actions, l_elseActions);
            }
            else
            {
                ///////
                    List<string> l_actions = new List<string>(Unit.GetActions(unitName));
                    if (l_actions.Contains(ins.Name))
                    {
                        Action l_a = new Action(ins.Name);
                        foreach (XmlAttribute att in ins.Attributes)
                        {
                            if (att.Name == Instruction.TEXT_ATTRIBUTE_NAME)
                                l_a.AddText = att.Value;
                        }
                        return l_a;
                    }
                    else
                    {
                        string tmp;
                    if (ins.Name.Contains("NEG"))
                    {
                        tmp = ins.Name.Remove(ins.Name.Length - 3);
                      //  System.Console.WriteLine("tmp (neg) : " + tmp);
                    }
                    else
                    {
                        tmp = ins.Name;
                        //System.Console.WriteLine("tmp : " + tmp);
                    }
                        List<string> l_conditions = new List<string>(Unit.GetConditions(unitName));
                    //for (int j = 0; j < l_conditions.Count; j++)
                    //{
                    //    System.Console.WriteLine(l_conditions[j].ToString());
                    //}
                        if (l_conditions.Contains(tmp))
                        {
                         
                            Condition l_c;
                            if (ins.Name.Contains("NEG"))
                                l_c = new Condition(ins.Name.Remove(ins.Name.Length - 3), true);
                            else
                                l_c = new Condition(ins.Name, false);
                            foreach (XmlAttribute att in ins.Attributes)
                            {
                                if (att.Name == Instruction.TEXT_ATTRIBUTE_NAME)
                                    l_c.AddText = att.Value;
                            }
                            return l_c;
                        }
                    }
                }
            
            System.Console.WriteLine("pas trouve" + ins.Name + ";");
            return new Action("Heal");
        }


        /**
         * Renvoie le nom du noeud correspondant a l'equipe recherché , un string vide sinon
         */
        public override string whichFileName(string teamName, string path)
        {
            string l_fileName = "";
            foreach (string file in Directory.GetFiles(path))
            {
                if (file.Contains(Constants.xmlExtension))
                {
                    //Debug.Log(file);
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
                l_fileName = teamName;

            Dictionary<string, List<Instruction>> behavior = new Dictionary<string, List<Instruction>>();

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

            using (FileStream stream = new FileStream(l_fileName, FileMode.Open))
            {
                Load(stream);
                // select the node containing the unit name
                XmlNode l_unitBehavior = SelectSingleNode("//" + Constants.nodeUnit + "[@" + Constants.attributeName + "='" + unitName + "']");
                if (l_unitBehavior != null && l_unitBehavior.HasChildNodes)
                {
                    foreach (XmlNode ins in l_unitBehavior.ChildNodes)
                        l_behavior.Add(whichInstruction(unitName, ins));
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
            string l_fileName = whichFileName(teamName, path);

            // If no file has been found, create a new one with the given team name
            if (l_fileName.Equals(""))
            {
                l_fileName = teamName + Constants.xmlExtension;
                generateEmptyFile(teamName, path);
            }

            // Load the file
            //Load(path + "/" + fileName);
            Load(l_fileName);

            // Get all nodes named "unit"
            XmlNode l_node = SelectSingleNode("//" + Constants.nodeUnit + "[@" + Constants.attributeName + "='" + unitName + "']");
            if (l_node == null)
                l_node = CreateElement(Constants.nodeUnit);

            //            Debug.Log(node.OuterXml);
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

}
