using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using BNG;
using System;

public static class Instructions
{
    public static readonly string Notbad = "NOT BAD";
    public static readonly string Goodshot = "GOOD SHOT";
    public static readonly string Almostthere = "ALMOST THERE";
    public static readonly string Perfect10 = "A PERFECT 10!!!";
    public static readonly string Poorshot = "POOR SHOT";
    public static readonly string Shotmissed = "SHOT MISSED. AIM ON THE TARGET!!";
}



public class PistolUIManager : MonoBehaviour
{
    public static PistolUIManager Instance;

    

    public GameObject shotParent;

    public float angle;
    public float shotScore;
    public int shotRoundScore;
    public string finalScore;
    

    public TextMeshProUGUI totalScoreTxt,totalGameScoreTxt;
    public TextMeshProUGUI series1Text,series2text,series3Text;
    public TextMeshProUGUI currentShotScore;
    public TextMeshProUGUI seriesNoTitle;
    public TextMeshProUGUI instructionText;
    public TextMeshProUGUI shotsHitText;
    public TextMeshProUGUI shotsMissText;
    public GameObject scorePanelData;

    public TextMeshProUGUI timerValue;

    public GameObject ScreenobjectToPlace;


    public GameObject resetShotPos,resetParent;
    public GameObject gamePopUp;
    public GameObject startBtn, resumeBtn;
    public GameObject scoreScreen;
    /// <summary>
    /// ----- Screen calc
    /// </summary>

    public GameObject screen, screenCenter, screenEnd;

    float targetscoreOff, screenscoreOff;
    public GameObject endMatchPopUp,settingPopUp,leaderPopUp;
    public bool setSwitch;

    public GameObject[] Leaders;
    public GameObject LeaderN;
    public Sprite leaderH;
    public TextMeshProUGUI title;

    private void Awake()
    {
        Instance = this;
        
    }
    // Start is called before the first frame update
    void Start()
    {
        screenscoreOff = Vector3.Distance(screenCenter.transform.localPosition, screenEnd.transform.localPosition) / 100;
        PistolUIManager.Instance.currentShotScore.text = "";

        PistolUIManager.Instance.shotsHitText.text = "0";
        PistolUIManager.Instance.shotsMissText.text = "0";
        setSwitch = false;
        settingPopUp.SetActive(false);
        gamePopUp.SetActive(true);
        startBtn.SetActive(true);
        resumeBtn.SetActive(false);
        scoreScreen.SetActive(false);
        leaderPopUp.SetActive(false);

        Debug.Log(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
    }

    // Update is called once per frame
    void Update()
    {
        ///float distance = Vector3.Distance(targetCenter.transform.position, end.transform.position);

        // float score = (float)((((distance * 10000) / 95.9) - 10.9f)-4.2f);

        // Debug.Log("Score : " +score + " | Dist :"+ (((score + 4.2f) + 10.9) * 95.9) / 10000);

        if (InputBridge.Instance.StartButtonDown == true || InputBridge.Instance.BackButton == true)
        {
           if(setSwitch == false)
            {
                settingPopUp.SetActive(true);
                GunGameManeger.Instance.isGamePause = true;
                setSwitch = true;
            }
            else
            {
                settingPopUp.SetActive(false);
                GunGameManeger.Instance.isGamePause = false;
                setSwitch = false;

            }

        }
    }



    public void saveData()
    {
        LiveUserDataManager.Instance.SaveGameDataToLiveDB();
    }   
    public void backBtnClick()
    {
        SceneManager.LoadScene("ProShooterVR_Hub", LoadSceneMode.Single);
    }
    public void RestartBtnClick()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex, LoadSceneMode.Single);
    }

    public void leaderBoardClick()
    {
        leaderPopUp.SetActive(true);
        endMatchPopUp.SetActive(false);
        LiveUserDataManager.Instance.sortGameLeaderBoard();
    }

    public void startBtnClick()
    {
        startBtn.SetActive(false);
        resumeBtn.SetActive(true);
        GunGameManeger.Instance.isGamePause = false;
        scoreScreen.SetActive(true);
        gamePopUp.SetActive(false);

    }
    public void resumeBtnClick()
    {
        GunGameManeger.Instance.isGamePause = false;
        settingPopUp.SetActive(false);
    }

    public void backToRange()
    {
        leaderPopUp.SetActive(false);
        endMatchPopUp.SetActive(true);
    }


    public void clearShotScreen()
    {

    }
    public void updateShotScreen(Vector3 pos, float scoreVal, float direction)
    {
        if(scoreVal < 1)
        {
            shotScore = 0;
            shotRoundScore = 0;
            finalScore = "0";
            GunGameManeger.Instance.noShotMissed++;
            shotsMissText.text = GunGameManeger.Instance.noShotMissed.ToString();
        }
        else {
            GunGameManeger.Instance.noShotsHit++;
            shotsHitText.text = GunGameManeger.Instance.noShotsHit.ToString();
            // shotScore = Mathf.Round(scoreVal * 10f) / 10f;
            Debug.Log("" + scoreVal);


            if (GunGameManeger.Instance.isPistolMode == true)
            {

                if (scoreVal < 10.4f)
                {
                    shotRoundScore = (int)scoreVal;

                    finalScore = shotRoundScore.ToString();
                }
                else if (scoreVal >= 10.4f)
                {
                    finalScore = "10x";
                    shotRoundScore = 10;
                    GunGameManeger.Instance.innerTno++;
                }
                else { }
            }
            if (GunGameManeger.Instance.isRifleMode == true)
            {

                if (scoreVal < 10.2f)
                {
                    finalScore = shotRoundScore.ToString();

                }
                else if (scoreVal >= 10.2f)
                {
                    finalScore = shotScore.ToString() + "x";
                    shotRoundScore = (int)shotScore;
                }
                else { }
            }
            Debug.Log("Final score : "+finalScore);
        }

        shotScore = Mathf.Round(scoreVal * 100f) / 100f; ;

       
        shotScore = Mathf.Floor(shotScore);
        Debug.Log("shotScore : " + shotScore + " || shotRoundScore : " + shotRoundScore);

        if (shotScore == 0)
        {
            instructionText.text = Instructions.Poorshot;
        }
        else if (shotScore >= 1 && shotScore < 7)
        {
            instructionText.text = Instructions.Notbad;
        }
        else if (shotScore >= 7 && shotScore < 9)
        {
            instructionText.text = Instructions.Goodshot;
        }
        else if (shotScore == 9)
        {
            instructionText.text = Instructions.Almostthere;
        }
        else if (shotScore >= 10)
        {
            instructionText.text = Instructions.Perfect10;
        }



        GameObject placedObject = Instantiate(ScreenobjectToPlace, screenCenter.transform.position, Quaternion.identity, screen.transform);
        placedObject.transform.localScale = screenCenter.transform.localScale;
        placedObject.transform.localPosition = pos;

       

        angle = direction - 180;
        Debug.Log(" Aft :: " + angle);

        placedObject.transform.parent = resetParent.transform;
        placedObject.transform.rotation = resetShotPos.transform.rotation;
        placedObject.transform.localPosition = new Vector3(placedObject.transform.localPosition.x, placedObject.transform.localPosition.y, resetShotPos.transform.localPosition.z);


    }
}
