using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Firebase.Firestore;
using Firebase.Extensions;

public class FirebaseManagerNew : MonoBehaviour
{

    private FirebaseFirestore firestoreDB;
    private CollectionReference usersCollection;


    private void Start()
    {
        // Initialize Firestore
        firestoreDB = FirebaseFirestore.DefaultInstance;
        
        // Get a reference to the users collection
        //saveMeta();
    }

    public void saveMeta()
    {
       

        // Save data to Firestore
        SaveUserData();
    }


    public void SaveUserData()
    {
        usersCollection = firestoreDB.Collection("MetaUsers");
        // Create a user data object
        UserData userData = new UserData("John", "Oculus", "johndoe@example.com", 10);

        // Convert user data object to a dictionary
        Dictionary<string, object> userDataDict = userData.ToDictionary();
        VRDebugManager.Instance.AddLog("In the save data!!");
        // Add the user data to Firestore
        usersCollection.Document("new").SetAsync(userDataDict)
            .ContinueWithOnMainThread(task => {
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

public class UserData
{
    public string firstName;
    public string lastName;
    public string email;
    public int age;

    public UserData(string firstName, string lastName, string email, int age)
    {
        this.firstName = firstName;
        this.lastName = lastName;
        this.email = email;
        this.age = age;
    }

    public Dictionary<string, object> ToDictionary()
    {
        Dictionary<string, object> result = new Dictionary<string, object>();
        result["firstName"] = firstName;
        result["lastName"] = lastName;
        result["email"] = email;
        result["age"] = age;
        return result;
    }
}


