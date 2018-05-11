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
                if (!System.IO.File.Exists(Application.streamingAssetsPath + "/Stats/" + Team + ".stat"))
                    System.IO.File.Create(Application.streamingAssetsPath + "/Stats/" + Team + ".stat");
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
            if (i > 0) i--;
            if (!isInT0 && Team != Teams[0])
            {
                i++;
                System.Array.Resize(ref Stats, i);
                //Stats = (string[]) ResizeArray(Stats, new string[] { i });
                if (Team == Winner) Stats[i-1] = Teams[0] + "/1/1";
                else Stats[i-1] = Teams[0] + "/1/0";//--------------------------------------------------------------------------------------
            }
            if (!isInT1 && Team != Teams[1])
            {
                i++;
                System.Array.Resize(ref Stats, i);
                //Stats = (string[]) ResizeArray(Stats, new string[] { i });
                if (Team == Winner) Stats[i-1] = Teams[1] + "/1/1";
                else Stats[i-1] = Teams[1] + "/1/0";
            }
            if (NbTeam >= 3 && !isInT2 && Team != Teams[2])
            {
                i++;
                System.Array.Resize(ref Stats, i);
                //Stats = (string[]) ResizeArray(Stats, new string[] { i });
                if (Team == Winner) Stats[i-1] = Teams[2] + "/1/1";
                else Stats[i-1] = Teams[2] + "/1/0";
            }
            if (NbTeam >= 4 && !isInT3 && Team != Teams[3])
            {
                i++;
                System.Array.Resize(ref Stats, i);
                //Stats = (string[]) ResizeArray(Stats, new string[] { i });
                if (Team == Winner) Stats[i-1] = Teams[3] + "/1/1";
                else Stats[i-1] = Teams[3] + "/1/0";
            }
    
            System.IO.File.WriteAllLines(Application.streamingAssetsPath + "/Stats/" + Team + ".stat", Stats);
        }
    }
    
    public void ComputeELO (string[] Teams, string Winner)
    {
        double[] ELOs = new double[2];
        double ProbaWinT1;
        double ProbaWinT2;
        double CoeffT1;
        double CoeffT2;
        int[] NewELOs = new int[2];

        if (!System.IO.File.Exists(Application.streamingAssetsPath + "/ELO/" + Teams[0] + ".elo"))
        {
            System.IO.File.Create(Application.streamingAssetsPath + "/ELO/" + Teams[0] + ".elo");
            System.IO.File.WriteAllLines(Application.streamingAssetsPath + "/ELO/" + Teams[0] + ".elo", new string[] { 2500 + "" });
        }

        if (!System.IO.File.Exists(Application.streamingAssetsPath + "/ELO/" + Teams[1] + ".elo"))
        {
            System.IO.File.Create(Application.streamingAssetsPath + "/ELO/" + Teams[1] + ".elo");
            System.IO.File.WriteAllLines(Application.streamingAssetsPath + "/ELO/" + Teams[1] + ".elo", new string[] { 2500 + "" });
        }

        string[] CurELOT1 = System.IO.File.ReadAllLines(Application.streamingAssetsPath + "/ELO/" + Teams[0] + ".elo");
        string[] CurELOT2 = System.IO.File.ReadAllLines(Application.streamingAssetsPath + "/ELO/" + Teams[1] + ".elo");
        Debug.Log("Equipe 1 : " + Teams[0]);
        Debug.Log("Equipe 2 : " + Teams[1]);
        ELOs[0] = double.Parse(CurELOT1[0]);
        ELOs[1] = double.Parse(CurELOT2[0]);
        
        ProbaWinT1 = 1.0 / (1.0 + System.Math.Pow(10, (ELOs[1] - ELOs[0]) / 400.0));
        ProbaWinT2 = 1.0 - ProbaWinT1;
            
        if      (ELOs[0] <  1000)                    CoeffT1 = 80;
        else if (ELOs[0] >= 1000 && ELOs[0] <  2000) CoeffT1 = 50;
        else if (ELOs[0] >= 2000 && ELOs[0] <= 2400) CoeffT1 = 30;
        else                                         CoeffT1 = 20;
                
        if      (ELOs[1] <  1000)                    CoeffT2 = 80;
        else if (ELOs[1] >= 1000 && ELOs[0] <  2000) CoeffT2 = 50;
        else if (ELOs[1] >= 2000 && ELOs[0] <= 2400) CoeffT2 = 30;
        else                                         CoeffT2 = 20;
            
        if (Teams[0] == Winner) {
            NewELOs[0] = (int) (ELOs[0] + CoeffT1 * (1 - ProbaWinT1));
            NewELOs[1] = (int) (ELOs[1] + CoeffT2 * (0 - ProbaWinT2));
        }
        else {
            NewELOs[0] = (int) (ELOs[0] + CoeffT1 * (0 - ProbaWinT1));
            NewELOs[1] = (int) (ELOs[1] + CoeffT2 * (1 - ProbaWinT2));
        }
                     
        System.IO.File.WriteAllLines(Application.streamingAssetsPath + "/ELO/" + Teams[0] + ".elo", new string[] { NewELOs[0]+"" });
        System.IO.File.WriteAllLines(Application.streamingAssetsPath + "/ELO/" + Teams[1] + ".elo", new string[] { NewELOs[1] + "" });
    }





    static public int GetTeamElo(string team)
    {
       /* string[] lines = System.IO.File.ReadAllLines(Application.streamingAssetsPath + "/ELO/" + team + ".elo");
        foreach (string line in lines)
        {
            string[] tmp = line.Split('=');
            if (tmp[0].Equals("elo"))
                return int.Parse(tmp[1]);
        }
        */
        if (System.IO.File.Exists(Application.streamingAssetsPath + "/ELO/" + team + ".elo"))
        {
            string[] lines = System.IO.File.ReadAllLines(Application.streamingAssetsPath + "/ELO/" + team + ".elo");
            if (lines.Length == 0)
            {
                System.IO.File.WriteAllText(Application.streamingAssetsPath + "/ELO/" + team + ".elo", "1000");
                return 1000;
            }
            return int.Parse(lines[0]);
        }
        else
        {
            System.IO.File.WriteAllText(Application.streamingAssetsPath + "/ELO/" + team + ".elo","1000");
            string[] lines = System.IO.File.ReadAllLines(Application.streamingAssetsPath + "/ELO/" + team + ".elo");
            return int.Parse(lines[0]);
        }
        
    }
    
    static public KeyValuePair<string,List<Matchup>>  getStats(string team)
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
