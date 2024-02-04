using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using BNG;
using System;
using NovaSamples.Inventory;
using ProshooterVR;
using Nova;
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
    

    public TextMeshPro currentSeriesScoreTxt,totalGameScoreTxt;
    public TextMeshPro series1Text,series2text,series3Text;
    public TextMeshPro currentShotScore;
    public TextMeshPro seriesNoTitle;
    public TextMeshPro instructionText;
    public TextMeshPro shotsHitText;
    public TextMeshPro shotsMissText;
    public GameObject scorePanelData;

    public TextMeshPro timerValue;

    public GameObject ScreenobjectToPlace;


    public GameObject resetShotPos,resetParent,rifleOffObj;
    public GameObject menuPanel;
    public GameObject startBtn, resumeBtn;
    public GameObject scoreScreen;
    public GameObject uxPanel;
    public List<GameObject> screenScores;

    public TextMeshProUGUI fadeScore;
    public GameObject fadeScorObj;
    /// <summary>
    /// ----- Screen calc -----
    /// </summary>

    public GameObject screen, screenCenter, screenEnd;

    float targetscoreOff, screenscoreOff;
    public GameObject endMatchPopUp,settingPopUp,endSessionPopup,standSettingPopup;
    public bool setSwitch;


    [SerializeField]
    private GameObject helpScr1, helpScr2, helpScr3;
    [SerializeField]
    private GameObject helpPopUp;

    public int btnCnt;
    
    [SerializeField]
    private GameObject upperLeft, lowerRight;

    public GameObject prevPlaced;

    public bool rfStandMove;
    public bool isReyOn;
    public GameObject gripAdjustUI;
    public GameObject startMsgUI;
    public GameObject rightHandController;
    public GameObject progresBar;
    public bool isOtherUIOpen;
    public TextMeshPro shotsLeftTxt;

    public GameObject AudioSettingsUIPanel, SettingsUIPanel;

    private void Awake()
    {
        Instance = this;
        
    }
    // Start is called before the first frame update
    void Start()
    {
        isOtherUIOpen = false;
        if (weaponManager.Instance.isPistolMode == true)
        {
            gripAdjustUI.SetActive(false);
        }
        if(GunGameManeger.Instance.isRankedMode == true)
        {
            startMsgUI.SetActive(true);
            menuPanel.SetActive(false);
            isOtherUIOpen = true;

        }
        if (GunGameManeger.Instance.isRankedMode == false)
        {
            if (GunGameManeger.Instance.isRankedMode == true)
            { 
                startMsgUI.SetActive(false); 
            }
            menuPanel.SetActive(true);
            startBtn.SetActive(true);
            resumeBtn.SetActive(false);
            scoreScreen.SetActive(false);
            endSessionPopup.SetActive(false);
        }

        btnCnt = 0;
        screenScores = new List<GameObject>();
        screenscoreOff = Vector3.Distance(screenCenter.transform.localPosition, screenEnd.transform.localPosition)/100;
        PistolUIManager.Instance.currentShotScore.text = "";

        PistolUIManager.Instance.shotsHitText.text = "0";
        PistolUIManager.Instance.shotsMissText.text = "0";
        setSwitch = false;
       // settingPopUp.SetActive(false);
       // menuPanel.SetActive(true);
       
        //startBtnClick();
        Debug.Log(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        instructionText.text = "";
        shotsLeftTxt.text = "30";
        //  

        AudioSettingsUIPanel.SetActive(false);
        SettingsUIPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        ///float distance = Vector3.Distance(targetCenter.transform.position, end.transform.position);

        // float score = (float)((((distance * 10000) / 95.9) - 10.9f)-4.2f);

        // Debug.Log("Score : " +score + " | Dist :"+ (((score + 4.2f) + 10.9) * 95.9) / 10000);

        if (InputBridge.Instance.StartButtonDown == true || InputBridge.Instance.BackButton == true || Input.GetKeyDown("space"))
        {
            if (isOtherUIOpen == false)
            {
                if (setSwitch == false)
                {
                    settingPopUp.SetActive(true);
                    endSessionPopup.SetActive(false);
                    endMatchPopUp.SetActive(false);
                    menuPanel.SetActive(true);
                    RayManager.Instance.EnableRey();

                    GunGameManeger.Instance.isGamePause = true;
                    setSwitch = true;
                }
            }



            //if (InputBridge.Instance.LeftTriggerDown == true)
            //{
            //    Debug.Log("calleed click");
            //}
            //if (InputBridge.Instance.YButtonDown == true)
            //{
            //    helpPopUp.SetActive(true);
            //    settingPopUp.SetActive(false);
            //    endSessionPopup.SetActive(false);
            //    endMatchPopUp.SetActive(false);
            //    GunGameManeger.Instance.isGamePause = true;
            //}

        }
    }


    public void okayStartBtnClick()
    {
        startMsgUI.SetActive(false);
        menuPanel.SetActive(true);
        isOtherUIOpen = false;
    }
    public void DisbaleUXBtnClick()
    {
        uxPanel.SetActive(false);
        UXManagerAirPistol.Instance.resetUXData();
        RayManager.Instance.DisableRey();
        GunGameManeger.Instance.isUXON = true;
        PistolUIManager.Instance.isOtherUIOpen = false;

    }
    public void ensbaleUXBtnClick()
    {
        uxPanel.SetActive(false);
        UXManagerAirPistol.Instance.resetUXData();
        RayManager.Instance.DisableRey();
        GunGameManeger.Instance.isUXON = false;
        PistolUIManager.Instance.isOtherUIOpen = false;
        DBAPIManagerNew.Instance.saveUXSettings(true);


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

    public void leaderBoardClick()
    {
        endSessionPopup.SetActive(true);
        endMatchPopUp.SetActive(false);
        // LiveUserDataManager.Instance.sortGameLeaderBoard();
        scoreScreen.SetActive(false);
        menuPanel.SetActive(false);

    }

    public void gameSettingsButtonClicked()
    {
        SettingsUIPanel.SetActive(true);
        menuPanel.SetActive(false);
        setSwitch = false;
        isOtherUIOpen = true;
    }
    public void GripAdjustmentButtonClicked()
    {
        SettingsUIPanel.SetActive(false);
        gripAdjustUI.SetActive(true);
        menuPanel.SetActive(false);
        setSwitch = false;
        isOtherUIOpen = true;
    }
    public void AudioSettingsButtonClicked()
    {
        AudioSettingsUIPanel.SetActive(true);
        menuPanel.SetActive(false);
    }
    public void AudioSettingsCloseButtonClicked()
    {
        AudioSettingsUIPanel.SetActive(false);
        menuPanel.SetActive(true);
    }
    public void AudioSettingsSaveButtonClicked()
    {
        AudioSettingsUIPanel.SetActive(false);
        menuPanel.SetActive(true);
    }
    public void GameSettingPanelCloseButtonClicked()
    {
        SettingsUIPanel.SetActive(false);
        menuPanel.SetActive(true);
    }
    public void gripAdjustBtnCLick()
    {
        rightHandController.GetComponent<HandRotation>().enabled = true;
        rightHandController.GetComponent<HandRotation>().onEnter();
        UXManagerAirPistol.Instance.resetUXData();
        RayManager.Instance.DisableRey();
        gripAdjustUI.SetActive(false);
    }
    public void HelpPopupCloseButton()
    {
        helpPopUp.SetActive(false);
        settingPopUp.SetActive(true);
        menuPanel.SetActive(true);
        setSwitch = false;
    }
    public void HelpButtonClick()
    {
        settingPopUp.SetActive(true);
        menuPanel.SetActive(false);
        helpPopUp.SetActive(true);
        helpScr1.SetActive(true);
        helpScr2.SetActive(false);
        helpScr3.SetActive(false);
        setSwitch = false;
        isOtherUIOpen = true;
    }

    public void startBtnClick()
    {
        startBtn.SetActive(false);
        resumeBtn.SetActive(true);
        GunGameManeger.Instance.isGamePause = false;
        scoreScreen.SetActive(true);
        menuPanel.SetActive(false);
        settingPopUp.SetActive(false);
        GunGameManeger.Instance.touchReloader.SetActive(true);

        gunRelodeManager.Instance.RelodTouch.SetActive(true);
        Debug.Log("fsdfsdfsdfsdfdfsdfsdfsdfsdf");
        GunGameManeger.Instance.spawnBullet = true;
        RayManager.Instance.DisableRey();
       

        clearUIScreen();
        GunGameManeger.Instance.clearScorePanel();



        UXManagerAirPistol.Instance.UXEvents(0);
        setSwitch = false;
        isOtherUIOpen = false;

        UXManagerAirPistol.Instance.resetUXData();


    }
    public void resumeBtnClick()
    {
        GunGameManeger.Instance.isGamePause = false;
        setSwitch = false;
        settingPopUp.SetActive(false);
        RayManager.Instance.DisableRey();
        setSwitch = false;

    }

    public void backToRange()
    {
        pistolPopUPUIManager.Instance.clearTargetScores();
        endSessionPopup.SetActive(false);
        endMatchPopUp.SetActive(false);
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

    public void clearUIScreen()
    {
        currentSeriesScoreTxt.text = "";
        totalGameScoreTxt.text = "";
        series1Text.text = "";
        series2text.text = "";
        series3Text.text = "";
        currentShotScore.text = "";
        instructionText.text = "";
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
        
        prevPlaced = null;
    }

    public void clearProgressBar()
    {
        for (int i = 0; i < 30; i++)
        {
            progresBar.transform.GetChild(i).GetComponent<UIBlock2D>().Color = HexToColor("#737171");

        }
    }
    Color HexToColor(string hex)
    {
        Color color = Color.black;
        if (ColorUtility.TryParseHtmlString(hex, out color))
        {
            return color;
        }
        else
        {
            Debug.LogError("Invalid hex color format: " + hex);
            return Color.black; // Default color (black) in case of an error
        }
    }
    public void updateShotScreen(Vector3 pos, float scoreVal, float direction)
    {
        progresBar.transform.GetChild(GunGameManeger.Instance.noOfShotsFired).GetComponent<UIBlock2D>().Color = HexToColor("#FF9F0A");
        shotsLeftTxt.text = (30 - (GunGameManeger.Instance.noOfShotsFired+1)).ToString();
        if (scoreVal < 1)
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

            if (weaponManager.Instance.isPistolMode == true)
            {

                if (shotScore < 10.4f)
                {
                    shotRoundScore = (int)shotScore ;
                    finalScore = shotRoundScore.ToString();

                }
                else if (shotScore >= 10.4f)
                {
                    shotRoundScore = (int)shotScore;
                    finalScore = "10x";
                    fadeScore.color = Color.blue;

                    GunGameManeger.Instance.innerTno++;
                }
                else { }
                shotScore = Mathf.Floor(shotScore);

                Debug.Log(" before :: " + direction);

                angle = direction - 45;
                Debug.Log(" Aft :: " + angle);
                ////// Precision zone multiplayer

            }
            if (weaponManager.Instance.isRifleMode == true)
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
                    //finalScore = finalScore + "x";
                }
                else if (shotScore == 10.9f)
                {

                    finalScore = shotScore.ToString();
                    shotRoundScoreRifle = shotScore;
                   
                }
                else { }

                Debug.Log(" before :: " + direction);

                angle = direction - 45;
                Debug.Log(" Aft :: " + angle);
            }
            Debug.Log("Final score : "+finalScore);
        }

        fadeScore.text = finalScore;

        Debug.Log("shotScore : " + shotScore + " || shotRoundScore : " + shotRoundScoreRifle);

        if (weaponManager.Instance.isPistolMode == true)
        {
            if (shotScore == 0)
            {
                instructionText.text = Instructions.Poorshot;
                fadeScore.color = Color.red;
                fadeScore.text = "MISS";
                FmodSetup.Instance.BadShotEvent();
            }
            else if (shotScore >= 1 && shotScore < 7)
            {
                instructionText.text = Instructions.Notbad;
                fadeScore.color = Color.yellow;
                FmodSetup.Instance.DecentShotEvent();
            }
            else if (shotScore >= 7 && shotScore < 9)
            {
                instructionText.text = Instructions.Goodshot;
                fadeScore.color = Color.green;
                FmodSetup.Instance.GoodShotEvent();

            }
            else if (shotScore == 9 || shotScore >= 9 && shotScore < 10)
            {
                fadeScore.color = Color.green;
                instructionText.text = Instructions.Almostthere;
                FmodSetup.Instance.GoodShotEvent();
            }
            else if (shotScore >= 10)
            {
                instructionText.text = Instructions.Perfect10;
                fadeScore.color = Color.blue;
                FmodSetup.Instance.EpicShotEvent();
            }
        }
        if (weaponManager.Instance.isRifleMode == true)
        {
            if (shotRoundScoreRifle == 0)
            {
                instructionText.text = Instructions.Poorshot;
                fadeScore.color = Color.red;
                fadeScore.text = "MISS";
                FmodSetup.Instance.BadShotEvent();
            }
            else if (shotRoundScoreRifle >= 1 && shotRoundScoreRifle < 7)
            {
                fadeScore.color = Color.yellow;
                instructionText.text = Instructions.Notbad;
                FmodSetup.Instance.DecentShotEvent();
            }
            else if (shotRoundScoreRifle >= 7 && shotRoundScoreRifle < 9)
            {
                fadeScore.color = Color.green;
                instructionText.text = Instructions.Goodshot;
                FmodSetup.Instance.GoodShotEvent();
            }
            else if (shotRoundScoreRifle == 9 || shotRoundScoreRifle >= 9 && shotRoundScoreRifle < 10)
            {
                fadeScore.color = Color.green;
                instructionText.text = Instructions.Almostthere;
                FmodSetup.Instance.GoodShotEvent();
            }
            else if (shotRoundScoreRifle >= 10)
            {
                fadeScore.color = Color.blue;
                instructionText.text = Instructions.Perfect10;
                FmodSetup.Instance.EpicShotEvent();
            }
        }

     

        StartCoroutine(playAnim());

        GameObject placedObject = Instantiate(ScreenobjectToPlace, screenCenter.transform.position, Quaternion.identity, screen.transform);

        Vector3 scle = screenCenter.transform.localScale;
        Debug.Log(scle);

        if (prevPlaced == null)
        {
            prevPlaced = placedObject;
        }
        else
        {
            prevPlaced.GetComponent<Renderer>().material.color = Color.green;
            prevPlaced = placedObject;
        }

        placedObject.transform.localScale = screenCenter.transform.localScale;

        placedObject.transform.localPosition = pos;


        

        placedObject.transform.parent = resetParent.transform;
        placedObject.transform.localScale = resetShotPos.transform.localScale;
        placedObject.transform.localRotation = resetShotPos.transform.localRotation;
       
        placedObject.transform.localPosition = new Vector3(placedObject.transform.localPosition.x, placedObject.transform.localPosition.y, resetShotPos.transform.localPosition.z);

        if (weaponManager.Instance.isRifleMode == true)
        {
            placedObject.SetActive(true);
            //if ((placedObject.transform.localPosition.x + placedObject.transform.localScale.x / 2) < upperLeft.transform.localPosition.x && (placedObject.transform.localPosition.y + placedObject.transform.localScale.y / 2) > upperLeft.transform.localPosition.y && (placedObject.transform.localPosition.x) > lowerRight.transform.localPosition.x + placedObject.transform.localScale.x / 2 && (placedObject.transform.localPosition.y) < lowerRight.transform.localPosition.y + placedObject.transform.localScale.y / 2)
            //{
            //    placedObject.SetActive(true);
            //}
            //else
            //{
            //    placedObject.SetActive(false);

            //   // Debug.Log("" + placedObject.transform.localPosition.x + placedObject.transform.localScale.x / 2 + " :: " + upperLeft.transform.localPosition.x);
            //}
        }
        if (weaponManager.Instance.isPistolMode == true || weaponManager.Instance.isRifleMode == true)
        {
            if (scoreVal == 0)
            { placedObject.SetActive(false); }
        }


            screenScores.Add(placedObject);

    }

    public void missedShots()
    {
        fadeScore.color = Color.red;
        fadeScore.text = "X";

        StartCoroutine(playAnim());

    }
    IEnumerator playAnim()
    {
        yield return new WaitForSeconds(0.2f);
        fadeScorObj.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        fadeScorObj.SetActive(false);
        yield return new WaitForSeconds(1f);

        if (GunGameManeger.Instance.isUXON == true)
        {
            UXManagerAirPistol.Instance.Lables[1].SetActive(true);
        }
    }
}
