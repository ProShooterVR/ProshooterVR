using Nova;
using UnityEngine;
using FMOD;
using FMODUnity;

/// <summary>
/// A sample component responsible for subscribing to and handling hover, unhover, and click events.
/// </summary>
public class ArenaUIButtonManager : MonoBehaviour
{
    // Serialize and assign in Editor
    public Interactable Button;

    private string hoverEventPath = "event:/UIUX/Hover";
    private string clickEventPath = "event:/UIUX/Click";

    private FMOD.Studio.EventInstance hoverEventInstance;
    private FMOD.Studio.EventInstance clickEventInstance;

    //public GameObject HoverPanel;
    //public GameObject ButtonBorder;

    //public Color borderNewColor;

    public void Start()
    {
        
    }
    Color HexToColor(string hex)
    {
        Color color = new Color();
        ColorUtility.TryParseHtmlString(hex, out color);
        return color;
    }

    public void OnEnable()
    {
        Button = this.GetComponent<Interactable>();
        Button.GetComponent<UIBlock2D>().Border.Enabled = true;

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
        Color targetColor = HexToColor("#FF9F0A");
        Button.GetComponent<UIBlock2D>().Border.Color = targetColor;

        if (hoverEventInstance.isValid())
        {
            hoverEventInstance.start();
        }
    }

    private void HandleUnhoverEvent(Gesture.OnUnhover evt)
    {
        //HoverPanel.SetActive(false);
        //ButtonBorder.SetActive(false);
        Button.GetComponent<UIBlock2D>().Border.Color = Color.white;
        // No need to handle anything for unhover in this example
    }

    private void HandleClickEvent(Gesture.OnClick evt)
    {
        //Debug.Log("Clicked!");

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
