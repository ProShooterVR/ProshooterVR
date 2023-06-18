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
    public float shotRoundScoreRifle;
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


    public GameObject resetShotPos,resetParent,rifleOffObj;
    public GameObject gamePopUp;
    public GameObject startBtn, resumeBtn;
    public GameObject scoreScreen;
    public List<GameObject> screenScores;
    /// <summary>
    /// ----- Screen calc -----
    /// </summary>

    public GameObject screen, screenCenter, screenEnd;

    float targetscoreOff, screenscoreOff;
    public GameObject endMatchPopUp,settingPopUp,leaderPopUp;
    public bool setSwitch;

    public GameObject[] Leaders;
    public GameObject LeaderN;
    public Sprite leaderH;
    public TextMeshProUGUI title;

    [SerializeField]
    private GameObject helpScr1, helpScr2, helpScr3;
    [SerializeField]
    private GameObject helpPopUp;

    public int btnCnt;
    
    [SerializeField]
    private GameObject upperLeft, lowerRight;
    private void Awake()
    {
        Instance = this;
        
    }
    // Start is called before the first frame update
    void Start()
    {
        btnCnt = 0;
        screenScores = new List<GameObject>();
        screenscoreOff = Vector3.Distance(screenCenter.transform.localPosition, screenEnd.transform.localPosition)/100;
        PistolUIManager.Instance.currentShotScore.text = "";

        PistolUIManager.Instance.shotsHitText.text = "0";
        PistolUIManager.Instance.shotsMissText.text = "0";
        setSwitch = false;
       // settingPopUp.SetActive(false);
        gamePopUp.SetActive(true);
        startBtn.SetActive(true);
        resumeBtn.SetActive(false);
        scoreScreen.SetActive(false);
        leaderPopUp.SetActive(false);
        //startBtnClick();
        Debug.Log(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        instructionText.text = "";

        PistolUIManager.Instance.startBtnClick();



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
                leaderPopUp.SetActive(false);
                endMatchPopUp.SetActive(false);

                GunGameManeger.Instance.isGamePause = true;
                setSwitch = true;
            }
            

        }
        

        if (InputBridge.Instance.YButtonDown == true)
        {
            helpPopUp.SetActive(true);
            settingPopUp.SetActive(false);
            leaderPopUp.SetActive(false);
            endMatchPopUp.SetActive(false);
            GunGameManeger.Instance.isGamePause = true;
        }

    }



    public void saveData()
    {
        LiveUserDataManager.Instance.SavePistolGameDataToLiveDB();
    }   
    public void backBtnClick()
    {
        SceneManager.LoadScene("ProShooterVR_Hub", LoadSceneMode.Single);
    }
    public void RestartBtnClick()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex, LoadSceneMode.Single);
        setSwitch = false;
    }

    public void leaderBoardClick()
    {
        leaderPopUp.SetActive(true);
        endMatchPopUp.SetActive(false);
        // LiveUserDataManager.Instance.sortGameLeaderBoard();
        scoreScreen.SetActive(false);
        gamePopUp.SetActive(false);

    }

    public void HelpPopupCloseButton()
    {
        helpPopUp.SetActive(false);
        settingPopUp.SetActive(true);
        setSwitch = false;

    }
    public void HelpButtonClick()
    {
        settingPopUp.SetActive(false);

        helpPopUp.SetActive(true);
        helpScr1.SetActive(true);
        helpScr2.SetActive(false);
        helpScr3.SetActive(false);
    }

   

    public void startBtnClick()
    {
        startBtn.SetActive(false);
        resumeBtn.SetActive(true);
        GunGameManeger.Instance.isGamePause = false;
        scoreScreen.SetActive(true);
        gamePopUp.SetActive(false);
        settingPopUp.SetActive(false);
        GunGameManeger.Instance.touchReloader.SetActive(true);
        GunGameManeger.Instance.relodePt.SetActive(true);

        Debug.Log("Why god why");
       // GunGameManeger.Instance.relodeHinge.SetActive(true);
        ///GunGameManeger.Instance.relodePin.SetActive(false);

       // Instantiate(GunGameManeger.Instance.palletSpawn, GunGameManeger.Instance.palletPos, GunGameManeger.Instance.palletObj.transform.rotation);


    }
    public void resumeBtnClick()
    {
        GunGameManeger.Instance.isGamePause = false;
        setSwitch = false;
        settingPopUp.SetActive(false);
    }

    public void backToRange()
    {
        leaderPopUp.SetActive(false);
        endMatchPopUp.SetActive(true);
        setSwitch = false;

    }
    public void CloseHelpPanel()
    {
        helpPopUp.SetActive(false);
        setSwitch = false;

    }
    public void nextButtonClick()
    {
        btnCnt++;
        if(btnCnt > 2)
        {
            btnCnt = 2;
        }
        switch (btnCnt)
        {
            case 0:
                helpScr1.SetActive(true);
                helpScr2.SetActive(false);
                helpScr3.SetActive(false);
                break;
            case 1:
                helpScr1.SetActive(false);
                helpScr2.SetActive(true);
                helpScr3.SetActive(false);
                break;
            case 2:
                helpScr1.SetActive(false);
                helpScr2.SetActive(false);
                helpScr3.SetActive(true);
                break;
        }
    }
    public void PrvButtonClick()
    {
        btnCnt--;
        if (btnCnt < 0)
        {
            btnCnt = 0;
        }
        switch (btnCnt)
        {
            case 0:
                helpScr1.SetActive(true);
                helpScr2.SetActive(false);
                helpScr3.SetActive(false);
                break;
            case 1:
                helpScr1.SetActive(false);
                helpScr2.SetActive(true);
                helpScr3.SetActive(false);
                break;
            case 2:
                helpScr1.SetActive(false);
                helpScr2.SetActive(false);
                helpScr3.SetActive(true);
                break;
        }
    }

    public void clearShotScreen()
    {
        if (screenScores.Count > 1)
        {
            for (int i = 0; i < screenScores.Count; i++)
            {
                Destroy(screenScores[i]);
            }
        }
    }
    public void updateShotScreen(Vector3 pos, float scoreVal, float direction)
    {
        if(scoreVal < 1)
        {
            shotScore = 0;
            shotRoundScore = 0;
            finalScore = "0";
            shotRoundScoreRifle = 0;
            GunGameManeger.Instance.noShotMissed++;
            shotsMissText.text = GunGameManeger.Instance.noShotMissed.ToString();
        }
        else {
            GunGameManeger.Instance.noShotsHit++;
            shotsHitText.text = GunGameManeger.Instance.noShotsHit.ToString();
            // shotScore = Mathf.Round(scoreVal * 10f) / 10f;
            Debug.Log("" + scoreVal);

            shotScore = Mathf.Round(scoreVal * 100f) / 100f;

            if (GunGameManeger.Instance.isPistolMode == true)
            {

                if (shotScore < 10.4f)
                {
                    shotRoundScore = (int)shotScore;

                    finalScore = shotRoundScore.ToString();
                }
                else if (shotScore >= 10.4f)
                {
                    finalScore = "10x";
                    shotRoundScore = 10;
                    GunGameManeger.Instance.innerTno++;
                }
                else { }
                shotScore = Mathf.Floor(shotScore);
            }
            if (GunGameManeger.Instance.isRifleMode == true)
            {

                if (shotScore < 10.2f)
                {
                    shotScore = Mathf.Floor(shotScore * 10) / 10;
                    finalScore = shotScore.ToString();
                   // finalScore = finalScore.Substring(0, finalScore.Length - 1);
                    shotRoundScoreRifle = float.Parse(finalScore);
                    
                }
                else if (shotScore >= 10.2f)
                {
                    shotScore = Mathf.Floor(shotScore * 10) / 10;
                    finalScore = shotScore.ToString();
                   // finalScore = finalScore.Substring(0, finalScore.Length - 1);
                    shotRoundScoreRifle = float.Parse(finalScore);
                    finalScore = finalScore + "x";
                }
                else if (shotScore == 10.9f)
                {

                    finalScore = shotScore.ToString();
                    shotRoundScoreRifle = shotScore;
                    finalScore = shotScore + "x";
                }
                else { }
            }
            Debug.Log("Final score : "+finalScore);
        }

       
        Debug.Log("shotScore : " + shotScore + " || shotRoundScore : " + shotRoundScoreRifle);

        if (GunGameManeger.Instance.isPistolMode == true)
        {
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
        }
        if (GunGameManeger.Instance.isRifleMode == true)
        {
            if (shotRoundScoreRifle == 0)
            {
                instructionText.text = Instructions.Poorshot;
            }
            else if (shotRoundScoreRifle >= 1 && shotRoundScoreRifle < 7)
            {
                instructionText.text = Instructions.Notbad;
            }
            else if (shotRoundScoreRifle >= 7 && shotRoundScoreRifle < 9)
            {
                instructionText.text = Instructions.Goodshot;
            }
            else if (shotRoundScoreRifle == 9)
            {
                instructionText.text = Instructions.Almostthere;
            }
            else if (shotRoundScoreRifle >= 10)
            {
                instructionText.text = Instructions.Perfect10;
            }
        }

        GameObject placedObject = Instantiate(ScreenobjectToPlace, screenCenter.transform.position, Quaternion.identity, screen.transform);

        Vector3 scle = screenCenter.transform.localScale;
        Debug.Log(scle);


        placedObject.transform.localScale = screenCenter.transform.localScale;

        placedObject.transform.localPosition = pos;

       

        angle = direction - 180;
        Debug.Log(" Aft :: " + angle);

        placedObject.transform.parent = resetParent.transform;
        placedObject.transform.localScale = resetShotPos.transform.localScale;
        placedObject.transform.localRotation = resetShotPos.transform.localRotation;
       
        placedObject.transform.localPosition = new Vector3(placedObject.transform.localPosition.x, placedObject.transform.localPosition.y, resetShotPos.transform.localPosition.z);

        if (GunGameManeger.Instance.isRifleMode == true)
        {
            if ((placedObject.transform.localPosition.x + placedObject.transform.localScale.x / 2) < upperLeft.transform.localPosition.x && (placedObject.transform.localPosition.y + placedObject.transform.localScale.y / 2) > upperLeft.transform.localPosition.y && (placedObject.transform.localPosition.x) > lowerRight.transform.localPosition.x + placedObject.transform.localScale.x / 2 && (placedObject.transform.localPosition.y) < lowerRight.transform.localPosition.y + placedObject.transform.localScale.y / 2)
            {
                placedObject.SetActive(true);
            }
            else
            {
                placedObject.SetActive(false);

               // Debug.Log("" + placedObject.transform.localPosition.x + placedObject.transform.localScale.x / 2 + " :: " + upperLeft.transform.localPosition.x);
            }
        }
        if (GunGameManeger.Instance.isPistolMode == true || GunGameManeger.Instance.isRifleMode == true)
        {
            if (scoreVal == 0)
            { placedObject.SetActive(false); }
        }


            screenScores.Add(placedObject);
        
    }
}
