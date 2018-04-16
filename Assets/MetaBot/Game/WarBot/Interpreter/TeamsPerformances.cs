using System.Collections.Generic;
using UnityEngine;

public class TeamsPerformance {

    public TeamsPerformance()
    {
    }

public void WriteStats(string[] Teams, string Winner, int NbTeam)
{
    foreach (string Team in Teams)
    {
        int i = 0;
        string[] Stats = System.IO.File.ReadAllLines(Application.streamingAssetsPath + "/Stats/" + Team + ".stat");
        bool isInT0 = false;
        bool isInT1 = false;
        bool isInT2 = false;
        bool isInT3 = false;
        foreach (string Stat in Stats)
        {
            string[] DetailStat = Stat.Split('/');
            if (DetailStat[0] == Teams[0])
            {
                isInT0 = true;
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
            if (DetailStat[0] == Teams[1])
            {
                isInT1 = true;
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
            if (NbTeam >= 3 && DetailStat[0] == Teams[2])
            {
                isInT2 = true;
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
            if (NbTeam >= 4 && DetailStat[0] == Teams[3])
            {
                isInT3 = true;
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
        i--;
        if (!isInT0 && Team != Teams[0])
        {
            i++;
            Array.Resize(ref Stats, i);
            //Stats = (string[]) ResizeArray(Stats, new string[] { i });
            if (Team == Winner) Stats[i] = Teams[0] + "/1/1";
            else Stats[i] = Teams[0] + "/1/0";
        }
        if (!isInT1 && Team != Teams[1])
        {
            i++;
            Array.Resize(ref Stats, i);
            //Stats = (string[]) ResizeArray(Stats, new string[] { i });
            if (Team == Winner) Stats[i] = Teams[1] + "/1/1";
            else Stats[i] = Teams[1] + "/1/0";
        }
        if (NbTeam >= 3 && !isInT2 && Team != Teams[2])
        {
            i++;
            Array.Resize(ref Stats, i);
            //Stats = (string[]) ResizeArray(Stats, new string[] { i });
            if (Team == Winner) Stats[i] = Teams[2] + "/1/1";
            else Stats[i] = Teams[2] + "/1/0";
        }
        if (NbTeam >= 4 && !isInT3 && Team != Teams[3])
        {
            i++;
            Array.Resize(ref Stats, i);
            //Stats = (string[]) ResizeArray(Stats, new string[] { i });
            if (Team == Winner) Stats[i] = Teams[3] + "/1/1";
            else Stats[i] = Teams[3] + "/1/0";
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
