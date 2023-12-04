using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Firebase.Database;
using Firebase;
using System;
using Newtonsoft.Json;
using System.Threading.Tasks;
using UnityEngine.Networking;

namespace ProshooterVR
{
    public class DBAPIManagerNew : MonoBehaviour
    {

        public static DBAPIManagerNew Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            DontDestroyOnLoad(Instance);
        }



        /// <summary>
        /// All the required api list and the links START
        /// </summary>
        /// Test API
       // private string createUserAPI = "http://54.201.154.149/api/user/createuser";
       // private string fetchUserProfileInfo = "http://54.201.154.149/api/user/getuserinfobymetaid/";
       // private string insertgamedata = "http://54.201.154.149/api/user/insertgamedata";

        // prod API
          private string createUserAPI = "http://15.206.116.210/api/user/createuser";
          private string fetchUserProfileInfo = "http://15.206.116.210/api/user/getuserinfobymetaid/";
          private string insertgamedata = "http://15.206.116.210/api/user/insertgamedata";

        string accessToken = "PROSHOOTERVR @$#PRO#$@";
        /// <summary>
        /// All the required api list and the links END
        /// 
        /// </summary>
        private void Start()
        {
            

        }

        public void Initialise_BackendDAta()
        {

            SaveMetaInfo(LocalUserDataManager.Instance.metaID, LocalUserDataManager.Instance.meta_username);

        }

