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
        firestoreDB = FirebaseFirestore.DefaultInstance;
    }


    private void Update()
    {

    }


   


    public void SaveUserDataToLiveDB()
    {
        string database = dbName;

        database += "_MetaUsers";

        usersCollection = firestoreDB.Collection(database);

        if (isUserExist == true)
        {  // Create a user data object
            UserProfileData userData = new UserProfileData(LocalUserDataManager.Instance.userID, LocalUserDataManager.Instance.userName);

            // Convert user data object to a dictionary
            Dictionary<string, object> userDataDict = userData.ToDictionary();
            // Add the user data to Firestore
            usersCollection.Document(LocalUserDataManager.Instance.userID).Collection("UserData").Document("ProfileData").SetAsync(userDataDict)
                .ContinueWithOnMainThread(task =>
                {
                    if (task.IsCanceled || task.IsFaulted)
                    {
                        Debug.Log("Failed to save user data: " + task.Exception);
                        return;
                    }

                    Debug.Log("User data saved successfully!");
                });
        }

        if (isUserExist == false)
        {  // Create a user data object
            UserProfileData userData = new UserProfileData(LocalUserDataManager.Instance.userID, LocalUserDataManager.Instance.userName, 0,0,0);

            // Convert user data object to a dictionary
            Dictionary<string, object> userDataDict = userData.ToDictionary();
            // Add the user data to Firestore
            usersCollection.Document(LocalUserDataManager.Instance.userID).Collection("UserData").Document("ProfileData").SetAsync(userDataDict)
                .ContinueWithOnMainThread(task =>
                {
                    if (task.IsCanceled || task.IsFaulted)
                    {
                        Debug.Log("Failed to save user data: " + task.Exception);
                        return;
                    }

                    Debug.Log("User data saved successfully!");
                });
        }

    }


    public void SaveGameDataToLiveDB()
    {

        string dt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

       

       

        GunDataManager.Instance.gameMode += dt;

        string database = dbName;

        database += "_MetaUsers";

        usersCollection = firestoreDB.Collection(database);

        
        // Create a user data object
        GameHistoryData GameData = new GameHistoryData(LocalUserDataManager.Instance.userID, LocalUserDataManager.Instance.userName,
                                                       GunDataManager.Instance.gameMode, GunDataManager.Instance.Scores, GunDataManager.Instance.sr1Score,
                                                       GunDataManager.Instance.sr2Score, GunDataManager.Instance.sr3Score, GunDataManager.Instance.totalGameScore,
                                                       GunDataManager.Instance.noOfShotsOnTarget, GunDataManager.Instance.noOfShotsMissed, GunDataManager.Instance.avgSrScore,
                                                       GunDataManager.Instance.noOfInnerTens, GunDataManager.Instance.totalTimeSpent, GunDataManager.Instance.personalGameBest);

        // Convert user data object to a dictionary
        Dictionary<string, object> gameDataDict = GameData.ToDictionary();
        // Add the user data to Firestore
        usersCollection.Document(LocalUserDataManager.Instance.userID).Collection("UserData").Document("GameHistory").Collection("GameHistoryData").Document(GunDataManager.Instance.gameMode).SetAsync(gameDataDict)
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsCanceled || task.IsFaulted)
                {
                    Debug.LogError("Failed to save game data: " + task.Exception);
                    return;
                }

                Debug.Log("User data saved successfully!");
            });
    }




    private void OnApplicationQuit()
    {


        string database = dbName;

        database += "_MetaUsers";

        usersCollection = firestoreDB.Collection(database);

            // Create a user data object
            UserProfileData userData = new UserProfileData(LocalUserDataManager.Instance.totalTime);

            // Convert user data object to a dictionary
            Dictionary<string, object> userDataDict = userData.ToDictionary();

            usersCollection.Document(LocalUserDataManager.Instance.userID).Collection("UserData").Document("TimeSpent").SetAsync(userDataDict)
                .ContinueWithOnMainThread(task =>
                {
                    if (task.IsCanceled || task.IsFaulted)
                    {
                        Debug.LogError("Failed to save game data: " + task.Exception);
                        return;
                    }

                    Debug.Log("User data saved successfully!");
                });


           
        
    }


    public async void getUserBestScore()
    {
        string projectId = project_id;

        String database = dbName;

        database += "_MetaUsers";


        DocumentReference docRef = firestoreDB.Collection(database).Document(LocalUserDataManager.Instance.userID).Collection("UserData").Document("GameHistory");
        DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

        if (snapshot.Exists)
        {
            GunDataManager.Instance.personalAmaBest = snapshot.GetValue<int>("UserAmatureBest");
            GunDataManager.Instance.personalSemiProBest = snapshot.GetValue<int>("UserSemiProBest");
            GunDataManager.Instance.personalProBest = snapshot.GetValue<int>("UserProBest");

        }
        else
        {
        }
    }
   

    public async void CheckIfDocumentExists()
    {
        string database = dbName;

        database += "_MetaUsers";


        DocumentReference docRef = firestoreDB.Collection(database).Document(LocalUserDataManager.Instance.userID);
        DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

        if (snapshot.Exists)
        {
            Debug.Log("Document  exists in collection .");
            isUserExist = true;
        }
        else
        {
            Debug.Log("Document  does not exist in collection .");
            isUserExist = false;
            

        }
        LiveUserDataManager.Instance.SaveUserDataToLiveDB();
    }




    // Leaderboard 
    public void saveLeaderBoardData()
    {
    
        String database = dbName;

        database += "_LeaderBoardData";

        usersCollection = firestoreDB.Collection(database);

        // Create a user data object
        LeaderboardData leaderData = new LeaderboardData(LocalUserDataManager.Instance.userName, LocalUserDataManager.Instance.totalScore, GunDataManager.Instance.noOfInnerTens,GunDataManager.Instance.personalGameBest);

        // Convert user data object to a dictionary
        Dictionary<string, object> userDataDict = leaderData.ToDictionary();
        // Add the user data to Firestore
        usersCollection.Document(LocalUserDataManager.Instance.selectedGameMode).Collection(LocalUserDataManager.Instance.SelectedGameLevel).Document(LocalUserDataManager.Instance.userID).SetAsync(userDataDict)
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsCanceled || task.IsFaulted)
                {
                    Debug.LogError("Failed to save user data: " + task.Exception);
                    return;
                }

                Debug.Log("User data saved successfully!");
            });
    }





    public async void getUserLeaderBoardData()
    {
        String database = dbName;

        database += "_LeaderBoardData";


        DocumentReference docRef = firestoreDB.Collection(database).Document(LocalUserDataManager.Instance.selectedGameMode).Collection(LocalUserDataManager.Instance.SelectedGameLevel).Document(LocalUserDataManager.Instance.userID);
        DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

        if (snapshot.Exists)
        {
          LocalUserDataManager.Instance.totalScore = snapshot.GetValue<int>("TotalScore");
          LocalUserDataManager.Instance.personalAmaBest = snapshot.GetValue<int>("BestScore");
          LocalUserDataManager.Instance.totalInnerTens = snapshot.GetValue<int>("InnerTens");

        }
        else
        {
            LocalUserDataManager.Instance.totalScore = 0;
            LocalUserDataManager.Instance.personalAmaBest = 0;
            LocalUserDataManager.Instance.totalInnerTens = 0;

        }
    }


    public async void sortLeaderBoard()
    {
        String database = dbName;

        database += "_LeaderBoardData";


        QuerySnapshot snapshot = await firestoreDB.Collection(database).Document(LocalUserDataManager.Instance.selectedGameMode).Collection(LocalUserDataManager.Instance.SelectedGameLevel).OrderByDescending("TotalScore").GetSnapshotAsync();

        LocalUserDataManager.Instance.Leaders = new List<Dictionary<string, object>>();


        pos = 0;

        foreach (DocumentSnapshot document in snapshot.Documents)
        {
            LocalUserDataManager.Instance.Leaders.Add(document.ToDictionary());

            if(String.Compare(LocalUserDataManager.Instance.userID, document.Id) == 0)
            {
                Debug.Log("found at"+pos);
                newLoc = pos;
                pos++;
            }
            else
            {
               // Debug.Log("Not found at" + pos);
                pos++;

            }
           
          //  Debug.Log(document.Id);
        }



        HUB_UIManager.Instance.title.text = LocalUserDataManager.Instance.selectedGameMode + " - " + LocalUserDataManager.Instance.SelectedGameLevel;
        Debug.Log("------ " + LocalUserDataManager.Instance.selectedGameMode);

        for (int i = 0; i < 5; i++)
        {
            Dictionary<string, object> document = LocalUserDataManager.Instance.Leaders[i];
         
            string totalscore = document["TotalScore"].ToString();
            string name = document["Name"].ToString();
            string bestscore = document["BestScore"].ToString();
            string innerTens = document["InnerTens"].ToString();

            HUB_UIManager.Instance.Leaders[i].gameObject.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = name;
            HUB_UIManager.Instance.Leaders[i].gameObject.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text = bestscore;
            HUB_UIManager.Instance.Leaders[i].gameObject.transform.GetChild(3).gameObject.GetComponent<TextMeshProUGUI>().text = innerTens;
            HUB_UIManager.Instance.Leaders[i].gameObject.transform.GetChild(4).gameObject.GetComponent<TextMeshProUGUI>().text = totalscore;


        }
       
        if(newLoc < 5)
        {
            HUB_UIManager.Instance.Leaders[newLoc].GetComponent<Image>().sprite = HUB_UIManager.Instance.leaderH;
        }
        else
        {
            Dictionary<string, object> document = LocalUserDataManager.Instance.Leaders[newLoc];

            string totalscore = document["TotalScore"].ToString();
            string name = document["Name"].ToString();
            string bestscore = document["BestScore"].ToString();
            string innerTens = document["InnerTens"].ToString();
            HUB_UIManager.Instance.LeaderN.SetActive(true);

            HUB_UIManager.Instance.LeaderN.gameObject.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = (newLoc+1).ToString();

            HUB_UIManager.Instance.LeaderN.gameObject.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = name;
            HUB_UIManager.Instance.LeaderN.gameObject.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text = bestscore;
            HUB_UIManager.Instance.LeaderN.gameObject.transform.GetChild(3).gameObject.GetComponent<TextMeshProUGUI>().text = innerTens;
            HUB_UIManager.Instance.LeaderN.gameObject.transform.GetChild(4).gameObject.GetComponent<TextMeshProUGUI>().text = totalscore;

        }
    }



    public async void sortGameLeaderBoard()
    {
        String database = dbName;

        database += "_LeaderBoardData";


        QuerySnapshot snapshot = await firestoreDB.Collection(database).Document(LocalUserDataManager.Instance.selectedGameMode).Collection(LocalUserDataManager.Instance.SelectedGameLevel).OrderByDescending("TotalScore").GetSnapshotAsync();

        LocalUserDataManager.Instance.Leaders = new List<Dictionary<string, object>>();


        pos = 0;

        foreach (DocumentSnapshot document in snapshot.Documents)
        {
            LocalUserDataManager.Instance.Leaders.Add(document.ToDictionary());

            if (String.Compare(LocalUserDataManager.Instance.userID, document.Id) == 0)
            {
                Debug.Log("found at" + pos);
                newLoc = pos;
                pos++;
            }
            else
            {
                // Debug.Log("Not found at" + pos);
                pos++;

            }

            //  Debug.Log(document.Id);
        }



        PistolUIManager.Instance.title.text = LocalUserDataManager.Instance.selectedGameMode + " - " + LocalUserDataManager.Instance.SelectedGameLevel;
        PistolUIManager.Instance.title.text = LocalUserDataManager.Instance.selectedGameMode + " - " + LocalUserDataManager.Instance.SelectedGameLevel;
        Debug.Log("------ " + LocalUserDataManager.Instance.selectedGameMode);

        for (int i = 0; i < 3; i++)
        {
            Dictionary<string, object> document = LocalUserDataManager.Instance.Leaders[i];

            string totalscore = document["TotalScore"].ToString();
            string name = document["Name"].ToString();
            string bestscore = document["BestScore"].ToString();
            string innerTens = document["InnerTens"].ToString();

            PistolUIManager.Instance.Leaders[i].gameObject.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = name;
            PistolUIManager.Instance.Leaders[i].gameObject.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text = bestscore;
            PistolUIManager.Instance.Leaders[i].gameObject.transform.GetChild(3).gameObject.GetComponent<TextMeshProUGUI>().text = innerTens;
            PistolUIManager.Instance.Leaders[i].gameObject.transform.GetChild(4).gameObject.GetComponent<TextMeshProUGUI>().text = totalscore;


        }

        if (newLoc < 3)
        {
            PistolUIManager.Instance.Leaders[newLoc].GetComponent<Image>().sprite = PistolUIManager.Instance.leaderH;
        }
        else
        {
            Dictionary<string, object> document = LocalUserDataManager.Instance.Leaders[newLoc];

            string totalscore = document["TotalScore"].ToString();
            string name = document["Name"].ToString();
            string bestscore = document["BestScore"].ToString();
            string innerTens = document["InnerTens"].ToString();
            PistolUIManager.Instance.LeaderN.SetActive(true);

            PistolUIManager.Instance.LeaderN.gameObject.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = (newLoc + 1).ToString();

            PistolUIManager.Instance.LeaderN.gameObject.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = name;
            PistolUIManager.Instance.LeaderN.gameObject.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text = bestscore;
            PistolUIManager.Instance.LeaderN.gameObject.transform.GetChild(3).gameObject.GetComponent<TextMeshProUGUI>().text = innerTens;
            PistolUIManager.Instance.LeaderN.gameObject.transform.GetChild(4).gameObject.GetComponent<TextMeshProUGUI>().text = totalscore;

        }
    }

}





