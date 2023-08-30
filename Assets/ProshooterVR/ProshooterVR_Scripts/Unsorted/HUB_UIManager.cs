using Firebase.Firestore;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HUB_UIManager : MonoBehaviour
{
    //UI Manger responsible for HUB UI calls

    //Making it static to access all over layers
    public static HUB_UIManager Instance;

    public GameObject gameModeUI, tutorailUI;
    public GameObject levelUI;

    public GameObject settingUI;
    public GameObject playBtn;
    public GameObject airPistolTut, airRifleTut, rapidRifeTUt;
    public GameObject musicPlayer;

    public enum gameType
    {
        match,
        practice
    }

    gameType myGameType;

    void Awake()
    {
        Instance = this;
    }

    // Get panles
   

    private void Start()
    {
        gameModeUI.SetActive(false);
        tutorailUI.SetActive(false);
        settingUI.SetActive(false);

        levelUI.SetActive(true);
        playBtn.SetActive(false);
        levelUI.GetComponent<CustomButtonNavigator>().onButtonClicked(0);
        HUB_UIManager.Instance.musicPlayer.SetActive(true);

    }

    public void singlePlayerBtnClicked()
    {
        gameModeUI.SetActive(true);
        myGameType = gameType.match;
    }

    public void PracticeBtnClicked()
    {
        gameModeUI.SetActive(true);
        myGameType = gameType.practice;

    }

    public void setingsBtnClicked()
    {
        settingUI.SetActive(true);
        gameModeUI.SetActive(false);
        tutorailUI.SetActive(false);

    }

    public void tutorialBtnClicked()
    {
        gameModeUI.SetActive(false);
        tutorailUI.SetActive(true);
        settingUI.SetActive(false);

    }

    public void airPistolTutorial()
    {
        airPistolTut.SetActive(true);
        airRifleTut.SetActive(false);
       // rapidRifeTUt.SetActive(false);
    }
    public void airRifleTutorial()
    {
        airPistolTut.SetActive(false);
        airRifleTut.SetActive(true);
      //  rapidRifeTUt.SetActive(false);

    }
    public void RapidFireTutorial()
    {
        airPistolTut.SetActive(false);
        airRifleTut.SetActive(false);
       // rapidRifeTUt.SetActive(true);
    }

    public void closeAggrMenu()
    {
        LocalUserDataManager.Instance.isAggrDone = true;
    }
    
    // 10m Air Pistol Scenes ----------------------------------------------------Start

    // Practice Modes ----------------------
    private void load10mPistolAmatuerPractice()
    {
        SceneManager.LoadSceneAsync("10m_Pistol_Amateur_Practice");
    }
    private void load10mPistolSemProPractice()
    {

        SceneManager.LoadSceneAsync("10m_Pistol_SemPro_Practice");
    }
    private void load10mPistolProPractice()
    {

        SceneManager.LoadSceneAsync("10m_Pistol_Pro_Practice");
    }
    // Macth  Modes ----------------------
    private void load10mPistolAmatuerMatch()
    {
        LocalUserDataManager.Instance.selectedGameMode = GameModes.AirPistol10m;
        LocalUserDataManager.Instance.SelectedGameLevel = GameLevel.Amateur;

        SceneManager.LoadSceneAsync("10m_Pistol_Amateur_MatchMode");
    }
    private void load10mPistolSemProMatch()
    {
        LocalUserDataManager.Instance.selectedGameMode = GameModes.AirPistol10m;
        LocalUserDataManager.Instance.SelectedGameLevel = GameLevel.SemiPro;
        SceneManager.LoadSceneAsync("10m_Pistol_SemPro_MatchMode");
    }
    private void load10mPistolProMatch()
    {
        LocalUserDataManager.Instance.selectedGameMode = GameModes.AirPistol10m;
        LocalUserDataManager.Instance.SelectedGameLevel = GameLevel.Pro;
        SceneManager.LoadSceneAsync("10m_Pistol_Pro_MatchMode");
    }


    // 10m Air Pistol Scenes ----------------------------------------------------End


    //10m Air Rifle Scenes -------------------------------------------------------Start
    // Practice Modes ----------------------
    private void load10mRifleAmatuerPractice()
    {
        SceneManager.LoadSceneAsync("10m_AirRifle_Amateur_Practice");
    }
    private void load10mRifleSemProPractice()
    {
        SceneManager.LoadSceneAsync("10m_AirRifle_SemPro_Practice");
    }
    private void load10mRifleProPractice()
    {
        SceneManager.LoadSceneAsync("10m_AirRifle_Pro_Practice");
    }
    // Macth  Modes ----------------------
    private void load10mRifleAmatuerMatch()
    {
        LocalUserDataManager.Instance.selectedGameMode = GameModes.AirRifle10m;
        LocalUserDataManager.Instance.SelectedGameLevel = GameLevel.Amateur;
        SceneManager.LoadSceneAsync("10m_AirRifle_Amateur_Match");
    }
    private void load10mRifleSemProMatch()
    {
        LocalUserDataManager.Instance.selectedGameMode = GameModes.AirRifle10m;
        LocalUserDataManager.Instance.SelectedGameLevel = GameLevel.SemiPro;
        SceneManager.LoadSceneAsync("10m_AirRifle_SemPro_Match");
    }
    private void load10mRifleProMatch()
    {
        LocalUserDataManager.Instance.selectedGameMode = GameModes.AirRifle10m;
        LocalUserDataManager.Instance.SelectedGameLevel = GameLevel.Pro;
        SceneManager.LoadSceneAsync("10m_AirRifle_Pro_Match");
    }

    private void load25mRFPro()
    {
        SceneManager.LoadSceneAsync("ProShooterVR_25mRF_Pro");
    }
    private void load25mRFSemPro()
    {
        SceneManager.LoadSceneAsync("ProShooterVR_25mRF_SemiPro");
    }
    private void load25mRFAmateur()
    {
        SceneManager.LoadSceneAsync("ProShooterVR_25mRF_Amateur");
    }

    //10m Air Rifle Scenes -------------------------------------------------------End

    public void setLevel(int no)
    {
        switch (no)
        {
            case 0:
                LocalUserDataManager.Instance.levelSelected = LocalUserDataManager.gamerLevel.Amateur;
                LocalUserDataManager.Instance.SelectedGameLevel = GameLevel.Amateur;
                //LiveUserDataManager.Instance.sortLeaderBoard();

                break;
            case 1:
                LocalUserDataManager.Instance.levelSelected = LocalUserDataManager.gamerLevel.SemiPro;
                LocalUserDataManager.Instance.SelectedGameLevel = GameLevel.SemiPro;
               // LiveUserDataManager.Instance.sortLeaderBoard();

                break;
            case 2:
                LocalUserDataManager.Instance.levelSelected = LocalUserDataManager.gamerLevel.Pro;
                LocalUserDataManager.Instance.SelectedGameLevel = GameLevel.Pro;
                //LiveUserDataManager.Instance.sortLeaderBoard();

                break;
        }
    }
    public void setMode(int no)
    {
        playBtn.SetActive(true);
        switch (no)
        {
            case 0:
                LocalUserDataManager.Instance.modeSelected = LocalUserDataManager.gameMode.AirPistol10M;
                LocalUserDataManager.Instance.selectedGameMode = GameModes.AirPistol10m;
               // LiveUserDataManager.Instance.sortLeaderBoard();
                break;
            case 1:
                LocalUserDataManager.Instance.modeSelected = LocalUserDataManager.gameMode.rapidFire25m;
              //  LiveUserDataManager.Instance.sortLeaderBoard();
                break;
            case 2:
                LocalUserDataManager.Instance.modeSelected = LocalUserDataManager.gameMode.airRifle10m;
                LocalUserDataManager.Instance.selectedGameMode = GameModes.AirRifle10m;

                //   LiveUserDataManager.Instance.sortLeaderBoard();
                break;
        }
       
    }

    public void PlayButtonClick()
    {
        if(myGameType == gameType.match)
        {
            loadMatchMode();
        }

        if (myGameType == gameType.practice)
        {
            loadPracticeMode();
        }
    }

    

    
    

    void loadPracticeMode()
    {
        switch (LocalUserDataManager.Instance.modeSelected)
        {
            case LocalUserDataManager.gameMode.AirPistol10M:
                switch (LocalUserDataManager.Instance.levelSelected)
                {
                    case LocalUserDataManager.gamerLevel.Amateur:
                        load10mPistolAmatuerPractice();
                        break;
                    case LocalUserDataManager.gamerLevel.SemiPro:
                        load10mPistolSemProPractice();
                        break;
                    case LocalUserDataManager.gamerLevel.Pro:
                        load10mPistolProPractice();
                        break;
                }
                break;
            case LocalUserDataManager.gameMode.rapidFire25m:
                switch (LocalUserDataManager.Instance.levelSelected)
                {
                    case LocalUserDataManager.gamerLevel.Amateur:
                        load25mRFAmateur();
                        break;
                    case LocalUserDataManager.gamerLevel.SemiPro:
                        load25mRFSemPro();
                        break;
                    case LocalUserDataManager.gamerLevel.Pro:
                        load25mRFPro();
                        break;
                }
                break;
            case LocalUserDataManager.gameMode.airRifle10m:
                switch (LocalUserDataManager.Instance.levelSelected)
                {
                    case LocalUserDataManager.gamerLevel.Amateur:
                        load10mRifleAmatuerPractice();
                        break;
                    case LocalUserDataManager.gamerLevel.SemiPro:
                        load10mRifleSemProPractice();
                        break;
                    case LocalUserDataManager.gamerLevel.Pro:
                        load10mRifleProPractice();
                        break;
                }
                break;
        }
    }
    void loadMatchMode()
    {
        switch (LocalUserDataManager.Instance.modeSelected)
        {
            case LocalUserDataManager.gameMode.AirPistol10M:
                switch (LocalUserDataManager.Instance.levelSelected)
                {
                    case LocalUserDataManager.gamerLevel.Amateur:
                        load10mPistolAmatuerMatch();
                        break;
                    case LocalUserDataManager.gamerLevel.SemiPro:
                        load10mPistolSemProMatch();
                        break;
                    case LocalUserDataManager.gamerLevel.Pro:
                        load10mPistolProMatch();
                        break;
                }
                break;
            case LocalUserDataManager.gameMode.rapidFire25m:
                switch (LocalUserDataManager.Instance.levelSelected)
                {
                    case LocalUserDataManager.gamerLevel.Amateur:
                        load25mRFAmateur();

                        break;
                    case LocalUserDataManager.gamerLevel.SemiPro:
                        load25mRFSemPro();

                        break;
                    case LocalUserDataManager.gamerLevel.Pro:
                        load25mRFPro();

                        break;
                }
                break;
            case LocalUserDataManager.gameMode.airRifle10m:
                switch (LocalUserDataManager.Instance.levelSelected)
                {
                    case LocalUserDataManager.gamerLevel.Amateur:
                        load10mRifleAmatuerMatch();
                        break;
                    case LocalUserDataManager.gamerLevel.SemiPro:
                        load10mRifleSemProMatch();
                        break;
                    case LocalUserDataManager.gamerLevel.Pro:
                        load10mRifleProMatch();
                        break;
                }
                break;
        }
    }


    public void leaderboardUIFill()
    {
        //LiveUserDataManager.Instance.sortLeaderBoard();
        

        
    }

}

