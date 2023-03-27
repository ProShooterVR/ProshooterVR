using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHistoryData
{
    public string metaUserId;
    public string metaUserName;
    public float timeSpent;

    public string gameMode;
    public int[] shotScores;
    public int sr1Score, sr2Score, sr3Score;
    public int totalGameScore;
    public int avgSeriesScore;

    public int ShotsOnTarget,ShotsMissed;
    public int InnerTensCount;
    public string totalTimeSpentInGameMode;
    public int PersonalBest;

   

    public GameHistoryData(string ghUserid, string ghUserName,string ghGameMode,int[] ghShotScores,
                           int ghSR1Score, int ghSR2Score, int ghSR3Score,int ghTotalScore,int ghShotsOnTarget,
                           int ghShotsMissed,int ghAvgSRScore,int ghInnerTens,string ghTimeSpent,int ghPersonalBest )
    {
        this.metaUserId = ghUserid;
        this.metaUserName = ghUserName;
        this.gameMode = ghGameMode;

        this.shotScores = ghShotScores;
        this.sr1Score = ghSR1Score;
        this.sr2Score = ghSR2Score;
        this.sr3Score = ghSR3Score;
        this.totalGameScore = ghTotalScore;
        this.avgSeriesScore = ghAvgSRScore;
        this.ShotsOnTarget = ghShotsOnTarget;
        this.ShotsMissed = ghShotsMissed;
        this.InnerTensCount = ghInnerTens;
        this.totalTimeSpentInGameMode = ghTimeSpent;
        this.PersonalBest = ghPersonalBest;



    }

    public Dictionary<string, object> ToDictionary()
    {
        Dictionary<string, object> result = new Dictionary<string, object>();
        result["MetaUserID"] = metaUserId;
        result["MetaUserName"] = metaUserName;

        //all the game history as required
        result["GameMode"] = gameMode;
        result["AllShotsScore"] = shotScores;
        result["Series1Score"] = sr1Score;
        result["Series2Score"] = sr2Score;
        result["Series3Score"] = sr3Score;
        result["TotalGameScore"] = totalGameScore;
        result["AvrageSeriesScore"] = avgSeriesScore;
        result["NoOfShotsMissed"] = ShotsMissed;
        result["NoOfShotsHitOnTarget"] = ShotsOnTarget;
        result["NoOfInnerTensInMatch"] = InnerTensCount;
        result["TotalTimeSpentinThisGameMode"] = totalTimeSpentInGameMode;
        result["PersonalGameBest"] = PersonalBest;
        return result;
    }

    
}

