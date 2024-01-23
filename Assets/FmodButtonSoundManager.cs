using Nova;
using UnityEngine;

/// <summary>
/// A sample component responsible for subscribing to and handling hover, unhover, and click events.
/// </summary>
public class FmodButtonSoundManager : MonoBehaviour
{
    // Serialize and assign in Editor
    public Interactable Button = null;

    public string hoverEventPath = "event:/UIUX/Hover";
    public string clickEventPath = "event:/UIUX/Click";

    private FMOD.Studio.EventInstance hoverEventInstance;
    private FMOD.Studio.EventInstance clickEventInstance;

    public void OnEnable()
    {
        /*** Subscribe to desired gesture events ***/
        Button.UIBlock.AddGestureHandler<Gesture.OnHover>(HandleHoverEvent);
        Button.UIBlock.AddGestureHandler<Gesture.OnUnhover>(HandleUnhoverEvent);
        Button.UIBlock.AddGestureHandler<Gesture.OnClick>(HandleClickEvent);

        // Initialize FMOD events
        hoverEventInstance = FMODUnity.RuntimeManager.CreateInstance(hoverEventPath);
        clickEventInstance = FMODUnity.RuntimeManager.CreateInstance(clickEventPath);
    }

    private void HandleHoverEvent(Gesture.OnHover evt)
    {
        if (hoverEventInstance.isValid())
        {
            hoverEventInstance.start();
        }
    }

    private void HandleUnhoverEvent(Gesture.OnUnhover evt)
    {
        // No need to handle anything for unhover in this example
    }

    private void HandleClickEvent(Gesture.OnClick evt)
    {
        Debug.Log("Clicked!");

        if (clickEventInstance.isValid())
        {
            clickEventInstance.start();
        }
    }

    public void OnDisable()
    {
        /*** Unsubscribe from gesture events previously subscribed to in OnEnable ***/
        Button.UIBlock.RemoveGestureHandler<Gesture.OnHover>(HandleHoverEvent);
        Button.UIBlock.RemoveGestureHandler<Gesture.OnUnhover>(HandleUnhoverEvent);
        Button.UIBlock.RemoveGestureHandler<Gesture.OnClick>(HandleClickEvent);

        // Release FMOD event instances
        if (hoverEventInstance.isValid())
        {
            hoverEventInstance.release();
        }

        if (clickEventInstance.isValid())
        {
            clickEventInstance.release();
        }
    }
}
