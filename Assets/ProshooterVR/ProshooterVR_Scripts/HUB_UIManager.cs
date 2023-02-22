using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HUB_UIManager : MonoBehaviour
{
    //UI Manger responsible for HUB UI calls

    //Making it static to access all over layers
    public static HUB_UIManager Instance;

    void Awake()
    {
        Instance = this;
    }

    // Get panles
    [SerializeField]
    GameObject mainMenu_UIPanel, PlayMode_UIPanel,startPanel;

    private void Start()
    {
        mainMenu_UIPanel.SetActive(true);
        PlayMode_UIPanel.SetActive(false);
        startPanel.SetActive(true);
    }

    public void PlayButtonClick()
    {

        mainMenu_UIPanel.SetActive(false);
        PlayMode_UIPanel.SetActive(true);
        startPanel.SetActive(false);

    }

    public void BakcToMainClick()
    {
        mainMenu_UIPanel.SetActive(true);
        PlayMode_UIPanel.SetActive(false);
        startPanel.SetActive(false);
    }

    public void StartPanelClick()
    {
        mainMenu_UIPanel.SetActive(true);
        PlayMode_UIPanel.SetActive(false);
        startPanel.SetActive(false);
    }


    public void Load_10mRangePractice_pro()
    {
        SceneManager.LoadSceneAsync("ProShooterVR_10m_Pistol_Practice_pro");
    }
    public void Load_10mRangePractice_semPro()
    {
        SceneManager.LoadSceneAsync("ProShooterVR_10m_Pistol_Practice_Sempro");
    }
    public void Load_10mRangePractice_Ama()
    {
        SceneManager.LoadSceneAsync("ProShooterVR_10m_Pistol_Practice_Amateur");
    }
    public void Load_10mRangeRanked()
    {
        SceneManager.LoadSceneAsync("ProShooterVR_10m_Pistol_ranked");
    }
    public void Load_10mRangeRifle_pro()
    {
        SceneManager.LoadSceneAsync("ProShooterVR_10m_Rifle_Practice_Pro");
    }
    public void Load_10mRangeRifle_sempro()
    {
        SceneManager.LoadSceneAsync("ProShooterVR_10m_Rifle_Practice_SemPro");
    }
    public void Load_10mRangeRifle_Ama()
    {
        SceneManager.LoadSceneAsync("ProShooterVR_10m_Rifle_Practice_Amateur");
    }
}
