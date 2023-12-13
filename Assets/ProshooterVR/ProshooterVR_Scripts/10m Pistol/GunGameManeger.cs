using BNG;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Nova;
using NovaSamples.Inventory;
using ProshooterVR;
using System.Collections;

public class GunGameManeger : MonoBehaviour
{
    

    public static GunGameManeger Instance;

    public bool isPracticeMode, isRankedMode;

    public GameObject pistolObj;

    public int noOfShotsFired,noShotMissed,noShotsHit;
    public int shotsFired;

    public float timeRemaining = 900;
    public float Totaltime= 900;


    public GameObject[] scorePanels;

    public int series1Score, series2Score, series3Score;
    public float sco1ten, sco2ten, sco3ten;
    public int gameTotalScore, Totalscore,matchTotalScore;
    public bool isScoreUpdated;

    //Rifle variable change 
    public float series1ScoreRifle, series2ScoreRifle, series3ScoreRifle;
    public float TotalScoreRifle,gameTotalScoreRifle;
    public float matchTotalScoreRifle;

    //

    public bool isReloaded,isReloading,isPallatPlaced;

    public Animator animator;
    public string clip1,clip2;

    public AudioSource audioSrc;
    public AudioClip[] pistol;

    public int avgScore,innerTno;
    public float timeSpent;

    public int shotsCount;
    public bool isGamePause;
    public bool isMatchDataUpdated;

    int seriesCount = 0;

    public TextMeshPro tempScore;

    public float totalGameTime; // Total time spent by user
    private DateTime startTime;


    public Material yellow,blue, green,black;
    public GameObject mat;




    public Vector3 palletPos;
    public GameObject palletObj,currentPallet;
    public GameObject palletSpawn;

    public GameObject touchReloader;
    public GameObject relodePt, pallatePt;

    public GameObject palletPrefab,tempPallet, palletHoldPos,palletParent;
    public bool spawnBullet;

    public bool isUXON;

    void Awake()
    {
        Instance = this;
    }



    // Start is called before the first frame update
    void Start()
    {
        spawnBullet = false;
        isGamePause = true;
        palletPos = palletObj.transform.position;
        GunGameManeger.Instance.tempPallet.SetActive(false);
        isUXON = LocalUserDataManager.Instance.isUXSaved;

        if (weaponManager.Instance.isPistolMode == true)
        {
            if (isPracticeMode == true)
            {
                pistolObj.GetComponent<RaycastWeapon>().ReloadMethod = ReloadType.InfiniteAmmo;
                PistolUIManager.Instance.timerValue.gameObject.SetActive(true);
                PistolUIManager.Instance.timerValue.text = "-- : --";
                shotsFired = 0;
            }

            if (isRankedMode == true)
            {
                //noOfShotsFired = 0 ;
                pistolObj.GetComponent<RaycastWeapon>().ReloadMethod = ReloadType.InfiniteAmmo;
                //pistolObj.GetComponent<RaycastWeapon>().InternalAmmo = 30;

                // scorePanels = new GameObject[3];
                noOfShotsFired = 0;
                shotsFired = 0;
                timeRemaining = 900f;
                PistolUIManager.Instance.timerValue.gameObject.SetActive(true);
                PistolUIManager.Instance.currentSeriesScoreTxt.gameObject.SetActive(true);
                Debug.Log("Rankded mode");


            }

            ////
            if (LocalUserDataManager.Instance.grabRotationAirPistol.x == 0 &&
          LocalUserDataManager.Instance.grabRotationAirPistol.y == 0 &&
          LocalUserDataManager.Instance.grabRotationAirPistol.z == 0 &&
          LocalUserDataManager.Instance.grabRotationAirPistol.w == 0)
            {
                Debug.Log("New settings");
            }
            else
            {
                loadSavedRotation(LocalUserDataManager.Instance.grabRotationAirPistol);
            }
            ////
        }
        if (weaponManager.Instance.isRifleMode == true)
        {
            if (isPracticeMode == true)
            {
                pistolObj.GetComponent<RaycastWeapon>().ReloadMethod = ReloadType.InfiniteAmmo;
                PistolUIManager.Instance.timerValue.gameObject.SetActive(true);
                PistolUIManager.Instance.timerValue.text = "-- : --";
                shotsFired = 0;
            }

            if (isRankedMode == true)
            {
              
                pistolObj.GetComponent<RaycastWeapon>().ReloadMethod = ReloadType.InfiniteAmmo;
            
                noOfShotsFired = 0;
                shotsFired = 0;
                timeRemaining = 900f;
                PistolUIManager.Instance.timerValue.gameObject.SetActive(true);
                PistolUIManager.Instance.currentSeriesScoreTxt.gameObject.SetActive(true);
                Debug.Log("Rankded mode");


            }
        }
        pistolObj.GetComponent<RaycastWeapon>().ReloadMethod = ReloadType.InfiniteAmmo;
        noOfShotsFired = 0;

        series1Score = series2Score = series3Score = 0;
        sco1ten = sco2ten = sco3ten = 0f;
        isScoreUpdated = true;
        isReloaded = false;
        isReloading = false;
        innerTno = 0;
        shotsCount = 0;
        clearScorePanel();
        PistolUIManager.Instance.endMatchPopUp.SetActive(false);
        isGamePause = true;
        isMatchDataUpdated = false;
        touchReloader.SetActive(false);

        relodePt.SetActive(false);
        pallatePt.SetActive(false);
        currentPallet = palletObj;

      //  PistolUIManager.Instance.startBtnClick();


    }

