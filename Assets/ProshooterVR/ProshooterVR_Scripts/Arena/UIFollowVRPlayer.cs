using UnityEngine;


public class UIFollowVRPlayer : MonoBehaviour
{
    public Transform playerHead; // Assign the VR Camera or Player Head Transform in the inspector
    public float distanceFromPlayer = 2.0f; // How far in front of the player the UI should appear
    public Vector3 offsetFromForward = new Vector3(0, -0.2f, 0); // Adjust if you want the UI to be higher, lower, or to the side
    public float torsoHeightOffset = -0.5f; // Height offset from the head position to approximate the torso level
    public float smoothSpeed = 10f; // Speed of smoothing for position and rotation
    public float maxHeightAboveTorso = 0.2f; // Limit the maximum height above the calculated torso level

    private void Update()
    {
        if (playerHead == null) return;

        // Calculate the new position in front of the player
        Vector3 targetPosition = playerHead.position + (playerHead.forward * distanceFromPlayer) + offsetFromForward;

        // Calculate the desired torso level position
        float desiredYPosition = playerHead.position.y + torsoHeightOffset;
        // Ensure the UI does not go below or above the torso level by a certain threshold
        targetPosition.y = Mathf.Clamp(targetPosition.y, desiredYPosition, desiredYPosition + maxHeightAboveTorso);

        // Smoothly update position
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smoothSpeed);

        // Calculate a smooth rotation to face the player
        Vector3 directionToFace = playerHead.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(directionToFace);
        // Adjust for the UI's default rotation (180 degrees around Y-axis)
        targetRotation *= Quaternion.Euler(0, 180, 0);

        // Smoothly update rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * smoothSpeed);
    }
}
