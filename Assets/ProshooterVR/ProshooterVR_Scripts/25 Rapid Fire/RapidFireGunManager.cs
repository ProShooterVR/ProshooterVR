using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using NovaSamples.Inventory;


public class RapidFireGunManager : MonoBehaviour
{
    public static RapidFireGunManager Instance;



    public bool isPracticeMode, isRankedMode;


    public int noOfShotsFired;

    float timeRemaining;



    public bool isScoreUpdated;


    public bool isReloaded, isReloading;


    public int timerValue;

    public bool resetData;



    public GameObject StartTimer;
    public GameObject redLights, greenLights;
    public bool startGame, startSeries;

    public int SeriesCounter;
    public float BufferTime;
    public bool countingScore;

    public float currentTimerValue;
    public GameObject readyPosObj;
    public bool seriesFoul,foulTimer;

    public int stageCounter;
    public bool stageChanged;
    public bool seriesStarted;

    public int series1Score, series2Score, series3Score;
    public int series4Score, series5Score, series6Score;

    public int currentSeriesScore;
    public float round1Timer, round2Timer, round3Timer,restTimer;
    public int round1Score, round2Score, round3Score;
    public int totalGameScore;

    public int shotsOnTarget, shotsMissed;
    public bool callInGameSounds;
    public enum gamestate
    {
        load,
        attention,
        round1,
        round2,
        round3,
        end
    }

    public gamestate state;

    private void Awake()
    {
        Instance = this;
        currentTimerValue = 10f;


    }
    // Start is called before the first frame update
    void Start()
    {
        
        timerValue = 8;
        currentTimerValue = 10f;
        resetData = true;
        redLights.SetActive(false);
        greenLights.SetActive(false);

        SeriesCounter = 0;
        stageCounter = 0;
        countingScore = false;
        stageChanged = false;
        seriesStarted = false;
        resetStage();

        series1Score = 0;
        series2Score = 0;
        series3Score = 0;
        series4Score = 0;
        series5Score = 0;
        series6Score = 0;
        currentSeriesScore = 0;
        round1Score = 0;
        round2Score = 0;
        round3Score = 0;

        totalGameScore = 0;
        callInGameSounds = false;
    }

    public void resetStage()
    {
        state = gamestate.load;
       
        SeriesCounter = 0;
        if (stageCounter == 0)
        {
            RapidFireUIManager.Instance.round1Lble.SetActive(true);
            RapidFireUIManager.Instance.round2Lble.SetActive(false);
            RapidFireUIManager.Instance.round3Lble.SetActive(false);


            RapidFireUIManager.Instance.roundDisplay[0].SetActive(true);
            RapidFireUIManager.Instance.roundDisplay[1].SetActive(false);
        }
        else if (stageCounter == 1)
        {
            RapidFireUIManager.Instance.roundDisplay[1].SetActive(true);
            RapidFireUIManager.Instance.roundDisplay[0].SetActive(false);
            RapidFireUIManager.Instance.round1Lble.SetActive(false);
            RapidFireUIManager.Instance.round2Lble.SetActive(true);
            RapidFireUIManager.Instance.round3Lble.SetActive(false);

        }
        else if (stageCounter == 2)
        {
            RapidFireUIManager.Instance.roundDisplay[2].SetActive(true);
            RapidFireUIManager.Instance.roundDisplay[1].SetActive(false);
            RapidFireUIManager.Instance.round1Lble.SetActive(false);
            RapidFireUIManager.Instance.round2Lble.SetActive(true);
            RapidFireUIManager.Instance.round3Lble.SetActive(true);

        }

        
    }

    // Update is called once per frame
    void Update()
    {
        
            if (timerValue > 0 && noOfShotsFired < 5)
            {
                timeRemaining -= Time.deltaTime;

                float minutes = Mathf.FloorToInt(timeRemaining / 60);
                float seconds = Mathf.FloorToInt(timeRemaining % 60);
                //timerValTxt.text = string.Format("{0:00}:{1:00}", minutes, seconds);

            }
        

            
        
    }


