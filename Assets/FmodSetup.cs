using UnityEngine;
using FMODUnity;

public class FmodSetup : MonoBehaviour
{
    public static FmodSetup Instance;
    [SerializeField]
    private StudioEventEmitter EpicShot, GoodShot, BadShot, DecentShot;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        
        if (EpicShot == null)
        {
            Debug.LogError("Event Emitter not assigned!");
            return;
        }
        if (GoodShot == null)
        {
            Debug.LogError("Event Emitter not assigned!");
            return;
        }
        if (BadShot == null)
        {
            Debug.LogError("Event Emitter not assigned!");
            return;
        }
        if (DecentShot == null)
        {
            Debug.LogError("Event Emitter not assigned!");
            return;
        }
    }

   
    public void EpicShotEvent()
    {
        // Ensure the event emitter is valid before attempting to play
        if (EpicShot)
        {
            // Play the assigned FMOD event
            EpicShot.Play();
        }
        else
        {
            Debug.LogError("Event Emitter not assigned!");
        }
    }
    public void GoodShotEvent()
    {
        // Ensure the event emitter is valid before attempting to play
        if (GoodShot)
        {
            // Play the assigned FMOD event
            GoodShot.Play();
        }
        else
        {
            Debug.LogError("Event Emitter not assigned!");
        }
    }
    public void BadShotEvent()
    {
        // Ensure the event emitter is valid before attempting to play
        if (BadShot)
        {
            // Play the assigned FMOD event
            BadShot.Play();
        }
        else
        {
            Debug.LogError("Event Emitter not assigned!");
        }
    }
    public void DecentShotEvent()
    {
        // Ensure the event emitter is valid before attempting to play
        if (DecentShot)
        {
            // Play the assigned FMOD event
            DecentShot.Play();
        }
        else
        {
            Debug.LogError("Event Emitter not assigned!");
        }
    }
}
