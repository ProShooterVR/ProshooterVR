using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;
using TMPro;

public class MainLeaderBoardManager : MonoBehaviour
{
    private string fetchMainLeaderboardAPI = "http://15.206.116.210/api/user/getcompetitionleaderboard";

    private string fetch10mAirPistolOverallLeaderBoardAPI = "http://15.206.116.210/api/user/getleaderboard/overall/1";

    private string fetch10mAirRifleOverallLeaderBoardAPI = "http://15.206.116.210/api/user/getleaderboard/overall/2";


    // Test API
    //private string fetchMainLeaderboardAPI = "http://15.206.116.210/api/user/getcompetitionleaderboard";

    //private string fetch10mAirPistolOverallLeaderBoardAPI = "http://15.206.116.210/api/user/getleaderboard/overall/1";

    //private string fetch10mAirRifleOverallLeaderBoardAPI = "http://15.206.116.210/api/user/getleaderboard/overall/2";

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
        HUB_UIManager.Instance.ClearMainLeaderboardRows();

        string LevelName = "Overall";
        HUB_UIManager.Instance.LBLevelText.text = LevelName;

        // Create a new dictionary to store meta_id and meta_name
        Dictionary<string, object> metaData = new Dictionary<string, object>
        {
            { "meta_unique_id", metaID},
            { "above_count" , 1},
            { "page_count", 6},
            { "page_number", 1},
            { "below_count" , 1},
        };

