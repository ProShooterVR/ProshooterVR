using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class shotPlacer : MonoBehaviour, IPointerClickHandler
{
    public GameObject imagePrefab;

    void Update()
    {
        Debug.Log("clickeddd");
        // Check if the left mouse button was clicked
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("clickeddd");
            // Create a ray from the mouse click position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // Perform a raycast to check if the mouse click hit anything in the scene
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                // Instantiate the image prefab at the hit point
                imagePrefab.transform.position = hit.transform.position;
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Get the mouse click position
        Vector2 mousePos = eventData.position;

        // Log the mouse position to the console
        Debug.Log(mousePos);
    }
}
