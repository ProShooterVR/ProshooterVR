using TMPro;
using UnityEngine;

public class FPSDisplay : MonoBehaviour
{
    public float updateInterval = 0.5f; // Update interval in seconds
    private float accum = 0; // FPS accumulated over the interval
    private int frames = 0; // Frames drawn over the interval
    private float timeLeft; // Left time for current interval

    [SerializeField]
    public TextMeshPro textMeshPro; // Reference to the TextMeshPro Text component

    private void Start()
    {
        timeLeft = updateInterval;

        // Get the reference to the TextMeshPro Text component
        textMeshPro = GetComponent<TextMeshPro>();
        if (textMeshPro == null)
        {
            Debug.LogError("FPSDisplay script requires a TextMeshPro Text component!");
            enabled = false;
        }
    }

    private void Update()
    {
        timeLeft -= Time.deltaTime;
        accum += Time.timeScale / Time.deltaTime;
        frames++;

        // Interval ended, update FPS display
        if (timeLeft <= 0.0f)
        {
            // Calculate average FPS
            float fps = accum / frames;

            // Update the text of the TextMeshPro Text component to display FPS
            textMeshPro.text = string.Format("{0:F2} FPS", fps);

            timeLeft = updateInterval;
            accum = 0;
            frames = 0;
        }
    }
}
