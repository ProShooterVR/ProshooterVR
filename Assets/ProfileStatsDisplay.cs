using UnityEngine;
using UnityEngine.UI;

public class ProfileStatsDisplay : MonoBehaviour
{
    public Text fpsText;
    public Text frameTimeText;
    public Text trianglesText;
    public Text verticesText;
    public Text drawCallsText;
    public Text dynamicBatchesText;
    public Text staticBatchesText;
    public float updateInterval = 0.5f;

    private float lastInterval;
    private int frames = 0;

    private void Start()
    {
        lastInterval = Time.realtimeSinceStartup;
        frames = 0;
    }

    private void Update()
    {
        frames++;
        float timeNow = Time.realtimeSinceStartup;

        if (timeNow > lastInterval + updateInterval)
        {
            float fps = frames / (timeNow - lastInterval);
            fpsText.text = "FPS: " + fps.ToString("F2");
            //frameTimeText.text = "Frame Time (ms): " + (1000.0 / Mathf.Max(fps, 0.00001)).ToString("F2");
            //trianglesText.text = "Triangles: " + UnityEngine.Rendering.GraphicsStats.triangles;
            //verticesText.text = "Vertices: " + UnityEngine.Rendering.GraphicsStats.vertices;
            //drawCallsText.text = "Draw Calls: " + UnityEngine.Rendering.GraphicsStats.drawCalls;
            //dynamicBatchesText.text = "Dynamic Batches: " + UnityEngine.Rendering.GraphicsStats.dynamicBatches;
            //staticBatchesText.text = "Static Batches: " + UnityEngine.Rendering.GraphicsStats.staticBatches;

            frames = 0;
            lastInterval = timeNow;
        }
    }
}