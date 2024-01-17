using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using TMPro;
using NovaSamples.UIControls;
using System.Collections.Generic;

public class AudioManagerAndMusicPlayer : MonoBehaviour
{
    public static AudioManagerAndMusicPlayer Instance;

    // AudioManager variables
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

    private Bus masterBus;
    private Bus musicBus;
    private Bus sfxBus;
    private Bus ambienceBus;

    // MusicPlayer variables
    public List<GameObject> playlist;
    private int currentIndex = 0;
    private bool isShuffled = false;
    private bool isLooping = false;
    private bool isPlaylist = false;
    private bool isPaused = false;

    public TextMeshPro songNameTextMeshPro;

    public GameObject playButton;
    public GameObject pauseButton;

    public GameObject shuffleOnButton;
    public GameObject shuffleOffButton;

    public GameObject loopOnButton;
    public GameObject loopOffButton;

    public GameObject PlayListPanel;
    public GameObject PlayListOnButton;
    public GameObject PlayListOffButton;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        masterBus = RuntimeManager.GetBus("bus:/");
        musicBus = RuntimeManager.GetBus("bus:/Music");
        sfxBus = RuntimeManager.GetBus("bus:/SFX");
        ambienceBus = RuntimeManager.GetBus("bus:/AMB");

        // Add debug logs
        Debug.Log("Master Bus: " + masterBus.isValid());
        Debug.Log("Music Bus: " + musicBus.isValid());
        Debug.Log("SFX Bus: " + sfxBus.isValid());
        Debug.Log("Ambience Bus: " + ambienceBus.isValid());

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

        // MusicPlayer initialization
        playButton.SetActive(false);
        pauseButton.SetActive(true);

        shuffleOnButton.SetActive(isShuffled);
        shuffleOffButton.SetActive(!isShuffled);
        loopOnButton.SetActive(isLooping);
        loopOffButton.SetActive(!isLooping);

        playlist[currentIndex].SetActive(true);
        PlaySong(currentIndex);
    }

    void Update()
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

        // Add your custom logic for playing or handling the state of GameObjects in the playlist.
        // For example, you might want to check for input or perform periodic updates.
    }

    public void PlayCurrentSong()
    {
        if (currentIndex >= 0 && currentIndex < playlist.Count)
        {
            EnableCurrentSong();
            DisplayPlaylist();
        }
    }

    public void PlayPause()
    {
        isPaused = !isPaused;

        playlist[currentIndex].SetActive(!isPaused);

        playButton.SetActive(isPaused);
        pauseButton.SetActive(!isPaused);
    }

    public void PlayNextSong()
    {
        playlist[currentIndex].SetActive(false);

        if (isShuffled)
        {
            currentIndex = Random.Range(0, playlist.Count);
        }
        else
        {
            currentIndex = (currentIndex + 1) % playlist.Count;
        }

        PlayCurrentSong();
    }

    public void PlayPreviousSong()
    {
        playlist[currentIndex].SetActive(false);

        if (isShuffled)
        {
            currentIndex = Random.Range(0, playlist.Count);
        }
        else
        {
            currentIndex = (currentIndex - 1 + playlist.Count) % playlist.Count;
        }

        PlayCurrentSong();
    }

    public void ToggleLoop()
    {
        isLooping = !isLooping;

        loopOnButton.SetActive(isLooping);
        loopOffButton.SetActive(!isLooping);
    }

    public void ToggleShuffle()
    {
        isShuffled = !isShuffled;

        if (isShuffled)
        {
            ShufflePlaylist();
        }
        else
        {
            playlist.Sort((x, y) => x.name.CompareTo(y.name));
        }

        shuffleOnButton.SetActive(isShuffled);
        shuffleOffButton.SetActive(!isShuffled);
    }

    void ShufflePlaylist()
    {
        for (int i = 0; i < playlist.Count; i++)
        {
            GameObject temp = playlist[i];
            int randomIndex = Random.Range(i, playlist.Count);
            playlist[i] = playlist[randomIndex];
            playlist[randomIndex] = temp;
        }
    }

    public void DisplayPlaylist()
    {
        if (currentIndex >= 0 && currentIndex < playlist.Count)
        {
            string songName = playlist[currentIndex].name;
            songNameTextMeshPro.text = songName;
        }
    }

    public void OnPlayListButtonClick()
    {
        isPlaylist = !isPlaylist;

        PlayListPanel.SetActive(!PlayListPanel.activeSelf);

        PlayListOnButton.SetActive(isPlaylist);
        PlayListOffButton.SetActive(!isPlaylist);
    }

    public void EnableCurrentSong()
    {
        for (int i = 0; i < playlist.Count; i++)
        {
            playlist[i].SetActive(i == currentIndex);
        }
    }

    public void PlaySong(int index)
    {
        if (index >= 0 && index < playlist.Count)
        {
            playlist[currentIndex].SetActive(false);

            currentIndex = index;
            playlist[currentIndex].SetActive(true);
            PlayCurrentSong();
        }
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

    // AudioManager methods
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
