
///
/// Currently in this product there are no DLC's are added.
/// We have implemented the required steps beforehand for future use as we expect to use DLC's in our game.
///

using UnityEngine;
using Oculus.Platform;
using Oculus.Platform.Models;
using UnityEngine.Networking;
using System.Collections;

public class DownloadManager : MonoBehaviour
{
    public static DownloadManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        DontDestroyOnLoad(Instance);

    }


    private bool headsetRemoved = false;
    private bool downloading = false;
    private UnityWebRequest downloadRequest;


    private void OnEnable()
    {
        OVRManager.HMDUnmounted += OnHeadsetRemoved;
    }

    private void OnDisable()
    {
        OVRManager.HMDUnmounted -= OnHeadsetRemoved;
    }

    private void OnHeadsetRemoved()
    {
        headsetRemoved = true;
    }

    private void Update()
    {
        if (headsetRemoved)
        {
            // Continue the download process or handle it accordingly.
            // For example, you can pause the download or show a message to the user.
        }
    }
    public void StartDownload(string url)
    {
        StartCoroutine(DownloadCoroutine(url));
    }
    private IEnumerator DownloadCoroutine(string url)
    {
        downloading = true;

        downloadRequest = UnityWebRequest.Get(url);
        downloadRequest.SendWebRequest();

        while (!downloadRequest.isDone)
        {
            if (headsetRemoved)
            {
                // Handle the pause or cancellation of the download process.
                // For example, you can call downloadRequest.Abort() to cancel the download.
                break;
            }

            // Update the progress or perform other necessary actions.
            float progress = downloadRequest.downloadProgress;
            Debug.Log("Download Progress: " + progress);

            yield return null;
        }

        downloading = false;

        if (downloadRequest.isDone && !headsetRemoved)
        {
            // Download completed successfully.
            Debug.Log("Download completed.");
            // ... Handle the downloaded content ...
        }
        else
        {
            // Download canceled or interrupted.
            Debug.Log("Download canceled or interrupted.");
            // ... Handle the interruption ...
        }
    }
}
