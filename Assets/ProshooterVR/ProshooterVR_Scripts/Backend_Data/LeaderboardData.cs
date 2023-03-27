using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardData
{

    public string metaUserName;
    public int personalBest;
    public int innerTens;
    public int totalScore;


    public LeaderboardData(string name, int ptotalScore, int pinnerTens, int ppersonalBest)
    {
        this.totalScore = ptotalScore;
        this.metaUserName = name;
        this.innerTens = pinnerTens;
        this.personalBest = ppersonalBest;


    }



    public Dictionary<string, object> ToDictionary()
    {
        Dictionary<string, object> result = new Dictionary<string, object>();
        result["TotalScore"] = totalScore;
        result["Name"] = metaUserName;
        result["InnerTens"] = innerTens;
        result["BestScore"] = personalBest;
        return result;
    }
}

