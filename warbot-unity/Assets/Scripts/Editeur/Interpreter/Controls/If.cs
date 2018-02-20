using System;
using System.Collections.Generic;
using System.Xml;

namespace WarBotEngine.Editeur
{
	/**
	 * @Author : Edouard BREUILLE
	 * 
	 * @Project : Warbot Unity version : FSM design & implementation
	 * 
	 * @Structure : 
	 * if (conditions)
	 * {
	 *  actions
	 * }
	 * else
	 * {
	 *  actions
	 * }
	 * 
	 **/
	class If: Control
	{
		public static readonly string ELSE_NODE_NAME = "elseActions";

        /**
         * Liste des actions dans le cas du "Else"
         **/
		private List<Instruction> elseActions;
		
		public If()
		{
			setConditions(new List<Condition>());
			setActions(new List<Instruction>());
			setElseActions (new List<Instruction> ());
		}

		public If(List<Condition> c, List<Instruction> a, List<Instruction> ea)
		{
			setConditions(c);
			setActions(a);
			setElseActions (ea);
		}

        /**
         * Recupere les actions du cas "Else"
         **/
		public List<Instruction> getElseActions()
		{
			return this.elseActions;
		}

		public void setElseActions(List<Instruction> ea)
		{
			elseActions = new List<Instruction> ();
			foreach (Instruction i in ea) 
			{
				if(i is Action || i is If)
					elseActions.Add(i); 
			}
		}

		/**
		 * Fais executer les conditions a l'unité,
         * Renvoie vrai si les conditions sont toutes OK (cas d'un "and") faux sinon
         * Si c'est bon , executer les actions
		 */
		public override bool execute(Unit u)
		{
			bool l_conditionIsTrue = true;

			foreach (Condition c in getConditions()) 
			{
				if (!c.execute(u))
				{
					l_conditionIsTrue = false;
					break;
				}
			}

			if (l_conditionIsTrue)
            {
				foreach (Action a in getActions())
                {
					if (a.execute (u))
						return true;
				}
			}
			else 
			{
				foreach (Action a in getElseActions())
                {
					if (a.execute (u))
						return true;
				}
			}

			return false;
		}

		public override string Description ()
		{
			return "Structure de contrôle pour organiser les actions en blocs de type [if() - then{} - else{}]";
		}

		/**
         * Renvoie la structure XML du if/then/else
         * <IfThenElse>
         *  <parameters>
         *      <condition></condition>
         *  </parameters>
         *  <actions>
         *      <action1></action1>
         *      <action2></action2>
         *  </actions>
         *  <elseActions>
         *   	<action3></action3>
         *  </elseActions>
         * </IfThenElse>
         */
		public override XmlNode xmlStructure()
		{
			XmlDocument l_doc = new XmlDocument();
			XmlNode l_ifNode = l_doc.CreateElement(this.Type());

			XmlNode l_paramNode = l_doc.CreateElement(Control.PARAM_NODE_NAME);
			if(getConditions().Count > 0)
            {
				foreach(Condition c in getConditions())
					l_paramNode.AppendChild(l_doc.ImportNode(c.xmlStructure(), true));
			}

			l_ifNode.AppendChild(l_paramNode);

			XmlNode l_actNode = l_doc.CreateElement(Control.ACTION_NODE_NAME);
			if (getActions().Count > 0) {
				foreach (Action a in getActions())
					l_actNode.AppendChild(l_doc.ImportNode(a.xmlStructure(), true));
			}

			l_ifNode.AppendChild(l_actNode);

			XmlNode l_elseActNode = l_doc.CreateElement(ELSE_NODE_NAME);
			if (getElseActions().Count > 0) {
				foreach (Action a in getElseActions())
					l_elseActNode.AppendChild(l_doc.ImportNode(a.xmlStructure(), true));
			}
			l_ifNode.AppendChild(l_elseActNode);

			return l_ifNode;
		}

        /**
         * Clone l'instance 
         **/
        public override Instruction Clone()
        {
            If res = new If();
            foreach (Condition cond in this.conditions)
                res.conditions.Add((Condition)cond.Clone());
            foreach (Instruction ins in this.actions)
                res.actions.Add(ins.Clone());
            foreach (Instruction ins in this.elseActions)
                res.elseActions.Add(ins.Clone());
            return res;
        }
    }
}
