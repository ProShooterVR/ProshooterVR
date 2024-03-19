using BNG;
using Nova;
using System;
using UnityEngine;

namespace NovaSamples.Inventory
{
    public class ControllerRaycast : InputManager
    {

        [Tooltip("Reference to the 3D object used as a point at the end of the line renderer")]
        public Transform pointMarker; // Assign a 3D object in the Unity Editor
        /// <summary>
        /// The controlID for mouse point events
        /// </summary>
        public const uint MousePointerControlID = 1;

        /// <summary>
        /// The controlID for mouse wheel events
        /// </summary>
        public const uint ScrollWheelControlID = 2;

        /// <summary>
        /// To store the button states of both the left and right mouse buttons.
        /// </summary>
        private static readonly InputData Data = new InputData();

        [Tooltip("Inverts the mouse wheel scroll direction.")]
        public bool InvertScrolling = true;

        /// <summary>
        /// The camera used to convert a mouse position into a world ray
        /// </summary>
        private Camera cam = null;
        public LayerMask raycastLayerMask; // Layer mask to filter which objects can be hit by the raycast
        public float maxRaycastDistance = 100f; // Maximum distance of the raycast
        public LineRenderer lineRenderer; // Reference to the LineRenderer component
        int i = 0;

        /// <summary>
        /// The camera used to convert a mouse position into a world ray
        /// </summary>
        public string allowdTag;

        private void Update()
        {
            if (i == 0)
            {
                // Enable and position the LineRenderer at the controller
                lineRenderer.enabled = true;
                lineRenderer.SetPosition(0, transform.position);

                // Raycast from the left controller
                Ray ray = new Ray(transform.position, transform.forward);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, maxRaycastDistance, raycastLayerMask))
                {

                    if (String.Compare(hit.transform.gameObject.tag, allowdTag) == 0)
                    {// Object hit by the raycast
                        lineRenderer.enabled = true;

                        GameObject hitObject = hit.collider.gameObject;
                        lineRenderer.SetPosition(1, hit.point);

                        if (pointMarker != null)
                        {
                            pointMarker.position = hit.point;
                            pointMarker.gameObject.SetActive(true); // Make sure the marker is enabled

                            float distance = Vector3.Distance(transform.position, hit.point);
                            // Scale factor - adjust the base value (0.02f) to control the starting size
                            // and the multiplier (0.02f) to control how much the size increases with distance
                            float scaleValue = Mathf.Clamp(0.01f + (distance * 0.01f), 0.02f, 4f); // Adjust these min and max values as needed
                            pointMarker.localScale = new Vector3(scaleValue, scaleValue, scaleValue);
                        }
                    
                        //Debug.Log("" + hitObject.name);
                        // Do something with the hit object (e.g., interact, highlight, etc.)
                        // ...
                    }
                    else
                    {
                        lineRenderer.enabled = false;
                        if (pointMarker != null) pointMarker.gameObject.SetActive(false);
                    }
                }
                else
                {
                    lineRenderer.enabled = false;
                    // No hit, set the LineRenderer's endpoint to the maximum distance
                    lineRenderer.SetPosition(1, transform.position + transform.forward * maxRaycastDistance);
                    // Position the point marker at the maximum distance
                    if (pointMarker != null)
                    {
                        pointMarker.position = transform.position + transform.forward * maxRaycastDistance;
                        pointMarker.gameObject.SetActive(false); // Optionally disable the marker when no object is hit
                    }
                }

                // Invert scrolling for a mouse-type experience,
                // otherwise will scroll track-pad style.


                // Create a new Interaction.Update from the mouse ray and scroll wheel control id


                // Store the button states for left/right mouse buttons
                Data.PrimaryButtonDown = InputBridge.Instance.LeftTriggerDown;
                Data.SecondaryButtonDown = Input.GetMouseButton(0);


                // Create a new Interaction.Update from the mouse ray and pointer control id
                Interaction.Update pointInteraction = new Interaction.Update(ray, userData: Data);

                // Feed the pointer update and pressed state to Nova's Interaction APIs
                Interaction.Point(pointInteraction, Data.AnyButtonPressed);
            }
        }

        public override bool TryGetRay(uint controlID, out Ray ray)
        {
            throw new System.NotImplementedException();
        }
    }
}

