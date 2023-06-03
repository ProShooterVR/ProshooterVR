using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunDataManager : MonoBehaviour
{

    public static GunDataManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    /// <summary>
    /// Data required to send to backend and save to UI fieldss
    /// </summary>

    public int[] ScoresPistol;
    public int totalShotsAllowed;
    public string gameMode;

    public int noOfSeries;
    public int currentShotScore;
    public int totalGameScorePistol;
    public int sr1ScorePistol, sr2ScorePistol, sr3ScorePistol;
    public int noOfShotsOnTarget, noOfShotsMissed;
    public int avgSrScorePistol;
    public int noOfInnerTens;
    public string totalTimeSpent;
    public int personalAmaBestPistol, personalSemiProBestPistol, personalProBestPistol;
    public int personalGameBestPistol;

    //Rifle variable change 
    public float[] ScoresRifle;

    public float sr1ScoreRifle, sr2ScoreRifle, sr3ScoreRifle;
    public float totalGameScoreRifle;

    public float avgSrScoreRifle;
    public float personalAmaBestRifle, personalSemiProBestRifle, personalProBestRifle;
    public float personalGameBestRifle;
    public float series1ScoreRifle, series2ScoreRifle, series3ScoreRifle;
    public float TotalScoreRifle, gameTotalScoreRifle;
    public float matchTotalScoreRifle;

    // Start is called before the first frame update
    void Start()
    {
        ScoresPistol = new int[totalShotsAllowed];
        ScoresRifle= new float[totalShotsAllowed];
        noOfSeries = 3;
        noOfShotsMissed = 0;
        noOfShotsOnTarget = 0;
        noOfInnerTens = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
