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
        //firestoreDB = FirebaseFirestore.DefaultInstance;
    }
}

