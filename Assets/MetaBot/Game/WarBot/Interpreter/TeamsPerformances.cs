using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using System.Xml;
using System.IO;


public class TeamsPerformance : XmlDocument {

    public TeamsPerformance()
    {
    }

    public void WriteStats (string[] Teams, string Winner)
    {
        foreach (string Team in Teams)
        {
            int i = 0;
            string[] Stats = System.IO.File.ReadAllLines(Application.streamingAssetsPath + "/Stats/" + Team + ".stat");
            foreach (string Stat in Stats) 
            {
                string[] DetailStat = Stat.Split('/');
                if (DetailStat[0] == Teams[0] || DetailStat[0] == Teams[1] || DetailStat[0] == Teams[2] || DetailStat[0] == Teams[3])
                {
                    int numVal = Int32.Parse(DetailStat[1]);
                    numVal++;
                    DetailStat[1] = numVal.ToString(); //NbMatch
                    if (Team == Winner)
                    {
                        numVal = Int32.Parse(DetailStat[2]);
                        numVal++;
                        DetailStat[2] = numVal.ToString(); //NbVictoire
                    }
                }
                Stats[i] = DetailStat[0] + '/' + DetailStat[1] + '/' + DetailStat[2];
                i++;
            }
            System.IO.File.WriteAllLines(Application.streamingAssetsPath + "/Stats/" + Team + ".stat", Stats);
        }
    }
    
    public Dictionary<string, List<int>> getStats(string path)
    {
        // Try to find an already existing file with this team name

        Dictionary<string, List<int>> stats = new Dictionary<string, List<int>>();

        string l_filename = path + "/stats.xml";

        List<string> teams = allTeamsInXmlFiles(l_filename);
  
        using (FileStream stream = new FileStream(l_filename, FileMode.Open))
        {
            Load(stream);
            foreach (string stmp in teams)
            {
                XmlNode teamNode = SelectSingleNode(stmp);
                List<int> statsTeam = new List<int>();
                foreach (XmlNode x in teamNode.ChildNodes)
                {
                    statsTeam.Add(int.Parse(x.Name));
                }
                stats.Add(stmp,statsTeam);

            }
        }
            return stats;
    }

        public List<string> allTeamsInXmlFiles(string path)
    {
        List<string> l_teams = new List<string>();

        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);

        foreach (string file in Directory.GetFiles(path))
        {
            if (file.Contains(Constants.xmlExtension))
            {
                using (var stream = new FileStream(file, FileMode.Open))
                {
                    if (stream.CanRead)
                    {
                        Load(stream);
                        XmlNode l_team = SelectSingleNode("//" + Constants.nodeTeam);
                        if (l_team.InnerText != null)
                            l_teams.Add(l_team.InnerText);
                    }
                }
            }
        }

        return l_teams;
    }
}