    public void callIngamesounds(int round)
    {
        switch (round)
        {
            case 0:
                
                RF_FmodSetup.Instance.EightSecSeriesLoadEvent();
                RapidFireUIManager.Instance.timeSer1Txt.text = round1Timer.ToString() + "Seconds";
                RapidFireUIManager.Instance.timeSer2Txt.text = round1Timer.ToString() + "Seconds";

                break;
            case 1:
                RF_FmodSetup.Instance.SixSecSeriesLoadEvent();
                RapidFireUIManager.Instance.timeSer1Txt.text = round2Timer.ToString() + "Seconds";
                RapidFireUIManager.Instance.timeSer2Txt.text = round2Timer.ToString() + "Seconds";
                break;
            case 2:
                RF_FmodSetup.Instance.FourSecSeriesLoadEvent();
                RapidFireUIManager.Instance.timeSer1Txt.text = round3Timer.ToString() + "Seconds";
                RapidFireUIManager.Instance.timeSer2Txt.text = round3Timer.ToString() + "Seconds";
                break;
            
        }
    }

    public void callGameState()
    {
        switch (state)
        {
            case gamestate.load:
                    RF_FmodSetup.Instance.AthletesToTheLineLoadEvent();
                    currentTimerValue = 10f;
                    StartCoroutine(weaponPrepareCall());
                break;
            case gamestate.attention:
                    StartCoroutine(attentionCall());
                break;
            case gamestate.round1:
                    series1Start();
                break;
            case gamestate.round2:
                    series2Start();
                break;
            case gamestate.end:
                break;

        }
    }

    public void shotFired(Vector3 pos, float scoreVal, float direction,int screenNo)
    {
        RapidFireUIManager.Instance.updateShotScreen(pos, scoreVal, direction,screenNo);
        RapidFireEndSessionManager.Instance.updateEndSessionShotEndScreen(pos, scoreVal, direction, screenNo);

    }

   public void updateMatchData()
    {
        RayManager.Instance.EnableRey();

        RapidFireUIManager.Instance.endSessionScreen.SetActive(true);
        RapidFireEndSessionManager.Instance.enableTargetScores();

        RapidFireEndSessionManager.Instance.userNameTxt.text = "";
        RapidFireEndSessionManager.Instance.srs1ScoreTxt.text = series1Score.ToString();
        RapidFireEndSessionManager.Instance.srs2ScoreTxt.text = series2Score.ToString();
        RapidFireEndSessionManager.Instance.srs3ScoreTxt.text = series3Score.ToString();
        RapidFireEndSessionManager.Instance.srs4ScoreTxt.text = series4Score.ToString();
        RapidFireEndSessionManager.Instance.srs5ScoreTxt.text = series5Score.ToString();
        RapidFireEndSessionManager.Instance.srs6ScoreTxt.text = series6Score.ToString();

        int avgScore = (series1Score + series2Score + series3Score + series4Score + series5Score + series6Score) / 6;
        RapidFireEndSessionManager.Instance.avgScoreTxt.text = avgScore.ToString();
        RapidFireEndSessionManager.Instance.round1ScoreTxt.text = round1Score.ToString();
        RapidFireEndSessionManager.Instance.round2ScoreTxt.text = round2Score.ToString();

        //   RapidFireEndSessionManager.Instance.innerTText.text = innerTno.ToString("F1");

        RapidFireEndSessionManager.Instance.shotsHitTxt.text = shotsOnTarget.ToString();
        RapidFireEndSessionManager.Instance.shotsmissTxt.text = shotsMissed.ToString();
        RapidFireEndSessionManager.Instance.totalgameScoretxt.text = totalGameScore.ToString();

        //RapidFireEndSessionManager.Instance.timeSpentTxt.text = totalGameTime.ToString("F1");
    }

