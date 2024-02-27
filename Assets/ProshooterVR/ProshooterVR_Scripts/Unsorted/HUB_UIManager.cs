using Firebase.Firestore;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using ProshooterVR;
using Nova;
using SimpleJSON;

public class HUB_UIManager : MonoBehaviour
{
    //UI Manger responsible for HUB UI calls

    //Making it static to access all over layers
    public static HUB_UIManager Instance;

    public GameObject tutorailUI;
    public GameObject levelUI;

    public GameObject settingUI;
    public GameObject airPistolTut, airRifleTut, rapidRifeTUt;
    public GameObject musicPlayer;
    public GameObject userProfileUI, mainUI;
    public TextMeshPro userNameTxtMainMenu;

    public GameObject assistedGudSubMenu,handSelectionMenu;


    public GameObject airPsubMenu, airRsubMenu, rfSubMenu;
    /// <summary>
    /// User Profile data local save
    /// </summary>
    ///

    public TextMeshPro userNameTxt;
    public TextMeshPro pbest_10mAirP_AmaTxt, pbest_10mAirP_SemPTxt, pbest_10mAirP_ProTxt;
    public TextMeshPro pbest_10mAirR_AmaTxt, pbest_10mAirR_SemPTxt, pbest_10mAirR_ProTxt;
    public TextMeshPro pbest_25mRF_AmaTxt, pbest_25mRF_SemPTxt, pbest_25mRF_ProTxt;

    public JSONNode MainLeaderboardJson;

    public GameObject MainLeaderBoardRow, MainLeaderBoardRowParent;
    public GameObject userPosOnMainLeaderBoardData;

    public TextMeshPro GamesPlayed_10mAP_OverallTxt, GamesPlayed_10mAR_OverallTxt, PrecisionPoints_10mAP_OverallTxt, PrecisionPoints_10mAR_OverallTxt;

    public TextMeshPro LBModeText, LBLevelText;

    public GameObject SoundControlPanel, AMB;


    public GameObject profBginPan, profIntmPan, profProffPan;

    public GameObject LeaderBoardModePanel, LeaderBoardDifficultyPanel;

    public GameObject LeaderboardModeDropDownlist, LeaderboardDifficultyDropDownlist;

    ////

    public enum gameType
    {
        match,
        practice,
        arcade,
    }

    gameType myGameType;

    void Awake()
    {
        Instance = this;

    }

    // Get panles


    private void Start()
    {

        // UI panels to turn OFF on start
        tutorailUI.SetActive(false);
        settingUI.SetActive(false);
        assistedGudSubMenu.SetActive(false);
        handSelectionMenu.SetActive(false);

        levelUI.SetActive(false);
        SoundControlPanel.SetActive(false);
        AMB.SetActive(true);
        airRifleTut.SetActive(false);
        rapidRifeTUt.SetActive(false);
        musicPlayer.SetActive(false);

        HUB_UIManager.Instance.musicPlayer.SetActive(true);



        // UI panels to turn ON on start
        mainUI.SetActive(true);
        userProfileUI.SetActive(true);
        profileBtnClicked();
        backToMainMenu();

        LocalUserDataManager.Instance.selectedGameMode = GameModes.AirPistol10m;
        LoadLeaderboardata(0);
    }

    public void leaderboardModeBtnClick()
    {
        LeaderboardDifficultyDropDownlist.SetActive(false);
        LeaderboardModeDropDownlist.SetActive(true);
    }

    public void leaderboardDifficultyBtnClick()
    {
        LeaderboardDifficultyDropDownlist.SetActive(true);
        LeaderboardModeDropDownlist.SetActive(false);
    }


    public void profAPBtn()
    {
        profBginPan.SetActive(true);
        profIntmPan.SetActive(false);
        profProffPan.SetActive(false);

    }
    public void profARBtn()
    {
        profBginPan.SetActive(false);
        profIntmPan.SetActive(true);
        profProffPan.SetActive(false);
    }
    public void profRFBtn()
    {
        profBginPan.SetActive(false);
        profIntmPan.SetActive(false);
        profProffPan.SetActive(true);
    }

