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

    public int[] Scores;
    public int totalShotsAllowed;
    public string gameMode;

    public int noOfSeries;
    public int currentShotScore;
    public int totalGameScore;
    public int sr1Score, sr2Score, sr3Score;
    public int noOfShotsOnTarget, noOfShotsMissed;
    public int avgSrScore;
    public int noOfInnerTens;
    public string totalTimeSpent;
    public int personalAmaBest, personalSemiProBest, personalProBest;
    public int personalGameBest;


    // Start is called before the first frame update
    void Start()
    {
        Scores = new int[totalShotsAllowed];
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
