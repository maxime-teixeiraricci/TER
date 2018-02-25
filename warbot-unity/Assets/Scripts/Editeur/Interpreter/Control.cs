using System;
using System.Collections.Generic;

namespace WarBotEngine.Editeur
{
	/**
	 * @Author : Celia Rouquairol
	 * 
	 * @Project : Warbot Unity version : FSM design & implementation
	 * 
	 * @Resume :
	 * Checks if all conditions are true
	 * If it is so, execute all actions
	 * Return true if all conditions were true, false otherwise.
	 * It doesn't matter if some actions return false, as long as the conditions are true the control is considered executed.
	 **/
	public abstract class Control : Instruction
	{
		/**
         * Nom du noeud des parametres dans le fichier xml
         **/
		public static readonly string PARAM_NODE_NAME = "parameters";

        /**
         * Nom du noeud des actions dans le fichier xml
         **/
        public static readonly string ACTION_NODE_NAME = "actions";

	    /**
         * Liste des conditions a verifier semantique "Et"
         **/
		protected List<Condition> conditions;

        protected Or Ou;
     

        /**
         * Liste des actions a executer
         **/
        protected List<Instruction> actions; 

		/**
         * Getters
         **/
		public List<Condition> getConditions() { return conditions; }
		public List<Instruction> getActions() { return actions; }
        public Or getConditionsOu() { return Ou; }

        /**
         * Setters
         **/
        public void setConditions(List<Condition> c) { conditions = c; }
        public void setConditionsOu(Or co) { Ou = co;  }
        public void setActions(List<Instruction> a) 
		{ 
			actions = new List<Instruction> ();
			foreach (Instruction i in a) 
			{
				if(i is Action || i is If)
					actions.Add(i); 
			}
		}

        /**
         * Ajouts dans les conditons
         **/

        public void addCondition(Condition c) { conditions.Add(c); }
        public void addConditionOu(Condition c) { Ou.Add(c); }
        public void addAction(Instruction a) { if(a is Action || a is If) actions.Add(a); }

        /**
         * Verifie si a structure de controle a besoin d'un message
         * Renvoie true si besoin d'un message, false sinon
         **/
        public override bool needTextInput ()
		{
			return false;
		}
    }

}