    public void beginnerBtnClick()
    {
        setLevel(0);
        PlayButtonClick();
    }
    public void interMBtnClick()
    {
        setLevel(1);
        PlayButtonClick();

    }
    public void professionalBtnClick()
    {
        setLevel(2);
        PlayButtonClick();

    }

    public void airPistolBtnClick()
    {
        mainUI.SetActive(false);
        airPsubMenu.SetActive(true);
        airRsubMenu.SetActive(false);
        rfSubMenu.SetActive(false);
        levelUI.SetActive(true);
        setMode(0);
        singlePlayerBtnClicked();
    }
    public void airRifleBtnClick()
    {
        mainUI.SetActive(false);
        airPsubMenu.SetActive(false);
        airRsubMenu.SetActive(true);
        rfSubMenu.SetActive(false);
        levelUI.SetActive(true);

        setMode(2);
        singlePlayerBtnClicked();


    }
    public void rapidFireBtnClick()
    {
        mainUI.SetActive(false);
        airPsubMenu.SetActive(false);
        airRsubMenu.SetActive(false);
        rfSubMenu.SetActive(true);
        levelUI.SetActive(true);

        setMode(1);
        singlePlayerBtnClicked();

    }

    public void backToMainMenu()
    {
        mainUI.SetActive(true);
        airPsubMenu.SetActive(false);
        airRsubMenu.SetActive(false);
        rfSubMenu.SetActive(false);
        levelUI.SetActive(false);
        settingUI.SetActive(false);
        tutorailUI.SetActive(false);
        SoundControlPanel.SetActive(false);
        handSelectionMenu.SetActive(false);
        assistedGudSubMenu.SetActive(false);

    }

    public void OnAudioSettingsButtonClick()
    {
        SoundControlPanel.SetActive(true);
        settingUI.SetActive(false);
    }

    public void OnAirPistolLevelButtonClick()
    {
        string ModeName = "AIR PISTOL";
        HUB_UIManager.Instance.LBModeText.text = ModeName;

        HUB_UIManager.Instance.ClearMainLeaderboardRows();
    }
    public void OnAirRifleLevelButtonClick()
    {
        string ModeName = "AIR RIFLE";
        HUB_UIManager.Instance.LBModeText.text = ModeName;

        HUB_UIManager.Instance.ClearMainLeaderboardRows();
    }

    public void singlePlayerBtnClicked()
    {
        levelUI.GetComponent<CustomButtonNavigator>().onButtonClicked(0);
        myGameType = gameType.match;
    }

    public void PracticeBtnClicked()
    {
        myGameType = gameType.practice;
        levelUI.GetComponent<CustomButtonNavigator>().onButtonClicked(1);
    }
    public void AracadeBtnClicked()
    {
        levelUI.SetActive(false);
        assistedGudSubMenu.SetActive(false);
        handSelectionMenu.SetActive(false);

        tutorailUI.SetActive(false);
        myGameType = gameType.arcade;

    }

    public void setingsBtnClicked()
    {
        tutorailUI.SetActive(false);
        airPistolTut.SetActive(false);
        airRifleTut.SetActive(false);
        rapidRifeTUt.SetActive(false);
        settingUI.SetActive(true);
        airPsubMenu.SetActive(false);
        airRsubMenu.SetActive(false);
        rfSubMenu.SetActive(false);
        levelUI.SetActive(false);
        mainUI.SetActive(false);
    }
    public void profileBtnClicked()
    {
       // LocalUserDataManager.Instance.metaID = "6889892497704835";
        DBAPIManagerNew.Instance.getProfileData(LocalUserDataManager.Instance.metaID);
    }
    public void assistedBtnClicked()
    {
        assistedGudSubMenu.SetActive(true);
        settingUI.SetActive(false);
        if (LocalUserDataManager.Instance.isUXSaved == true)
        {
            assistedGudSubMenu.GetComponent<CustomButtonNavigator>().onButtonClicked(0);
        }
        else
        {
            assistedGudSubMenu.GetComponent<CustomButtonNavigator>().onButtonClicked(1);

        }
    }

