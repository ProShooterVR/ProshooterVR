using BNG;
using NovaSamples.Inventory;
using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArcadeGameUIManager : MonoBehaviour
{
    public static ArcadeGameUIManager Instance;
    public bool setSwitch;

   public JSONNode leaderboardJson;
    private void Awake()
    {
        Instance = this;
    }

    public GameObject scorePanel,mainMenuPanel, endSessionScreen;
    public GameObject leaderBoardPanel,endGamePopUP;
    public TextMeshPro userName,totalScoreText;

    public GameObject leaderboardRow, leaderboardRowParent;
    public GameObject userPosOnLeaderBoardData;

    public GameObject startBtn, restartBtn;

    public GameObject envSound;

    // Start is called before the first frame update
    void Start()
    {
        scorePanel.SetActive(false);
        leaderBoardPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
        endGamePopUP.SetActive(false);
        ArcadeGameManager.instance.isReloaded = false;
        userName.text = LocalUserDataManager.Instance.meta_username;
        envSound.SetActive(true);
        startBtn.SetActive(true);
        restartBtn.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (InputBridge.Instance.StartButtonDown == true || InputBridge.Instance.BackButton == true || Input.GetKeyDown("space"))
        {
            if (setSwitch == false)
            {
                mainMenuPanel.SetActive(true);
                RayManager.Instance.EnableRey();
                endSessionScreen.SetActive(false);
                setSwitch = true;
                startBtn.SetActive(false);
                restartBtn.SetActive(true);
            }

        }
    }

    public void startBtnClick()
    {
        scorePanel.SetActive(true);
        ArcadeGameManager.instance.isReloaded = true;
        ArcadeGameManager.instance.initCLipObj.SetActive(true);
        ArcadeGameManager.instance.gunObj.SetActive(true);
        ArcadeGameManager.instance.targetDeployer.SetActive(true);
        ArcadeGameManager.instance.initCLipObj.SetActive(true);
        startBtn.SetActive(false);
        restartBtn.SetActive(true);

        mainMenuPanel.SetActive(false);
        RayManager.Instance.DisableRey();



    }

    public void restartBtnClick()
    {
        SceneManager.LoadSceneAsync("ProShooterVR_ArcadeMode");

    }
    public void backBtnClick()
    {
        SceneManager.LoadSceneAsync("ProShooterVR_Hub");
    }

    public void LeaderboardBtnClick()
    {
        leaderBoardPanel.SetActive(true);
        endGamePopUP.SetActive(false);
        updateLeaderboardUI();
    }

    public void updateLeaderboardUI()
    {
        int count = leaderboardJson["leaderboardResults"].Count;

        for (int i = 0; i < count; i++)
        {
            GameObject NewObj = Instantiate(ArcadeGameUIManager.Instance.leaderboardRow, ArcadeGameUIManager.Instance.leaderboardRowParent.transform);
            NewObj.transform.GetChild(2).gameObject.GetComponent<TextMeshPro>().text = leaderboardJson["leaderboardResults"][i]["ranks"];
            NewObj.transform.GetChild(3).gameObject.GetComponent<TextMeshPro>().text = leaderboardJson["leaderboardResults"][i]["meta_quest_username"];
            NewObj.transform.GetChild(4).gameObject.GetComponent<TextMeshPro>().text = leaderboardJson["leaderboardResults"][i]["total_game_score"];

            if (string.Compare(LocalUserDataManager.Instance.metaID, leaderboardJson["leaderboardResults"][i]["meta_unique_id"]) == 0)
            {
                NewObj.transform.GetChild(0).gameObject.SetActive(true);

                ArcadeGameUIManager.Instance.userPosOnLeaderBoardData.transform.GetChild(2).gameObject.GetComponent<TextMeshPro>().text = leaderboardJson["leaderboardResults"][i]["ranks"];
                ArcadeGameUIManager.Instance.userPosOnLeaderBoardData.transform.GetChild(3).gameObject.GetComponent<TextMeshPro>().text = leaderboardJson["leaderboardResults"][i]["meta_quest_username"];
                ArcadeGameUIManager.Instance.userPosOnLeaderBoardData.transform.GetChild(4).gameObject.GetComponent<TextMeshPro>().text = leaderboardJson["leaderboardResults"][i]["total_game_score"];


            }


            Debug.Log(leaderboardJson["leaderboardResults"][i]["total_game_score"]);
        }

    }
}
