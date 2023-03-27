using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LocalUserDataManager : MonoBehaviour
{

    public static LocalUserDataManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        DontDestroyOnLoad(Instance);

    }



    /// <summary>
    /// Local Meta User data manager to save data locally and update later in Live database
    /// </summary>
    public string userID, userName;

    public float totalTime; // Total time spent by user
    private DateTime startTime;

    public string selectedGameMode, SelectedGameLevel;
    public int totalScore,totalInnerTens,personalAmaBest,personalSemiProBest, personalProBest;
    public List<Dictionary<string, object>> Leaders;

    public enum gamerLevel
    {
        Amateur,
        SemiPro,
        Pro
    }
    public enum gameMode
    {
        AirPistol10M,
        rapidFire25m,
        airRifle10m
    }

    public gamerLevel levelSelected;
    public gameMode modeSelected;

    public bool temp = false;
    // Start is called before the first frame update
    void Start()
    {
        startTime = DateTime.Now;
    }

    // Update is called once per frame
    void Update()
    {

        TimeSpan elapsedTime = DateTime.Now - startTime;
        totalTime = (float)elapsedTime.TotalSeconds;

    }

    
}
public static class GameModes
{
    public static readonly string AirPistol10m = "AirPistol10M";
    public static readonly string AirRifle10m = "AirRifle10m";
}
public static class GameLevel
{
    public static readonly string Amateur = "Amateur";
    public static readonly string SemiPro = "SemiPro";
    public static readonly string Pro = "Pro";

}