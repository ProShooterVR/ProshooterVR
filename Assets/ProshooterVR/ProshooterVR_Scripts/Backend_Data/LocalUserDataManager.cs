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
    public string metaID, meta_username,metauser_profileImage_url;

    public bool isAggrDone;

    public float totalTime; // Total time spent by user
    private DateTime startTime;

    public string selectedGameMode, SelectedGameLevel;
    public int totalScorePistol,totalInnerTens,personalAmaBestPistol,personalSemiProBestPistol, personalProBestPistol;
    public List<Dictionary<string, object>> Leaders;

    //Rifle Data

    public float totalScoreRifle, personalAmaBestRifle, personalSemiProBestRifle, personalProBestRifle;
    public int noOfGamesAirPistol, noOfGamesAirRifle, noOfGamesRapidFire;


    //user profile data
    public string userNameTxt;
    public string totalScoreTxt, matchesPlayedTxt, accuracyTxt;
    public string pbest_10mAirP_AmaTxt, pbest_10mAirP_SemPTxt, pbest_10mAirP_ProTxt;
    public string pbest_10mAirR_AmaTxt, pbest_10mAirR_SemPTxt, pbest_10mAirR_ProTxt;
    public string pbest_25mRF_AmaTxt, pbest_25mRF_SemPTxt, pbest_25mRF_ProTxt;


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
        isAggrDone = false;
        //metaID = "6889892497704835";
        //meta_username = "Trickerion";
    }

    // Update is called once per frame
    void Update()
    {

        totalTime += Time.deltaTime;
        
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