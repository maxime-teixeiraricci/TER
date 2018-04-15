using System.Collections.Generic;
using UnityEngine;

public class TeamsPerformance {

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
                    int numVal = int.Parse(DetailStat[1]);
                    numVal++;
                    DetailStat[1] = numVal.ToString(); //NbMatch
                    if (Team == Winner)
                    {
                        numVal = int.Parse(DetailStat[2]);
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
    
    public KeyValuePair<string,List<Matchup>>  getStats(string team)
    {
        List<Matchup> matchs = new List<Matchup>();

        string[] Stats = System.IO.File.ReadAllLines(Application.streamingAssetsPath + "/Stats/" + team + ".stat");
        foreach (string stat in Stats)
        {
            string[] DetailStat = stat.Split('/');
            Matchup tmp = new Matchup();
            tmp.opponent = DetailStat[0];
            tmp.totalMatchCount = int.Parse(DetailStat[1]);
            tmp.victoryCount = int.Parse(DetailStat[2]);

            matchs.Add(tmp);
        }

        KeyValuePair<string, List<Matchup>> result = new KeyValuePair<string, List<Matchup>>(team,matchs);
        return result;
    }

   
}

[System.Serializable]
public struct Matchup
{
    public string opponent;
    public int victoryCount;
    public int totalMatchCount;
}
