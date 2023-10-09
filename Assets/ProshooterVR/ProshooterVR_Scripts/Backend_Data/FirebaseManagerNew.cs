using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Firebase.Database;
using Firebase;
using System;
using Newtonsoft.Json;
using System.Threading.Tasks;

public class FirebaseManagerNew : MonoBehaviour
{

    public static FirebaseManagerNew Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        DontDestroyOnLoad(Instance);
    }
    DatabaseReference databaseReference;
    FirebaseDatabase firebaseDB;
  
    public bool isUserPresent;
    public bool isUDUpdated;


    public int currentUID,currentGID;
    public string uidToUpdate_retrive;

    private void Start()
    {
        firebaseDB = FirebaseDatabase.GetInstance("https://proshootervr-d82e9-default-rtdb.asia-southeast1.firebasedatabase.app");
        databaseReference = firebaseDB.RootReference;
        LiveUserDataManagerRealtime.Instance.universal_databaseReference = databaseReference;
        isUserPresent = false;
        isUDUpdated = false;
        getCurrent_uniqueIDs();

        ///for testing purpose------------------------
        
        ///-------------------------------------------



        
        // userProfileUpdate();

        // Initialize Firestore
        //firestoreDB = FirebaseFirestore.DefaultInstance;
    }

    public void Initialise_BackendDAta()
    {
        LiveUserDataManager.Instance.FetchRulesData();
        LiveUserDataManager.Instance.StartCoroutine(LiveUserDataManager.Instance.lb_ID_airpistolData());
        LiveUserDataManager.Instance.StartCoroutine(LiveUserDataManager.Instance.lb_ID_airRifleData());
        LiveUserDataManager.Instance.StartCoroutine(LiveUserDataManager.Instance.lb_ID_rapidFireData());

        LiveUserDataManager.Instance.StartCoroutine(LiveUserDataManager.Instance.fetchNoOfGamesPlayedAirPistol());
        LiveUserDataManager.Instance.StartCoroutine(LiveUserDataManager.Instance.fetchNoOfGamesPlayedAirRifle());
        LiveUserDataManager.Instance.StartCoroutine(LiveUserDataManager.Instance.fetchNoOfGamesPlayedRapidFire());

        userProfileUpdate();
    }

    public void getCurrent_uniqueIDs()
    {
        StartCoroutine(getUIDData());
    }

    public IEnumerator getUIDData()
    {
        var test = databaseReference.Child("tableUID_mst").Child("user_UID").GetValueAsync();
        yield return new WaitUntil(predicate: () => test.IsCompleted);

        if(test != null)
        {
            DataSnapshot snapshot = test.Result;
            currentUID = int.Parse(snapshot.Value.ToString());
            LiveUserDataManager.Instance.currentUID = currentUID;
        }
    }

    public IEnumerator getGameIDData()
    {
        var test = databaseReference.Child("tableUID_mst").Child("gdata_ID").GetValueAsync();
        yield return new WaitUntil(predicate: () => test.IsCompleted);

        if (test != null)
        {
            DataSnapshot snapshot = test.Result;
            currentGID = int.Parse(snapshot.Value.ToString());
            LiveUserDataManager.Instance.currentUID = currentGID;

        }
    }



    /// <summary>
    /// 
    /// </summary>
    /// <param name="isUserPresent"></param>
    /// 

    async Task<bool> CheckMetaIdPresence(string targetMetaId)
    {
        bool isMetaIdPresent = false;
        
       await LiveUserDataManagerRealtime.Instance.universal_databaseReference
            .Child("user_master").GetValueAsync().ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    Debug.LogError("Error fetching Firebase data: " + task.Exception);
                    return;
                }

                if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;

                    if (snapshot.Exists)
                    {
                        foreach (var childSnapshot in snapshot.Children)
                        {
                            string jsonData = childSnapshot.GetRawJsonValue();
                            Dictionary<string, string> userDict = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonData);

                            if (userDict.ContainsKey("meta_id"))
                            {
                                string metaId = userDict["meta_id"];
                               // Debug.Log("User: " + childSnapshot.Key + ", meta_id: " + metaId);

                                if (metaId == targetMetaId)
                                {
                                    Debug.Log("meta_id " + targetMetaId + " is present for user " + childSnapshot.Key);
                                    isMetaIdPresent = true;
                                    uidToUpdate_retrive = childSnapshot.Key;
                                    break;
                                }
                            }
                        }
                    }
                }
            });

        return isMetaIdPresent;
    }

    
    public void userProfileUpdate()
    {
        

        Task<bool> isMetaIdPresentTask = CheckMetaIdPresence(LocalUserDataManager.Instance.userID);
        isMetaIdPresentTask.ContinueWith(resultTask =>
        {
            if (resultTask.IsCompleted)
            {
                bool isMetaIdPresent = resultTask.Result;
                //Debug.Log("Is meta_id 2234 present: " + isMetaIdPresent);
                if (isMetaIdPresent == true)
                {
                    dontUpdateData();
                }
                else
                {
                    UpdateNewUserData();
                    // The user is not present, handle it as needed
                }
            }

        });

        //if (isPresent == true)
        //{
        //    dontUpdateData();
        //}
        //else
        //{
        //    UpdateNewUserData();

        //    // The user is not present, handle it as needed
        //}
    }

    private void dontUpdateData()
    {

        Debug.Log("present");
        FetchProfileData();

        isUDUpdated = true;

        // Implement your specific method here
    }
    private void UpdateNewUserData()
    {
        Debug.Log("not present");
        if (isUDUpdated == false)
        {
            SaveMetaInfo(LocalUserDataManager.Instance.userID, LocalUserDataManager.Instance.userName);
            isUDUpdated = true;
        }
        // Implement your specific method here
    }



    ////////////////////




    public void SaveMetaInfo(string metaID, string metaName)
    {
        if (databaseReference != null)
        {
            // Create a new dictionary to store meta_id and meta_name
            Dictionary<string, object> metaData = new Dictionary<string, object>
            {
                { "meta_id", metaID },
                { "meta_name", metaName },
                { "matches_played", "0" },
                { "totalPlayerScore", "0" },
                { "accuracy", "0" },
                { "pbest_10mAirP_Ama", "0" },
                { "pbest_10mAirP_SemP", "0" },
                { "pbest_10mAirP_Pro", "0" },
                { "pbest_10mAirR_Ama", "0" },
                { "pbest_10mAirR_SemP", "0" },
                { "pbest_10mAirR_Pro", "0" },
                { "pbest_25mRF_Ama", "0" },
                { "pbest_25mRF_SemP", "0" },
                { "pbest_25mRF_Pro", "0" },
            };

            // Push the data to a new child node under "user_meta"
            databaseReference.Child("user_master").Child("UID"+currentUID).SetValueAsync(metaData);

            currentUID++;
            UpdateUIDValue(currentUID.ToString());

        }
        else
        {
            Debug.LogError("Firebase Database reference is not initialized.");
        }
    }





    public void UpdateUIDValue(string newValue)
    {
        if (databaseReference != null)
        {
            // Update the value at the specified node path
            databaseReference.Child("tableUID_mst").Child("user_UID").SetValueAsync(newValue)
                .ContinueWith(task =>
                {
                    if (task.IsCompleted)
                    {
                        Debug.Log("Update completed successfully.");
                    }
                    else if (task.IsFaulted)
                    {
                        Debug.LogError("Update failed. Error: " + task.Exception.ToString());
                    }
                });
        }
        else
        {
            Debug.LogError("Firebase Database reference is not initialized.");
        }
    }


    public async Task<bool> CheckIfUser_IDPresent(string metaID,string id)
    {
        try
        {
            DataSnapshot snapshot = await LiveUserDataManagerRealtime.Instance.universal_databaseReference
                .Child("user_master").Child(id).Child("meta_id").GetValueAsync();

            if (snapshot.Exists && snapshot.Child(metaID).Exists)
            {
                Debug.Log("ID is present in user d." + snapshot.Child(metaID).Key);
                
                return true;
            }
            else
            {
                Debug.Log("ID is not present.");
                return false;
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Failed to fetch data from Firebase: " + ex.Message);
            return false;
        }
    }



    private void FetchProfileDataFromFirebase()
    {
        PlayerData playerData;
        DatabaseReference playerDataReference = LiveUserDataManagerRealtime.Instance.universal_databaseReference;
        Debug.Log(LiveUserDataManager.Instance.currentUserUID);
        playerDataReference.Child("user_master").Child(LiveUserDataManager.Instance.currentUserUID).GetValueAsync().ContinueWith(task => {
            if (task.IsFaulted || task.IsCanceled)
            {
                Debug.LogError("Failed to fetch data from Firebase: " + task.Exception);
                return;
            }

            DataSnapshot snapshot = task.Result;
            string jsonData = snapshot.GetRawJsonValue();
            Debug.Log(jsonData);
            if (!string.IsNullOrEmpty(jsonData))
            {
                playerData = JsonUtility.FromJson<PlayerData>(jsonData);

                // Access the data using playerData object
                Debug.Log("User Master: " + playerData.user_master);
                Debug.Log("UID0: " + playerData.UID0);
                Debug.Log("UID1: " + playerData.UID1);
                // ... and so on for other variables
            }
            else
            {
                Debug.Log("No data found in the specified Firebase node.");
            }
        });
    }

    public void FetchProfileData()
    {
        DatabaseReference databaseReference = LiveUserDataManagerRealtime.Instance.universal_databaseReference;
        databaseReference.Child("user_master").GetValueAsync().ContinueWith(task => {
            if (task.IsFaulted)
            {
                Debug.LogError("Failed to fetch data from Firebase: " + task.Exception);
                return;
            }

            DataSnapshot snapshot = task.Result;
            DataSnapshot ruleSnapshot = snapshot.Child(uidToUpdate_retrive); // Assuming we're fetching data for R001

            Debug.Log("Fetching data " + ruleSnapshot.Child("meta_name").Value);
            // Parse data and save to local variables


            LocalUserDataManager.Instance.userNameTxt = ruleSnapshot.Child("meta_name").Value.ToString();
            LocalUserDataManager.Instance.totalScoreTxt = ruleSnapshot.Child("totalPlayerScore").Value.ToString();
            LocalUserDataManager.Instance.matchesPlayedTxt = ruleSnapshot.Child("matches_played").Value.ToString();
            LocalUserDataManager.Instance.accuracyTxt = ruleSnapshot.Child("accuracy").Value.ToString();

            LocalUserDataManager.Instance.pbest_10mAirP_AmaTxt = ruleSnapshot.Child("pbest_10mAirP_Ama").Value.ToString();
            LocalUserDataManager.Instance.pbest_10mAirP_SemPTxt = ruleSnapshot.Child("pbest_10mAirP_SemP").Value.ToString();
            LocalUserDataManager.Instance.pbest_10mAirP_ProTxt = ruleSnapshot.Child("pbest_10mAirP_Pro").Value.ToString();

            LocalUserDataManager.Instance.pbest_10mAirR_AmaTxt = ruleSnapshot.Child("pbest_10mAirR_Ama").Value.ToString();
            LocalUserDataManager.Instance.pbest_10mAirR_SemPTxt = ruleSnapshot.Child("pbest_10mAirR_SemP").Value.ToString();
            LocalUserDataManager.Instance.pbest_10mAirR_ProTxt = ruleSnapshot.Child("pbest_10mAirR_Pro").Value.ToString();

            LocalUserDataManager.Instance.pbest_25mRF_AmaTxt = ruleSnapshot.Child("pbest_25mRF_Ama").Value.ToString();
            LocalUserDataManager.Instance.pbest_25mRF_SemPTxt = ruleSnapshot.Child("pbest_25mRF_Pro").Value.ToString();
            LocalUserDataManager.Instance.pbest_25mRF_ProTxt = ruleSnapshot.Child("pbest_25mRF_SemP").Value.ToString();

            Debug.Log("Data feteched successfully------------------");
            
            HUB_UIManager.Instance.userNameTxtMainMenu.text = LocalUserDataManager.Instance.userNameTxt;
            



        });
       
    }

}


[System.Serializable]
public class PlayerData
{
    public string user_master;
    public string UID0;
    public string UID1;
    public int accuracy;
    public int matches_played;
    public int meta_id;
    public string meta_name;
    public int pbest_10mAirP_Ama;
    public int pbest_10mAirP_Pro;
    public int pbest_10mAirP_SemP;
    public int pbest_10mAirR_Ama;
    public int pbest_10mAirR_Pro;
    public int pbest_10mAirR_SemP;
    public int pbest_25mRF_Ama;
    public int pbest_25mRF_Pro;
    public int pbest_25mRF_SemP;
    public int totalPlayerScore;
}