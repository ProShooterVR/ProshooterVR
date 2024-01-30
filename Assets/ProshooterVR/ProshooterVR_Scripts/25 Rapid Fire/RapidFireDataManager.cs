using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RapidFireDataManager : MonoBehaviour
{

    public static RapidFireDataManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public int series1Score, series2Score, series3Score;
    public int series4Score, series5Score, series6Score;
    public float acgScore;
    public int currentSeriesScore;
    public float round1Timer, round2Timer, round3Timer, restTimer;
    public int round1Score, round2Score, round3Score;
    public int totalGameScore;
    public float[] ScoresRapidFire;
    public int shotsOnTarget, shotsMissed;

    public int personalAmaBestRF, personalSemiProBestRF, personalProBestRF;
    public int personalGameBestRF;
    // Start is called before the first frame update
    void Start()
    {
        ScoresRapidFire = new float[30];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
