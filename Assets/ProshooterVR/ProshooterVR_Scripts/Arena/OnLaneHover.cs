using UnityEngine;
using UnityEngine.EventSystems; // Required for the event system
using UnityEngine.UI; // Required for UI components
using Nova;

public class OnLaneHover : MonoBehaviour
{ 
    // Serialize and assign in Editor
    private Interactable Button;


    public GameObject MyHover;

    public void Start()
    {
        MyHover.SetActive(false);
    }

    public void OnEnable()
    {
        Button = this.GetComponent<Interactable>();

        /*** Subscribe to desired gesture events ***/
        Button.UIBlock.AddGestureHandler<Gesture.OnHover>(HandleHoverEvent);
        Button.UIBlock.AddGestureHandler<Gesture.OnUnhover>(HandleUnhoverEvent);
        Button.UIBlock.AddGestureHandler<Gesture.OnClick>(HandleClickEvent);

        // Initialize FMOD events
    }

    private void HandleHoverEvent(Gesture.OnHover evt)
    {
        MyHover.SetActive(true);

    }

    private void HandleUnhoverEvent(Gesture.OnUnhover evt)
    {
        MyHover.SetActive(false);
        // No need to handle anything for unhover in this example
    }

    private void HandleClickEvent(Gesture.OnClick evt)
    {
        //Debug.Log("Clicked!");

        
    }

    public void OnDisable()
    {
        /*** Unsubscribe from gesture events previously subscribed to in OnEnable ***/
        Button.UIBlock.RemoveGestureHandler<Gesture.OnHover>(HandleHoverEvent);
        Button.UIBlock.RemoveGestureHandler<Gesture.OnUnhover>(HandleUnhoverEvent);
        Button.UIBlock.RemoveGestureHandler<Gesture.OnClick>(HandleClickEvent);

        // Release FMOD event instances
       
    }
}