    void loadSavedRotation(Quaternion mySavedRotation )
    {
       PistolUIManager.Instance.rightHandController.transform.localRotation = mySavedRotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (isRankedMode == true)
        {
            if (isGamePause == false)
            {
                if (timeRemaining > 0 && noOfShotsFired < 30)
                {
                    timeRemaining -= Time.deltaTime;
                    totalGameTime += Time.deltaTime;

                    float minutes = Mathf.FloorToInt(timeRemaining / 60);
                    float seconds = Mathf.FloorToInt(timeRemaining % 60);

                    PistolUIManager.Instance.timerValue.text = string.Format("{0:00}:{1:00}", minutes, seconds);
                    
                }
                else
                {
                    if (isMatchDataUpdated == false)
                    {
                        Debug.Log("calling the end session");
                        UpdateMatchData();
                        isMatchDataUpdated = true;
                    }
                    Debug.Log("Time has run out!");
                }

            }
        }
        if (isPracticeMode == true)
        {
            if (isGamePause == false)
            { 
                if (noOfShotsFired < 30)
                {
                    totalGameTime += Time.deltaTime;
                }
                else
                {
                    if (isMatchDataUpdated == false)
                    {
                        Debug.Log("calling the end session");

                        // UpdateMatchData();
                        StartCoroutine(upadateEndGamePopUP());
                        isMatchDataUpdated = true;
                    }
                }
             }
        }



       

    }

    IEnumerator upadateEndGamePopUP() 
    {
        yield return new WaitForSeconds(2f);
        UpdateMatchData();

    }

