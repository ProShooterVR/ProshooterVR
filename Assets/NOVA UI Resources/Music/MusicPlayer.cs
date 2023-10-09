using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class MusicPlayer : MonoBehaviour
{
    public List<AudioClip> playlist;
    public AudioSource audioSource;
    private int currentIndex = 0;
    private bool isPaused = false;
    private bool isShuffled = false;
    private bool isLooping = false;
    private bool isPlaylist = false;

    public TextMeshPro songNameTextMeshPro; // Reference to the UI Text component displaying the song name.

    public GameObject playButton; // Reference to the Play button GameObject on the UI.
    public GameObject pauseButton; // Reference to the Pause button GameObject on the UI.

    public GameObject shuffleOnButton; // Reference to the Shuffle button GameObject on the UI.
    public GameObject shuffleOffButton; // Reference to the ShuffleON button GameObject on the UI.

    public GameObject loopOnButton; // Reference to the Loop button GameObject on the UI.
    public GameObject loopOffButton; // Reference to the LoopON button GameObject on the UI.

    public GameObject PlayListPanel;
    public Button songButton1; // Reference to the first song button.
    public Button songButton2; // Reference to the second song button.
    public Button songButton3; // Reference to the third song button.
    public Button songButton4; // Reference to the fourth song button.
    public Button songButton5; // Reference to the fifth song button.

    public GameObject PlayListOnButton;
    public GameObject PlayListOffButton;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PlayCurrentSong();

        // Set the initial state of the play and pause buttons based on whether the music is playing or paused.
        playButton.SetActive(isPaused);
        pauseButton.SetActive(!isPaused);

        // Set the initial state of the shuffle and loop buttons.
        shuffleOnButton.gameObject.SetActive(isShuffled);
        shuffleOffButton.gameObject.SetActive(!isShuffled);
        loopOnButton.gameObject.SetActive(isLooping);
        loopOffButton.gameObject.SetActive(!isLooping);
    }

    void Update()
    {
        if (!audioSource.isPlaying && !isPaused)
            PlayNextSong();
    }

    public void PlayCurrentSong()
    {
        if (currentIndex >= 0 && currentIndex < playlist.Count)
        {
            audioSource.clip = playlist[currentIndex];

            audioSource.Play();
        }

        DisplayPlaylist();
    }

    public void PlayPause()
    {
            if (isPaused)
            {
                audioSource.UnPause();
                isPaused = false;
                playButton.SetActive(false); // Hide the Play button.
                pauseButton.SetActive(true); // Show the Pause button.
            }
            else
            {
                audioSource.Pause();
                isPaused = true;
                playButton.SetActive(true); // Show the Play button.
                pauseButton.SetActive(false); // Hide the Pause button.
            }
    }

    public void PlayNextSong()
    {
        if (isShuffled)
        {
            currentIndex = Random.Range(0, playlist.Count);

        }
        else
        {
            currentIndex = (currentIndex + 1) % playlist.Count;

        }

        PlayCurrentSong();

        playButton.SetActive(false); // Hide the Play button.
        pauseButton.SetActive(true); // Show the Pause button.

    }

    public void PlayPreviousSong()
    {
        if (isShuffled)
        {
            currentIndex = Random.Range(0, playlist.Count);
        }
        else
        {
            currentIndex = (currentIndex - 1 + playlist.Count) % playlist.Count;
        }

        PlayCurrentSong();

        playButton.SetActive(false); // Hide the Play button.
        pauseButton.SetActive(true); // Show the Pause button.
    }

    public void ToggleLoop()
    {
        isLooping = !isLooping;
        audioSource.loop = isLooping;

        // Update the loop button visuals.
        loopOnButton.gameObject.SetActive(isLooping);
        loopOffButton.gameObject.SetActive(!isLooping);
    }

    public void ToggleShuffle()
    {
        isShuffled = !isShuffled;

        if (isShuffled)
        {
            // Shuffle the playlist.
            for (int i = 0; i < playlist.Count; i++)
            {
                AudioClip temp = playlist[i];
                int randomIndex = Random.Range(i, playlist.Count);
                playlist[i] = playlist[randomIndex];
                playlist[randomIndex] = temp;
            }
        }
        else
        {
            // Reset the playlist to its original order.
            playlist.Sort((x, y) => x.name.CompareTo(y.name));
        }

        // Update the shuffle button visuals.
        shuffleOnButton.gameObject.SetActive(isShuffled);
        shuffleOffButton.gameObject.SetActive(!isShuffled);
    }

    // Additional functionality to display playlist on the UI.
    public void DisplayPlaylist()
    {
        if (currentIndex >= 0 && currentIndex < playlist.Count)
        {
            string songName = playlist[currentIndex].name;
            songNameTextMeshPro.text = songName;
        }
    }

    public void onPlayListButtonClick()
    {
        isPlaylist = !isPlaylist;

        PlayListPanel.SetActive(!PlayListPanel.activeSelf);

        PlayListOnButton.gameObject.SetActive(isPlaylist);
        PlayListOffButton.gameObject.SetActive(!isPlaylist);

    }

    public void PlaySong1()
    {
        currentIndex = 0;
        PlayCurrentSong();

        playButton.SetActive(false); // Hide the Play button.
        pauseButton.SetActive(true); // Show the Pause button.
    }

    public void PlaySong2()
    {
        currentIndex = 1;
        PlayCurrentSong();

        playButton.SetActive(false); // Hide the Play button.
        pauseButton.SetActive(true); // Show the Pause button.
    }

    public void PlaySong3()
    {
        currentIndex = 2;
        PlayCurrentSong();

        playButton.SetActive(false); // Hide the Play button.
        pauseButton.SetActive(true); // Show the Pause button.
    }

    public void PlaySong4()
    {
        currentIndex = 3;
        PlayCurrentSong();

        playButton.SetActive(false); // Hide the Play button.
        pauseButton.SetActive(true); // Show the Pause button.
    }

    public void PlaySong5()
    {
        currentIndex = 4;
        PlayCurrentSong();

        playButton.SetActive(false); // Hide the Play button.
        pauseButton.SetActive(true); // Show the Pause button.
    }

}
