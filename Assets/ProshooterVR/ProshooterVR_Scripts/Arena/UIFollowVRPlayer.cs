using BNG;
using System.Collections;
using UnityEngine;

public class UIFollowVRPlayer : MonoBehaviour
{
    public Transform playerHead; // Assign the VR Camera or Player Head Transform in the inspector
    public float distanceFromPlayer = 2.0f; // How far in front of the player the UI should appear
    public Vector3 offsetFromForward = new Vector3(0, -0.2f, 0); // Adjust if you want the UI to be higher, lower, or to the side
    public float torsoHeightOffset = -0.5f; // Height offset from the head position to approximate the torso level
    public float smoothSpeed = 10f; // Speed of smoothing for position and rotation
    public float maxHeightAboveTorso = 0.2f; // Limit the maximum height above the calculated torso level
    public GameObject uiObject; // The UI GameObject that should appear/disappear

    private bool uiActive = false;
    private Vector3 activePosition;
    private Quaternion activeRotation;

    private void Update()
    {
        if (playerHead == null) return;

        if (InputBridge.Instance.XButtonDown && !uiActive)
        {
            // Start appearing
            StartCoroutine(Appear());
        }
        else if (!InputBridge.Instance.XButtonDown && uiActive)
        {
            // Start disappearing
            StartCoroutine(Disappear());
        }

        if (uiActive)
        {
            // Keep the UI facing the player once active
            FacePlayer();
        }
    }

    IEnumerator Appear()
    {
        uiActive = true;
        uiObject.SetActive(true);

        Vector3 targetPosition = CalculateTargetPosition();
        Quaternion targetRotation = CalculateTargetRotation();

        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smoothSpeed);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * smoothSpeed);
            yield return null;
        }

        activePosition = targetPosition;
        activeRotation = targetRotation;
    }

    IEnumerator Disappear()
    {
        uiActive = false;

        while (uiObject.activeSelf)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, Time.deltaTime * smoothSpeed);
            if (transform.localScale.magnitude < 0.01f)
            {
                uiObject.SetActive(false);
            }
            yield return null;
        }

        // Reset scale for next appearance
        transform.localScale = Vector3.one;
    }

    private void FacePlayer()
    {
        transform.position = activePosition;
        transform.rotation = activeRotation;
    }

    private Vector3 CalculateTargetPosition()
    {
        Vector3 targetPosition = playerHead.position + (playerHead.forward * distanceFromPlayer) + offsetFromForward;
        float desiredYPosition = playerHead.position.y + torsoHeightOffset;
        targetPosition.y = Mathf.Clamp(targetPosition.y, desiredYPosition, desiredYPosition + maxHeightAboveTorso);
        return targetPosition;
    }

    private Quaternion CalculateTargetRotation()
    {
        Vector3 directionToFace = playerHead.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(directionToFace);
        targetRotation *= Quaternion.Euler(0, 180, 0);
        return targetRotation;
    }
}
