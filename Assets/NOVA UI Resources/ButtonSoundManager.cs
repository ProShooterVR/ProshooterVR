using Nova;
using UnityEngine;
using UnityEngine.InputSystem.XR;

/// <summary>
/// A sample component responsible for subscribing to and handling hover, unhover, and click events.
/// </summary>
public class ButtonSoundManager : MonoBehaviour
{
    // Serialize and assign in Editor
    public Interactable Button = null;
    //public Color DefaultColor = Color.grey;
    //public Color HoverColor = Color.white;

    public AudioClip hoverSound;
    public AudioClip clickSound;

    public AudioSource audioSource;

    public void OnEnable()
    {
        /*** Subscribe to desired gesture events ***/
        Button.UIBlock.AddGestureHandler<Gesture.OnHover>(HandleHoverEvent);
        Button.UIBlock.AddGestureHandler<Gesture.OnUnhover>(HandleUnhoverEvent);
        Button.UIBlock.AddGestureHandler<Gesture.OnClick>(HandleClickEvent);
    }

    private void HandleHoverEvent(Gesture.OnHover evt)
    {
        // Change the color of the UIBlock under the pointer.
        //Button.UIBlock.Color = HoverColor;
        if (hoverSound != null)
        {
            audioSource.PlayOneShot(hoverSound);
        }
    }

    private void HandleUnhoverEvent(Gesture.OnUnhover evt)
    {
        // Change the color of the UIBlock exited by the pointer.
        //Button.UIBlock.Color = DefaultColor;
    }

    private void HandleClickEvent(Gesture.OnClick evt)
    {
        // Log a message each time the Button is clicked.
        Debug.Log("Clicked!");

        if (clickSound != null)
        {
            audioSource.PlayOneShot(clickSound);
        }
    }

    public void OnDisable()
    {
        /*** Unsubscribe from gesture events previously subscribed to in OnEnable ***/
        Button.UIBlock.RemoveGestureHandler<Gesture.OnHover>(HandleHoverEvent);
        Button.UIBlock.RemoveGestureHandler<Gesture.OnUnhover>(HandleUnhoverEvent);
        Button.UIBlock.RemoveGestureHandler<Gesture.OnClick>(HandleClickEvent);
    }
}