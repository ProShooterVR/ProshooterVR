using UnityEngine;
using FMODUnity;

public class RF_FmodSetup: MonoBehaviour
{
    public static RF_FmodSetup Instance;
    [SerializeField]
    private StudioEventEmitter Attention, Unload, MatchStarts, FourSecSeriesLoad, SixSecSeriesLoad, EightSecSeriesLoad, AthletesToTheLine;
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        /*if (Attention == null)
        {
            Debug.LogError("Event Emitter not assigned!");
            return;
        }
        if (Unload == null)
        {
            Debug.LogError("Event Emitter not assigned!");
            return;
        }
        if (MatchStarts == null)
        {
            Debug.LogError("Event Emitter not assigned!");
            return;
        }
        if (FourSecSeriesLoad == null)
        {
            Debug.LogError("Event Emitter not assigned!");
            return;
        }
        if (SixSecSeriesLoad == null)
        {
            Debug.LogError("Event Emitter not assigned!");
            return;
        }
        if (EightSecSeriesLoad == null)
        {
            Debug.LogError("Event Emitter not assigned!");
            return;
        }
        if (AthletesToTheLine == null)
        {
            Debug.LogError("Event Emitter not assigned!");
            return;
        }*/
    }

    public void AttentionEvent()
    {
        // Ensure the event emitter is valid before attempting to play
        if (Attention)
        {
            // Play the assigned FMOD event
            Attention.Play();
        }
        else
        {
            Debug.LogError("Event Emitter not assigned!");
        }
    }
    public void UnloadEvent()
    {
        // Ensure the event emitter is valid before attempting to play
        if (Unload)
        {
            // Play the assigned FMOD event
            Unload.Play();
        }
        else
        {
            Debug.LogError("Event Emitter not assigned!");
        }
    }
    public void MatchStartsEvent()
    {
        // Ensure the event emitter is valid before attempting to play
        if (MatchStarts)
        {
            // Play the assigned FMOD event
            MatchStarts.Play();
        }
        else
        {
            Debug.LogError("Event Emitter not assigned!");
        }
    }
    public void FourSecSeriesLoadEvent()
    {
        // Ensure the event emitter is valid before attempting to play
        if (FourSecSeriesLoad)
        {
            // Play the assigned FMOD event
            FourSecSeriesLoad.Play();
        }
        else
        {
            Debug.LogError("Event Emitter not assigned!");
        }
    }
    public void SixSecSeriesLoadEvent()
    {
        // Ensure the event emitter is valid before attempting to play
        if (SixSecSeriesLoad)
        {
            // Play the assigned FMOD event
            SixSecSeriesLoad.Play();
        }
        else
        {
            Debug.LogError("Event Emitter not assigned!");
        }
    }
    public void EightSecSeriesLoadEvent()
    {
        // Ensure the event emitter is valid before attempting to play
        if (EightSecSeriesLoad)
        {
            // Play the assigned FMOD event
            EightSecSeriesLoad.Play();
        }
        else
        {
            Debug.LogError("Event Emitter not assigned!");
        }
    }
    public void AthletesToTheLineLoadEvent()
    {
        // Ensure the event emitter is valid before attempting to play
        if (AthletesToTheLine)
        {
            // Play the assigned FMOD event
            AthletesToTheLine.Play();
        }
        else
        {
            Debug.LogError("Event Emitter not assigned!");
        }
    }
}
