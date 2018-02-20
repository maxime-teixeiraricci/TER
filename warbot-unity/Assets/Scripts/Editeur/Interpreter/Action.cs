using System;
using System.Xml;

namespace WarBotEngine.Editeur
{
	/**
	 * @Author : Celia Rouquairol
	 * 
	 * @Project : Warbot Unity version : FSM design & implementation
	 *
	 * @Structure : 
	 * action.execute (unit)
	 * {
	 *  unit.ThisAction()
	 * }
	 * @Resume :
	 * Calls the corresponding action of the unit
	 **/
	public class Action : Instruction
	{
        private string action;

        public Action(string a)
        {
            action = a;
        }

        /**
         * Parametres : Unit sur laquelle va etre execute l'action
         * Retour : Boolean, true si l'unite a effectue l'action, false sinon
         **/
        public override bool execute(Unit u)
        {
            if (needTextInput())
                u.Message = this.AddText;
            return u.GetAction(action).Invoke();
        }

        /**
         * Retour : String , type d'action 
         **/
        public override string Type()
        {
            return action;
        }

        /**
         * Retour : string Description de l'action, utile pour l'affichage
         **/
		public override string Description ()
		{
			return Unit.GetDescription(action);
		}

        /**
         * Retour : Renvoie la structure XML de l'action pour le stockage (noeud XML)
         **/
        public override XmlNode xmlStructure()
        {
            XmlDocument l_doc = new XmlDocument();

			XmlNode l_node = l_doc.CreateElement(this.Type());

			if (this.needTextInput () && this.AddText.Length > 0) 
			{
				XmlAttribute addText = l_doc.CreateAttribute(TEXT_ATTRIBUTE_NAME);
				addText.Value = this.AddText;
				l_node.Attributes.Append(addText);
			}

            return l_node;
        }

        /**
         * Clone l'action et la renvoie
         **/
        public override Instruction Clone()
        {
			Action l_a = new Action(action);
			l_a.AddText = this.AddText;
			return l_a;
        }

        /**
         * Verifie si l'action a besoin d'un message
         * Renvoie true si besoin d'un message, false sinon
         **/
        public override bool needTextInput()
		{
			return Unit.NeedMessage(this.action);
		}
    }
}