        /// <summary>
        /// Create USER API                        START-----------------------------------------------------------------
        /// Create User IN DB 
        /// If user is not present it will create the New user with default values 
        /// If user is already present it will return the data.
        /// </summary>
        /// <param name="metaID"></param>
        /// <param name="metaName"></param>
        public void SaveMetaInfo(string metaID, string metaName)
        {
            // Create a new dictionary to store meta_id and meta_name
            Dictionary<string, object> metaData = new Dictionary<string, object>
            {
                { "meta_unique_id", metaID },
                { "meta_quest_username", metaName },
                { "device_id", "123" },
                { "device_name", "MetaQuest 2"},
                { "profile_url",LocalUserDataManager.Instance.metauser_profileImage_url }
            };
           
           string data = JsonConvert.SerializeObject(metaData);
           Debug.Log("coverted data is ||  "+data);
           StartCoroutine(createUserDB(data));

        }
        IEnumerator createUserDB(string JSONdata)
        {

            using (UnityWebRequest www = UnityWebRequest.Post(createUserAPI, JSONdata, "application/json"))
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
                    Debug.Log("Form upload complete!");
                    getProfileData(LocalUserDataManager.Instance.metaID);
                }
            }
           
        }

        /// <summary>
        /// Create User API Implementation END ----------------------------------------------------------------------
        /// </summary>
        /// <param name="newValue"></param>




        /// <summary>
        /// Fetch Profile data                                          Start ---------------------------------------------------
        /// </summary>
        /// <returns></returns>
        public void getProfileData(string metaID)
        {
            StartCoroutine(FetchProfileData(metaID));
        }

        public IEnumerator FetchProfileData(string metaid)
        {



            // Create a UnityWebRequest with the desired HTTP method (GET in this case)
            UnityWebRequest request = UnityWebRequest.Get(fetchUserProfileInfo + metaid);

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
                Debug.Log(jsonString);


                PlayerData PlayerDataObj = JsonUtility.FromJson<PlayerData>(jsonString);


                LocalUserDataManager.Instance.userNameTxt = PlayerDataObj.meta_quest_username;
                LocalUserDataManager.Instance.totalScoreTxt = PlayerDataObj.total_player_score.ToString();
                LocalUserDataManager.Instance.matchesPlayedTxt = PlayerDataObj.matches_played.ToString();
                LocalUserDataManager.Instance.accuracyTxt = PlayerDataObj.accuracy.ToString();

                LocalUserDataManager.Instance.pbest_10mAirP_AmaTxt = PlayerDataObj.p_best_10m_airp_ama.ToString();
                LocalUserDataManager.Instance.pbest_10mAirP_SemPTxt = PlayerDataObj.p_best_10m_airp_semp.ToString();
                LocalUserDataManager.Instance.pbest_10mAirP_ProTxt = PlayerDataObj.p_best_10m_airp_pro.ToString();

                LocalUserDataManager.Instance.pbest_10mAirR_AmaTxt = PlayerDataObj.p_best_10m_air_r_ama.ToString();
                LocalUserDataManager.Instance.pbest_10mAirR_SemPTxt = PlayerDataObj.p_best_10m_air_r_semp.ToString();
                LocalUserDataManager.Instance.pbest_10mAirR_ProTxt = PlayerDataObj.p_best_10m_air_r_pro.ToString();

                LocalUserDataManager.Instance.pbest_25mRF_AmaTxt = PlayerDataObj.p_best_25m_rf_ama.ToString();
                LocalUserDataManager.Instance.pbest_25mRF_SemPTxt = PlayerDataObj.p_best_25m_rf_semp.ToString();
                LocalUserDataManager.Instance.pbest_25mRF_ProTxt = PlayerDataObj.p_best_25m_rf_pro.ToString();

                HUB_UIManager.Instance.UpdateProfileButton();

                // Now you can access class fields directly
                // Debug.Log(PlayerData.meta_unique_id);

                // Add more lines as needed for other fields
            }


        }
        /// <summary>
        /// Fetch Profile data                                          End ---------------------------------------------------
        /// </summary>
        /// <returns></returns>



      

        /// <summary>
        /// Create USER API                        START-----------------------------------------------------------------
        /// Create User IN DB 
        /// If user is not present it will create the New user with default values 
        /// If user is already present it will return the data.
        /// </summary>
        /// <param name="metaID"></param>
        /// <param name="metaName"></param>
        /// 
        
        public void SaveGameDataPistol(int gameMode)
        {
            // Create a new dictionary to store meta_id and meta_name
            Dictionary<int, float> ScoresPistol = ConvertArrayToDictionary(GunDataManager.Instance.ScoresPistol);

            Dictionary<string, object> metaData = new Dictionary<string, object>
            {
                { "meta_unique_id", LocalUserDataManager.Instance.metaID },
                { "game_mode",  gameMode},
                { "difficulty_level", LocalUserDataManager.Instance.SelectedGameLevel },
                { "30_shots_score", ScoresPistol},
                { "average_score",GunDataManager.Instance.avgSrScorePistol },
                { "no_of_inner_shots",GunDataManager.Instance.noOfInnerTens },
                { "no_of_shots_on_target",GunDataManager.Instance.noOfShotsOnTarget },
                { "no_of_shots_missed",GunDataManager.Instance.noOfShotsMissed },
                { "personal_best",GunDataManager.Instance.personalGameBestPistol },
                { "series_1_score",GunDataManager.Instance.sr1ScorePistol },
                { "series_2_score",GunDataManager.Instance.sr2ScorePistol },
                { "series_3_score",GunDataManager.Instance.sr3ScorePistol },
                { "total_game_score",GunDataManager.Instance.totalGameScorePistol },
                { "total_time_spent",GunDataManager.Instance.totalTimeSpent },
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
                dictionary.Add(i,array[i]);
            }

            return dictionary;
        }
        public void SaveGameDataRifle(int gameMode)
        {

            Dictionary<int, float> ScoresRifle = ConvertArrayToDictionary(GunDataManager.Instance.ScoresRifle);
            // Create a new dictionary to store meta_id and meta_name
            Dictionary<string, object> metaData = new Dictionary<string, object>
            {
                { "meta_unique_id", LocalUserDataManager.Instance.metaID },
                { "gamemode",  gameMode},
                { "difficulty_level", LocalUserDataManager.Instance.SelectedGameLevel },
                { "30_shots_score", ScoresRifle},
                { "average_score",GunDataManager.Instance.avgSrScoreRifle },
                { "no_of_inner_shots",GunDataManager.Instance.noOfInnerTens },
                { "no_of_shots_on_target",GunDataManager.Instance.noOfShotsOnTarget },
                { "no_of_shots_missed",GunDataManager.Instance.noOfShotsMissed },
                { "personal_best",GunDataManager.Instance.personalGameBestRifle },
                { "series_1_score",GunDataManager.Instance.series1ScoreRifle },
                { "series_2_score",GunDataManager.Instance.series2ScoreRifle },
                { "series_3_score",GunDataManager.Instance.series3ScoreRifle },
                { "total_game_score",GunDataManager.Instance.TotalScoreRifle },
                { "total_time_spent",GunDataManager.Instance.totalTimeSpent },
            };

            string data = JsonConvert.SerializeObject(metaData);
            Debug.Log("coverted data is ||  " + data);
            StartCoroutine(SaveGameDataUserDB(data));

        }
        IEnumerator SaveGameDataUserDB(string JSONdata)
        {

            using (UnityWebRequest www = UnityWebRequest.Post(insertgamedata, JSONdata, "application/json"))
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
                    getProfileData(LocalUserDataManager.Instance.metaID);
                }
            }

        }

    }

    public class PlayerData
    {

        public string user_id;
        public string meta_unique_id;
        public string meta_quest_username;
        public string profile_url;
        public string is_verified;
        public string first_name;
        public string last_name;
        public string email;
        public string phone_number;
        public string country_name;
        public string country_code;
        public string device_id;
        public string device_name;
        public float accuracy;
        public int matches_played;
        public float p_best_10m_airp_ama;
        public float p_best_10m_airp_semp;
        public float p_best_10m_airp_pro;
        public float p_best_10m_air_r_ama;
        public float p_best_10m_air_r_pro;
        public float p_best_10m_air_r_semp;
        public float p_best_25m_rf_ama;
        public float p_best_25m_rf_pro;
        public float p_best_25m_rf_semp;
        public float total_player_score;


        /// <summary>
        /// Constructor to init all the values
        /// </summary>
        /// <param name="user_id"></param>
        /// <param name="meta_unique_id"></param>
        /// <param name="meta_quest_username"></param>
        /// <param name="profile_url"></param>
        /// <param name="is_verified"></param>
        /// <param name="first_name"></param>
        /// <param name="last_name"></param>
        /// <param name="email"></param>
        /// <param name="phone_number"></param>
        /// <param name="country_name"></param>
        /// <param name="country_code"></param>
        /// <param name="device_id"></param>
        /// <param name="device_name"></param>
        /// <param name="accuracy"></param>
        /// <param name="matches_played"></param>
        /// <param name="p_best_10m_airp_ama"></param>
        /// <param name="p_best_10m_airp_semp"></param>
        /// <param name="p_best_10m_airp_pro"></param>
        /// <param name="p_best_10m_air_r_ama"></param>
        /// <param name="p_best_10m_air_r_pro"></param>
        /// <param name="p_best_10m_air_r_semp"></param>
        /// <param name="p_best_25m_rf_ama"></param>
        /// <param name="p_best_25m_rf_pro"></param>
        /// <param name="p_best_25m_rf_semp"></param>
        /// <param name="total_player_score"></param>
        public PlayerData(
            string user_id,
            string meta_unique_id,
            string meta_quest_username,
            string profile_url,
            string is_verified,
            string first_name,
            string last_name,
            string email,
            string phone_number,
            string country_name,
            string country_code,
            string device_id,
            string device_name,
            float accuracy,
            int matches_played,
            float p_best_10m_airp_ama,
            float p_best_10m_airp_semp,
            float p_best_10m_airp_pro,
            float p_best_10m_air_r_ama,
            float p_best_10m_air_r_pro,
            float p_best_10m_air_r_semp,
            float p_best_25m_rf_ama,
            float p_best_25m_rf_pro,
            float p_best_25m_rf_semp,
            float total_player_score)
        {
            this.user_id = user_id;
            this.meta_unique_id = meta_unique_id;
            this.meta_quest_username = meta_quest_username;
            this.profile_url = profile_url;
            this.is_verified = is_verified;
            this.first_name = first_name;
            this.last_name = last_name;
            this.email = email;
            this.phone_number = phone_number;
            this.country_name = country_name;
            this.country_code = country_code;
            this.device_id = device_id;
            this.device_name = device_name;
            this.accuracy = accuracy;
            this.matches_played = matches_played;
            this.p_best_10m_airp_ama = p_best_10m_airp_ama;
            this.p_best_10m_airp_semp = p_best_10m_airp_semp;
            this.p_best_10m_airp_pro = p_best_10m_airp_pro;
            this.p_best_10m_air_r_ama = p_best_10m_air_r_ama;
            this.p_best_10m_air_r_pro = p_best_10m_air_r_pro;
            this.p_best_10m_air_r_semp = p_best_10m_air_r_semp;
            this.p_best_25m_rf_ama = p_best_25m_rf_ama;
            this.p_best_25m_rf_pro = p_best_25m_rf_pro;
            this.p_best_25m_rf_semp = p_best_25m_rf_semp;
            this.total_player_score = total_player_score;
        }

    }
}