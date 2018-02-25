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
	 * condition ()
	 * {
	 *  unit.ThisAction()
	 * }
	 * @Resume :
	 * Calls the corresponding action of the unit
	 * What is "Condition" in the design pattern is classical method for the unit
	 **/
	public class Condition : Instruction
	{

        protected string condition;
        protected bool neg; 
	
        public Condition(string c,bool b)
        {
            condition = c;
            neg = b;
        }

		/**
         * Parametres : Unit
         * Appelle la methode correspondante sur l'unite
         * Renvoie true si appel réussi , false sinon
         **/
        public override bool execute(Unit u)
        {
            if (needTextInput())
                u.Message = this.AddText;

            if (neg)
                return !(u.GetCondition(condition).Invoke());
            else
              return u.GetCondition(condition).Invoke();
        }

        /**
         * Retour : String , type de condition
         **/
        public override string Type()
        {
            if (!neg)
                return condition;
            else
                return condition + "NEG";
        }

        /**
         * Retour : string Description de la condition, utile pour l'affichage
         **/
        public override string Description ()
		{
			return Unit.GetDescription (condition);
		}

        /**
        * Retour : Renvoie la structure XML de la condition pour le stockage (noeud XML)
        **/
        public override XmlNode xmlStructure()
        {
            XmlDocument l_doc = new XmlDocument();

            XmlNode l_node;
            l_node = l_doc.CreateElement(this.Type());

            if (this.needTextInput () && this.AddText.Length > 0) 
			{
                XmlAttribute l_addText;
                l_addText = l_doc.CreateAttribute(TEXT_ATTRIBUTE_NAME);
				l_addText.Value = this.AddText;
				l_node.Attributes.Append(l_addText);
			}

            return l_node;
        }

        /**
        * Clone la condition et la renvoie
        **/
        public override Instruction Clone()
        {
			Condition c = new Condition (condition,neg);
			c.AddText = this.AddText;
            return c;
        }

        /**
         * Verifie si la condition a besoin d'un message
         * Renvoie true si besoin d'un message, false sinon
         **/
		public override bool needTextInput ()
		{
			return Unit.NeedMessage(this.condition);
		}

        public override string ToString()
        {
            return condition + " ";
        }
    }
}
