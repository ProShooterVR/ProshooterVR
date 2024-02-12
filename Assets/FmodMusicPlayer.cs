using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using FMODUnity;
using System.Collections;

public class FmodMusicPlayer : MonoBehaviour
{
    public static FmodMusicPlayer Instance;

    void Awake()
    {
        Instance = this;
    }

    public List<GameObject> playlist;
    public int currentIndex = 0;
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

    public GameObject StartSound;

    void Start()
    {
        playButton.SetActive(false);
        pauseButton.SetActive(true);

        shuffleOnButton.SetActive(isShuffled);
        shuffleOffButton.SetActive(!isShuffled);
        loopOnButton.SetActive(isLooping);
        loopOffButton.SetActive(!isLooping);


        currentIndex = 0;

        //StartCoroutine(disbaleAllTracks());
        PlayCurrentSong();
    }

    IEnumerator disbaleAllTracks()
    {
        for( int i =0; i < this.gameObject.transform.childCount; i++)
        {
            this.gameObject.transform.GetChild(i).gameObject.SetActive(false);
        }
        PlayCurrentSong();
        yield return null;

    }

    void Update()
    {
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
            PlayCurrentSong();
        }
    }
}
