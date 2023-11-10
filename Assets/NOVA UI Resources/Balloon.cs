using System.Collections;
using UnityEngine;

public class Balloon : MonoBehaviour
{
    public static Balloon Instance;
    private void Awake()
    {
        Instance = this;
    }

    public float riseSpeed = 0.5f; // Speed at which the balloon rises
    public float resetHeight = 10.0f; // Height at which the balloon resets

    private void Update()
    {
        RiseBalloon();

        // Reset the balloon's position when it goes out of view
        if (transform.position.y > resetHeight)
        {
            ResetBalloonPosition();
        }
    }

    public void RiseBalloon()
    {
        // Move the balloon up
        transform.Translate(Vector3.up * riseSpeed * Time.deltaTime);
    }

    public void ResetBalloonPosition()
    {
        // Reset the balloon's position to the bottom
        Vector3 newPosition = new Vector3(transform.position.x, -resetHeight, transform.position.z);
        transform.position = newPosition;
    }
}
