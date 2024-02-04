using UnityEngine;
using FMODUnity;

public class GunFmodSetup : MonoBehaviour
{
    public static GunFmodSetup Instance;
    [SerializeField]
    private StudioEventEmitter ReloadOpen, ReloadClose;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if (ReloadOpen == null)
        {
            Debug.LogError("Event Emitter not assigned!");
            return;
        }
        if (ReloadClose == null)
        {
            Debug.LogError("Event Emitter not assigned!");
            return;
        }
       
    }

    public void ReloadOpenEvent()
    {
        // Ensure the event emitter is valid before attempting to play
        if (ReloadOpen)
        {
            // Play the assigned FMOD event
            ReloadOpen.Play();
        }
        else
        {
            Debug.LogError("Event Emitter not assigned!");
        }
    }
    public void ReloadCloseEvent()
    {
        // Ensure the event emitter is valid before attempting to play
        if (ReloadClose)
        {
            // Play the assigned FMOD event
            ReloadClose.Play();
        }
        else
        {
            Debug.LogError("Event Emitter not assigned!");
        }
    }
    
}