    // update all the Game History Here
    void UpdateMatchData()
    {
        RayManager.Instance.EnableRey();
        Debug.Log("Updating the end session");

        PistolUIManager.Instance.endSessionPopup.SetActive(true);
        pistolPopUPUIManager.Instance.enableTargetScores();

        if (weaponManager.Instance.isPistolMode == true)
        { 
            //calculate avg series score
            int avgScore = (series1Score + series2Score + series3Score) / 3;
           // LiveUserDataManager.Instance.getUserBestScore();
            //Display data on UI
            PistolUIManager.Instance.endMatchPopUp.SetActive(true);

            pistolPopUPUIManager.Instance.userNameTxt.text = LocalUserDataManager.Instance.meta_username;
            pistolPopUPUIManager.Instance.srs1ScoreTxt.text = series1Score.ToString("F1");
            pistolPopUPUIManager.Instance.srs2ScoreTxt.text = series2Score.ToString("F1");
            pistolPopUPUIManager.Instance.srs3ScoreTxt.text = series3Score.ToString("F1");
            pistolPopUPUIManager.Instance.gameTotalScoreTxt.text = gameTotalScore.ToString("F1");
            pistolPopUPUIManager.Instance.avgScoreTxt.text = avgScore.ToString("F1");
            pistolPopUPUIManager.Instance.innerTText.text = innerTno.ToString("F1");

            pistolPopUPUIManager.Instance.shotsHitTxt.text = noShotsHit.ToString("F1");
            pistolPopUPUIManager.Instance.shotsmissTxt.text = noShotMissed.ToString("F1"); 
            pistolPopUPUIManager.Instance.timeSpentTxt.text = totalGameTime.ToString("F1");

            if (LocalUserDataManager.Instance.SelectedGameLevel == GameLevel.amateur)
            {
                if (int.Parse(LocalUserDataManager.Instance.pbest_10mAirP_AmaTxt) < gameTotalScore)
                {
                    GunDataManager.Instance.personalAmaBestRifle = gameTotalScoreRifle;
                    GunDataManager.Instance.personalGameBestRifle = gameTotalScoreRifle;
                    pistolPopUPUIManager.Instance.pBestScoreTxt.text = gameTotalScoreRifle.ToString();

                }
                else
                {
                    pistolPopUPUIManager.Instance.pBestScoreTxt.text = LocalUserDataManager.Instance.pbest_10mAirP_AmaTxt;
                }
            }
            else if (LocalUserDataManager.Instance.SelectedGameLevel == GameLevel.semi_pro)
            {
                if (int.Parse(LocalUserDataManager.Instance.pbest_10mAirP_SemPTxt) < gameTotalScore)
                {
                    GunDataManager.Instance.personalSemiProBestRifle = gameTotalScoreRifle;
                    GunDataManager.Instance.personalGameBestRifle = gameTotalScoreRifle;
                    pistolPopUPUIManager.Instance.pBestScoreTxt.text = gameTotalScoreRifle.ToString();

                }
                else
                {
                    pistolPopUPUIManager.Instance.pBestScoreTxt.text = LocalUserDataManager.Instance.pbest_10mAirP_SemPTxt.ToString();
                }
            }
            else if (LocalUserDataManager.Instance.SelectedGameLevel == GameLevel.pro)
            {
                if (int.Parse(LocalUserDataManager.Instance.pbest_10mAirP_ProTxt) < gameTotalScore)
                {
                    GunDataManager.Instance.personalProBestRifle = gameTotalScoreRifle;
                    GunDataManager.Instance.personalGameBestRifle = gameTotalScoreRifle;
                    pistolPopUPUIManager.Instance.pBestScoreTxt.text = gameTotalScoreRifle.ToString();


                }
                else
                {
                    pistolPopUPUIManager.Instance.pBestScoreTxt.text = LocalUserDataManager.Instance.pbest_10mAirP_ProTxt.ToString();
                }
            }


            // Add data to Data manager
            GunDataManager.Instance.sr1ScorePistol = series1Score;
            GunDataManager.Instance.sr2ScorePistol = series2Score;
            GunDataManager.Instance.sr3ScorePistol = series3Score;
            GunDataManager.Instance.totalGameScorePistol = gameTotalScore;


            GunDataManager.Instance.avgSrScorePistol = avgScore;
            GunDataManager.Instance.noOfInnerTens = innerTno;
            GunDataManager.Instance.noOfShotsMissed = noShotMissed;
            GunDataManager.Instance.noOfShotsOnTarget = noShotsHit;

            float minutes = Mathf.FloorToInt(GunGameManeger.Instance.totalGameTime / 60);
            float seconds = Mathf.FloorToInt(GunGameManeger.Instance.totalGameTime % 60);
            GunDataManager.Instance.totalTimeSpent = string.Format("{0:00}:{1:00}", minutes, seconds);
           
         
            if (isRankedMode == true)
            {
                LocalUserDataManager.Instance.totalScorePistol = LocalUserDataManager.Instance.totalScorePistol + gameTotalScore;

                //upload data to the backend
                // 1 : AirPistol ,2 : Air Rifle , 3: Rapid Fire
                DBAPIManagerNew.Instance.SaveGameDataPistol(1);


            }
        }

        if (weaponManager.Instance.isRifleMode == true)
        {
            //calculate avg series score
            float avgScore = (series1ScoreRifle + series2ScoreRifle + series3ScoreRifle) / 3;
           // LiveUserDataManager.Instance.getUserBestScore();
            //Display data on UI
            PistolUIManager.Instance.endMatchPopUp.SetActive(true);

            pistolPopUPUIManager.Instance.userNameTxt.text = LocalUserDataManager.Instance.meta_username;
            pistolPopUPUIManager.Instance.srs1ScoreTxt.text = series1ScoreRifle.ToString("F1");
            pistolPopUPUIManager.Instance.srs2ScoreTxt.text = series2ScoreRifle.ToString("F1");
            pistolPopUPUIManager.Instance.srs3ScoreTxt.text = series3ScoreRifle.ToString("F1");
            pistolPopUPUIManager.Instance.gameTotalScoreTxt.text = gameTotalScoreRifle.ToString("F1");
            pistolPopUPUIManager.Instance.avgScoreTxt.text = avgScore.ToString("F1");
            pistolPopUPUIManager.Instance.innerTText.text = innerTno.ToString("F1");

            pistolPopUPUIManager.Instance.shotsHitTxt.text = noShotsHit.ToString("F1");
            pistolPopUPUIManager.Instance.shotsmissTxt.text = noShotMissed.ToString("F1");
            pistolPopUPUIManager.Instance.timeSpentTxt.text = totalGameTime.ToString("F1");

            if (LocalUserDataManager.Instance.SelectedGameLevel == GameLevel.amateur)
            {
                if (float.Parse(LocalUserDataManager.Instance.pbest_10mAirR_AmaTxt) < gameTotalScoreRifle)
                {
                    GunDataManager.Instance.personalAmaBestRifle = gameTotalScoreRifle;
                    GunDataManager.Instance.personalGameBestRifle = gameTotalScoreRifle;
                    pistolPopUPUIManager.Instance.pBestScoreTxt.text = gameTotalScoreRifle.ToString();

                }
                else
                {
                    pistolPopUPUIManager.Instance.pBestScoreTxt.text = LocalUserDataManager.Instance.pbest_10mAirR_AmaTxt.ToString();
                }
            }
            else if (LocalUserDataManager.Instance.SelectedGameLevel == GameLevel.semi_pro)
            {
                if (float.Parse(LocalUserDataManager.Instance.pbest_10mAirR_SemPTxt) < gameTotalScoreRifle)
                {
                    GunDataManager.Instance.personalSemiProBestRifle = gameTotalScoreRifle;
                    GunDataManager.Instance.personalGameBestRifle = gameTotalScoreRifle;
                    pistolPopUPUIManager.Instance.pBestScoreTxt.text = gameTotalScoreRifle.ToString();

                }
                else
                {
                    pistolPopUPUIManager.Instance.pBestScoreTxt.text = LocalUserDataManager.Instance.pbest_10mAirR_SemPTxt.ToString();
                }
            }
            else if (LocalUserDataManager.Instance.SelectedGameLevel == GameLevel.pro)
            {
                if (float.Parse(LocalUserDataManager.Instance.pbest_10mAirR_ProTxt) < gameTotalScoreRifle)
                {
                    GunDataManager.Instance.personalProBestRifle = gameTotalScoreRifle;
                    GunDataManager.Instance.personalGameBestRifle = gameTotalScoreRifle;
                    pistolPopUPUIManager.Instance.pBestScoreTxt.text = gameTotalScoreRifle.ToString();


                }
                else
                {
                    pistolPopUPUIManager.Instance.pBestScoreTxt.text = LocalUserDataManager.Instance.pbest_10mAirR_ProTxt.ToString();
                }
            }


            // Add data to Data manager
            GunDataManager.Instance.sr1ScoreRifle = series1ScoreRifle;
            GunDataManager.Instance.sr2ScoreRifle = series2ScoreRifle;
            GunDataManager.Instance.sr3ScoreRifle = series3ScoreRifle;
            GunDataManager.Instance.totalGameScoreRifle = gameTotalScoreRifle;


            GunDataManager.Instance.avgSrScoreRifle = avgScore;
            GunDataManager.Instance.noOfInnerTens = innerTno;
            GunDataManager.Instance.noOfShotsMissed = noShotMissed;
            GunDataManager.Instance.noOfShotsOnTarget = noShotsHit;

            float minutes = Mathf.FloorToInt(GunGameManeger.Instance.totalGameTime / 60);
            float seconds = Mathf.FloorToInt(GunGameManeger.Instance.totalGameTime % 60);
            GunDataManager.Instance.totalTimeSpent = string.Format("{0:00}:{1:00}", minutes, seconds);

          

           
            if (isRankedMode == true)
            {
                LocalUserDataManager.Instance.totalScoreRifle = LocalUserDataManager.Instance.totalScoreRifle + gameTotalScoreRifle;
                //upload data to the backend
                // 1 : AirPistol ,2 : Air Rifle , 3: Rapid Fire
                DBAPIManagerNew.Instance.SaveGameDataRifle(2);
            }
        }

    }


