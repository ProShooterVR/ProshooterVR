using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Platform;
using Oculus.Platform.Models;

public static class releaseDatabase
{
    public static readonly string alpha = "PSVR_Alpha";
    public static readonly string prod = "PSVR_Prod";
    public static readonly string dev = "Development";

}
public class OculusProdState : MonoBehaviour
{
    public static OculusProdState Instance;


    

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        DontDestroyOnLoad(Instance);

    }

    private void Start()
    {
        LiveUserDataManager.Instance.dbName = releaseDatabase.prod;
    }

}
