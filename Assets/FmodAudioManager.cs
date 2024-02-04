using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using TMPro;
using NovaSamples.UIControls;
using System.Collections;

public class FmodAudioManager : MonoBehaviour
{
    public static FmodAudioManager Instance;
    void Awake()
    {
        Instance = this;

    }

    public Slider masterSlider;
    public Slider musicSlider;
    public Slider sfxSlider;
    public Slider ambienceSlider;

    public TextMeshPro masterVolumeText;
    public TextMeshPro musicVolumeText;
    public TextMeshPro sfxVolumeText;
    public TextMeshPro ambienceVolumeText;

    private float initialMasterVolume;
    private float initialMusicVolume;
    private float initialSFXVolume;
    private float initialAmbienceVolume;

    [Header("Volume")]
    [Range(0, 1)]
    public float MasterVolume = 1;

    [Range(0, 1)]
    public float MusicVolume = 1;

    [Range(0, 1)]
    public float SFXVolume = 1;

    [Range(0, 1)]
    public float AmbienceVolume = 1;

    public Bus masterBus;
    public Bus musicBus;
    public Bus sfxBus;
    public Bus ambienceBus;
    private bool isFMODInitialized = false;
    private void Start()
    {
       // StartCoroutine(WaitForFMODInitialization());
    }

    private IEnumerator WaitForFMODInitialization()
    {
        // Wait until FMOD is initialized
        while (!RuntimeManager.IsInitialized)
        {
            yield return null;
        }

        // FMOD is now initialized, you can proceed with your operations
        isFMODInitialized = true;

        // Example: Play an FMOD event after initialization
        if (isFMODInitialized)
        {
            Debug.Log("Ready to Initilized");
            StartInit();
           
        }
    }

    public void StartInit()
    {
        
        masterBus = RuntimeManager.GetBus("bus:/");
        musicBus = RuntimeManager.GetBus("bus:/Music");
        sfxBus = RuntimeManager.GetBus("bus:/SFX");
        ambienceBus = RuntimeManager.GetBus("bus:/AMB");

        // Add debug logs
        //Debug.Log("Master Bus: " + masterBus.isValid());
        //Debug.Log("Music Bus: " + musicBus.isValid());
        //Debug.Log("SFX Bus: " + sfxBus.isValid());
        //Debug.Log("Ambience Bus: " + ambienceBus.isValid());

        // Set initial volumes to maximum
        initialMasterVolume = MasterVolume;
        initialMusicVolume = MusicVolume;
        initialSFXVolume = SFXVolume;
        initialAmbienceVolume = AmbienceVolume;

        // Initialize sliders to maximum values
        masterSlider.Value = MasterVolume;
        musicSlider.Value = MusicVolume;
        sfxSlider.Value = SFXVolume;
        ambienceSlider.Value = AmbienceVolume;

        // Subscribe to slider events
        masterSlider.OnValueChanged.AddListener(OnMasterSliderValueChanged);
        musicSlider.OnValueChanged.AddListener(OnMusicSliderValueChanged);
        sfxSlider.OnValueChanged.AddListener(OnSFXSliderValueChanged);
        ambienceSlider.OnValueChanged.AddListener(OnAmbienceSliderValueChanged);


        FmodMusicPlayer.Instance.currentIndex = 1;
        FmodMusicPlayer.Instance.PlayCurrentSong();
        FmodMusicPlayer.Instance.DisplayPlaylist();
    }




    private void Update()
    {
        // Update FMOD bus volumes based on script values
        masterBus.setVolume(MasterVolume);
        musicBus.setVolume(MusicVolume);
        sfxBus.setVolume(SFXVolume);
        ambienceBus.setVolume(AmbienceVolume);

        // Update UI Text with volume percentages
        masterVolumeText.text = $"{Mathf.RoundToInt(MasterVolume * 100)}%";
        musicVolumeText.text = $"{Mathf.RoundToInt(MusicVolume * 100)}%";
        sfxVolumeText.text = $"{Mathf.RoundToInt(SFXVolume * 100)}%";
        ambienceVolumeText.text = $"{Mathf.RoundToInt(AmbienceVolume * 100)}%";
    }

    private void OnMasterSliderValueChanged(float newValue)
    {
        MasterVolume = newValue;
    }

    private void OnMusicSliderValueChanged(float newValue)
    {
        MusicVolume = newValue;
    }

    private void OnSFXSliderValueChanged(float newValue)
    {
        SFXVolume = newValue;
    }

    private void OnAmbienceSliderValueChanged(float newValue)
    {
        AmbienceVolume = newValue;
    }

    public void OnSaveButtonClicked()
    {
        // Save the current volumes as initial values
        initialMasterVolume = MasterVolume;
        initialMusicVolume = MusicVolume;
        initialSFXVolume = SFXVolume;
        initialAmbienceVolume = AmbienceVolume;

        HUB_UIManager.Instance.SoundControlPanel.SetActive(false);
        HUB_UIManager.Instance.settingUI.SetActive(true);
        Debug.Log("Volumes saved!");
    }

    public void OnCloseButtonClicked()
    {
        // Reset volumes to initial values
        MasterVolume = initialMasterVolume;
        MusicVolume = initialMusicVolume;
        SFXVolume = initialSFXVolume;
        AmbienceVolume = initialAmbienceVolume;

        // Update sliders and UI Text
        masterSlider.Value = MasterVolume;
        musicSlider.Value = MusicVolume;
        sfxSlider.Value = SFXVolume;
        ambienceSlider.Value = AmbienceVolume;

        HUB_UIManager.Instance.SoundControlPanel.SetActive(false);
        HUB_UIManager.Instance.settingUI.SetActive(true);
    }
}