    IEnumerator weaponPrepareCall()
    {
        state = gamestate.attention;
        startSeries = true;
        RapidFireGunManager.Instance.callInGameSounds = true;
        //float timeVal = InstructionManager.Instance.playInstruction(0);

        RapidFireUIManager.Instance.instructionTxt.text = RapidFireUIManager.Instance.instructions[0];

        //yield return new WaitForSeconds(timeVal);

        yield return new WaitForSeconds(5f);

        //timeVal = InstructionManager.Instance.playInstruction(1);

         RapidFireUIManager.Instance.instructionTxt.text = RapidFireUIManager.Instance.instructions[1];

        //yield return new WaitForSeconds(timeVal);
        currentTimerValue = 10f;
        StartTimer.GetComponent<Mytimer>().enabled = true;

        RapidFireUIManager.Instance.instructionTxt.text = "";

    }

    public IEnumerator attentionCall()
    {

        //float timeVal = InstructionManager.Instance.playInstruction(2);
        RF_FmodSetup.Instance.AttentionEvent();
        RapidFireUIManager.Instance.instructionTxt.text = RapidFireUIManager.Instance.instructions[2];
        yield return new WaitForSeconds(5f);
        readyPosObj.SetActive(true);
        RapidFireGunManager.Instance.isReloaded = true;


    }
   public IEnumerator StartTimerStart()
    {
        yield return new WaitForSeconds(3);
        StartTimer.GetComponent<Mytimer>().enabled = true;


    }

    // return after timer value specified

    public void series1Start()
    {
        
        startSeries01();
        Debug.Log("Series 01");
    }

    public void series2Start()
    {
        startSeries02();
        Debug.Log("Series 02");

    }

   
    public void startSeries01()
    {
        if(stageCounter == 0) {
            StartCoroutine(series(round1Timer));
        }else if (stageCounter == 1)
        {
            StartCoroutine(series(round2Timer));
        }
        else if (stageCounter == 2)
        {
            StartCoroutine(series(round3Timer));
        }

    }

    public void startSeries02()
    {
        if (stageCounter == 0)
        {
            StartCoroutine(series(round1Timer));
        }
        else if (stageCounter == 1)
        {
            StartCoroutine(series(round2Timer));
        }
        else if (stageCounter == 2)
        {
            StartCoroutine(series(round3Timer));
        }
    }

   

    IEnumerator series(float timeVal)
    {
        SeriesCounter++;

        RapidFireUIManager.Instance.clearScreen();

        redLights.SetActive(true);
        greenLights.SetActive(false);
        yield return new WaitForSeconds(7f);
        readyPosObj.SetActive(false);
        RapidFireGunManager.Instance.foulTimer = false;
        RapidFireGunManager.Instance.seriesFoul = false;

        countingScore = true;
        redLights.SetActive(false);
        greenLights.SetActive(true);
        yield return new WaitForSeconds(timeVal);
        countingScore = false;


        Debug.Log("Series Ended ::  " + SeriesCounter);
        

        redLights.SetActive(false);
        greenLights.SetActive(false);
        RF_FmodSetup.Instance.UnloadEvent();

        //float val = InstructionManager.Instance.playInstruction(3);
        //RapidFireUIManager.Instance.instructionTxt.text = RapidFireUIManager.Instance.instructions[3];


        currentTimerValue = restTimer;
        state = gamestate.load;
        RapidFireGunManager.Instance.seriesStarted = false;
        RapidFireGunManager.Instance.isReloaded = false;
        callInGameSounds = true;
        if (SeriesCounter == 2)
        {
            Debug.Log("Next Stage");
            if (stageCounter == 2)
            {
                updateMatchData();
                state = gamestate.end;
            }
            else
            {
                stageCounter++;
                yield return new WaitForSeconds(10f);

                StartTimer.GetComponent<Mytimer>().enabled = true;
                RapidFireUIManager.Instance.instructionTxt.text = "";

                resetStage();
            }
        }
        else
        {
            StartTimer.GetComponent<Mytimer>().enabled = true;
            RapidFireUIManager.Instance.instructionTxt.text = "";
        }
        yield return new WaitForSeconds(2f);
        Debug.Log("END OF SERIESSSS");

    }


}
    