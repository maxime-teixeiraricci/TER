using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

namespace WarBotEngine.Editeur
{
    public class Or : Condition    {

        public List<Condition> conditionsOu;

        public List<Condition> getConditionsOu()
        {
            return conditionsOu;
        }

        public Or(string s, bool b)
            : base(s, b)
        {
            setConditionsOu(new List<Condition>());
        }
        
        public void setConditionsOu(List<Condition> c)
        {
        conditionsOu = c;
        }

        public override bool execute(Unit u)
        {
            bool l_condOu = false;
            foreach (Condition c in getConditionsOu())
            {
                if (c.execute(u))
                {
                    l_condOu = true;
                    break;
                }
            }

            return l_condOu;
        }
        public override string Type()
        {
            return "Or";
        }

        public void Add(Condition c)
        {
            conditionsOu.Add(c);
        }

        public override XmlNode xmlStructure()
        {
            XmlDocument l_doc = new XmlDocument();
            XmlNode l_orNode = l_doc.CreateElement(this.Type());

            XmlNode l_paramNode = l_doc.CreateElement(Control.PARAM_NODE_NAME);
            if (getConditionsOu().Count > 0)
            {
                foreach (Condition c in getConditionsOu())
                    l_paramNode.AppendChild(l_doc.ImportNode(c.xmlStructure(), true));
            }

            l_orNode.AppendChild(l_paramNode);

            return l_orNode;

        }

        public override Instruction Clone()
        {
            Or res = new Or("or",false);
            foreach (Condition cond in this.conditionsOu)
                res.conditionsOu.Add((Condition)cond.Clone());

            return res;
        }

        public override string ToString()
        {
            string s = "or : ";
            s = s + "Taille : " + conditionsOu.Count;
            foreach (Condition c in this.conditionsOu)
            {
                
                s = s + c.ToString();
            }
            return s;
        }
    }

}