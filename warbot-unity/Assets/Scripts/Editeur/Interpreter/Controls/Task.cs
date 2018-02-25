using System.Collections.Generic;
using System.Xml;

namespace WarBotEngine.Editeur
{
    /**
	 * @Author : Celia Rouquairol
	 * 
	 * @Project : Warbot Unity version : FSM design & implementation
	 * 
	 * @Structure : 
	 * When (conditions)
	 * {
	 *  actions
	 * }
	 * 
	 * @Resume :
	 * The execute method returns True if the conditions were all true and the actions had been executed, false otherwise.
	 * It doesn't matter if some actions return false as long as the conditions are true, the when control is considered executed.
	 **/
    class Task : Control
    {
        public Task()
        {
            setConditions(new List<Condition>());
            setActions(new List<Instruction>());
        }

        public Task(List<Condition> c, List<Instruction> a)
        {
            setConditions(c);
            setActions(a);
        }

        /**
		 * Fais executer les conditions a l'unité,
         * Renvoie vrai si les conditions sont toutes OK (cas d'un "and") faux sinon
         * Si c'est bon , executer les actions
		 */
        public override bool execute(Unit u)
        {
            bool l_conditionIsTrue = true;
            bool l_flagOu = false;

            foreach (Condition c in getConditions())
            {
                if (!c.execute(u))
                {
                    l_conditionIsTrue = false;
                    break;
                }
            }

         //   l_flagOu = Ou.execute(u);

            if (l_conditionIsTrue)
            {
                foreach (Action a in getActions())
                {
                    if (a.execute(u))
                        return true;
                }
            }

            return false;
        }

        public override string Description()
        {
            return "Structure de contrôle pour organiser les actions en tâches";
        }

        /**
         * Renvoie la structure XML du if
         * <when>
         *  <parameter>
         *      <condition></condition>
         *  </parameter>
         *  <action>
         *      <action1></action1>
         *      <action2></action2>
         *  </action>
         * </when>
         */
        public override XmlNode xmlStructure()
        {
            XmlDocument l_doc = new XmlDocument();
            XmlNode l_whenNode = l_doc.CreateElement(this.Type());

            XmlNode paramNode = l_doc.CreateElement(Control.PARAM_NODE_NAME);
            if (getConditions().Count > 0)
            {
                foreach (Condition c in getConditions())
                    paramNode.AppendChild(l_doc.ImportNode(c.xmlStructure(), true));
            }

            l_whenNode.AppendChild(paramNode);

            XmlNode actNode = l_doc.CreateElement(Control.ACTION_NODE_NAME);
            if (getActions().Count > 0)
            {
                foreach (Instruction a in getActions())
                    actNode.AppendChild(l_doc.ImportNode(a.xmlStructure(), true));
            }
            l_whenNode.AppendChild(actNode);

            return l_whenNode;
        }

        /**
         * Clone l'instance 
         **/
        public override Instruction Clone()
        {
            Task l_res = new Task();
            foreach (Condition cond in this.conditions)
                l_res.conditions.Add((Condition)cond.Clone());
            foreach (Instruction ins in this.actions)
                l_res.actions.Add(ins.Clone());
            return l_res;
        }

        public override string ToString()
        {
            string s = "task : condition : ";
            foreach (Condition c in conditions)
            {
                s = s + c.ToString();
            }

            foreach (Action c in actions)
            {
                s = s + " actions : ";
                s = s + c.ToString();
            }

            return s;
        }
    }
}
