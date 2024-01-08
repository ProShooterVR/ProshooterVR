using UnityEngine;
using BNG;
using ProshooterVR;
using NovaSamples.Inventory;

public class HandRotation : MonoBehaviour
{

    [SerializeField]
    private float rotationSpeed = 50f;

    [SerializeField]
    private float maxRotationUp = 45f;

    [SerializeField]
    private float minRotation = 0f;

    public Quaternion mySavedRotation;

    public GameObject controllerGFX;

    private void Start()
    {

      
    }

    public void onEnter()
    {
        controllerGFX.SetActive(true);
    }

    public void onExit()
    {
        controllerGFX.SetActive(false);

    }

    void Update()
    {
        // Get thumbstick input or keyboard input for testing
        float thumbstickX = InputBridge.Instance.RightThumbstickAxis.y;

        // Check if the right thumbstick of the right controller is moved
        if (thumbstickX != 0)
        {
            RotateHand(thumbstickX);
        }
        else
        {
            // For testing with keyboard keys
            if (Input.GetKey(KeyCode.L))
            {
                RotateHand(1f); // Simulate thumbstick to the left
            }
            else if (Input.GetKey(KeyCode.K))
            {
                RotateHand(-1f); // Simulate thumbstick to the right
            }
        }

        // For Cancel/Reset Grip To Default  
        if(InputBridge.Instance.AButtonDown == true || Input.GetKey(KeyCode.A))
        {
           PistolUIManager.Instance.rightHandController.GetComponent<HandRotation>().enabled = false;
           PistolUIManager.Instance.settingPopUp.SetActive(true);
            PistolUIManager.Instance.menuPanel.SetActive(true);
                
            RayManager.Instance.EnableRey();
            onExit();
            PistolUIManager.Instance.isOtherUIOpen = false;
            PistolUIManager.Instance.setSwitch = false;
        }

        // For Save/Comfirm Adjusted Grip Settings
        if (InputBridge.Instance.BButtonDown == true || Input.GetKey(KeyCode.B))
        {
            PistolUIManager.Instance.rightHandController.GetComponent<HandRotation>().enabled = false;
            PistolUIManager.Instance.settingPopUp.SetActive(true);
            PistolUIManager.Instance.menuPanel.SetActive(true);

            RayManager.Instance.EnableRey();
            saveGripAdjustment();
            onExit();
            PistolUIManager.Instance.isOtherUIOpen = false;
            PistolUIManager.Instance.setSwitch = false;


        }
    }

    void RotateHand(float thumbstickX)
    {
        float rotationAngle = thumbstickX * rotationSpeed * Time.deltaTime;

        // Calculate the new rotation angle
        float newRotation = transform.localRotation.eulerAngles.x - rotationAngle;

        // Clamp the rotation between minRotation and maxRotationUp
        float clampedRotation = Mathf.Clamp(newRotation, minRotation, maxRotationUp);

        // Apply the rotation to the object
        transform.localRotation = Quaternion.Euler(clampedRotation, 0f, 0f);
        mySavedRotation = transform.localRotation;

    }

    

    void saveGripAdjustment()
    {
        DBAPIManagerNew.Instance.saveGripAdjustmentSettings(mySavedRotation);
    }
}
