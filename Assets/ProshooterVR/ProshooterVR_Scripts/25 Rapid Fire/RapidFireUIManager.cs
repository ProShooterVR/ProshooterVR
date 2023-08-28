using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Nova;
using BNG;
using UnityEngine.SceneManagement;
using NovaSamples.Inventory;


public class RapidFireUIManager : MonoBehaviour
{

    public static RapidFireUIManager Instance;

    public float angle;
    public float shotScore;
    public int shotRoundScore;
    public float shotRoundScoreRifle;
    public string finalScore;
    
    public GameObject ScreenobjectToPlace;


    GameObject resetShotPos, resetParent, rifleOffObj;
   

    GameObject screenCenter, screenEnd;

    public GameObject[] screens,screentxt;
    public int screenNo;
   // public TextMeshPro sr1Score, sr2Score, sr3Score; // Reference to the UI Text component
   // public GameObject[] scoresDisp;
    public GameObject[] roundDisplay;

    public string[] instructions;
    public TextMeshPro instructionTxt;
   // public TextMeshPro sr1ScoreTxt,sr2ScoreTxt,sr3ScoreTxt;
    public TextMeshPro currentSrScoreTxt;
    public TextMeshPro shotOnTargetTxt,shotMissedTxt;

    public TextMeshPro r1ScoreTxt,r2ScoreTxt,totalGameScoreTxt;
    public GameObject round1Lble, round2Lble;

    [SerializeField]
    private GameObject helpPopUp;
    [SerializeField]
    private GameObject settingPopUp;
    public bool setSwitch;
    public GameObject menuPanel;
    public GameObject startBtn, resumeBtn;
    public GameObject scoreScreen;
    public GameObject endSessionScreen;
    private void Awake()
    {
        Instance = this;
       // Debug.Log(instructionTxt.GetComponent<TextMeshPro>().text);
    }


    // Start is called before the first frame update
    void Start()
    {
        clearScreen();
        ClearSeriesScreen();
        setSwitch = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (InputBridge.Instance.StartButtonDown == true || InputBridge.Instance.BackButton == true || Input.GetKeyDown("space"))
        {
            if (setSwitch == false)
            {
                settingPopUp.SetActive(true);
                RayManager.Instance.EnableRey();

                setSwitch = true;
            }

        }
    }

    public void clearScreen()
    {
        for (int i = 0; i < 5; i++)
        {
            screentxt[i].transform.GetChild(0).GetComponent<TextMeshPro>().text = "";
        }
        RapidFireGunManager.Instance.currentSeriesScore = 0;
        currentSrScoreTxt.text = "0";
    }

    public void ClearSeriesScreen()
    {
        for (int j = 0; j < 3; j++)
        {
            for (int i = 0; i < 5; i++)
            {
                roundDisplay[RapidFireGunManager.Instance.stageCounter].transform.GetChild(j).transform.GetChild(i).GetComponent<TextMeshPro>().text = "";
            }
        }
         

        roundDisplay[RapidFireGunManager.Instance.stageCounter].transform.GetChild(3).gameObject.GetComponent<TextMeshPro>().text = "";
        roundDisplay[RapidFireGunManager.Instance.stageCounter].transform.GetChild(4).gameObject.GetComponent<TextMeshPro>().text = "";
        roundDisplay[RapidFireGunManager.Instance.stageCounter].transform.GetChild(5).gameObject.GetComponent<TextMeshPro>().text = "";

    }

