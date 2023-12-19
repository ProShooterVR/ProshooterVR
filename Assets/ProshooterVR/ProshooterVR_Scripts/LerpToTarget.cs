using UnityEngine;
using System.Collections;
using BNG;

public class LerpToTarget : MonoBehaviour
{
    public GameObject originalObject;   // The object to move and reset rotation
    public GameObject targetObject;     // The target GameObject to move towards
    private float lerpPositionSpeed = 10f;  // The speed of the position interpolation
    private float lerpRotationSpeed = 10f;  // The speed of the rotation interpolation
    private float stoppingSpeed = 5f;     // The speed at which the object stops

    private Vector3 initialPosition;
    private Vector3 targetPosition;
    private Quaternion initialRotation;
    private Quaternion targetRotation;

    public GameObject gunObj;

    public bool isLerp;
    public bool isUXCalled;
    private void Start()
    {
        if (originalObject != null && targetObject != null)
        {
            initialPosition = originalObject.transform.position;
            targetPosition = targetObject.transform.position;
            initialRotation = originalObject.transform.rotation;
            targetRotation = targetObject.transform.rotation;
        }
        isLerp = false;
        isUXCalled = false;
        originalObject.SetActive(false);
    }
    private void Update()
    {
        if(gunObj.GetComponent<Grabbable>().BeingHeld == false)
        {
            if (isLerp == true)
            {
                StartLerping();
                isLerp = false;
                if (isUXCalled == true)
                {
                   // UXManagerAirPistol.Instance.UXEvents(0);
                    isUXCalled = false;
                }
            }
        }
        if (gunObj.GetComponent<Grabbable>().BeingHeld == true)
        {
            isLerp = true;
            if(isUXCalled == false)
            {
              //  UXManagerAirPistol.Instance.UXEvents(1);
                isUXCalled = true;
            }
        }

    }
    public void StartLerping()
    {

        originalObject.SetActive(true);
        originalObject.transform.position = gunObj.transform.position;
        originalObject.transform.rotation = gunObj.transform.rotation;

        gunObj.GetComponent<myorgPos>().resetPos();
        gunObj.SetActive(false);
        StartCoroutine(LerpPositionAndRotation());
    }

    private IEnumerator LerpPositionAndRotation()
    {

       

        float distanceToTarget = Vector3.Distance(originalObject.transform.position, targetPosition);
        float angleToTarget = Quaternion.Angle(originalObject.transform.rotation, targetRotation);

        while (distanceToTarget > stoppingSpeed * Time.deltaTime || angleToTarget > 1.0f)
        {
            originalObject.transform.position = Vector3.Lerp(originalObject.transform.position, targetPosition, lerpPositionSpeed * Time.deltaTime);
            originalObject.transform.rotation = Quaternion.Slerp(originalObject.transform.rotation, targetRotation, lerpRotationSpeed * Time.deltaTime);

            distanceToTarget = Vector3.Distance(originalObject.transform.position, targetPosition);
            angleToTarget = Quaternion.Angle(originalObject.transform.rotation, targetRotation);

            yield return null;
        }

        // Ensure we end up at the exact target position and rotation
        originalObject.transform.position = targetPosition;
        originalObject.transform.rotation = targetRotation;
       
        originalObject.SetActive(false);
        gunObj.SetActive(true);


    }
}
