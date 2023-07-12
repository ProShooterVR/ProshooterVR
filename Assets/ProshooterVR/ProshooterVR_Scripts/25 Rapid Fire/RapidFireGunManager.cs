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

    public TextMeshProUGUI timerValTxt;


    public bool isScoreUpdated;


    public bool isReloaded, isReloading;

    public Animator animator;
    public string animationName;

    public AudioSource audioSrc;
    public AudioClip[] pistol;
    public int timerValue;

    public bool resetData;

    public AudioClip attention;
    public AudioSource RFAudioSource;


    public GameObject StartTimer;
    public GameObject redLights, greenLights, noLights;
    public bool startGame;

    public int SeriesCounter;
    public float BufferTime;
    public bool countingScore;


    private void Awake()
    {
        Instance = this;

    }
    // Start is called before the first frame update
    void Start()
    {
        timerValue = 8;
        resetData = true;
        redLights.SetActive(false);
        greenLights.SetActive(false);
        noLights.SetActive(true);

        StartCoroutine(StartTimerStart());
        SeriesCounter = 0;
        countingScore = false;
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
            if(startGame == true)
            {
             RapidFireGunManager.Instance.isReloaded = true;

            if (SeriesCounter == 0)
                {
                    stage1Start();
                    SeriesCounter++;
                }
                else if (SeriesCounter == 1)
                {
                    stage2Start();
                    SeriesCounter++;

                }
                else if (SeriesCounter == 2)
                {
                    stage3Start();
                    SeriesCounter++;
                }
            startGame = false;
            }

            
        
    }

    public void shotFired(Vector3 pos, float scoreVal, float direction,int screenNo)
    {
        RapidFireUIManager.Instance.updateShotScreen(pos, scoreVal, direction,screenNo);

    }


    IEnumerator StartTimerStart()
    {
        yield return new WaitForSeconds(3);
        StartTimer.SetActive(true);

    }

    // return after timer value specified

    public void stage1Start()
    {
        startSeries01();
    }

    public void stage2Start()
    {
        startSeries02();
    }

    public void stage3Start()
    {
        startSeries03();
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
        redLights.SetActive(true);
        greenLights.SetActive(false);
        noLights.SetActive(false);
        yield return new WaitForSeconds(7f);

        countingScore = true;
        redLights.SetActive(false);
        greenLights.SetActive(true);
        noLights.SetActive(false);
        yield return new WaitForSeconds(timeVal);

        countingScore = false;

        redLights.SetActive(false);
        greenLights.SetActive(false);
        noLights.SetActive(true);

        yield return new WaitForSeconds(BufferTime);
        StartTimer.SetActive(true);
    }


}
    