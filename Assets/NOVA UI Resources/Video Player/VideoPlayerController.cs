using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using TMPro;

public class VideoPlayerController : MonoBehaviour
{
    public static VideoPlayerController Instance;

    public VideoPlayer videoPlayer;
    public TextMeshPro titleText;
    public GameObject musicGameObject;
    public GameObject playButton, BigPlayButton;
    public GameObject pauseButton;
    public GameObject videoplayerUI, MusicControlBar;

    private AudioSource musicAudioSource;
    private bool isPaused = false;

    // Reference to the Renderer component of the GameObject
    public Renderer rend;

    public Slider slider;
    private bool isDragging;

    public TextMeshPro startTimeText;
    public TextMeshPro endTimeText;

    private void Awake()
    {
        // Get the Renderer component attached to this GameObject
        rend = GetComponent<Renderer>();
    }
    void Start()
    {
        // Initialize video player settings
        videoPlayer.playOnAwake = false;
        videoPlayer.isLooping = false;

        BigPlayButton.SetActive(true);
        playButton.SetActive(true);
        pauseButton.SetActive(false);

        // Get the AudioSource component from the music GameObject
        musicAudioSource = musicGameObject.GetComponent<AudioSource>();

        // Set the slider max value to the video clip's length
        slider.maxValue = (float)videoPlayer.clip.length;

        UpdateEndTimeText(videoPlayer.clip.length);
    }

    void Update()
    {
        // Update the slider value based on the video's current time
        slider.value = (float)videoPlayer.time;
        UpdateStartTimeText(videoPlayer.time);
    }

    public void OnSliderValueChanged()
    {
        // Seek to the selected time when the slider value changes
        videoPlayer.time = slider.value;
        UpdateStartTimeText(slider.value);
    }

    private void UpdateStartTimeText(double time)
    {
        startTimeText.text = FormatTime(time);
    }

    private void UpdateEndTimeText(double time)
    {
        endTimeText.text = FormatTime(time);
    }

    private string FormatTime(double time)
    {
        int minutes = Mathf.FloorToInt((float)time / 60f);
        int seconds = Mathf.FloorToInt((float)time % 60f);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    public void PlayVideo()
    {
        videoPlayer.Play();
        isPaused = false;
        //musicAudioSource.enabled = false; // Disable music when video plays
        BigPlayButton.SetActive(false);
        playButton.SetActive(false);
        pauseButton.SetActive(true);

        // Check if the Renderer and its material are valid
        if (rend != null && rend.material != null)
        {
            // Set the main texture of the material to null, effectively disabling it
            rend.material.mainTexture = null;
        }
    }

    public void PauseVideo()
    {
        videoPlayer.Pause();
        isPaused = true;
        //musicAudioSource.enabled = true; // Enable music when video is paused
        BigPlayButton.SetActive(true);
        playButton.SetActive(true);
        pauseButton.SetActive(false);
    }

    public void CloseVideo()
    {
        videoPlayer.Stop();
        gameObject.SetActive(false);
        videoplayerUI.SetActive(false);
        musicAudioSource.enabled = true; // Enable music when video is closed
        playButton.SetActive(true);
        pauseButton.SetActive(false);
        MusicControlBar.SetActive(true);
        slider.value = 0f;
        UpdateStartTimeText(0f);
    }

    // Use this function to set the title of the video
    /*public void SetVideoTitle(string title)
    {
        titleText.text = title;
    }*/
}
