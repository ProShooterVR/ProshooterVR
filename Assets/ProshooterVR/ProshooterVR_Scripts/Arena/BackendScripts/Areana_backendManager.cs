using Newtonsoft.Json;
using SimpleJSON;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using backend_management;
using TMPro;

public class Areana_backendManager : MonoBehaviour
{

    public static Areana_backendManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    string accessToken = "PROSHOOTERVR @$#PRO#$@";
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitForAPIToInitialize());

    }
    IEnumerator WaitForAPIToInitialize()
    {
        // Wait for the initialization process to complete
        yield return StartCoroutine(global_API_manager.Instance.initAPIcalls());

        // Once initialization is done, proceed with other actions
        Debug.Log("Initialization completed. Proceeding with other actions...");

       AirPistolOverallLeaderBoardData(global_API_manager.Instance.AP_arena_Daily_total_score_leaderboard, Arena_AirPistol_mananger.Instance.AP_arenaDaily_totalscore_leaderboardParent, "total_score");
       AirPistolOverallLeaderBoardData(global_API_manager.Instance.AP_arena_Overall_total_score_leaderboard, Arena_AirPistol_mananger.Instance.AP_arenaOverall_totalscore_leaderboardParent, "total_score");

       AirPistolOverallLeaderBoardData(global_API_manager.Instance.AP_arena_Daily_total_shots_leaderboard, Arena_AirPistol_mananger.Instance.AP_arenaDaily_totalshots_leaderboardParent, "total_shots_target");
       AirPistolOverallLeaderBoardData(global_API_manager.Instance.AP_arena_Overall_total_shots_leaderboard, Arena_AirPistol_mananger.Instance.AP_arenaOverall_totalshots_leaderboardParent, "total_shots_target");

       AirPistolOverallLeaderBoardData(global_API_manager.Instance.AP_arena_Daily_tenPointers_leaderboard, Arena_AirPistol_mananger.Instance.AP_arenaDaily_tens_leaderboardParent, "total_ten_pointers");
       AirPistolOverallLeaderBoardData(global_API_manager.Instance.AP_arena_Overall_tenPointers_leaderboard, Arena_AirPistol_mananger.Instance.AP_arenaOverall_tens_leaderboardParent, "total_ten_pointers");

    }




    /// <summary>
    /// AIR PISTOl API BACKEND INTEGRATION STARTS -----------------------------------------------------
    /// </summary>

    public void SaveGameDataPistol()
    {
        // Create a new dictionary to store meta_id and meta_name
        Dictionary<int, float> ScoresPistol = ConvertArrayToDictionary(Arena_AirPistol_mananger.Instance.shotScores);

        Dictionary<string, object> metaData = new Dictionary<string, object>
            {
                { "meta_unique_id", LocalUserDataManager.Instance.metaID },
                { "shots_score",  Arena_AirPistol_mananger.Instance.shotScores},
                { "difficulty_level",  Arena_AirPistol_mananger.Instance.selectedSkillLevel},
            };

        string data = JsonConvert.SerializeObject(metaData);
        Debug.Log("coverted data is ||  " + data);
        StartCoroutine(SaveGameDataUserDB(data));

    }
    Dictionary<int, float> ConvertArrayToDictionary(float[] array)
    {
        Dictionary<int, float> dictionary = new Dictionary<int, float>();

        // Assuming the array length is the same as the number of keys you want
        for (int i = 0; i < array.Length; i++)
        {
            // You can assign a value to each key based on your requirements
            dictionary.Add(i, array[i]);
        }

        return dictionary;
    }
    IEnumerator SaveGameDataUserDB(string JSONdata)
    {

        using (UnityWebRequest www = UnityWebRequest.Post(global_API_manager.Instance.insertArenaGameData, JSONdata, "application/json"))
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
            }
        }

    }


    public void AirPistolOverallLeaderBoardData(string req_api, GameObject leaderBoardRef,string scoreRef)
    {
        StartCoroutine(fetch_AP_arenaLeaderBoardData(req_api, leaderBoardRef, scoreRef));
    }

    IEnumerator fetch_AP_arenaLeaderBoardData(string req_api,GameObject leaderBoardRef, string scoreRef)
    {
        UnityWebRequest request = UnityWebRequest.Get(req_api);

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
            JSONNode data = JSON.Parse(jsonString);

            for (int i = 0; i < leaderBoardRef.transform.childCount; i++)
            {
                leaderBoardRef.transform.GetChild(i).GetChild(0).GetComponent<TextMeshPro>().text = data["leaderboardResults"][i]["meta_quest_username"];
                leaderBoardRef.transform.GetChild(i).GetChild(1).GetComponent<TextMeshPro>().text = data["leaderboardResults"][i][scoreRef];

            }
        }
            //-------------

        //    using (UnityWebRequest webRequest = UnityWebRequest.Get(req_api))
        //{
        //    // Send the request and wait for a response
        //    webRequest.SetRequestHeader("authorization", accessToken);

        //    yield return webRequest.SendWebRequest();

        //    // Check for errors
        //    if (webRequest.result == UnityWebRequest.Result.ConnectionError ||
        //        webRequest.result == UnityWebRequest.Result.ProtocolError)
        //    {
        //        Debug.LogError("Error: " + webRequest.error);
        //    }
        //    else
        //    {
        //        // Get the response text
        //        string responseData = webRequest.downloadHandler.text;

        //        // Parse the response data as needed
        //        JSONNode data = JSON.Parse(responseData);
        //        Arena_AirPistol_mananger.Instance.AP_arena_Daily_total_score_leaderboard = JSON.Parse(responseData);


        //        for(int i = 0;i < leaderBoardRef.transform.childCount; i++)
        //        {
        //            leaderBoardRef.transform.GetChild(i).GetChild(0).GetComponent<TextMeshPro>().text = data["leaderboardResults"][i]["meta_quest_username"];
        //            leaderBoardRef.transform.GetChild(i).GetChild(1).GetComponent<TextMeshPro>().text = data["leaderboardResults"][i]["total_score"];

        //        }
                
        //    }
        
    }


    /// <summary>
    /// AIR PISTOl API BACKEND INTEGRATION END -----------------------------------------------------
    /// </summary>
}
