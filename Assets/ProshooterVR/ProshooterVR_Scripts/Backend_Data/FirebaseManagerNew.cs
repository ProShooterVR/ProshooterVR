using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Firebase.Database;
using Firebase;
using System;
using Newtonsoft.Json;

public class FirebaseManagerNew : MonoBehaviour
{
    DatabaseReference databaseReference;
    FirebaseDatabase firebaseDB;
  
    public bool isUserPresent;
    public bool isUDUpdated;


    public int currentUID,currentGID;


    private void Start()
    {
        firebaseDB = FirebaseDatabase.GetInstance("https://proshootervr-d82e9-default-rtdb.asia-southeast1.firebasedatabase.app");
        databaseReference = firebaseDB.RootReference;
        LiveUserDataManagerRealtime.Instance.universal_databaseReference = databaseReference;
        isUserPresent = false;
        isUDUpdated = false;
        FetchGameDataFromFirebase();
        getCurrent_uniqueIDs();
        LiveUserDataManager.Instance.FetchRulesData();
        // Initialize Firestore
        //firestoreDB = FirebaseFirestore.DefaultInstance;
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



    private async void FetchGameDataFromFirebase()
    {
        if (databaseReference != null)
        {
            DataSnapshot snapshot = await databaseReference.Child("user_master").GetValueAsync();
            if (snapshot.Exists)
            {
                // Convert the JSON data to a dictionary
                Dictionary<string, Dictionary<string, string>> gameDataDict = new Dictionary<string, Dictionary<string, string>>();
                foreach (var gameHistorySnapshot in snapshot.Children)
                {
                    string uid = gameHistorySnapshot.Key;
                    Debug.Log(uid);
                    fetchData(uid);
                }

               
            }
            else
            {
                Debug.Log("No game data in the Firebase database.");
            }
        }
        else
        {
            Debug.LogError("Firebase Database reference is not initialized.");
        }
    }




    public void fetchData(string uid)
    {
        string userUIDToCheck = uid; // Replace with the actual user UID to check
        StartCoroutine(checkData(userUIDToCheck, OnCheckDataCompleted));

    }

    public IEnumerator checkData(string uid, Action<bool> callback)
    {
        var test = databaseReference.Child("user_master").Child(uid).Child("meta_id").GetValueAsync();
        yield return new WaitUntil(() => test.IsCompleted);

        if (test != null)
        {
            DataSnapshot snapshot = test.Result;
            bool isUserPresent = false;

            if (string.Compare(LocalUserDataManager.Instance.userID, snapshot.Value.ToString()) == 0)
            {
                Debug.Log("User is present.");
                isUserPresent = true;
                LiveUserDataManager.Instance.currentUserUID = uid;
            }

            // Call the callback function and pass the result
            callback(isUserPresent);
        }
    }

   

    private void OnCheckDataCompleted(bool isUserPresent)
    {
        if (isUserPresent)
        {
            // The user is present, perform your specific method here
            dontUpdateData();

        }
        else
        {
            UpdateNewUserData();

            // The user is not present, handle it as needed
        }
    }

    private void dontUpdateData()
    {
       
            Debug.Log("present");
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



    public void SaveMetaInfo(string metaID, string metaName)
    {
        if (databaseReference != null)
        {
            // Create a new dictionary to store meta_id and meta_name
            Dictionary<string, object> metaData = new Dictionary<string, object>
            {
                { "meta_id", metaID },
                { "meta_name", metaName },
                { "games_played", "0" },
                { "totalPlayerScore", "0" },
                { "accuracy", "0" },
                  
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

}


