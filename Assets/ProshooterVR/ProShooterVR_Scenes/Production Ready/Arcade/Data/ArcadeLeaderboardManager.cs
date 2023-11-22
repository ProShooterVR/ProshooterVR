using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;
using TMPro;

public class ArcadeLeaderboardManager : MonoBehaviour
{
    //test
   // private string insertArcadeGameDataAPI = "http://54.201.154.149/api/user/insertarcadegamedata";
   // private string fetchArcadeLeaderboardAPI = "http://54.201.154.149/api/user/getarcadeleaderboard/";

    //prod
    private string insertArcadeGameDataAPI = "http://15.206.116.210/api/user/insertarcadegamedata";
    private string fetchArcadeLeaderboardAPI = "http://15.206.116.210/api/user/getarcadeleaderboard/";


    public static ArcadeLeaderboardManager Instance;
    string accessToken = "PROSHOOTERVR @$#PRO#$@";

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //getArcadeLeaderboardData("6889892497704835");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveArcadeGameData(string metaID, string totalScore)
    {
        // Create a new dictionary to store meta_id and meta_name
        Dictionary<string, object> metaData = new Dictionary<string, object>
            {
                { "meta_unique_id", metaID },
                { "total_game_score", totalScore }
            };

        string data = JsonConvert.SerializeObject(metaData);
        Debug.Log("coverted data is ||  " + data);
        StartCoroutine(createUserDB(data));

    }
    IEnumerator createUserDB(string JSONdata)
    {

        using (UnityWebRequest www = UnityWebRequest.Post(insertArcadeGameDataAPI, JSONdata, "application/json"))
        {
            www.SetRequestHeader("authorization", accessToken);

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
                ArcadeLeaderboardManager.Instance.getArcadeLeaderboardData(LocalUserDataManager.Instance.metaID);


            }
        }
       
    }

    public void getArcadeLeaderboardData(string metaID)
    {
        StartCoroutine(FetchArcadeLeaderboard(metaID));
    }

    public IEnumerator FetchArcadeLeaderboard(string metaid)
    {

        // Create a UnityWebRequest with the desired HTTP method (GET in this case)
        UnityWebRequest request = UnityWebRequest.Get(fetchArcadeLeaderboardAPI + metaid);

        // Set the request headers
        request.SetRequestHeader("Content-Type", "application/json");

        // Set the download handler to handle the response
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();

        request.SetRequestHeader("authorization", accessToken);

        // Send the request and wait for the response
        yield return request.SendWebRequest();

        // Check for errors
        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError(request.error);
        }
        else
        {
            // Request was successful, map JSON directly to a class
            string jsonString = request.downloadHandler.text;
            ArcadeGameUIManager.Instance.leaderboardJson = JSON.Parse(jsonString);
            Debug.Log(jsonString);
        }


    }
}
