using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class RapidFireGunManager : MonoBehaviour
{
    public static RapidFireGunManager Instance;

    public bool isRifleMode, isPistolMode, isRapidFireMode;

    public TextMeshProUGUI timer;

    public bool isPracticeMode, isRankedMode;

    public GameObject pistolObj;

    public int noOfShotsFired;

    float timeRemaining;



    public bool isScoreUpdated;


    public bool isReloaded, isReloading;


    public int timerValue;

    public bool resetData;



    public GameObject StartTimer;
    public GameObject redLights, greenLights, noLights;
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

    public enum gamestate
    {
        load,
        attention,
        sr1,
        sr2,
        sr3,
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
        noLights.SetActive(true);

        StartCoroutine(StartTimerStart());
        SeriesCounter = 0;
        stageCounter = 0;
        countingScore = false;
        stageChanged = false;
        seriesStarted = false;
        resetStage();
    }

    public void resetStage()
    {
        state = gamestate.load;
        SeriesCounter = 0;

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


    public void callGameState()
    {
        switch (state)
        {
            case gamestate.load:
                currentTimerValue = 10f;
                    StartCoroutine(weaponPrepareCall());
                break;
            case gamestate.attention:
                    StartCoroutine(attentionCall());
                break;
            case gamestate.sr1:
                    series1Start();
                break;
            case gamestate.sr2:
                     series2Start();
                break;
            case gamestate.sr3:
                     series3Start();
                break;
            case gamestate.end:
                break;

        }
    }

    public void shotFired(Vector3 pos, float scoreVal, float direction,int screenNo)
    {
        RapidFireUIManager.Instance.updateShotScreen(pos, scoreVal, direction,screenNo);

    }

   

    IEnumerator weaponPrepareCall()
    {
        state = gamestate.attention;
        startSeries = true;
        float timeVal = InstructionManager.Instance.playInstruction(0);
        RapidFireUIManager.Instance.instructionTxt.text = RapidFireUIManager.Instance.instructions[0];

        yield return new WaitForSeconds(timeVal);

        yield return new WaitForSeconds(5f);

        timeVal = InstructionManager.Instance.playInstruction(1);

        RapidFireUIManager.Instance.instructionTxt.text = RapidFireUIManager.Instance.instructions[1];

        yield return new WaitForSeconds(timeVal);
        currentTimerValue = 10f;
        StartTimer.SetActive(true);

        RapidFireUIManager.Instance.instructionTxt.text = "";

    }

    public IEnumerator attentionCall()
    {

        float timeVal = InstructionManager.Instance.playInstruction(2);
        RapidFireUIManager.Instance.instructionTxt.text = RapidFireUIManager.Instance.instructions[2];
        yield return new WaitForSeconds(timeVal);
        readyPosObj.SetActive(true);
        RapidFireGunManager.Instance.isReloaded = true;


    }
    IEnumerator StartTimerStart()
    {
        yield return new WaitForSeconds(3);
        StartTimer.SetActive(true);

    }

    // return after timer value specified

    public void series1Start()
    {
        
        startSeries01();
        SeriesCounter++;
        Debug.Log("Series 01");

    }

    public void series2Start()
    {
        startSeries02();
        SeriesCounter++;
        Debug.Log("Series 02");


    }

    public void series3Start()
    {
        startSeries03();
        SeriesCounter++;
        stageCounter++;
        if (stageCounter > 2)
        {
            state = gamestate.end;
        }
        else
        {
            resetStage();
        }

    }
    public void startSeries01()
    {
        StartCoroutine(series(8.2f));
    }

    public void startSeries02()
    {
        StartCoroutine(series(6.2f));
    }

    public void startSeries03()
    {
        StartCoroutine(series(4.2f));
    }

    IEnumerator series(float timeVal)
    {
        RapidFireUIManager.Instance.clearScreen();

        redLights.SetActive(true);
        greenLights.SetActive(false);
        noLights.SetActive(false);
        yield return new WaitForSeconds(7f);
        readyPosObj.SetActive(false);
        RapidFireGunManager.Instance.foulTimer = false;
        RapidFireGunManager.Instance.seriesFoul = false;

        countingScore = true;
        redLights.SetActive(false);
        greenLights.SetActive(true);
        noLights.SetActive(false);
        yield return new WaitForSeconds(timeVal);
        countingScore = false;

        redLights.SetActive(false);
        greenLights.SetActive(false);
        noLights.SetActive(true);

        float val = InstructionManager.Instance.playInstruction(3);
        RapidFireUIManager.Instance.instructionTxt.text = RapidFireUIManager.Instance.instructions[3];
        Debug.Log("okay okay count");
        
        currentTimerValue = 30f;
        state = gamestate.load;
        RapidFireGunManager.Instance.seriesStarted = false;
        RapidFireGunManager.Instance.isReloaded = false;
        StartTimer.SetActive(true);
        
    }


}
    