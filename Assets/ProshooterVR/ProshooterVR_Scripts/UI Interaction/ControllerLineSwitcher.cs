using BNG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nova;

namespace NovaSamples.Inventory
{
    public class ControllerLineSwitcher : MonoBehaviour
    {
        public GameObject leftController, rightController;
        public LineRenderer leftControllerLine;
        public LineRenderer rightControllerLine;

        // Use this to avoid constant switching when holding down the button
        private bool isSwitchingAllowed = true;

        private void Start()
        {
            leftControllerLine.enabled = true;
            rightControllerLine.enabled = false;
            leftController.GetComponent<ControllerRaycast>().enabled = true;
            rightControllerLine.GetComponent<ControllerRaycast>().enabled = false;
        }

        private void Update()
        {

            if (InputBridge.Instance.LeftThumbstickDown ||
                InputBridge.Instance.RightThumbstickDown )
            {
                if (isSwitchingAllowed)
                {
                    // Switch active controller
                    SetActiveController(!(leftControllerLine.enabled));
                    isSwitchingAllowed = false; // Prevent continuous switching
                }
            }
            else
            {
                isSwitchingAllowed = true; // Allow switching once the trigger is released
            }


        }

        private void SetActiveController(bool isLeftControllerActive)
        {
            // Enable the line renderer of the active controller and disable the other
            leftControllerLine.enabled = isLeftControllerActive;
            rightControllerLine.enabled = !isLeftControllerActive;
            leftController.GetComponent<ControllerRaycast>().enabled = isLeftControllerActive;
            rightControllerLine.GetComponent<ControllerRaycast>().enabled = !isLeftControllerActive;

            leftController.GetComponent<ControllerRaycast>().pointMarker.gameObject.SetActive(isLeftControllerActive);
            rightControllerLine.GetComponent<ControllerRaycast>().pointMarker.gameObject.SetActive(!isLeftControllerActive);
        }
    }
}