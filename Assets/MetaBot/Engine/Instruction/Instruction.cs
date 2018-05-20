using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

[System.Serializable]
public class Instruction {

    public string[] _listeStringPerceptsVoulus;
    public MessageStruct[] _stringActionsNonTerminales;
    public string _stringAction;

    public new string ToString()
    {
        return "Percepts : " + _listeStringPerceptsVoulus.Length + " ANT : " + _stringActionsNonTerminales.Length + " Action : " + _stringAction;  
    } 
    
   public Instruction(string[] ins, MessageStruct[] actionsNonTerminales, string act)
    {
        _stringAction = act;
        _listeStringPerceptsVoulus = ins;
        _stringActionsNonTerminales = actionsNonTerminales;

    }

    public Instruction(string[] ins, MessageStruct[] actionsNonTerminales)
    {
        _stringAction = "";
        _listeStringPerceptsVoulus = ins;
        _stringActionsNonTerminales = actionsNonTerminales;
    }

    public Instruction(string[] ins, string act)
    {
        _stringAction = act;
        _listeStringPerceptsVoulus = ins;
        _stringActionsNonTerminales = new MessageStruct[0];
    }

    public Instruction(string act)
    {
        _stringAction = act;
        _listeStringPerceptsVoulus = new string[0];
        _stringActionsNonTerminales = new MessageStruct[0];
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
        if (_stringAction != "")
        {
            XmlNode a = l_doc.CreateElement(_stringAction);
            actNode.AppendChild(a);

            l_whenNode.AppendChild(actNode);
        }

        

        return l_whenNode;
    }

    public string toString()
    {
        return _listeStringPerceptsVoulus.ToString() + _stringAction;
    }

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