    public void clearScorePanel()
    {
        for(int i = 0; i < 10; i++)
        {
            PistolUIManager.Instance.scorePanelData.gameObject.transform.GetChild(i).GetChild(1).gameObject.GetComponent<TextMeshPro>().text = "";
            PistolUIManager.Instance.scorePanelData.gameObject.transform.GetChild(i).GetChild(2).gameObject.SetActive(false);
            PistolUIManager.Instance.scorePanelData.gameObject.transform.GetChild(i).GetChild(2).gameObject.transform.Rotate(0, 0, 0);
        }

        PistolUIManager.Instance.clearShotScreen();
        Debug.Log("called Screen clear");
    }
    public void shotFired(Vector3 pos, float scoreVal, float direction)
    {
        UXManagerAirPistol.Instance.UXEvents(6);
        

        if (weaponManager.Instance.isPistolMode == true)
        {
            if (isRankedMode == true)
            {
                if (seriesCount < GunDataManager.Instance.noOfSeries)
                {
                    if (shotsFired < 10)
                    {
                        if (shotsFired == 0)
                        {
                            clearScorePanel();
                        }

                        PistolUIManager.Instance.updateShotScreen(pos, scoreVal, direction);
                        pistolPopUPUIManager.Instance.updateShotEndScreen(pos, scoreVal, direction);


                        PistolUIManager.Instance.scorePanelData.gameObject.transform.GetChild(shotsFired).GetChild(1).gameObject.GetComponent<TextMeshPro>().text = PistolUIManager.Instance.finalScore;

                        PistolUIManager.Instance.currentShotScore.text = PistolUIManager.Instance.finalScore;
                        PistolUIManager.Instance.scorePanelData.gameObject.transform.GetChild(shotsFired).GetChild(2).gameObject.SetActive(true);
                        PistolUIManager.Instance.scorePanelData.gameObject.transform.GetChild(shotsFired).GetChild(2).gameObject.transform.Rotate(1, 1, -PistolUIManager.Instance.angle);
                        Debug.Log("Rotation Z : "+PistolUIManager.Instance.scorePanelData.gameObject.transform.GetChild(shotsFired).GetChild(2).gameObject.transform.rotation.z);
                        GunDataManager.Instance.ScoresPistol[noOfShotsFired] = PistolUIManager.Instance.shotRoundScore;

                        Totalscore = Totalscore + PistolUIManager.Instance.shotRoundScore;
                        matchTotalScore = matchTotalScore + PistolUIManager.Instance.shotRoundScore;

                        PistolUIManager.Instance.totalGameScoreTxt.text = matchTotalScore.ToString();

                        noOfShotsFired++;
                        shotsFired++;
                        if (seriesCount == 0)
                        {
                            PistolUIManager.Instance.seriesNoTitle.text = "SERIES " + (seriesCount + 1).ToString();
                            series1Score = series1Score + PistolUIManager.Instance.shotRoundScore;
                            PistolUIManager.Instance.series1Text.text = series1Score.ToString("F1");
                            PistolUIManager.Instance.currentSeriesScoreTxt.text = "SERIES SCORE : " + series1Score.ToString();

                        }
                        else if (seriesCount == 1)
                        {
                            PistolUIManager.Instance.seriesNoTitle.text = "SERIES " + (seriesCount + 1).ToString();
                            series2Score = series2Score + PistolUIManager.Instance.shotRoundScore;
                            PistolUIManager.Instance.series2text.text = series2Score.ToString("F1");
                            PistolUIManager.Instance.currentSeriesScoreTxt.text = "SERIES SCORE : " + series2Score.ToString();

                        }
                        else if (seriesCount == 2)
                        {
                            PistolUIManager.Instance.seriesNoTitle.text = "SERIES " + (seriesCount + 1).ToString();
                            series3Score = series3Score + PistolUIManager.Instance.shotRoundScore;
                            PistolUIManager.Instance.series3Text.text = series3Score.ToString("F1");
                            PistolUIManager.Instance.currentSeriesScoreTxt.text = "SERIES SCORE : " + series3Score.ToString();

                        }
                        else
                        {

                        }
                        if (shotsFired == 10)
                        {
                            gameTotalScore = matchTotalScore;

                            seriesCount++;
                            shotsFired = 0;
                            Totalscore = 0;

                        }

                    }
                    else
                    {

                    }

                }
            }
            if (isPracticeMode == true)
            {
                if (seriesCount < GunDataManager.Instance.noOfSeries)
                {
                    if (shotsFired < 10)
                    {
                        if (shotsFired == 0)
                        {
                            clearScorePanel();
                        }

                        PistolUIManager.Instance.updateShotScreen(pos, scoreVal, direction);
                        pistolPopUPUIManager.Instance.updateShotEndScreen(pos, scoreVal, direction);


                        PistolUIManager.Instance.scorePanelData.gameObject.transform.GetChild(shotsFired).GetChild(1).gameObject.GetComponent<TextMeshPro>().text = PistolUIManager.Instance.finalScore;

                        PistolUIManager.Instance.currentShotScore.text = PistolUIManager.Instance.finalScore;
                        PistolUIManager.Instance.scorePanelData.gameObject.transform.GetChild(shotsFired).GetChild(2).gameObject.SetActive(true);

                        PistolUIManager.Instance.scorePanelData.gameObject.transform.GetChild(shotsFired).GetChild(2).gameObject.transform.Rotate(0, 0, PistolUIManager.Instance.angle);

                        GunDataManager.Instance.ScoresPistol[noOfShotsFired] = PistolUIManager.Instance.shotRoundScore;

                        Totalscore = Totalscore + PistolUIManager.Instance.shotRoundScore;
                        matchTotalScore = matchTotalScore + PistolUIManager.Instance.shotRoundScore;

                        PistolUIManager.Instance.totalGameScoreTxt.text = matchTotalScore.ToString();

                        noOfShotsFired++;
                        shotsFired++;
                        if (seriesCount == 0)
                        {
                            PistolUIManager.Instance.seriesNoTitle.text = "SERIES " + (seriesCount + 1).ToString();
                            series1Score = series1Score + PistolUIManager.Instance.shotRoundScore;
                            PistolUIManager.Instance.series1Text.text = series1Score.ToString("F1");
                            PistolUIManager.Instance.currentSeriesScoreTxt.text = "SERIES SCORE : " + series1Score.ToString();

                        }
                        else if (seriesCount == 1)
                        {
                            PistolUIManager.Instance.seriesNoTitle.text = "SERIES " + (seriesCount + 1).ToString();
                            series2Score = series2Score + PistolUIManager.Instance.shotRoundScore;
                            PistolUIManager.Instance.series2text.text = series2Score.ToString("F1");
                            PistolUIManager.Instance.currentSeriesScoreTxt.text = "SERIES SCORE : " + series2Score.ToString();

                        }
                        else if (seriesCount == 2)
                        {
                            PistolUIManager.Instance.seriesNoTitle.text = "SERIES " + (seriesCount + 1).ToString();
                            series3Score = series3Score + PistolUIManager.Instance.shotRoundScore;
                            PistolUIManager.Instance.series3Text.text = series3Score.ToString("F1");
                            PistolUIManager.Instance.currentSeriesScoreTxt.text = "SERIES SCORE : " + series3Score.ToString();

                        }
                        else
                        {

                        }
                        if (shotsFired == 10)
                        {
                            gameTotalScore = matchTotalScore;

                            seriesCount++;
                            shotsFired = 0;
                            Totalscore = 0;

                        }

                    }
                    else
                    {

                    }

                }
            }
        }


        if (weaponManager.Instance.isRifleMode == true)
        {

            if (isRankedMode == true)
            {
                if (seriesCount < GunDataManager.Instance.noOfSeries)
                {
                    if (shotsFired < 10)
                    {
                        if (shotsFired == 0)
                        {
                            clearScorePanel();
                        }

                        PistolUIManager.Instance.updateShotScreen(pos, scoreVal, direction);
                        pistolPopUPUIManager.Instance.updateShotEndScreen(pos, scoreVal, direction);


                        PistolUIManager.Instance.scorePanelData.gameObject.transform.GetChild(shotsFired).GetChild(1).gameObject.GetComponent<TextMeshPro>().text = PistolUIManager.Instance.finalScore;

                        PistolUIManager.Instance.currentShotScore.text = PistolUIManager.Instance.finalScore;
                        PistolUIManager.Instance.scorePanelData.gameObject.transform.GetChild(shotsFired).GetChild(2).gameObject.SetActive(true);
                        Vector3 eulerRotation = new Vector3(0f, 0f, PistolUIManager.Instance.angle); // Example euler rotation values
                        Quaternion quaternionRotation = Quaternion.Euler(eulerRotation);
                        PistolUIManager.Instance.scorePanelData.gameObject.transform.GetChild(shotsFired).GetChild(2).gameObject.transform.localRotation = quaternionRotation;



                        GunDataManager.Instance.ScoresRifle[noOfShotsFired] = PistolUIManager.Instance.shotRoundScoreRifle;

                        TotalScoreRifle = TotalScoreRifle + PistolUIManager.Instance.shotRoundScoreRifle;
                        matchTotalScoreRifle = matchTotalScoreRifle + PistolUIManager.Instance.shotRoundScoreRifle;

                        TotalScoreRifle = Mathf.Round(TotalScoreRifle * 100f) / 100f;
                        matchTotalScoreRifle = Mathf.Round(matchTotalScoreRifle * 100f) / 100f;

                        PistolUIManager.Instance.totalGameScoreTxt.text = matchTotalScoreRifle.ToString("F1");

                        noOfShotsFired++;
                        shotsFired++;
                        if (seriesCount == 0)
                        {
                            PistolUIManager.Instance.seriesNoTitle.text = "SERIES " + (seriesCount + 1).ToString();
                            series1ScoreRifle = series1ScoreRifle + PistolUIManager.Instance.shotRoundScoreRifle;
                            PistolUIManager.Instance.series1Text.text = series1ScoreRifle.ToString("F1");
                            PistolUIManager.Instance.currentSeriesScoreTxt.text = "SERIES SCORE : " + series1ScoreRifle.ToString("F1");

                        }
                        else if (seriesCount == 1)
                        {
                            PistolUIManager.Instance.seriesNoTitle.text = "SERIES " + (seriesCount + 1).ToString();
                            series2ScoreRifle = series2ScoreRifle + PistolUIManager.Instance.shotRoundScoreRifle;
                            PistolUIManager.Instance.series2text.text = series2ScoreRifle.ToString("F1");
                            PistolUIManager.Instance.currentSeriesScoreTxt.text = "SERIES SCORE : " + series2ScoreRifle.ToString("F1");

                        }
                        else if (seriesCount == 2)
                        {
                            PistolUIManager.Instance.seriesNoTitle.text = "SERIES " + (seriesCount + 1).ToString();
                            series3ScoreRifle = series3ScoreRifle + PistolUIManager.Instance.shotRoundScoreRifle;
                            PistolUIManager.Instance.series3Text.text = series3ScoreRifle.ToString("F1");
                            PistolUIManager.Instance.currentSeriesScoreTxt.text = "SERIES SCORE : " + series3ScoreRifle.ToString("F1");

                        }
                        else
                        {

                        }
                        if (shotsFired == 10)
                        {
                            gameTotalScoreRifle = matchTotalScoreRifle;

                            seriesCount++;
                            shotsFired = 0;
                            Totalscore = 0;

                        }

                    }
                    else
                    {

                    }

                }

            }
            if (isPracticeMode == true)
            {
                if (seriesCount < GunDataManager.Instance.noOfSeries)
                {
                    if (shotsFired < 10)
                    {
                        if (shotsFired == 0)
                        {
                            clearScorePanel();
                        }

                        PistolUIManager.Instance.updateShotScreen(pos, scoreVal, direction);
                        pistolPopUPUIManager.Instance.updateShotEndScreen(pos, scoreVal, direction);


                        PistolUIManager.Instance.scorePanelData.gameObject.transform.GetChild(shotsFired).GetChild(1).gameObject.GetComponent<TextMeshPro>().text = PistolUIManager.Instance.finalScore;

                        PistolUIManager.Instance.currentShotScore.text = PistolUIManager.Instance.finalScore;
                        PistolUIManager.Instance.scorePanelData.gameObject.transform.GetChild(shotsFired).GetChild(2).gameObject.SetActive(true);
                        Vector3 eulerRotation = new Vector3(0f, 0f, PistolUIManager.Instance.angle); // Example euler rotation values
                        Quaternion quaternionRotation = Quaternion.Euler(eulerRotation);
                        PistolUIManager.Instance.scorePanelData.gameObject.transform.GetChild(shotsFired).GetChild(2).gameObject.transform.localRotation = quaternionRotation;

                        GunDataManager.Instance.ScoresRifle[noOfShotsFired] = PistolUIManager.Instance.shotRoundScoreRifle;

                        TotalScoreRifle = TotalScoreRifle + PistolUIManager.Instance.shotRoundScoreRifle;
                        matchTotalScoreRifle = matchTotalScoreRifle + PistolUIManager.Instance.shotRoundScore;

                        TotalScoreRifle = Mathf.Round(TotalScoreRifle * 100f) / 100f;
                        matchTotalScoreRifle = Mathf.Round(matchTotalScoreRifle * 100f) / 100f;

                        PistolUIManager.Instance.totalGameScoreTxt.text = matchTotalScoreRifle.ToString("F1");

                        noOfShotsFired++;
                        shotsFired++;
                        if (seriesCount == 0)
                        {
                            PistolUIManager.Instance.seriesNoTitle.text = "SERIES " + (seriesCount + 1).ToString();
                            series1ScoreRifle = series1ScoreRifle + PistolUIManager.Instance.shotRoundScoreRifle;
                            PistolUIManager.Instance.series1Text.text = series1ScoreRifle.ToString();
                            PistolUIManager.Instance.currentSeriesScoreTxt.text = "SERIES SCORE : " + series1ScoreRifle.ToString("F1");

                        }
                        else if (seriesCount == 1)
                        {
                            PistolUIManager.Instance.seriesNoTitle.text = "SERIES " + (seriesCount + 1).ToString();
                            series2ScoreRifle = series2ScoreRifle + PistolUIManager.Instance.shotRoundScoreRifle;
                            PistolUIManager.Instance.series2text.text = series2ScoreRifle.ToString("F1");
                            PistolUIManager.Instance.currentSeriesScoreTxt.text = "SERIES SCORE : " + series2ScoreRifle.ToString("F1");

                        }
                        else if (seriesCount == 2)
                        {
                            PistolUIManager.Instance.seriesNoTitle.text = "SERIES " + (seriesCount + 1).ToString();
                            series3ScoreRifle = series3ScoreRifle + PistolUIManager.Instance.shotRoundScoreRifle;
                            PistolUIManager.Instance.series3Text.text = series3ScoreRifle.ToString("F1");
                            PistolUIManager.Instance.currentSeriesScoreTxt.text = "SERIES SCORE : " + series3ScoreRifle.ToString("F1");

                        }
                        else
                        {

                        }
                        if (shotsFired == 10)
                        {
                            //gameTotalScoreRifle = gameTotalScoreRifle + TotalScoreRifle ;
                            gameTotalScoreRifle = matchTotalScoreRifle;


                            seriesCount++;
                            shotsFired = 0;
                            Totalscore = 0;

                        }

                    }
                    else
                    {

                    }

                }

            }
        }

    }
}
