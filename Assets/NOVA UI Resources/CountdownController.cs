using UnityEngine;
using UnityEngine.UI;

public class CountdownController : MonoBehaviour
{
    [SerializeField]
    public GameObject[] countdownUIs; // Assign the UI panels in the Inspector (3, 2, 1, Go)
    private int currentCountdownIndex = 0;
    private bool isCounting = false;

    public void StartCountdown()
    {
        if (!isCounting)
        {
            isCounting = true;
            currentCountdownIndex = 0;
            ShowNextCountdown();
        }
    }

    private void ShowNextCountdown()
    {
        if (currentCountdownIndex < countdownUIs.Length)
        {
            // Disable all UI panels
            foreach (GameObject ui in countdownUIs)
            {
                ui.SetActive(false);
            }

            // Enable the current countdown UI
            countdownUIs[currentCountdownIndex].SetActive(true);

            // Move to the next countdown number
            currentCountdownIndex++;

            // If the countdown is not finished yet, call the ShowNextCountdown method after 1 second
            if (currentCountdownIndex < countdownUIs.Length)
            {
                Invoke("ShowNextCountdown", 1f);
            }
            else
            {
                // The countdown is finished, hide all UI panels after showing "Go!"
                Invoke("HideAllCountdownUIs", 1f);
            }
        }
    }

    private void HideAllCountdownUIs()
    {
        isCounting = false;
        // Hide all UI panels
        foreach (GameObject ui in countdownUIs)
        {
            ui.SetActive(false);
        }
    }
}
