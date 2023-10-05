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
using Firebase.Database;

public class LiveUserDataManagerRealtime : MonoBehaviour
{
    public static LiveUserDataManagerRealtime Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        DontDestroyOnLoad(Instance);
    }

    public DatabaseReference universal_databaseReference;

}