    public void handSelectionBtnClicked()
    {
        handSelectionMenu.SetActive(true);
        settingUI.SetActive(false);
        if (LocalUserDataManager.Instance.isUXSaved == true)
        {
            handSelectionMenu.GetComponent<CustomButtonNavigator>().onButtonClicked(0);
        }
        else
        {
            handSelectionMenu.GetComponent<CustomButtonNavigator>().onButtonClicked(1);

        }
    }

    public void lefthandedBtnClicked()
    {
        LocalUserDataManager.Instance.isRightHand = false;
        DBAPIManagerNew.Instance.saveHandSelectionSettings("left");

    }

    public void righthandedBtnClicked()
    {
        LocalUserDataManager.Instance.isRightHand = true;
        DBAPIManagerNew.Instance.saveHandSelectionSettings("right");

    }

    public void assistedGuidEnable()
    {
        DBAPIManagerNew.Instance.saveUXSettings(true);
        LocalUserDataManager.Instance.isUXSaved = true;
    }

    public void assistedGuidDisable()
    {
        DBAPIManagerNew.Instance.saveUXSettings(false);
        LocalUserDataManager.Instance.isUXSaved = false;

    }
    public void update_playerProfileData()
    {
        Debug.Log("0000000000000000000000000000000000000000000000000000000000000000000");

        userNameTxt.text = LocalUserDataManager.Instance.userNameTxt;
       // totalScoreTxt.text = LocalUserDataManager.Instance.totalScoreTxt;
      //  matchesPlayedTxt.text = LocalUserDataManager.Instance.matchesPlayedTxt;
     //   accuracyTxt.text = LocalUserDataManager.Instance.accuracyTxt;

        pbest_10mAirP_AmaTxt.text = LocalUserDataManager.Instance.pbest_10mAirP_AmaTxt;
        pbest_10mAirP_SemPTxt.text = LocalUserDataManager.Instance.pbest_10mAirP_SemPTxt;
        pbest_10mAirP_ProTxt.text = LocalUserDataManager.Instance.pbest_10mAirP_ProTxt;

        pbest_10mAirR_AmaTxt.text = LocalUserDataManager.Instance.pbest_10mAirR_AmaTxt;
        pbest_10mAirR_SemPTxt.text = LocalUserDataManager.Instance.pbest_10mAirR_SemPTxt;
        pbest_10mAirR_ProTxt.text = LocalUserDataManager.Instance.pbest_10mAirR_ProTxt;

        pbest_25mRF_AmaTxt.text = LocalUserDataManager.Instance.pbest_25mRF_AmaTxt;
        pbest_25mRF_SemPTxt.text = LocalUserDataManager.Instance.pbest_25mRF_SemPTxt;
        pbest_25mRF_ProTxt.text = LocalUserDataManager.Instance.pbest_25mRF_ProTxt;

        GamesPlayed_10mAP_OverallTxt.text = LocalUserDataManager.Instance.OverallGamesPlayed_10AP_Txt;
        GamesPlayed_10mAR_OverallTxt.text = LocalUserDataManager.Instance.OverallGamesPlayed_10AR_Txt;
        PrecisionPoints_10mAP_OverallTxt.text = LocalUserDataManager.Instance.OverallPoints_10AP_Txt;
        PrecisionPoints_10mAR_OverallTxt.text = LocalUserDataManager.Instance.OverallPoints_10AR_Txt;
        // StartCoroutine(LoadImageFromURL(LocalUserDataManager.Instance.metauser_profileImage_url));
    }

    IEnumerator LoadImageFromURL(string url)
    {
        using (WWW www = new WWW(url))
        {
            yield return www;

            if (www.error != null)
            {
                Debug.LogError("Error loading image: " + www.error);
            }
            else
            {
                Texture2D texture = www.texture;
            }
        }
    }


    public void UpdateProfileButton()
    {
        // StartCoroutine(LoadProfileBtnImgFromURL(LocalUserDataManager.Instance.metauser_profileImage_url));

    }

