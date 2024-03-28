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
    public int totalScorePistol,totalInnerTens;
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
    public string OverallGamesPlayed_10AP_Txt, OverallGamesPlayed_10AR_Txt, OverallPoints_10AP_Txt, OverallPoints_10AR_Txt;

    //user settings data
    public bool isGrabRotaionSaved;
    public bool isUXSaved;
    public Quaternion grabRotationAirPistol;
    public bool isRightHand;

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
        // metaID = "6653729967977144";
       //  meta_username = "AyodhyanandanB";

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
    public static readonly string RapidFire = "RapidFire";

}
public static class GameLevel
{
    public static readonly string amateur = "amateur";
    public static readonly string semi_pro = "semi_pro";
    public static readonly string pro = "pro";

}