using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Mytimer : MonoBehaviour
{
    public float totalTime; // Total time for the countdown
    public float currentTime; // Current time remaining
    public TextMeshPro countdownText; // Reference to the UI Text component
    bool isReady;

    bool isCalled;
    private void awake()
    {
       
    }
    private void Start()
    {
        currentTime = totalTime;
        isReady = false;
        isCalled = false;
    }
    private void OnEnable()
    {
        if (isReady == true)
        {
            currentTime = RapidFireGunManager.Instance.currentTimerValue;
            totalTime = RapidFireGunManager.Instance.currentTimerValue;
         
        }
    }
    private void Update()
    {

        
        // Update the countdown timer
        currentTime -= Time.deltaTime;
        if (currentTime <= totalTime / 2)
        {
            if (RapidFireGunManager.Instance.callInGameSounds == true) {
                if (RapidFireGunManager.Instance.isRankedMode == true)
                { RapidFireGunManager.Instance.callIngamesounds(RapidFireGunManager.Instance.stageCounter);
                    RapidFireGunManager.Instance.callInGameSounds = false;
                }
                if (RapidFireGunManager.Instance.isPracticeMode == true)
                {
                    RapidFireGunManager.Instance.callIngamesoundsPractice(RapidFireGunManager.Instance.practiceSeriesSound);
                    RapidFireGunManager.Instance.callInGameSounds = false;
                }
            }
        }
            // Check if the countdown has reached zero
            if (currentTime <= 0f)
        {
            currentTime = 0f;
            isReady = true;
            RapidFireGunManager.Instance.callGameState();
            endTimer();
            this.gameObject.GetComponent<Mytimer>().enabled = false;

            // Do something when the countdown reaches zero (e.g., game over)
        }
        RapidFireGunManager.Instance.countingScore = false;

        // Update the UI text
        countdownText.text = currentTime.ToString("F1"); // Display one decimal place

        // Optional: Change the color of the text based on time remaining
        if (currentTime <= 1f)
        {
            countdownText.color = Color.red;
        }
        else
        {
            countdownText.color = Color.white;
        }
    }

    public void endTimer()
    {
        countdownText.text = "-- : --";
    }
}
