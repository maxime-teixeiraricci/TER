using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.IO;

public abstract class XMLInterpreter : XmlDocument
{
    public XMLInterpreter()
    {

    }

        /**
         * Genere un fichier XML contenant uniquement le nom de l'equipe
         */
        public abstract void generateEmptyFile(string teamName, string path);

        /**
         * Retourne les conditions correspondants aux noeuds
         */
        public abstract Instruction whichInstruction(string unitName, XmlNode ins);


        /**
         * Renvoie le nom du noeud correspondant a l'equipe recherché , un string vide sinon
         */
        public abstract string whichFileName(string teamName, string path);

        /**
         * Vas chercher un fichier XML et remplit un comportement 
         **/
        public abstract Dictionary<string, List<Instruction>> xmlToBehavior(string teamName, string path);

        /**
         * Vas creer un fichier XML correspondant au comportement passé en parametre
         **/
        public abstract void behaviorToXml(string teamName, string path, string unitName, List<Instruction> behavior);
        /**
         * Recupere la liste des equipes pour l'editeur
         **/
        public abstract List<string> allTeamsInXmlFiles(string path);

 }

        