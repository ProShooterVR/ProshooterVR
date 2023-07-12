using BNG;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GunGameManeger : MonoBehaviour
{

    public static GunGameManeger Instance;

    public bool isRifleMode, isPistolMode,isRapidFireMode;

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

    public TextMeshProUGUI tempScore;

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


        if (isPistolMode == true)
        {
            if (isPracticeMode == true)
            {
                pistolObj.GetComponent<RaycastWeapon>().ReloadMethod = ReloadType.InfiniteAmmo;
                PistolUIManager.Instance.timerValue.gameObject.SetActive(true);
                PistolUIManager.Instance.timerValue.text = "-- : --";
                GunDataManager.Instance.gameMode = "10m Air Pistol PracticeMode - " + LocalUserDataManager.Instance.SelectedGameLevel;
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
                PistolUIManager.Instance.totalScoreTxt.gameObject.SetActive(true);
                Debug.Log("Rankded mode");
                GunDataManager.Instance.gameMode = "10m Air Pistol Match Mode - " + LocalUserDataManager.Instance.SelectedGameLevel;


            }
        }
        if (isRifleMode == true)
        {
            if (isPracticeMode == true)
            {
                pistolObj.GetComponent<RaycastWeapon>().ReloadMethod = ReloadType.InfiniteAmmo;
                PistolUIManager.Instance.timerValue.gameObject.SetActive(true);
                PistolUIManager.Instance.timerValue.text = "-- : --";
                GunDataManager.Instance.gameMode = "10m Air Rifle PracticeMode - " + LocalUserDataManager.Instance.SelectedGameLevel;
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
                PistolUIManager.Instance.totalScoreTxt.gameObject.SetActive(true);
                Debug.Log("Rankded mode");
                GunDataManager.Instance.gameMode = "10m Air Rifle Match Mode - " + LocalUserDataManager.Instance.SelectedGameLevel;


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
                        UpdateMatchData();
                        isMatchDataUpdated = true;
                    }
                }
             }
        }



       

    }


    // update all the Game History Here
    void UpdateMatchData()
    {
        //PistolUIManager.Instance.leaderPopUp.SetActive(true);

        
       

        if (isPistolMode == true)
        { 
            //calculate avg series score
            int avgScore = (series1Score + series2Score + series3Score) / 3;
            LiveUserDataManager.Instance.getUserBestScore();
            //Display data on UI
            PistolUIManager.Instance.endMatchPopUp.SetActive(true);

            pistolPopUPUIManager.Instance.userNameTxt.text = LocalUserDataManager.Instance.userName;
            pistolPopUPUIManager.Instance.srs1ScoreTxt.text = series1Score.ToString();
            pistolPopUPUIManager.Instance.srs2ScoreTxt.text = series2Score.ToString();
            pistolPopUPUIManager.Instance.srs3ScoreTxt.text = series3Score.ToString();
            pistolPopUPUIManager.Instance.gameTotalScoreTxt.text = gameTotalScore.ToString();
            pistolPopUPUIManager.Instance.avgScoreTxt.text = avgScore.ToString();
            pistolPopUPUIManager.Instance.innerTText.text = innerTno.ToString();

            pistolPopUPUIManager.Instance.shotsHitMisTxt.text = "" + noShotsHit + "/" + noShotMissed;
            pistolPopUPUIManager.Instance.timeSpentTxt.text = totalGameTime.ToString();

            if (LocalUserDataManager.Instance.SelectedGameLevel == GameLevel.Amateur)
            {
                if (GunDataManager.Instance.personalAmaBestPistol < gameTotalScore)
                {
                    GunDataManager.Instance.personalAmaBestPistol = gameTotalScore;
                    GunDataManager.Instance.personalGameBestPistol = gameTotalScore;
                }
            }
            else if (LocalUserDataManager.Instance.SelectedGameLevel == GameLevel.SemiPro)
            {
                if (GunDataManager.Instance.personalSemiProBestPistol < gameTotalScore)
                {
                    GunDataManager.Instance.personalSemiProBestPistol = gameTotalScore;
                    GunDataManager.Instance.personalGameBestPistol = gameTotalScore;

                }
            }
            else if (LocalUserDataManager.Instance.SelectedGameLevel == GameLevel.Pro)
            {
                if (GunDataManager.Instance.personalProBestPistol < gameTotalScore)
                {
                    GunDataManager.Instance.personalProBestPistol = gameTotalScore;
                    GunDataManager.Instance.personalGameBestPistol = gameTotalScore;

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
            //upload data to the backend
            LiveUserDataManager.Instance.SavePistolGameDataToLiveDB();

            if (isRankedMode == true)
            {
                LocalUserDataManager.Instance.totalScorePistol = LocalUserDataManager.Instance.totalScorePistol + gameTotalScore;
                // LiveUserDataManager.Instance.saveLeaderBoardData();
            }
        }

        if (isRifleMode == true)
        {
            //calculate avg series score
            float avgScore = (series1ScoreRifle + series2ScoreRifle + series3ScoreRifle) / 3;
            LiveUserDataManager.Instance.getUserBestScore();
            //Display data on UI
            PistolUIManager.Instance.endMatchPopUp.SetActive(true);

            pistolPopUPUIManager.Instance.userNameTxt.text = LocalUserDataManager.Instance.userName;
            pistolPopUPUIManager.Instance.srs1ScoreTxt.text = series1ScoreRifle.ToString();
            pistolPopUPUIManager.Instance.srs2ScoreTxt.text = series2ScoreRifle.ToString();
            pistolPopUPUIManager.Instance.srs3ScoreTxt.text = series3ScoreRifle.ToString();
            pistolPopUPUIManager.Instance.gameTotalScoreTxt.text = gameTotalScoreRifle.ToString();
            pistolPopUPUIManager.Instance.avgScoreTxt.text = avgScore.ToString();
            pistolPopUPUIManager.Instance.innerTText.text = innerTno.ToString();

            pistolPopUPUIManager.Instance.shotsHitMisTxt.text = "" + noShotsHit + "/" + noShotMissed;
            pistolPopUPUIManager.Instance.timeSpentTxt.text = totalGameTime.ToString();

            if (LocalUserDataManager.Instance.SelectedGameLevel == GameLevel.Amateur)
            {
                if (GunDataManager.Instance.personalAmaBestPistol < gameTotalScore)
                {
                    GunDataManager.Instance.personalAmaBestRifle = gameTotalScoreRifle;
                    GunDataManager.Instance.personalGameBestRifle = gameTotalScoreRifle;
                }
            }
            else if (LocalUserDataManager.Instance.SelectedGameLevel == GameLevel.SemiPro)
            {
                if (GunDataManager.Instance.personalSemiProBestPistol < gameTotalScore)
                {
                    GunDataManager.Instance.personalSemiProBestRifle = gameTotalScoreRifle;
                    GunDataManager.Instance.personalGameBestRifle = gameTotalScoreRifle;

                }
            }
            else if (LocalUserDataManager.Instance.SelectedGameLevel == GameLevel.Pro)
            {
                if (GunDataManager.Instance.personalProBestPistol < gameTotalScore)
                {
                    GunDataManager.Instance.personalProBestRifle = gameTotalScoreRifle;
                    GunDataManager.Instance.personalGameBestRifle = gameTotalScoreRifle;

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
            //upload data to the backend
            LiveUserDataManager.Instance.SaveRifleGameDataToLiveDB();

            if (isRankedMode == true)
            {
                LocalUserDataManager.Instance.totalScoreRifle = LocalUserDataManager.Instance.totalScoreRifle + gameTotalScoreRifle;
                // LiveUserDataManager.Instance.saveLeaderBoardData();
            }
        }

    }


    void clearScorePanel()
    {
        for(int i = 0; i < 10; i++)
        {
            PistolUIManager.Instance.scorePanelData.gameObject.transform.GetChild(i).GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = "";
            PistolUIManager.Instance.scorePanelData.gameObject.transform.GetChild(i).GetChild(2).gameObject.GetComponent<Image>().enabled = false;
            PistolUIManager.Instance.scorePanelData.gameObject.transform.GetChild(i).GetChild(2).gameObject.transform.rotation = Quaternion.identity;
        }
        PistolUIManager.Instance.clearShotScreen();
    }
    public void shotFired(Vector3 pos, float scoreVal, float direction)
    {

        if (isPistolMode == true)
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

                        PistolUIManager.Instance.scorePanelData.gameObject.transform.GetChild(shotsFired).GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = PistolUIManager.Instance.finalScore;

                        PistolUIManager.Instance.currentShotScore.text = PistolUIManager.Instance.finalScore;
                        PistolUIManager.Instance.scorePanelData.gameObject.transform.GetChild(shotsFired).GetChild(2).gameObject.GetComponent<Image>().enabled = true;
                        PistolUIManager.Instance.scorePanelData.gameObject.transform.GetChild(shotsFired).GetChild(2).gameObject.transform.Rotate(0, 0, PistolUIManager.Instance.angle);

                        GunDataManager.Instance.ScoresPistol[noOfShotsFired] = PistolUIManager.Instance.shotRoundScore;

                        Totalscore = Totalscore + PistolUIManager.Instance.shotRoundScore;
                        matchTotalScore = matchTotalScore + PistolUIManager.Instance.shotRoundScore;

                        PistolUIManager.Instance.totalScoreTxt.text = "SERIES SCORE : " + Totalscore.ToString();
                        PistolUIManager.Instance.totalGameScoreTxt.text = "TOTAL SCORE : " + matchTotalScore.ToString();

                        noOfShotsFired++;
                        shotsFired++;
                        if (seriesCount == 0)
                        {
                            PistolUIManager.Instance.seriesNoTitle.text = "SERIES " + (seriesCount + 1).ToString();
                            series1Score = series1Score + PistolUIManager.Instance.shotRoundScore;
                            PistolUIManager.Instance.series1Text.text = series1Score.ToString();
                        }
                        else if (seriesCount == 1)
                        {
                            PistolUIManager.Instance.seriesNoTitle.text = "SERIES " + (seriesCount + 1).ToString();
                            series2Score = series2Score + PistolUIManager.Instance.shotRoundScore;
                            PistolUIManager.Instance.series2text.text = series2Score.ToString();
                        }
                        else if (seriesCount == 2)
                        {
                            PistolUIManager.Instance.seriesNoTitle.text = "SERIES " + (seriesCount + 1).ToString();
                            series3Score = series3Score + PistolUIManager.Instance.shotRoundScore;
                            PistolUIManager.Instance.series3Text.text = series3Score.ToString();
                        }
                        else
                        {

                        }
                        if (shotsFired == 10)
                        {
                            gameTotalScore = gameTotalScore + Totalscore + innerTno;

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

                        PistolUIManager.Instance.scorePanelData.gameObject.transform.GetChild(shotsFired).GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = PistolUIManager.Instance.finalScore;

                        PistolUIManager.Instance.currentShotScore.text = PistolUIManager.Instance.finalScore;
                        PistolUIManager.Instance.scorePanelData.gameObject.transform.GetChild(shotsFired).GetChild(2).gameObject.GetComponent<Image>().enabled = true;
                        PistolUIManager.Instance.scorePanelData.gameObject.transform.GetChild(shotsFired).GetChild(2).gameObject.transform.Rotate(0, 0, PistolUIManager.Instance.angle);

                        GunDataManager.Instance.ScoresPistol[noOfShotsFired] = PistolUIManager.Instance.shotRoundScore;

                        Totalscore = Totalscore + PistolUIManager.Instance.shotRoundScore;
                        matchTotalScore = matchTotalScore + PistolUIManager.Instance.shotRoundScore;

                        PistolUIManager.Instance.totalScoreTxt.text = "SERIES SCORE : " + Totalscore.ToString();
                        PistolUIManager.Instance.totalGameScoreTxt.text = "TOTAL SCORE : " + matchTotalScore.ToString();

                        noOfShotsFired++;
                        shotsFired++;
                        if (seriesCount == 0)
                        {
                            PistolUIManager.Instance.seriesNoTitle.text = "SERIES " + (seriesCount + 1).ToString();
                            series1Score = series1Score + PistolUIManager.Instance.shotRoundScore;
                            PistolUIManager.Instance.series1Text.text = series1Score.ToString();
                        }
                        else if (seriesCount == 1)
                        {
                            PistolUIManager.Instance.seriesNoTitle.text = "SERIES " + (seriesCount + 1).ToString();
                            series2Score = series2Score + PistolUIManager.Instance.shotRoundScore;
                            PistolUIManager.Instance.series2text.text = series2Score.ToString();
                        }
                        else if (seriesCount == 2)
                        {
                            PistolUIManager.Instance.seriesNoTitle.text = "SERIES " + (seriesCount + 1).ToString();
                            series3Score = series3Score + PistolUIManager.Instance.shotRoundScore;
                            PistolUIManager.Instance.series3Text.text = series3Score.ToString();
                        }
                        else
                        {

                        }
                        if (shotsFired == 10)
                        {
                            gameTotalScore = gameTotalScore + Totalscore + innerTno;

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


        if (isRifleMode == true)
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

                        PistolUIManager.Instance.scorePanelData.gameObject.transform.GetChild(shotsFired).GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = PistolUIManager.Instance.finalScore;

                        PistolUIManager.Instance.currentShotScore.text = PistolUIManager.Instance.finalScore;
                        PistolUIManager.Instance.scorePanelData.gameObject.transform.GetChild(shotsFired).GetChild(2).gameObject.GetComponent<Image>().enabled = true;
                        Vector3 eulerRotation = new Vector3(0f, 0f, PistolUIManager.Instance.angle); // Example euler rotation values
                        Quaternion quaternionRotation = Quaternion.Euler(eulerRotation);
                        PistolUIManager.Instance.scorePanelData.gameObject.transform.GetChild(shotsFired).GetChild(2).gameObject.transform.localRotation = quaternionRotation;



                        GunDataManager.Instance.ScoresRifle[noOfShotsFired] = PistolUIManager.Instance.shotRoundScoreRifle;

                        TotalScoreRifle = TotalScoreRifle + PistolUIManager.Instance.shotRoundScoreRifle;
                        matchTotalScoreRifle = matchTotalScoreRifle + PistolUIManager.Instance.shotRoundScoreRifle;

                        TotalScoreRifle = Mathf.Round(TotalScoreRifle * 100f) / 100f;
                        matchTotalScoreRifle = Mathf.Round(matchTotalScoreRifle * 100f) / 100f;

                        PistolUIManager.Instance.totalScoreTxt.text = "SERIES SCORE : " + TotalScoreRifle.ToString("F1");
                        PistolUIManager.Instance.totalGameScoreTxt.text = "TOTAL SCORE : " + matchTotalScoreRifle.ToString("F1");

                        noOfShotsFired++;
                        shotsFired++;
                        if (seriesCount == 0)
                        {
                            PistolUIManager.Instance.seriesNoTitle.text = "SERIES " + (seriesCount + 1).ToString();
                            series1ScoreRifle = series1ScoreRifle + PistolUIManager.Instance.shotRoundScoreRifle;
                            PistolUIManager.Instance.series1Text.text = series1ScoreRifle.ToString();
                        }
                        else if (seriesCount == 1)
                        {
                            PistolUIManager.Instance.seriesNoTitle.text = "SERIES " + (seriesCount + 1).ToString();
                            series2ScoreRifle = series2ScoreRifle + PistolUIManager.Instance.shotRoundScoreRifle;
                            PistolUIManager.Instance.series2text.text = series2ScoreRifle.ToString();
                        }
                        else if (seriesCount == 2)
                        {
                            PistolUIManager.Instance.seriesNoTitle.text = "SERIES " + (seriesCount + 1).ToString();
                            series3ScoreRifle = series3ScoreRifle + PistolUIManager.Instance.shotRoundScoreRifle;
                            PistolUIManager.Instance.series3Text.text = series3ScoreRifle.ToString();
                        }
                        else
                        {

                        }
                        if (shotsFired == 10)
                        {
                            gameTotalScoreRifle = gameTotalScoreRifle + TotalScoreRifle + innerTno;

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

                        PistolUIManager.Instance.scorePanelData.gameObject.transform.GetChild(shotsFired).GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = PistolUIManager.Instance.finalScore;

                        PistolUIManager.Instance.currentShotScore.text = PistolUIManager.Instance.finalScore;
                        PistolUIManager.Instance.scorePanelData.gameObject.transform.GetChild(shotsFired).GetChild(2).gameObject.GetComponent<Image>().enabled = true;
                        Vector3 eulerRotation = new Vector3(0f, 0f, PistolUIManager.Instance.angle); // Example euler rotation values
                        Quaternion quaternionRotation = Quaternion.Euler(eulerRotation);
                        PistolUIManager.Instance.scorePanelData.gameObject.transform.GetChild(shotsFired).GetChild(2).gameObject.transform.localRotation = quaternionRotation;

                        GunDataManager.Instance.ScoresRifle[noOfShotsFired] = PistolUIManager.Instance.shotRoundScoreRifle;

                        TotalScoreRifle = TotalScoreRifle + PistolUIManager.Instance.shotRoundScoreRifle;
                        matchTotalScoreRifle = matchTotalScoreRifle + PistolUIManager.Instance.shotRoundScore;

                        TotalScoreRifle = Mathf.Round(TotalScoreRifle * 100f) / 100f;
                        matchTotalScoreRifle = Mathf.Round(matchTotalScoreRifle * 100f) / 100f;

                        PistolUIManager.Instance.totalScoreTxt.text = "SERIES SCORE : " + TotalScoreRifle.ToString("F1");
                        PistolUIManager.Instance.totalGameScoreTxt.text = "TOTAL SCORE : " + matchTotalScoreRifle.ToString("F1");

                        noOfShotsFired++;
                        shotsFired++;
                        if (seriesCount == 0)
                        {
                            PistolUIManager.Instance.seriesNoTitle.text = "SERIES " + (seriesCount + 1).ToString();
                            series1ScoreRifle = series1ScoreRifle + PistolUIManager.Instance.shotRoundScoreRifle;
                            PistolUIManager.Instance.series1Text.text = series1ScoreRifle.ToString();
                        }
                        else if (seriesCount == 1)
                        {
                            PistolUIManager.Instance.seriesNoTitle.text = "SERIES " + (seriesCount + 1).ToString();
                            series2ScoreRifle = series2ScoreRifle + PistolUIManager.Instance.shotRoundScoreRifle;
                            PistolUIManager.Instance.series2text.text = series2ScoreRifle.ToString();
                        }
                        else if (seriesCount == 2)
                        {
                            PistolUIManager.Instance.seriesNoTitle.text = "SERIES " + (seriesCount + 1).ToString();
                            series3ScoreRifle = series3ScoreRifle + PistolUIManager.Instance.shotRoundScoreRifle;
                            PistolUIManager.Instance.series3Text.text = series3ScoreRifle.ToString();
                        }
                        else
                        {

                        }
                        if (shotsFired == 10)
                        {
                            gameTotalScoreRifle = gameTotalScoreRifle + TotalScoreRifle + innerTno;

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
