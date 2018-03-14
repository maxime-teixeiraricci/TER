using System.Collections.Generic;
using System.Reflection;
using System.Xml;

namespace WarBotEngine.Editeur
{
	/**
	 * @Author : Celia Rouquairol
	 * 
	 * @Project : Warbot Unity version : FSM design & implementation
	 * 
	 * @Resume :
	 * An instruction to execute
	 **/
	public abstract class Instruction
    {
		/**
         * Nom de l'attribut contenant le texte additionel dans le fichier XML
		**/
		public static readonly string TEXT_ATTRIBUTE_NAME = "additionalText";

        /**
         * Le texte additionel lui meme
		**/
        private string additionalText = "";

        /***********/
        /* METHODS */
        /***********/

		/**
         * Getter et setters sur le texte additionel
         **/
		public string AddText{
			get{return this.additionalText;} 
			set{ 
				if (needTextInput())
					this.additionalText = value;	
			}
		}

        /**
         * Parametres : Unit sur laquelle va etre execute l'instruction
         * Retour : Boolean, true si l'unite a effectue l'instruction, false sinon
         **/
        public abstract bool execute(Unit u);

        /**
        * Retour : Renvoie la structure XML de l'instruction pour le stockage (noeud XML)
        **/
        public abstract XmlNode xmlStructure();

        /**
         * Retour : String , type de l'instruction
         **/
        public virtual string Type()
		{
			return this.GetType ().Name;
		}

        /**
         * Retour : string Description de l'instruction, utile pour l'affichage
         **/
        public virtual string Description()
		{
			return "Instruction";
		}

        /**
        * Clone l'instruction et la renvoie
        **/
        public abstract Instruction Clone();

        /**
         * Verifie si l'instruction a besoin d'un message
         * Renvoie true si besoin d'un message, false sinon
         **/
        public abstract bool needTextInput ();
    }
}
