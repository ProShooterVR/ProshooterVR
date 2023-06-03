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


    private void Awake()
    {
        Instance = this;

    }
    // Start is called before the first frame update
    void Start()
    {
        timerValue = 8;
        resetData = true;
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

    public void shotFired(Vector3 pos, float scoreVal, float direction,int screenNo)
    {
        RapidFireUIManager.Instance.updateShotScreen(pos, scoreVal, direction,screenNo);

    }
}
