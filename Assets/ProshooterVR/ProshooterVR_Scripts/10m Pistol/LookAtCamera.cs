using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    void Update()
    {
        // Ensure there is a Camera in the scene
        if (Camera.main == null)
        {
            Debug.LogError("No main camera found in the scene!");
            return;
        }

        // Get the position of the camera
        Vector3 targetPosition = Camera.main.transform.position;

        // Make the object face the camera
        transform.LookAt(-targetPosition);
    }
}
