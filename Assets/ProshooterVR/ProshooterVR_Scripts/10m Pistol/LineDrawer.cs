using UnityEngine;

public class LineDrawer : MonoBehaviour
{
    public GameObject startPoint, endPoint;
    private LineRenderer lineRenderer;

    void Start()
    {
        // Ensure LineRenderer component is attached to the GameObject
        lineRenderer = this.gameObject.GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;

        // Set initial positions
        lineRenderer.SetPosition(0, startPoint.transform.position);
        lineRenderer.SetPosition(1, endPoint.transform.position);
    }

    void Update()
    {
        // Update line positions in case the connected GameObjects move
        lineRenderer.SetPosition(0, startPoint.transform.position);
        lineRenderer.SetPosition(1, endPoint.transform.position);
    }
}
