using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHistoryDataRifle 
{
    public string metaUserId;
    public string metaUserName;
    public float timeSpent;

    public string gameMode;
    public float[] shotScores;
    public float sr1ScoreRifle, sr2ScoreRifle, sr3ScoreRifle;
    public float totalGameScoreRifle;
    public float avgSeriesScoreRifle;

    public int ShotsOnTarget, ShotsMissed;
    public int InnerTensCount;
    public string totalTimeSpentInGameMode;
    public float PersonalBestRifle;


    /// <summary>
    /// Rifle variable Changes
    /// </summary>
    /// <param name="ghUserid"></param>
    /// <param name="ghUserName"></param>
    /// <param name="ghGameMode"></param>
    /// <param name="ghShotScores"></param>
    /// <param name="ghSR1Score"></param>
    /// <param name="ghSR2Score"></param>
    /// <param name="ghSR3Score"></param>
    /// <param name="ghTotalScore"></param>
    /// <param name="ghShotsOnTarget"></param>
    /// <param name="ghShotsMissed"></param>
    /// <param name="ghAvgSRScore"></param>
    /// <param name="ghInnerTens"></param>
    /// <param name="ghTimeSpent"></param>
    /// <param name="ghPersonalBest"></param>


    public GameHistoryDataRifle(string ghUserid, string ghUserName, string ghGameMode, float[] ghShotScores,
                           float ghSR1Score, float ghSR2Score, float ghSR3Score, float ghTotalScore, int ghShotsOnTarget,
                           int ghShotsMissed, float ghAvgSRScore, int ghInnerTens, string ghTimeSpent, float ghPersonalBest)
    {
        this.metaUserId = ghUserid;
        this.metaUserName = ghUserName;
        this.gameMode = ghGameMode;

        this.shotScores = ghShotScores;
        this.sr1ScoreRifle = ghSR1Score;
        this.sr2ScoreRifle = ghSR2Score;
        this.sr3ScoreRifle = ghSR3Score;
        this.totalGameScoreRifle = ghTotalScore;
        this.avgSeriesScoreRifle = ghAvgSRScore;
        this.ShotsOnTarget = ghShotsOnTarget;
        this.ShotsMissed = ghShotsMissed;
        this.InnerTensCount = ghInnerTens;
        this.totalTimeSpentInGameMode = ghTimeSpent;
        this.PersonalBestRifle = ghPersonalBest;



    }



    public Dictionary<string, object> ToDictionary()
    {
        Dictionary<string, object> result = new Dictionary<string, object>();
        result["MetaUserID"] = metaUserId;
        result["MetaUserName"] = metaUserName;

        //all the game history as required
        result["GameMode"] = gameMode;
        result["AllShotsScore"] = shotScores;
        result["Series1Score"] = sr1ScoreRifle;
        result["Series2Score"] = sr2ScoreRifle;
        result["Series3Score"] = sr3ScoreRifle;
        result["TotalGameScore"] = totalGameScoreRifle;
        result["AvrageSeriesScore"] = avgSeriesScoreRifle;
        result["NoOfShotsMissed"] = ShotsMissed;
        result["NoOfShotsHitOnTarget"] = ShotsOnTarget;
        result["NoOfInnerTensInMatch"] = InnerTensCount;
        result["TotalTimeSpentinThisGameMode"] = totalTimeSpentInGameMode;
        result["PersonalGameBest"] = PersonalBestRifle;
        return result;
    }

}
