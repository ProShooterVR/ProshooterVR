using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHistoryDataRifle 
{
    public string meta_id;
    public string meta_username;
    public string game_mode;
    public float zoneA_mult;
    public float zoneB_mult;
    public float zoneC_mult;
    public float diffculty_mult;
    public int[] shots;
    public float avg_score;
    public float no_InTens;
    public float no_shots_target;
    public float no_shots_missed;
    public float personal_best;
    public float sr1Score;
    public float sr2Score;
    public float sr3Score;
    public float total_game_score;
    public string total_timespent;

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


    public GameHistoryDataRifle(string ghUserid, string ghUserName, string ghGameMode, int[] ghShotScores,
                           int ghSR1Score, int ghSR2Score, int ghSR3Score, float ghTotalScore, int ghShotsOnTarget,
                           int ghShotsMissed, int ghAvgSRScore, int ghInnerTens, string ghTimeSpent, int ghPersonalBest, float ghzoneAmult,
                            float ghzoneBmult, float ghzoneCmult, float diffcult_mult)
    {
        this.meta_id = ghUserid;
        this.meta_username = ghUserName;
        this.game_mode = ghGameMode;
        this.zoneA_mult = ghzoneAmult;
        this.zoneB_mult = ghzoneBmult;
        this.zoneC_mult = ghzoneCmult;
        this.diffculty_mult = diffcult_mult;
        this.shots = ghShotScores;
        this.sr1Score = ghSR1Score;
        this.sr2Score = ghSR2Score;
        this.sr3Score = ghSR3Score;
        this.total_game_score = ghTotalScore;
        this.avg_score = ghAvgSRScore;
        this.no_shots_target = ghShotsOnTarget;
        this.no_shots_missed = ghShotsMissed;
        this.no_InTens = ghInnerTens;
        this.total_timespent = ghTimeSpent;
        this.personal_best = ghPersonalBest;



    }




}