        string data = JsonConvert.SerializeObject(metaData);
        Debug.Log("Main LEaderboard mai ye aya re ||  " + data);
        StartCoroutine(fetch10mAirPistolOverallLeaderBoardData(data));
    }

    public void AirRifleOverallLeaderBoardData(string metaID)
    {
        string LevelName = "Overall";
        HUB_UIManager.Instance.LBLevelText.text = LevelName;

        HUB_UIManager.Instance.ClearMainLeaderboardRows();
        // Create a new dictionary to store meta_id and meta_name
        Dictionary<string, object> metaData = new Dictionary<string, object>
        {
            { "meta_unique_id", metaID},
            { "above_count" , 1},
            { "page_count", 6},
            { "page_number", 1},
            { "below_count" , 1},
        };

        string data = JsonConvert.SerializeObject(metaData);
        Debug.Log("coverted data is ||  " + data);
        StartCoroutine(fetch10mAirRifleOverallLeaderBoardData(data));
    }

    public void AirPistolOverallPlayerProfileData(string metaID)
    {
        HUB_UIManager.Instance.ClearMainLeaderboardRows();
        // Create a new dictionary to store meta_id and meta_name
        Dictionary<string, object> metaData = new Dictionary<string, object>
        {
            { "meta_unique_id", metaID},
            { "above_count" , 1},
            { "below_count" , 1},
            { "page_count", 6},
            { "page_number", 1},
        };

        string data = JsonConvert.SerializeObject(metaData);
        Debug.Log("coverted data is ||  " + data);
        StartCoroutine(fetch10mAirPistolOverallPlayerProfileData(data));
    }

    public void AirRifleOverallPlayerProfileData(string metaID)
    {
        HUB_UIManager.Instance.ClearMainLeaderboardRows();
        // Create a new dictionary to store meta_id and meta_name
        Dictionary<string, object> metaData = new Dictionary<string, object>
        {
            { "meta_unique_id", metaID},
            { "above_count" , 1},
            { "below_count" , 1},
            { "page_count", 6},
            { "page_number", 1},
        };

        string data = JsonConvert.SerializeObject(metaData);
        Debug.Log("coverted data is ||  " + data);
        StartCoroutine(fetch10mAirRifleOverallPlayerProfileData(data));
    }

    public void AirPistolAmateurLeaderBoardData(string metaID)
    {
        string LevelName = "Amateur";
        HUB_UIManager.Instance.LBLevelText.text = LevelName;
        HUB_UIManager.Instance.ClearMainLeaderboardRows();
        // Create a new dictionary to store meta_id and meta_name
        Dictionary<string, object> metaData = new Dictionary<string, object>
        {
            { "meta_unique_id", metaID},
            { "difficulty_level", "amateur"},
            { "game_mode", 1},
            { "page_number", 1},
            { "page_count", 6},
        };

        string data = JsonConvert.SerializeObject(metaData);
        Debug.Log("coverted data is ||  " + data);
        StartCoroutine(fetchMainLeaderBoardData(data));
    }
    public void AirPistolSemiProLeaderBoardData(string metaID)
    {
        string LevelName = "SemiPro";
        HUB_UIManager.Instance.LBLevelText.text = LevelName;
        HUB_UIManager.Instance.ClearMainLeaderboardRows();
        // Create a new dictionary to store meta_id and meta_name
        Dictionary<string, object> metaData = new Dictionary<string, object>
        {
            { "meta_unique_id", metaID},
            { "difficulty_level", "semi_pro"},
            { "game_mode", 1},
            { "page_number", 1},
            { "page_count", 6},
        };

        string data = JsonConvert.SerializeObject(metaData);
        Debug.Log("coverted data is ||  " + data);
        StartCoroutine(fetchMainLeaderBoardData(data));

    }
    public void AirPistolProLeaderBoardData(string metaID)
    {
        string LevelName = "Pro";
        HUB_UIManager.Instance.LBLevelText.text = LevelName;
        HUB_UIManager.Instance.ClearMainLeaderboardRows();
        // Create a new dictionary to store meta_id and meta_name
        Dictionary<string, object> metaData = new Dictionary<string, object>
        {
            { "meta_unique_id", metaID},
            { "difficulty_level", "pro"},
            { "game_mode", 1},
            { "page_number", 1},
            { "page_count", 6},
        };

        string data = JsonConvert.SerializeObject(metaData);
        Debug.Log("coverted data is ||  " + data);
        StartCoroutine(fetchMainLeaderBoardData(data));
    }

    public void AirRifleAmateurLeaderBoardData(string metaID)
    {
        string LevelName = "Amateur";
        HUB_UIManager.Instance.LBLevelText.text = LevelName;
        HUB_UIManager.Instance.ClearMainLeaderboardRows();
        // Create a new dictionary to store meta_id and meta_name
        Dictionary<string, object> metaData = new Dictionary<string, object>
        {
            { "meta_unique_id", metaID},
            { "difficulty_level", "amateur"},
            { "game_mode", 2},
            { "page_number", 1},
            { "page_count", 6},
        };

        string data = JsonConvert.SerializeObject(metaData);
        Debug.Log("coverted data is ||  " + data);
        StartCoroutine(fetchMainLeaderBoardData(data));
    }

    public void AirRifleSemiproLeaderBoardData(string metaID)
    {
        string LevelName = "SemiPro";
        HUB_UIManager.Instance.LBLevelText.text = LevelName;
        HUB_UIManager.Instance.ClearMainLeaderboardRows();
        // Create a new dictionary to store meta_id and meta_name
        Dictionary<string, object> metaData = new Dictionary<string, object>
        {
            { "meta_unique_id", metaID},
            { "difficulty_level", "semi_pro"},
            { "game_mode", 2},
            { "page_number", 1},
            { "page_count", 6},
        };

        string data = JsonConvert.SerializeObject(metaData);
        Debug.Log("coverted data is ||  " + data);
        StartCoroutine(fetchMainLeaderBoardData(data));
    }

    public void AirRifleProLeaderBoardData(string metaID)
    {
        string LevelName = "Pro";
        HUB_UIManager.Instance.LBLevelText.text = LevelName;
        HUB_UIManager.Instance.ClearMainLeaderboardRows();
        // Create a new dictionary to store meta_id and meta_name
        Dictionary<string, object> metaData = new Dictionary<string, object>
        {
            { "meta_unique_id", metaID},
            { "difficulty_level", "pro"},
            { "game_mode", 2},
            { "page_number", 1},
            { "page_count", 6},
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

    IEnumerator fetch10mAirRifleOverallPlayerProfileData(string JSONdata)
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

                for (int i = 0; i < HUB_UIManager.Instance.MainLeaderboardJson.Count; i++)
                {
                    if (string.Compare(LocalUserDataManager.Instance.metaID, HUB_UIManager.Instance.MainLeaderboardJson["leaderboardResults"][i]["meta_unique_id"]) == 0)
                    {
                        LocalUserDataManager.Instance.OverallPoints_10AR_Txt = HUB_UIManager.Instance.MainLeaderboardJson["leaderboardResults"][i]["total_score"];
                        LocalUserDataManager.Instance.OverallGamesPlayed_10AR_Txt = HUB_UIManager.Instance.MainLeaderboardJson["leaderboardResults"][i]["matches_played"];

                        Debug.Log("Fetched!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                    }
                }
            }
        }
    }

    IEnumerator fetch10mAirPistolOverallPlayerProfileData(string JSONdata)
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
               

                for (int i = 0; i < HUB_UIManager.Instance.MainLeaderboardJson.Count; i++)
                {
                    if (string.Compare(LocalUserDataManager.Instance.metaID, HUB_UIManager.Instance.MainLeaderboardJson["leaderboardResults"][i]["meta_unique_id"]) == 0)
                    {
                        LocalUserDataManager.Instance.OverallPoints_10AP_Txt = HUB_UIManager.Instance.MainLeaderboardJson["leaderboardResults"][i]["total_score"];
                        LocalUserDataManager.Instance.OverallGamesPlayed_10AP_Txt = HUB_UIManager.Instance.MainLeaderboardJson["leaderboardResults"][i]["matches_played"];

                        Debug.Log("Fetched!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                    }
                }
            }
        }
    }
}