    IEnumerator LoadProfileBtnImgFromURL(string url)
    {
        using (WWW www = new WWW(url))
        {
            yield return www;

            if (www.error != null)
            {
                Debug.LogError("Error loading image: " + www.error);
            }
            else
            {
                Texture2D texture = www.texture;
            }
        }
    }

  

    public void closeAssistesGud()
    {
        assistedGudSubMenu.SetActive(false);
        settingUI.SetActive(true);

    }

    public void tutorialBtnClicked()
    {
        tutorailUI.SetActive(true);
        mainUI.SetActive(false);
        airPsubMenu.SetActive(false);
        airRsubMenu.SetActive(false);
        rfSubMenu.SetActive(false);
        levelUI.SetActive(false);
        settingUI.SetActive(false);


    }

    public void airPistolTutorial()
    {
        tutorailUI.SetActive(false);

        airPistolTut.SetActive(true);
        airRifleTut.SetActive(false);
        rapidRifeTUt.SetActive(false);
        musicPlayer.SetActive(false);
        VideoPlayerController.Instance.BigPlayButton.SetActive(true);
        //HUB_UIManager.Instance.musicPlayer.GetComponent<MusicPlayer>().audioSource.enabled = false;
    }
    public void airRifleTutorial()
    {
        tutorailUI.SetActive(false);

        airPistolTut.SetActive(false);
        airRifleTut.SetActive(true);
        rapidRifeTUt.SetActive(false);
        musicPlayer.SetActive(false);
        VideoPlayerController.Instance.BigPlayButton.SetActive(true);
        // HUB_UIManager.Instance.musicPlayer.GetComponent<MusicPlayer>().audioSource.enabled = false;
    }
    public void RapidFireTutorial()
    {
        tutorailUI.SetActive(false);

        airPistolTut.SetActive(false);
        airRifleTut.SetActive(false);
        rapidRifeTUt.SetActive(true);
        musicPlayer.SetActive(false);
        VideoPlayerController.Instance.BigPlayButton.SetActive(true);
        // HUB_UIManager.Instance.musicPlayer.GetComponent<MusicPlayer>().audioSource.enabled = false;
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
    public void load10mPistolProPractice()
    {

        SceneManager.LoadSceneAsync("10m_Pistol_Pro_Practice");
    }

    public void loadArcadeMode()
    {

        SceneManager.LoadSceneAsync("ProShooterVR_ArcadeMode");
    }

    // Macth  Modes ----------------------
    private void load10mPistolAmatuerMatch()
    {
        LocalUserDataManager.Instance.SelectedGameLevel = GameLevel.amateur;

        SceneManager.LoadSceneAsync("10m_Pistol_Amateur_MatchMode");
    }
    private void load10mPistolSemProMatch()
    {
        LocalUserDataManager.Instance.selectedGameMode = GameModes.AirPistol10m;
        LocalUserDataManager.Instance.SelectedGameLevel = GameLevel.semi_pro;
        SceneManager.LoadSceneAsync("10m_Pistol_SemPro_MatchMode");
    }
    private void load10mPistolProMatch()
    {
        LocalUserDataManager.Instance.selectedGameMode = GameModes.AirPistol10m;
        LocalUserDataManager.Instance.SelectedGameLevel = GameLevel.pro;
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
        LocalUserDataManager.Instance.SelectedGameLevel = GameLevel.amateur;
        SceneManager.LoadSceneAsync("10m_AirRifle_Amateur_Match");
    }
    private void load10mRifleSemProMatch()
    {
        LocalUserDataManager.Instance.selectedGameMode = GameModes.AirRifle10m;
        LocalUserDataManager.Instance.SelectedGameLevel = GameLevel.semi_pro;
        SceneManager.LoadSceneAsync("10m_AirRifle_SemPro_Match");
    }
    private void load10mRifleProMatch()
    {
        LocalUserDataManager.Instance.selectedGameMode = GameModes.AirRifle10m;
        LocalUserDataManager.Instance.SelectedGameLevel = GameLevel.pro;
        SceneManager.LoadSceneAsync("10m_AirRifle_Pro_Match");
    }

    private void load25mRFPro()
    {
        SceneManager.LoadScene("ProShooterVR_25mRF_Pro");
    }
    private void load25mRFSemPro()
    {
        SceneManager.LoadScene("ProShooterVR_25mRF_SemiPro");
    }
    private void load25mRFAmateur()
    {
        SceneManager.LoadScene("ProShooterVR_25mRF_Amateur");
    }


    private void load25mRFProPractice()
    {
        SceneManager.LoadScene("ProShooterVR_25mRF_Pro_Practice");
    }
    private void load25mRFSemProPractice()
    {
        SceneManager.LoadScene("ProShooterVR_25mRF_SemPro_Practice");
    }
    private void load25mRFAmateurPractice()
    {
        SceneManager.LoadScene("ProShooterVR_25mRF_Amateur_Practice");
    }

    //10m Air Rifle Scenes -------------------------------------------------------End

    public void setLevel(int no)
    {
        switch (no)
        {
            case 0:
                LocalUserDataManager.Instance.levelSelected = LocalUserDataManager.gamerLevel.Amateur;
                LocalUserDataManager.Instance.SelectedGameLevel = GameLevel.amateur;
                //LiveUserDataManager.Instance.sortLeaderBoard();

                break;
            case 1:
                LocalUserDataManager.Instance.levelSelected = LocalUserDataManager.gamerLevel.SemiPro;
                LocalUserDataManager.Instance.SelectedGameLevel = GameLevel.semi_pro;
                // LiveUserDataManager.Instance.sortLeaderBoard();

                break;
            case 2:
                LocalUserDataManager.Instance.levelSelected = LocalUserDataManager.gamerLevel.Pro;
                LocalUserDataManager.Instance.SelectedGameLevel = GameLevel.pro;
                //LiveUserDataManager.Instance.sortLeaderBoard();

                break;
        }
    }

    public void setArcade()
    {
        myGameType = gameType.arcade;
    }
    public void setMode(int no)
    {
        switch (no)
        {
            case 0:
                LocalUserDataManager.Instance.modeSelected = LocalUserDataManager.gameMode.AirPistol10M;
                LocalUserDataManager.Instance.selectedGameMode = GameModes.AirPistol10m;
                break;
            case 1:
                LocalUserDataManager.Instance.modeSelected = LocalUserDataManager.gameMode.rapidFire25m;
                break;
            case 2:
                LocalUserDataManager.Instance.modeSelected = LocalUserDataManager.gameMode.airRifle10m;
                LocalUserDataManager.Instance.selectedGameMode = GameModes.AirRifle10m;
                break;
        }

    }







    public void PlayButtonClick()
    {
        if (myGameType == gameType.match)
        {
            loadMatchMode();
        }

        if (myGameType == gameType.practice)
        {
            loadPracticeMode();
        }
        if (myGameType == gameType.arcade)
        {
            loadArcadeMode();
        }
    }



    public void LoadLeaderboardata(int no)
    {
        LeaderboardDifficultyDropDownlist.SetActive(false);
        LeaderboardModeDropDownlist.SetActive(false);
        switch (no)
        {
            case 0:
                if (LocalUserDataManager.Instance.selectedGameMode == GameModes.AirPistol10m)
                {
                    MainLeaderBoardManager.Instance.AirPistolOverallLeaderBoardData(LocalUserDataManager.Instance.metaID);
                }
                if (LocalUserDataManager.Instance.selectedGameMode == GameModes.AirRifle10m)
                {
                    MainLeaderBoardManager.Instance.AirRifleOverallLeaderBoardData(LocalUserDataManager.Instance.metaID);
                }
                break;
            case 1:
                if (LocalUserDataManager.Instance.selectedGameMode == GameModes.AirPistol10m)
                {
                    MainLeaderBoardManager.Instance.AirPistolAmateurLeaderBoardData(LocalUserDataManager.Instance.metaID);
                }
                if (LocalUserDataManager.Instance.selectedGameMode == GameModes.AirRifle10m)
                {
                    MainLeaderBoardManager.Instance.AirRifleAmateurLeaderBoardData(LocalUserDataManager.Instance.metaID);
                }
                break;
            case 2:
                if (LocalUserDataManager.Instance.selectedGameMode == GameModes.AirPistol10m)
                {
                    MainLeaderBoardManager.Instance.AirPistolSemiProLeaderBoardData(LocalUserDataManager.Instance.metaID);
                }
                if (LocalUserDataManager.Instance.selectedGameMode == GameModes.AirRifle10m)
                {
                    MainLeaderBoardManager.Instance.AirRifleSemiproLeaderBoardData(LocalUserDataManager.Instance.metaID);
                }
                break;
            case 3:
                if (LocalUserDataManager.Instance.selectedGameMode == GameModes.AirPistol10m)
                {
                    MainLeaderBoardManager.Instance.AirPistolProLeaderBoardData(LocalUserDataManager.Instance.metaID);
                }
                if (LocalUserDataManager.Instance.selectedGameMode == GameModes.AirRifle10m)
                {
                    MainLeaderBoardManager.Instance.AirRifleProLeaderBoardData(LocalUserDataManager.Instance.metaID);
                }
                break;
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
                        load25mRFAmateurPractice();
                        break;
                    case LocalUserDataManager.gamerLevel.SemiPro:
                        load25mRFSemProPractice();
                        break;
                    case LocalUserDataManager.gamerLevel.Pro:
                        load25mRFProPractice();
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
    public void MainLeaderboardUIFill()
    {
        int count = MainLeaderboardJson["leaderboardResults"].Count;

        for (int i = 0; i < count; i++)
        {
            GameObject NewObj = Instantiate(HUB_UIManager.Instance.MainLeaderBoardRow, HUB_UIManager.Instance.MainLeaderBoardRowParent.transform);
            NewObj.transform.GetChild(2).gameObject.GetComponent<TextMeshPro>().text = MainLeaderboardJson["leaderboardResults"][i]["ranks"];
            NewObj.transform.GetChild(3).gameObject.GetComponent<TextMeshPro>().text = MainLeaderboardJson["leaderboardResults"][i]["meta_quest_username"];
            NewObj.transform.GetChild(4).gameObject.GetComponent<TextMeshPro>().text = MainLeaderboardJson["leaderboardResults"][i]["total_score"];
            NewObj.transform.GetChild(5).gameObject.GetComponent<TextMeshPro>().text = MainLeaderboardJson["leaderboardResults"][i]["matches_played"];

            if (string.Compare(LocalUserDataManager.Instance.metaID, MainLeaderboardJson["leaderboardResults"][i]["meta_unique_id"]) == 0)
            {
                NewObj.transform.GetChild(0).gameObject.SetActive(true);

                //HUB_UIManager.Instance.userPosOnMainLeaderBoardData.transform.GetChild(2).gameObject.GetComponent<TextMeshPro>().text = MainLeaderboardJson["leaderboardResults"][i]["ranks"];
                //HUB_UIManager.Instance.userPosOnMainLeaderBoardData.transform.GetChild(3).gameObject.GetComponent<TextMeshPro>().text = MainLeaderboardJson["leaderboardResults"][i]["meta_quest_username"];
                //HUB_UIManager.Instance.userPosOnMainLeaderBoardData.transform.GetChild(4).gameObject.GetComponent<TextMeshPro>().text = MainLeaderboardJson["leaderboardResults"][i]["total_score"];
                //HUB_UIManager.Instance.userPosOnMainLeaderBoardData.transform.GetChild(5).gameObject.GetComponent<TextMeshPro>().text = MainLeaderboardJson["leaderboardResults"][i]["matches_played"];



                Debug.Log("Fetched!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            }
            Debug.Log(MainLeaderboardJson["leaderboardResults"][i]["total_score"]);
        }
    }

    
    public void ClearMainLeaderboardRows()
    {
        // Destroy existing rows
        Transform parentTransform = HUB_UIManager.Instance.MainLeaderBoardRowParent.transform;
        foreach (Transform child in parentTransform)
        {
            Destroy(child.gameObject);
        }

    }
}

