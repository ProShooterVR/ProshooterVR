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
    GameObject mainMenu_UIPanel, PlayMode_UIPanel;

    private void Start()
    {
        mainMenu_UIPanel.SetActive(true);
        PlayMode_UIPanel.SetActive(false);
    }

    public void PlayButtonClick()
    {

        mainMenu_UIPanel.SetActive(false);
        PlayMode_UIPanel.SetActive(true);

    }

    public void BakcToMainClick()
    {
        mainMenu_UIPanel.SetActive(true);
        PlayMode_UIPanel.SetActive(false);
    }

    public void Load_10mRange()
    {
        SceneManager.LoadSceneAsync("ProShooterVR_10m_v2C");
    }
}
