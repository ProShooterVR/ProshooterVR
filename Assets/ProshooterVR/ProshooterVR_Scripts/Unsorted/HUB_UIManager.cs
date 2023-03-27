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
    [SerializeField]
    private GameObject gameModePopup;

    [SerializeField]
    public TextMeshProUGUI title;

    [SerializeField]
    public GameObject[] Leaders;
    public GameObject LeaderN;
    public Sprite leaderH;
   

    void Awake()
    {
        Instance = this;
    }

    // Get panles
   

    private void Start()
    {
        
        gameModePopup.SetActive(false);
        LocalUserDataManager.Instance.levelSelected = LocalUserDataManager.gamerLevel.Amateur;
        LocalUserDataManager.Instance.modeSelected = LocalUserDataManager.gameMode.AirPistol10M;
        Debug.Log("" + DateTime.Now.ToString());
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

        SceneManager.LoadSceneAsync("10m_Rifle_Amateur_Practice");
    }
    private void load10mRifleSemProPractice()
    {
        SceneManager.LoadSceneAsync("10m_Rifle_SemPro_Practice");
    }
    private void load10mRifleProPractice()
    {
        SceneManager.LoadSceneAsync("10m_Rifle_Pro_Practice");
    }
    // Macth  Modes ----------------------
    private void load10mRifleAmatuerMatch()
    {
        LocalUserDataManager.Instance.selectedGameMode = GameModes.AirRifle10m;
        LocalUserDataManager.Instance.SelectedGameLevel = GameLevel.Amateur;
        SceneManager.LoadSceneAsync("10m_Rifle_Amateur_MatchMode");
    }
    private void load10mRifleSemProMatch()
    {
        LocalUserDataManager.Instance.selectedGameMode = GameModes.AirRifle10m;
        LocalUserDataManager.Instance.SelectedGameLevel = GameLevel.SemiPro;
        SceneManager.LoadSceneAsync("10m_Rifle_SemPro_MatchMode");
    }
    private void load10mRifleProMatch()
    {
        LocalUserDataManager.Instance.selectedGameMode = GameModes.AirRifle10m;
        LocalUserDataManager.Instance.SelectedGameLevel = GameLevel.Pro;
        SceneManager.LoadSceneAsync("10m_Rifle_Pro_MatchMode");
    }


    //10m Air Rifle Scenes -------------------------------------------------------End

    public void setLevel(int no)
    {
        switch (no)
        {
            case 0:
                LocalUserDataManager.Instance.levelSelected = LocalUserDataManager.gamerLevel.Amateur;
                LocalUserDataManager.Instance.SelectedGameLevel = GameLevel.Amateur;
                LiveUserDataManager.Instance.sortLeaderBoard();

                break;
            case 1:
                LocalUserDataManager.Instance.levelSelected = LocalUserDataManager.gamerLevel.SemiPro;
                LocalUserDataManager.Instance.SelectedGameLevel = GameLevel.SemiPro;
                LiveUserDataManager.Instance.sortLeaderBoard();

                break;
            case 2:
                LocalUserDataManager.Instance.levelSelected = LocalUserDataManager.gamerLevel.Pro;
                LocalUserDataManager.Instance.SelectedGameLevel = GameLevel.Pro;
                LiveUserDataManager.Instance.sortLeaderBoard();

                break;
        }
    }
    public void setMode(int no)
    {
        switch (no)
        {
            case 0:
                LocalUserDataManager.Instance.modeSelected = LocalUserDataManager.gameMode.AirPistol10M;
                LocalUserDataManager.Instance.selectedGameMode = GameModes.AirPistol10m;
                LiveUserDataManager.Instance.sortLeaderBoard();
                break;
            case 1:
                LocalUserDataManager.Instance.modeSelected = LocalUserDataManager.gameMode.rapidFire25m;
                LiveUserDataManager.Instance.sortLeaderBoard();
                break;
            case 2:
                LocalUserDataManager.Instance.modeSelected = LocalUserDataManager.gameMode.airRifle10m;
                LiveUserDataManager.Instance.sortLeaderBoard();
                break;
        }
       
    }

    public void PlayButtonClick()
    {
        gameModePopup.SetActive(true);
    }

    public void gameModePopupCloseButton()
    {
        gameModePopup.SetActive(false);
    }

    public void loadPracticeMode()
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
                        break;
                    case LocalUserDataManager.gamerLevel.SemiPro:
                        break;
                    case LocalUserDataManager.gamerLevel.Pro:
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
    public void loadMatchMode()
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
                        break;
                    case LocalUserDataManager.gamerLevel.SemiPro:
                        break;
                    case LocalUserDataManager.gamerLevel.Pro:
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
        LiveUserDataManager.Instance.sortLeaderBoard();
        

        
    }

}

