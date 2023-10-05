using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.CloudSave;
using Firebase.Firestore;
using Firebase.Extensions;
using System;
using System.Threading.Tasks;
using TMPro;
using UnityEngine.UI;
using Newtonsoft.Json;
using Firebase.Database;

public class LiveUserDataManager : MonoBehaviour
{
    public static LiveUserDataManager Instance;
    private CollectionReference usersCollection;
    public FirebaseFirestore firestoreDB;
    private bool dataSent = false;
    private bool isUserExist;
    int pos = 0;
    int newLoc;
    [SerializeField]
    string project_id;

    // :: Meta Users Data required for the Game :: START ::
    public string userID
    {
        get { return metaUserId; }
        set { metaUserId = userID; }
    }
    private string metaUserId;

    public string userName;


    private string metaUserName;

    public bool isauth
    {
        get { return isMetaAuth; }
        set { isMetaAuth = isauth; }
    }
    private bool isMetaAuth;

    public bool isUserDataUp
    {
        get { return isMetaUserDataUp; }
        set { isMetaUserDataUp = isUserDataUp; }
    }
    private bool isMetaUserDataUp;

    public string dbName;

    public int currentUID, currentGID;

    public float zoneAMulti, zoneBMulti, zoneCMulti, difficulty_muliti;
    public float amateurValue, proValue, semiProValue;
    public string currentUserUID;
    // :: Meta Users Data required for the Game :: END ::

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        DontDestroyOnLoad(Instance);
    }

    async void serInit()
    {
        await UnityServices.InitializeAsync();
    }

    private void Start()
    {
        isauth = false;
        isUserExist = false;
        isMetaUserDataUp = false;

    }


    private void Update()
    {

    }



    public void FetchRulesData()
    {
        DatabaseReference databaseReference = LiveUserDataManagerRealtime.Instance.universal_databaseReference;
        databaseReference.Child("rule_engine_master").GetValueAsync().ContinueWith(task => {
            if (task.IsFaulted)
            {
                Debug.LogError("Failed to fetch data from Firebase: " + task.Exception);
                return;
            }

            DataSnapshot snapshot = task.Result;
            DataSnapshot ruleSnapshot = snapshot.Child("R001"); // Assuming we're fetching data for R001

            // Parse data and save to local variables
            amateurValue = float.Parse(ruleSnapshot.Child("Amateur").Value.ToString());
            zoneAMulti = float.Parse(ruleSnapshot.Child("Precision Zone A Multiplier").Value.ToString());
            zoneBMulti = float.Parse(ruleSnapshot.Child("Precision Zone B Multiplier").Value.ToString());
            zoneCMulti = float.Parse(ruleSnapshot.Child("Precision Zone C Multiplier").Value.ToString());
            proValue = float.Parse(ruleSnapshot.Child("Pro").Value.ToString());
            semiProValue = float.Parse(ruleSnapshot.Child("Semi Pro").Value.ToString());
        });
    }




    public void SavePistolGameDataToLiveDB()
    {

        string dt = DateTime.Now.ToString(" | yyyy-MM-dd HH:mm:ss");

        GunDataManager.Instance.gameMode += dt;

        // Create a user data object
        GameHistoryDataPistol GameData = new GameHistoryDataPistol(LocalUserDataManager.Instance.userID, LocalUserDataManager.Instance.userName,
                                                       GunDataManager.Instance.gameMode, GunDataManager.Instance.ScoresPistol, GunDataManager.Instance.sr1ScorePistol,
                                                       GunDataManager.Instance.sr2ScorePistol, GunDataManager.Instance.sr3ScorePistol, GunDataManager.Instance.totalGameScorePistol,
                                                       GunDataManager.Instance.noOfShotsOnTarget, GunDataManager.Instance.noOfShotsMissed, GunDataManager.Instance.avgSrScorePistol,
                                                       GunDataManager.Instance.noOfInnerTens, GunDataManager.Instance.totalTimeSpent, GunDataManager.Instance.personalGameBestPistol,
                                                       GunDataManager.Instance.zoneA_mult, GunDataManager.Instance.zoneB_mult, GunDataManager.Instance.zoneC_mult, GunDataManager.Instance.diffculty_mult);

        string pistolGameData = JsonConvert.SerializeObject(GameData);

        PushDataToFirebase(pistolGameData);

    }

    async void PushDataToFirebase(string jsonData)
    {
        try
        {
            // Push the JSON data to Firebase
            await LiveUserDataManagerRealtime.Instance.universal_databaseReference.Child("game_history").Child("GH" + currentGID).SetRawJsonValueAsync(jsonData);
            Debug.Log("Data successfully sent to Firebase.");
            currentGID++;
            UpdateGIDValue(currentUID.ToString());
        }
        catch (Exception e)
        {
            Debug.LogError("Failed to send data to Firebase: " + e.Message);
        }
    }

    public void UpdateGIDValue(string newValue)
    {
        DatabaseReference databaseReference = LiveUserDataManagerRealtime.Instance.universal_databaseReference;

        if (databaseReference != null)
        {
            // Update the value at the specified node path
            databaseReference.Child("tableUID_mst").Child("gh_GID").SetValueAsync(newValue)
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
    public void SaveRifleGameDataToLiveDB()
    {

        string dt = DateTime.Now.ToString(" | yyyy-MM-dd HH:mm:ss");

        GunDataManager.Instance.gameMode += dt;

        // Create a user data object
        GameHistoryDataRifle GameData = new GameHistoryDataRifle(LocalUserDataManager.Instance.userID, LocalUserDataManager.Instance.userName,
                                                       GunDataManager.Instance.gameMode, GunDataManager.Instance.ScoresPistol, GunDataManager.Instance.sr1ScorePistol,
                                                       GunDataManager.Instance.sr2ScorePistol, GunDataManager.Instance.sr3ScorePistol, GunDataManager.Instance.totalGameScoreRifle,
                                                       GunDataManager.Instance.noOfShotsOnTarget, GunDataManager.Instance.noOfShotsMissed, GunDataManager.Instance.avgSrScorePistol,
                                                       GunDataManager.Instance.noOfInnerTens, GunDataManager.Instance.totalTimeSpent, GunDataManager.Instance.personalGameBestPistol,
                                                       GunDataManager.Instance.zoneA_mult, GunDataManager.Instance.zoneB_mult, GunDataManager.Instance.zoneC_mult, GunDataManager.Instance.diffculty_mult);

        string rifleGameData = JsonConvert.SerializeObject(GameData);

        PushDataToFirebase(rifleGameData);
    }







    public void UpdateUserTableData(string newValue, string key)
    {
        DatabaseReference databaseReference = LiveUserDataManagerRealtime.Instance.universal_databaseReference;

        if (databaseReference != null)
        {
            // Update the value at the specified node path
            databaseReference.Child("user_master").Child(LiveUserDataManager.Instance.currentUserUID).Child(key).SetValueAsync(newValue)
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

    public void saveMainLeaderBoardDataAirPistol()
    {

    }
    public void saveProLeaderBoardDataAirPistol()
    {

    }
    public void saveAmateurLeaderBoardDataAirPistol()
    {

    }
    public void saveSemiProLeaderBoardDataAirPistol()
    {

    }
}





