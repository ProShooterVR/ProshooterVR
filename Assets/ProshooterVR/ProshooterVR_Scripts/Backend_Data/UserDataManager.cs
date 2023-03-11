using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.CloudSave;
using Firebase.Firestore;
using Firebase.Extensions;

public class UserDataManager : MonoBehaviour
{
    public static UserDataManager Instance;
    private CollectionReference usersCollection;
    public FirebaseFirestore firestoreDB;

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

   

    // :: Meta Users Data required for the Game :: END ::

    private void Awake()
    {
        Instance = this;
        
       // serInit();
    }

    async void serInit()
    {
        await UnityServices.InitializeAsync();
    }

    private void Start()
    {
       
        isauth = false;
        isMetaUserDataUp = false;
        
        firestoreDB = FirebaseFirestore.DefaultInstance;
        saveMeta();
    }

    //public async void saveData(string nonce)
    //{

    //    await AuthenticationService.Instance.SignInWithOculusAsync(nonce,userID.ToString());
    //    var data = new Dictionary<string, object> { { userID.ToString(), metaUserName } };
    //    await CloudSaveService.Instance.Data.ForceSaveAsync(data);
    //    Debug.Log("Data Uploaded");
    //    VRDebugManager.Instance.AddLog("User Id By Oculus : " + userID);


    //}

    public void saveMeta()
    {

        // Save data to Firestore
        SaveUserData();
    }


    public void SaveUserData()
    {
        usersCollection = firestoreDB.Collection("MetaUsers");

        // Create a user data object
        UserData userData = new UserData("John", "Doe", "johndoe@example.com", 30);

        // Convert user data object to a dictionary
        Dictionary<string, object> userDataDict = userData.ToDictionary();
        VRDebugManager.Instance.AddLog("In the save data!!");
        // Add the user data to Firestore
        usersCollection.Document(userName).SetAsync(userDataDict)
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsCanceled || task.IsFaulted)
                {
                    Debug.LogError("Failed to save user data: " + task.Exception);
                    VRDebugManager.Instance.AddLog("Failed to save user data: " + task.Exception);
                    return;
                }

                Debug.Log("User data saved successfully!");
                VRDebugManager.Instance.AddLog("User data saved successfully!");
            });
    }
}

//public class UserData
//{
//    public string firstName;
//    public string lastName;
//    public string email;
//    public int age;

//    public UserData(string firstName, string lastName, string email, int age)
//    {
//        this.firstName = firstName;
//        this.lastName = lastName;
//        this.email = email;
//        this.age = age;
//    }

//    public Dictionary<string, object> ToDictionary()
//    {
//        Dictionary<string, object> result = new Dictionary<string, object>();
//        result["firstName"] = firstName;
//        result["lastName"] = lastName;
//        result["email"] = email;
//        result["age"] = age;
//        return result;
//    }


