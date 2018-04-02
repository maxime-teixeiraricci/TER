using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class Instruction {

    public string[] _listeStringPerceptsVoulus;
    public MessageStruct[] _stringActionsNonTerminales;
    public string _stringAction;

    
   public Instruction(string[] ins, MessageStruct[] actionsNonTerminales, string act)
    {
        _stringAction = act;
        _listeStringPerceptsVoulus = ins;
        _stringActionsNonTerminales = actionsNonTerminales;
        // _listeStringPerceptsOu = ou; ;

    }

    public Instruction(string[] ins, string act)
    {
        _stringAction = act;
        _listeStringPerceptsVoulus = ins;
        _stringActionsNonTerminales = new MessageStruct[0];
        // _listeStringPerceptsOu = ou; ;

    }

    public Instruction(string act)
    {
        _stringAction = act;
        _listeStringPerceptsVoulus = new string[0];
        _stringActionsNonTerminales = new MessageStruct[0];
        // _listeStringPerceptsOu = ou; ;

    }

    public XmlNode xmlStructure()
    {
        XmlDocument l_doc = new XmlDocument();
        XmlNode l_whenNode = l_doc.CreateElement("instruction");

        XmlNode paramNode = l_doc.CreateElement("parameters");
        foreach (string c in _listeStringPerceptsVoulus)
        {
            XmlElement t = l_doc.CreateElement(c);
            paramNode.AppendChild(t);
        }
        /*
        if (_listeStringPerceptsOu.Length > 0)
        {
            XmlNode ouNode = l_doc.CreateElement("or");
            foreach (string c2 in _listeStringPerceptsOu)
            {
                XmlElement t2 = l_doc.CreateElement(c2);
                ouNode.AppendChild(t2);
            }

            paramNode.AppendChild(ouNode);
        }*/
        l_whenNode.AppendChild(paramNode);

        if (_stringActionsNonTerminales.Length > 0)
        {
            XmlNode MsgNode = l_doc.CreateElement("message");
            foreach (MessageStruct c2 in _stringActionsNonTerminales)
            {
                XmlNode t2 = l_doc.CreateElement(c2._intitule);
                XmlNode t2d = l_doc.CreateElement(c2._destinataire);
                t2.AppendChild(t2d);
                MsgNode.AppendChild(t2);
            }

            l_whenNode.AppendChild(MsgNode);
        }

            XmlNode actNode = l_doc.CreateElement("actions");
        XmlNode a = l_doc.CreateElement(_stringAction);

        actNode.AppendChild(a);

        l_whenNode.AppendChild(actNode);

        return l_whenNode;
    }

    public string toString()
    {
        return _listeStringPerceptsVoulus.ToString() + _stringAction;
    }

    /*
    public bool verify()
    {
        Percept[] listePerceptsUtilisables = GetComponent<UnitManager>().GetComponent<PerceptManager>()._percepts;
        bool verifie = true;
        foreach(string s in _listeStringPerceptsVoulus)
        {
            foreach(Percept p2 in listePerceptsUtilisables)
            {
                if(s.Equals(p2._perceptName))
                {
                    verifie = p2._value;
                }
            }

            if (!verifie)
            {
                break;
            }
        }
        if (verifie)
        {
            return true;
        }


        return false;
    }
    */

    public string[] getListeStringPerceptsVoulus()
    {
        return this._listeStringPerceptsVoulus;
    }

    public void setListeStringPerceptsVoulus(string[] percepts)
    {
        this._listeStringPerceptsVoulus = percepts;
    }

    public string getStringAction()
    {
        return this._stringAction;
    }

    public void setStringAction(string action)
    {
        this._stringAction = action;
    }

}
