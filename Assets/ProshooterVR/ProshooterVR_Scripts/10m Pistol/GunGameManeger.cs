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


    public bool isReloaded,isReloading;

    public Animator animator;
    public string animationName;

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
    void Awake()
    {
        Instance = this;
    }



    // Start is called before the first frame update
    void Start()
    {

        isGamePause = true;

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
            GunDataManager.Instance.gameMode = "10m Air Pistol Match Mode - " + LocalUserDataManager.Instance .SelectedGameLevel;


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

       // PistolUIManager.Instance.startBtnClick();

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
            if (GunDataManager.Instance.personalAmaBest < gameTotalScore)
            {
                GunDataManager.Instance.personalAmaBest = gameTotalScore;
                GunDataManager.Instance.personalGameBest = gameTotalScore; 
           }
        }
        else if (LocalUserDataManager.Instance.SelectedGameLevel == GameLevel.SemiPro)
        {
            if (GunDataManager.Instance.personalSemiProBest < gameTotalScore)
            {
                GunDataManager.Instance.personalSemiProBest = gameTotalScore;
                GunDataManager.Instance.personalGameBest = gameTotalScore;

            }
        }
        else if (LocalUserDataManager.Instance.SelectedGameLevel == GameLevel.Pro)
        {
            if (GunDataManager.Instance.personalProBest < gameTotalScore)
            {
                GunDataManager.Instance.personalProBest = gameTotalScore;
                GunDataManager.Instance.personalGameBest = gameTotalScore;

            }
        }


        // Add data to Data manager
        GunDataManager.Instance.sr1Score = series1Score;
        GunDataManager.Instance.sr2Score = series2Score;
        GunDataManager.Instance.sr3Score = series3Score;
        GunDataManager.Instance.totalGameScore = gameTotalScore;

        
        GunDataManager.Instance.avgSrScore = avgScore;
        GunDataManager.Instance.noOfInnerTens = innerTno;
        GunDataManager.Instance.noOfShotsMissed = noShotMissed;
        GunDataManager.Instance.noOfShotsOnTarget = noShotsHit;

        float minutes = Mathf.FloorToInt(GunGameManeger.Instance.totalGameTime / 60);
        float seconds = Mathf.FloorToInt(GunGameManeger.Instance.totalGameTime % 60);
        GunDataManager.Instance.totalTimeSpent = string.Format("{0:00}:{1:00}", minutes, seconds);
        //upload data to the backend
        LiveUserDataManager.Instance.SaveGameDataToLiveDB();

        if (isRankedMode == true)
        {
            LocalUserDataManager.Instance.totalScore = LocalUserDataManager.Instance.totalScore + gameTotalScore;
            LiveUserDataManager.Instance.saveLeaderBoardData();
        }

    }


    void clearScorePanel()
    {
        for(int i = 0; i < 10; i++)
        {
            PistolUIManager.Instance.scorePanelData.gameObject.transform.GetChild(i).GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = "";
            PistolUIManager.Instance.scorePanelData.gameObject.transform.GetChild(i).GetChild(2).gameObject.GetComponent<Image>().enabled = false;

        }
    }
    public void shotFired()
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


                    PistolUIManager.Instance.scorePanelData.gameObject.transform.GetChild(shotsFired).GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = PistolUIManager.Instance.finalScore;

                    PistolUIManager.Instance.currentShotScore.text = PistolUIManager.Instance.finalScore;
                    PistolUIManager.Instance.scorePanelData.gameObject.transform.GetChild(shotsFired).GetChild(2).gameObject.GetComponent<Image>().enabled = true;
                    PistolUIManager.Instance.scorePanelData.gameObject.transform.GetChild(shotsFired).GetChild(2).gameObject.transform.Rotate(0, 0, PistolUIManager.Instance.angle);

                    GunDataManager.Instance.Scores[noOfShotsFired] = PistolUIManager.Instance.shotRoundScore;

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


                    PistolUIManager.Instance.scorePanelData.gameObject.transform.GetChild(shotsFired).GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = PistolUIManager.Instance.finalScore;

                    PistolUIManager.Instance.currentShotScore.text = PistolUIManager.Instance.finalScore;
                    PistolUIManager.Instance.scorePanelData.gameObject.transform.GetChild(shotsFired).GetChild(2).gameObject.GetComponent<Image>().enabled = true;
                    PistolUIManager.Instance.scorePanelData.gameObject.transform.GetChild(shotsFired).GetChild(2).gameObject.transform.Rotate(0, 0, PistolUIManager.Instance.angle);

                    GunDataManager.Instance.Scores[noOfShotsFired] = PistolUIManager.Instance.shotRoundScore;

                    Totalscore = Totalscore + PistolUIManager.Instance.shotRoundScore;
                    matchTotalScore = matchTotalScore + PistolUIManager.Instance.shotRoundScore;

                    PistolUIManager.Instance.totalScoreTxt.text = "SERIES SCORE : " + Totalscore.ToString();
                    PistolUIManager.Instance.totalGameScoreTxt.text = "TOTAL SCORE : " + matchTotalScore.ToString();

                    noOfShotsFired++;
                    shotsFired++;
                    if (seriesCount == 0)
                    {
                        PistolUIManager.Instance.seriesNoTitle.text = "SERIES " + (seriesCount+1).ToString();
                        series1Score = series1Score + PistolUIManager.Instance.shotRoundScore;
                        PistolUIManager.Instance.series1Text.text = series1Score.ToString();
                    }
                    else if(seriesCount == 1)
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
}
