using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBehaviour : MonoBehaviour
{
    public Camera centerEyeCamera;
    public GameObject objectToControl;
    public float distanceInFrontOfCamera = 2.0f;
    public float delayBeforeActivating = 0.5f; // Delay in seconds
    public float animationDuration = 0.5f; // Duration of the animations

    private Vector3 originalScale;

    private void Awake()
    {
        if (objectToControl != null)
        {
            originalScale = objectToControl.transform.localScale;
            objectToControl.transform.localScale = Vector3.zero;
        }
    }

    private void OnEnable()
    {
        StartCoroutine(ActivateAndPositionAfterDelay(objectToControl));
    }

    private IEnumerator ActivateAndPositionAfterDelay(GameObject targetObject)
    {
        yield return new WaitForSeconds(delayBeforeActivating);

        if (targetObject != null && centerEyeCamera != null)
        {
            targetObject.transform.position = centerEyeCamera.transform.position + centerEyeCamera.transform.forward * distanceInFrontOfCamera;
            Vector3 directionToFace = (targetObject.transform.position - centerEyeCamera.transform.position).normalized;
            targetObject.transform.rotation = Quaternion.LookRotation(directionToFace);
            targetObject.SetActive(true); // Make sure to activate the GameObject here if it's not already

            StartCoroutine(PopOutEffect(targetObject));
        }
    }

    private IEnumerator PopOutEffect(GameObject targetObject)
    {
        float elapsedTime = 0;
        Vector3 startingScale = Vector3.zero; // Start from zero scale
        while (elapsedTime < animationDuration)
        {
            if (targetObject == null) yield break; // Exit if the targetObject is destroyed
            targetObject.transform.localScale = Vector3.Lerp(startingScale, originalScale, elapsedTime / animationDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        targetObject.transform.localScale = originalScale;
    }

    // Adjusted to be called explicitly when needed
    public void StartPopInEffect()
    {
        if (objectToControl.activeSelf) // Only start the effect if the object is active
        {
            StartCoroutine(PopInEffect(objectToControl));
        }
    }

    private IEnumerator PopInEffect(GameObject targetObject)
    {
        float elapsedTime = 0;
        Vector3 endingScale = Vector3.zero; // End with zero scale
        while (elapsedTime < animationDuration)
        {
            if (targetObject == null) yield break; // Exit if the targetObject is destroyed
            targetObject.transform.localScale = Vector3.Lerp(originalScale, endingScale, elapsedTime / animationDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        targetObject.transform.localScale = endingScale;
        targetObject.SetActive(false); // Deactivate after animation
    }
}
