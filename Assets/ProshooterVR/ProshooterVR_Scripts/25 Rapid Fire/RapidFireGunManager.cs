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



    // Start is called before the first frame update
    void Start()
    {
        timerValue = 8;
    }

    // Update is called once per frame
    void Update()
    {
        if (isRankedMode == true)
        {
            if (timerValue > 0 && noOfShotsFired < 5)
            {
                timeRemaining -= Time.deltaTime;

                float minutes = Mathf.FloorToInt(timeRemaining / 60);
                float seconds = Mathf.FloorToInt(timeRemaining % 60);
                timerValTxt.text = string.Format("{0:00}:{1:00}", minutes, seconds);

            }
            else
            {

                Debug.Log("Time has run out!");
            }
        }
    }


}
