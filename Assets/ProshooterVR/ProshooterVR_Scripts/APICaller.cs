using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class APICaller : MonoBehaviour
{
    // The URL of the API
    private string apiUrl = "https://jsonplaceholder.typicode.com/todos/1";
    public TextMeshPro debugTextField;

    void Start()
    {
        // Start the API request
        StartCoroutine(CallAPI());
    }

    IEnumerator CallAPI()
    {
        // Create a UnityWebRequest object
        UnityWebRequest webRequest = UnityWebRequest.Get(apiUrl);

        // Send the request and wait for a response
        yield return webRequest.SendWebRequest();

        // Check for errors
        if (webRequest.isNetworkError || webRequest.isHttpError)
        {
            Debug.LogError("API request error: " + webRequest.error);
        }
        else
        {
            // API request was successful
            //Debug.Log("API response: " + webRequest.downloadHandler.text);
            debugTextField.text = webRequest.downloadHandler.text;
            // You can process the API response here
        }
    }
}
