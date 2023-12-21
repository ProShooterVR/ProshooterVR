using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;
using TMPro;

public class MainLeaderBoardManager : MonoBehaviour
{
    private string fetchMainLeaderboardAPI = "http://54.201.154.149/api/user/getcompetitionleaderboard";

    private string fetch10mAirPistolOverallLeaderBoardAPI = "http://54.201.154.149/api/user/getleaderboard/overall/1";

    private string fetch10mAirRifleOverallLeaderBoardAPI = "http://54.201.154.149/api/user/getleaderboard/overall/2";

    public static MainLeaderBoardManager Instance;

    string accessToken = "PROSHOOTERVR @$#PRO#$@";

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AirPistolOverallLeaderBoardData(string metaID)
    {
        // Create a new dictionary to store meta_id and meta_name
        Dictionary<string, object> metaData = new Dictionary<string, object>
        {
            { "meta_unique_id", metaID},
            { "above_count" , 1},
            { "below_count" , 1},
        };

        string data = JsonConvert.SerializeObject(metaData);
        Debug.Log("coverted data is ||  " + data);
        StartCoroutine(fetch10mAirPistolOverallLeaderBoardData(data));
    }

    public void AirRifleOverallLeaderBoardData()
    {
        // Create a new dictionary to store meta_id and meta_name
        Dictionary<string, object> metaData = new Dictionary<string, object>
        {
            { "meta_unique_id", "784578547854345784502"},
            { "above_count" , 1},
            { "below_count" , 1},
        };

        string data = JsonConvert.SerializeObject(metaData);
        Debug.Log("coverted data is ||  " + data);
        StartCoroutine(fetch10mAirRifleOverallLeaderBoardData(data));
    }

    public void AirPistolAmateurLeaderBoardData()
    {
        // Create a new dictionary to store meta_id and meta_name
        Dictionary<string, object> metaData = new Dictionary<string, object>
        {
            { "meta_unique_id", "6438980339485102"},
            { "difficulty_level", "amateur"},
            { "game_mode", 1},
            { "page_number", 1},
            { "page_count", 10},
        };

        string data = JsonConvert.SerializeObject(metaData);
        Debug.Log("coverted data is ||  " + data);
        StartCoroutine(fetchMainLeaderBoardData(data));
    }
    public void AirPistolSemiProLeaderBoardData()
    {
        // Create a new dictionary to store meta_id and meta_name
        Dictionary<string, object> metaData = new Dictionary<string, object>
        {
            { "meta_unique_id", "6438980339485102"},
            { "difficulty_level", "semi_pro"},
            { "game_mode", 1},
            { "page_number", 1},
            { "page_count", 10},
        };

        string data = JsonConvert.SerializeObject(metaData);
        Debug.Log("coverted data is ||  " + data);
        StartCoroutine(fetchMainLeaderBoardData(data));

    }
    public void AirPistolProLeaderBoardData()
    {

        // Create a new dictionary to store meta_id and meta_name
        Dictionary<string, object> metaData = new Dictionary<string, object>
        {
            { "meta_unique_id", "6438980339485102"},
            { "difficulty_level", "pro"},
            { "game_mode", 1},
            { "page_number", 1},
            { "page_count", 10},
        };

        string data = JsonConvert.SerializeObject(metaData);
        Debug.Log("coverted data is ||  " + data);
        StartCoroutine(fetchMainLeaderBoardData(data));
    }

    public void AirRifleAmateurLeaderBoardData()
    {
        // Create a new dictionary to store meta_id and meta_name
        Dictionary<string, object> metaData = new Dictionary<string, object>
        {
            { "meta_unique_id", 6438980339485102},
            { "difficulty_level", "amateur"},
            { "game_mode", 2},
            { "page_number", 1},
            { "page_count", 10},
        };

        string data = JsonConvert.SerializeObject(metaData);
        Debug.Log("coverted data is ||  " + data);
        StartCoroutine(fetchMainLeaderBoardData(data));
    }

    public void AirRifleSemiproLeaderBoardData()
    {
        // Create a new dictionary to store meta_id and meta_name
        Dictionary<string, object> metaData = new Dictionary<string, object>
        {
            { "meta_unique_id", 6438980339485102},
            { "difficulty_level", "semipro"},
            { "game_mode", 2},
            { "page_number", 1},
            { "page_count", 10},
        };

        string data = JsonConvert.SerializeObject(metaData);
        Debug.Log("coverted data is ||  " + data);
        StartCoroutine(fetchMainLeaderBoardData(data));
    }

    public void AirRifleProLeaderBoardData()
    {
        // Create a new dictionary to store meta_id and meta_name
        Dictionary<string, object> metaData = new Dictionary<string, object>
        {
            { "meta_unique_id", 6438980339485102},
            { "difficulty_level", "pro"},
            { "game_mode", 2},
            { "page_number", 1},
            { "page_count", 10},
        };

        string data = JsonConvert.SerializeObject(metaData);
        Debug.Log("coverted data is ||  " + data);
        StartCoroutine(fetchMainLeaderBoardData(data));
    }

    IEnumerator fetchMainLeaderBoardData(string JSONdata)
    {
        using (UnityWebRequest www = UnityWebRequest.Post(fetchMainLeaderboardAPI, JSONdata, "application/json"))
        {
            // Set the Authorization header

            www.SetRequestHeader("authorization", accessToken);

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("API Data Fetched!");
                string jsonString = www.downloadHandler.text;
                Debug.Log(jsonString);

                JSONNode data = JSON.Parse(jsonString);
                HUB_UIManager.Instance.MainLeaderboardJson = JSON.Parse(jsonString);
                HUB_UIManager.Instance.MainLeaderboardUIFill();
            }
        }
    }
    IEnumerator fetch10mAirPistolOverallLeaderBoardData(string JSONdata)
    {
        using (UnityWebRequest www = UnityWebRequest.Post(fetch10mAirPistolOverallLeaderBoardAPI, JSONdata, "application/json"))
        {
            // Set the Authorization header

            www.SetRequestHeader("authorization", accessToken);

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("API Data Fetched!");
                string jsonString = www.downloadHandler.text;
                Debug.Log(jsonString);

                JSONNode data = JSON.Parse(jsonString);
                HUB_UIManager.Instance.MainLeaderboardJson = JSON.Parse(jsonString);
                HUB_UIManager.Instance.MainLeaderboardUIFill();
            }
        }
    }
    IEnumerator fetch10mAirRifleOverallLeaderBoardData(string JSONdata)
    {
        using (UnityWebRequest www = UnityWebRequest.Post(fetch10mAirRifleOverallLeaderBoardAPI, JSONdata, "application/json"))
        {
            // Set the Authorization header

            www.SetRequestHeader("authorization", accessToken);

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("API Data Fetched!");
                string jsonString = www.downloadHandler.text;
                Debug.Log(jsonString);

                JSONNode data = JSON.Parse(jsonString);
                HUB_UIManager.Instance.MainLeaderboardJson = JSON.Parse(jsonString);
                HUB_UIManager.Instance.MainLeaderboardUIFill();
            }
        }
    }
}