    public void foulSeries()
    {
          for (int i = 0; i < 5; i++)
            {
                screentxt[i].transform.GetChild(0).GetComponent<TextMeshPro>().text = "0";
                roundDisplay[RapidFireGunManager.Instance.stageCounter].transform.GetChild(RapidFireGunManager.Instance.SeriesCounter).transform.GetChild(i).GetComponent<TextMeshPro>().text = "0";

            }
        
        if (RapidFireGunManager.Instance.SeriesCounter == 1)
        { 
            RapidFireGunManager.Instance.series1Score = 0;
            roundDisplay[RapidFireGunManager.Instance.stageCounter].transform.GetChild(3).gameObject.GetComponent<TextMeshPro>().text = "0";

        }
        else if (RapidFireGunManager.Instance.SeriesCounter == 2)
        {
            RapidFireGunManager.Instance.series2Score = 0;
            roundDisplay[RapidFireGunManager.Instance.stageCounter].transform.GetChild(4).gameObject.GetComponent<TextMeshPro>().text = "0";


        }
        else if (RapidFireGunManager.Instance.SeriesCounter == 3)
        {
            RapidFireGunManager.Instance.series3Score = 0;
            roundDisplay[RapidFireGunManager.Instance.stageCounter].transform.GetChild(5).gameObject.GetComponent<TextMeshPro>().text = "0";


        }
        currentSrScoreTxt.text = "0";

    }
    public void updateShotScreen(Vector3 pos, float scoreVal, float direction,int ShotScreenNo)
    {

        if (RapidFireGunManager.Instance.countingScore == true)
        {
            screenNo = ShotScreenNo;
            screenCenter = screens[screenNo].transform.GetChild(0).transform.GetChild(0).transform.gameObject;
            screenEnd = screens[screenNo].transform.GetChild(0).transform.GetChild(1).transform.gameObject;
            resetShotPos = screens[screenNo].transform.GetChild(0).transform.GetChild(2).transform.gameObject;
            resetParent = screens[screenNo].transform.GetChild(0).gameObject;


            if (scoreVal < 5)
            {
                shotScore = 0;
                shotRoundScore = 0;
                finalScore = "0";
                Debug.Log("Score to record" + shotScore);
                RapidFireGunManager.Instance.shotsMissed++;
                shotMissedTxt.text = RapidFireGunManager.Instance.shotsMissed.ToString();

            }
            else
            {

                Debug.Log("Score to record" + scoreVal);

                shotScore = Mathf.Round(scoreVal * 100f) / 100f;

                shotScore = Mathf.Floor(shotScore);

                RapidFireGunManager.Instance.shotsOnTarget++;
                shotOnTargetTxt.text = RapidFireGunManager.Instance.shotsOnTarget.ToString();

                Debug.Log("Final score : " + shotScore);
                screentxt[screenNo].transform.GetChild(0).GetComponent<TextMeshPro>().text = shotScore.ToString();
            }

            GameObject placedObject = Instantiate(ScreenobjectToPlace, screenCenter.transform.position, Quaternion.identity, screens[screenNo].transform.GetChild(0).transform);

            Vector3 scle = screenCenter.transform.localScale;
            Debug.Log(scle);


            placedObject.transform.localScale = screenCenter.transform.localScale;
            placedObject.transform.localPosition = pos;



            angle = direction - 180;
            //  Debug.Log(" Aft :: " + angle);

            placedObject.transform.parent = resetParent.transform;
            placedObject.transform.localScale = resetShotPos.transform.localScale;
            placedObject.transform.localRotation = resetShotPos.transform.localRotation;

            placedObject.transform.localPosition = new Vector3(-placedObject.transform.localPosition.x/100, placedObject.transform.localPosition.y/100, placedObject.transform.localPosition.z);
        }
        else
        {
            shotScore = 0;
        }

        RapidFireGunManager.Instance.currentSeriesScore = RapidFireGunManager.Instance.currentSeriesScore + (int)shotScore;
        currentSrScoreTxt.text = RapidFireGunManager.Instance.currentSeriesScore.ToString();

        if (RapidFireGunManager.Instance.seriesFoul == true)
        {
            foulSeries();
        }

        if(RapidFireGunManager.Instance.SeriesCounter == 1)
        {
            Debug.Log("11111111111111"+screenNo);
            roundDisplay[RapidFireGunManager.Instance.stageCounter].transform.GetChild(0).transform.GetChild(screenNo).gameObject.GetComponent<TextMeshPro>().text = shotScore.ToString();

            if (RapidFireGunManager.Instance.stageCounter == 0)
            {
                RapidFireGunManager.Instance.series1Score = RapidFireGunManager.Instance.series1Score + (int)shotScore;
            }
            if (RapidFireGunManager.Instance.stageCounter == 1)
            {
                RapidFireGunManager.Instance.series4Score = RapidFireGunManager.Instance.series4Score + (int)shotScore;
            }
            //sr1ScoreTxt = roundDisplay[RapidFireGunManager.Instance.stageCounter].transform.GetChild(3).gameObject.GetComponent<TextMeshPro>();
            roundDisplay[RapidFireGunManager.Instance.stageCounter].transform.GetChild(3).gameObject.GetComponent<TextMeshPro>().text = RapidFireGunManager.Instance.series1Score.ToString();
        }
        
        if (RapidFireGunManager.Instance.SeriesCounter == 2)
        {
            Debug.Log("22222222222222" + screenNo);

            roundDisplay[RapidFireGunManager.Instance.stageCounter].transform.GetChild(1).transform.GetChild(screenNo).gameObject.GetComponent<TextMeshPro>().text = shotScore.ToString();

            if (RapidFireGunManager.Instance.stageCounter == 0)
            {
            
                RapidFireGunManager.Instance.series2Score = RapidFireGunManager.Instance.series2Score + (int)shotScore;

            }
            if (RapidFireGunManager.Instance.stageCounter == 1)
            {

                RapidFireGunManager.Instance.series5Score = RapidFireGunManager.Instance.series5Score + (int)shotScore;

            }


            //   sr2ScoreTxt = roundDisplay[RapidFireGunManager.Instance.stageCounter].transform.GetChild(4).gameObject.GetComponent<TextMeshPro>();

            roundDisplay[RapidFireGunManager.Instance.stageCounter].transform.GetChild(4).gameObject.GetComponent<TextMeshPro>().text = RapidFireGunManager.Instance.series2Score.ToString();

        }
       
        if (RapidFireGunManager.Instance.SeriesCounter == 3)
        {
            Debug.Log("33333333333" + screenNo);

            roundDisplay[RapidFireGunManager.Instance.stageCounter].transform.GetChild(2).transform.GetChild(screenNo).gameObject.GetComponent<TextMeshPro>().text = shotScore.ToString();

            if (RapidFireGunManager.Instance.stageCounter == 0)
            {
                RapidFireGunManager.Instance.series3Score = RapidFireGunManager.Instance.series3Score + (int)shotScore;
            }
            if (RapidFireGunManager.Instance.stageCounter == 1)
            {
                RapidFireGunManager.Instance.series6Score = RapidFireGunManager.Instance.series6Score + (int)shotScore;
            }

            //sr3ScoreTxt = roundDisplay[RapidFireGunManager.Instance.stageCounter].transform.GetChild(5).gameObject.GetComponent<TextMeshPro>();
            roundDisplay[RapidFireGunManager.Instance.stageCounter].transform.GetChild(5).gameObject.GetComponent<TextMeshPro>().text = RapidFireGunManager.Instance.series3Score.ToString();

        }

        if (RapidFireGunManager.Instance.stageCounter == 0)
        {
            RapidFireGunManager.Instance.round1Score = RapidFireGunManager.Instance.round1Score + (int)shotScore;
            r1ScoreTxt.text = RapidFireGunManager.Instance.round1Score.ToString();
            RapidFireGunManager.Instance.totalGameScore = RapidFireGunManager.Instance.totalGameScore + (int)shotScore;
            totalGameScoreTxt.text = RapidFireGunManager.Instance.totalGameScore.ToString();
        }
        else if (RapidFireGunManager.Instance.stageCounter == 1)
        {
            RapidFireGunManager.Instance.round2Score = RapidFireGunManager.Instance.round2Score + (int)shotScore;
            r2ScoreTxt.text = RapidFireGunManager.Instance.round2Score.ToString();
            RapidFireGunManager.Instance.totalGameScore = RapidFireGunManager.Instance.totalGameScore + (int)shotScore;
            totalGameScoreTxt.text = RapidFireGunManager.Instance.totalGameScore.ToString();

        }

        //screenScores.Add(placedObject);

    }

    public void StartButtonClick()
    {
        startBtn.SetActive(false);
        resumeBtn.SetActive(true);
        scoreScreen.SetActive(true);
        menuPanel.SetActive(false);
        settingPopUp.SetActive(false);
        StartCoroutine(RapidFireGunManager.Instance.StartTimerStart());
        RayManager.Instance.DisableRey();


    }
    public void resumeBtnClick()
    {
        setSwitch = false;
        settingPopUp.SetActive(false);
        RayManager.Instance.DisableRey();

    }
    public void HelpButtonClick()
    {
       
    }
    public void exitBtnCliked()
    {
        SceneManager.LoadScene("ProShooterVR_Hub", LoadSceneMode.Single);
    }
    public void RestartBtnClick()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex, LoadSceneMode.Single);
        setSwitch = false;
        RayManager.Instance.EnableRey();

    }
    public void backButtonClick()
    {
        endSessionScreen.SetActive(false);
        RapidFireEndSessionManager.Instance.disableTargetScores();
    }
}
