using UnityEngine;
using BNG;
using UnityEngine.InputSystem.XR;
using Nova;

public class NOVAUIInteraction : MonoBehaviour
{
    public GameObject MainUIPanel, MainUIButton, MainUIBorder, airpistolUIPanel, airpistolUIButton, airpistolUIBorder, rapidfireUIPanel, rapidfireUIButton, rapidfireUIBorder, SessionEndUIPanel, SessionEndUIButton, SessionEndUIBorder;

    public void Start()
    {
        MainUIPanel.SetActive(false);
        MainUIButton.SetActive(true);
        MainUIBorder.SetActive(false);
        airpistolUIPanel.SetActive(false);
        airpistolUIButton.SetActive(true);
        airpistolUIBorder.SetActive(false);
        rapidfireUIPanel.SetActive(false);
        rapidfireUIButton.SetActive(true);
        rapidfireUIBorder.SetActive(false);
        SessionEndUIPanel.SetActive(false);
        SessionEndUIButton.SetActive(true);
        SessionEndUIBorder.SetActive(false);
    }

    public void onMainMenuUIbuttonClick()
    {
        MainUIPanel.SetActive(true);
        MainUIButton.SetActive(false);
        MainUIBorder.SetActive(true);
    }

    public void onMainMenuBorderUIbuttonClick()
    {
        MainUIPanel.SetActive(false);
        MainUIButton.SetActive(true);
        MainUIBorder.SetActive(false);
    }

    public void onairpistolUIbuttonClick()
    {
        airpistolUIPanel.SetActive(true);
        airpistolUIButton.SetActive(false);
        airpistolUIBorder.SetActive(true);

        if(rapidfireUIPanel == true)
        {
            rapidfireUIPanel.SetActive(false);
            rapidfireUIButton.SetActive(true);
            rapidfireUIBorder.SetActive(false);
        }

        if(SessionEndUIPanel == true)
        {
            SessionEndUIPanel.SetActive(false);
            SessionEndUIButton.SetActive(true);
            SessionEndUIBorder.SetActive(false);
        }
    }

    public void onairpistolborderUIbuttonClick()
    {
        airpistolUIPanel.SetActive(false);
        airpistolUIButton.SetActive(true);
        airpistolUIBorder.SetActive(false);
    }

    public void onrapidfireUIbuttonClick()
    {
        rapidfireUIPanel.SetActive(true);
        rapidfireUIButton.SetActive(false);
        rapidfireUIBorder.SetActive(true);
        
        if(airpistolUIPanel == true)
        {
            airpistolUIPanel.SetActive(false);
            airpistolUIButton.SetActive(true);
            airpistolUIBorder.SetActive(false);
        }
        if (SessionEndUIPanel == true)
        {
            SessionEndUIPanel.SetActive(false);
            SessionEndUIButton.SetActive(true);
            SessionEndUIBorder.SetActive(false);
        }
    }

    public void onrapidfireborderUIbuttonClick()
    {
        rapidfireUIPanel.SetActive(false);
        rapidfireUIButton.SetActive(true);
        rapidfireUIBorder.SetActive(false);
    }

    public void onSessionEndUIbuttonClick()
    {
        SessionEndUIPanel.SetActive(true);
        SessionEndUIButton.SetActive(false);
        SessionEndUIBorder.SetActive(true);

        if (airpistolUIPanel == true)
        {
            airpistolUIPanel.SetActive(false);
            airpistolUIButton.SetActive(true);
            airpistolUIBorder.SetActive(false);
        }

        if (rapidfireUIPanel == true)
        {
            rapidfireUIPanel.SetActive(false);
            rapidfireUIButton.SetActive(true);
            rapidfireUIBorder.SetActive(false);
        }
    }

    public void onSessionEndborderUIbuttonClick()
    {
        SessionEndUIPanel.SetActive(false);
        SessionEndUIButton.SetActive(true);
        SessionEndUIBorder.SetActive(false);
    }
}
