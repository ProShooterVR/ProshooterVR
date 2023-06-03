using System.Collections.Generic;

public class GameHistoryDataPistol
{
    public string metaUserId;
    public string metaUserName;
    public float timeSpent;

    public string gameMode;
    public int[] shotScores;
    public int sr1ScorePistol, sr2ScorePistol, sr3ScorePistol;
    public int totalGameScorePistol;
    public int avgSeriesScorePistol;

    public int ShotsOnTarget,ShotsMissed;
    public int InnerTensCount;
    public string totalTimeSpentInGameMode;
    public int PersonalBestPistol;


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
   

    public GameHistoryDataPistol(string ghUserid, string ghUserName,string ghGameMode,int[] ghShotScores,
                           int ghSR1Score, int ghSR2Score, int ghSR3Score,int ghTotalScore,int ghShotsOnTarget,
                           int ghShotsMissed,int ghAvgSRScore,int ghInnerTens,string ghTimeSpent,int ghPersonalBest )
    {
        this.metaUserId = ghUserid;
        this.metaUserName = ghUserName;
        this.gameMode = ghGameMode;

        this.shotScores = ghShotScores;
        this.sr1ScorePistol = ghSR1Score;
        this.sr2ScorePistol = ghSR2Score;
        this.sr3ScorePistol = ghSR3Score;
        this.totalGameScorePistol = ghTotalScore;
        this.avgSeriesScorePistol = ghAvgSRScore;
        this.ShotsOnTarget = ghShotsOnTarget;
        this.ShotsMissed = ghShotsMissed;
        this.InnerTensCount = ghInnerTens;
        this.totalTimeSpentInGameMode = ghTimeSpent;
        this.PersonalBestPistol = ghPersonalBest;



    }

   

    public Dictionary<string, object> ToDictionary()
    {
        Dictionary<string, object> result = new Dictionary<string, object>();
        result["MetaUserID"] = metaUserId;
        result["MetaUserName"] = metaUserName;

        //all the game history as required
        result["GameMode"] = gameMode;
        result["AllShotsScore"] = shotScores;
        result["Series1Score"] = sr1ScorePistol;
        result["Series2Score"] = sr2ScorePistol;
        result["Series3Score"] = sr3ScorePistol;
        result["TotalGameScore"] = totalGameScorePistol;
        result["AvrageSeriesScore"] = avgSeriesScorePistol;
        result["NoOfShotsMissed"] = ShotsMissed;
        result["NoOfShotsHitOnTarget"] = ShotsOnTarget;
        result["NoOfInnerTensInMatch"] = InnerTensCount;
        result["TotalTimeSpentinThisGameMode"] = totalTimeSpentInGameMode;
        result["PersonalGameBest"] = PersonalBestPistol;
        return result;
    }

    
}

