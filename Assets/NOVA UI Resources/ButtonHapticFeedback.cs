using UnityEngine;
using UnityEngine.XR;

public class ButtonHapticFeedback : MonoBehaviour
{
    public XRNode controllerNode;
    public float hapticDuration = 0.2f;
    public float hapticAmplitude = 0.5f;

    private void Update()
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(controllerNode);

        if (device.TryGetFeatureValue(CommonUsages.triggerButton, out bool buttonPressed) && buttonPressed)
        {
            device.SendHapticImpulse(0, hapticAmplitude, hapticDuration);
        }
    }
}