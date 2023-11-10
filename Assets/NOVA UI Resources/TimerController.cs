using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerController : MonoBehaviour
{
    public static TimerController Instance;

    public void Awake()
    {
        Instance = this;
    }
    public TextMeshPro timerText;
    public float totalTime = 90.0f;

    public MonoBehaviour[] CubeSpawnerScript;

    public float currentTime;

    public void Start()
    {
        currentTime = totalTime;
    }

    public void Update()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            UpdateTimerUI();
        }
        else
        {
            currentTime = 0;
            foreach (var script in CubeSpawnerScript)
            {
                if (script != null)
                {
                    script.enabled = false; // Disable the attached script
                }
            }
        }
    }

    public void UpdateTimerUI()
    {
        timerText.text = Mathf.Ceil(Mathf.Max(0, currentTime)).ToString();
    }
}
