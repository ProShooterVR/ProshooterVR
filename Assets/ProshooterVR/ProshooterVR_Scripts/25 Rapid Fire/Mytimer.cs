using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Mytimer : MonoBehaviour
{
    public float totalTime = 10f; // Total time for the countdown
    public float currentTime; // Current time remaining
    public TextMeshProUGUI countdownText; // Reference to the UI Text component
    bool isReady;
    private void awake()
    {
       
    }
    private void Start()
    {
        currentTime = totalTime;
        isReady = false;
    }
    private void OnEnable()
    {
        if (isReady == true)
        {
            currentTime = RapidFireGunManager.Instance.currentTimerValue;
        }
    }
    private void Update()
    {

        
        // Update the countdown timer
        currentTime -= Time.deltaTime;
        
        // Check if the countdown has reached zero
        if (currentTime <= 0f)
        {
            currentTime = 0f;
            isReady = true;
            RapidFireGunManager.Instance.callGameState();
            this.gameObject.SetActive(false);

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
}
