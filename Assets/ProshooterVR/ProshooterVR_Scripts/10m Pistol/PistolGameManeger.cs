using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using BNG;
using TMPro;

public class PistolGameManeger : MonoBehaviour
{

    public static PistolGameManeger Instance;

    public bool isPracticeMode, isRankedMode;

    public GameObject pistolObj;

    public int noOfShotsFired;

    public float timeRemaining = 15;

    public TextMeshProUGUI timerValue;

    public GameObject[] scorePanels;
    public int sc1ten, sc2ten, sc3ten;
    public float sco1ten, sco2ten, sco3ten;

    public bool isScoreUpdated;

    void Awake()
    {
        Instance = this;
    }



    // Start is called before the first frame update
    void Start()
    {
        if(isPracticeMode == true)
        {
            pistolObj.GetComponent<RaycastWeapon>().ReloadMethod = ReloadType.InfiniteAmmo;
            timerValue.gameObject.SetActive(false);
            UIManager.Instance.totalScoreTxt.gameObject.SetActive(false);
        }

        if (isRankedMode == true)
        {
            //noOfShotsFired = 0 ;
            pistolObj.GetComponent<RaycastWeapon>().ReloadMethod = ReloadType.InfiniteAmmo;
            //pistolObj.GetComponent<RaycastWeapon>().InternalAmmo = 30;

           // scorePanels = new GameObject[3];
            noOfShotsFired = 0;
            
        }
        pistolObj.GetComponent<RaycastWeapon>().ReloadMethod = ReloadType.InfiniteAmmo;
        noOfShotsFired = 0;

        sc1ten = sc2ten = sc3ten = 0;
        sco1ten = sco2ten = sco3ten = 0f;
        isScoreUpdated = true;

    }

    // Update is called once per frame
    void Update()
    {
        if (isRankedMode == true)
        {
            if (timeRemaining > 0 && noOfShotsFired < 30)
            {
                timeRemaining -= Time.deltaTime;

                float minutes = Mathf.FloorToInt(timeRemaining / 60);
                float seconds = Mathf.FloorToInt(timeRemaining % 60);
                timerValue.text = "Timer :" + string.Format("{0:00}:{1:00}", minutes, seconds);

            }
            else
            {
                
                Debug.Log("Time has run out!");
            }
        }

    }

    public void shotFired()
    {
       
        if (isRankedMode == true)
        {
           


                if (noOfShotsFired <= 9)
                {
                    scorePanels[0].SetActive(true);
                    scorePanels[1].SetActive(false);
                    scorePanels[2].SetActive(false);


                    scorePanels[0].gameObject.transform.GetChild(noOfShotsFired).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = (noOfShotsFired + 1).ToString();
                    scorePanels[0].gameObject.transform.GetChild(noOfShotsFired).GetChild(3).gameObject.SetActive(true);
                    scorePanels[0].gameObject.transform.GetChild(noOfShotsFired).GetChild(3).gameObject.transform.Rotate(0, 180, UIManager.Instance.angle);
                    scorePanels[0].gameObject.transform.GetChild(noOfShotsFired).GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = UIManager.Instance.shotRoundScore.ToString();
                    scorePanels[0].gameObject.transform.GetChild(noOfShotsFired).GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text = UIManager.Instance.shotScore.ToString();

                    sc1ten = sc1ten + UIManager.Instance.shotRoundScore;
                    sco1ten = sco1ten + UIManager.Instance.shotScore;
                    scorePanels[0].gameObject.transform.GetChild(10).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = sc1ten.ToString();
                    scorePanels[0].gameObject.transform.GetChild(10).GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = sco1ten.ToString();
                    UIManager.Instance.Totalscore = UIManager.Instance.Totalscore + UIManager.Instance.shotRoundScore;
                 
                }
                else if (noOfShotsFired > 9 && noOfShotsFired <= 19)
                {
                    scorePanels[1].SetActive(true);
                    scorePanels[0].SetActive(false);
                    scorePanels[2].SetActive(false);

                    scorePanels[1].gameObject.transform.GetChild(noOfShotsFired - 10).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = (noOfShotsFired + 1).ToString();
                    scorePanels[1].gameObject.transform.GetChild(noOfShotsFired - 10).GetChild(3).gameObject.SetActive(true);
                    scorePanels[1].gameObject.transform.GetChild(noOfShotsFired - 10).GetChild(3).gameObject.transform.Rotate(0, 180, UIManager.Instance.angle);
                    scorePanels[1].gameObject.transform.GetChild(noOfShotsFired - 10).GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = UIManager.Instance.shotRoundScore.ToString();
                    scorePanels[1].gameObject.transform.GetChild(noOfShotsFired - 10).GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text = UIManager.Instance.shotScore.ToString();


                    sc2ten = sc2ten + UIManager.Instance.shotRoundScore;
                    sco2ten = sco2ten + UIManager.Instance.shotScore;
                    scorePanels[1].gameObject.transform.GetChild(10).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = sc2ten.ToString();
                    scorePanels[1].gameObject.transform.GetChild(10).GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = sco2ten.ToString();

                    UIManager.Instance.Totalscore = UIManager.Instance.Totalscore + UIManager.Instance.shotRoundScore;
                   
                }
                else if (noOfShotsFired > 19 && noOfShotsFired <= 29)
                {
                    scorePanels[2].SetActive(true);
                    scorePanels[1].SetActive(false);
                    scorePanels[0].SetActive(false);

                    scorePanels[2].gameObject.transform.GetChild(noOfShotsFired - 20).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = (noOfShotsFired + 1).ToString();
                    scorePanels[2].gameObject.transform.GetChild(noOfShotsFired - 20).GetChild(3).gameObject.SetActive(true);
                    scorePanels[2].gameObject.transform.GetChild(noOfShotsFired - 20).GetChild(3).gameObject.transform.Rotate(0, 180, UIManager.Instance.angle);
                    scorePanels[2].gameObject.transform.GetChild(noOfShotsFired - 20).GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = UIManager.Instance.shotRoundScore.ToString();
                    scorePanels[2].gameObject.transform.GetChild(noOfShotsFired - 20).GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text = UIManager.Instance.shotScore.ToString();

                    sc3ten = sc3ten + UIManager.Instance.shotRoundScore;
                    sco3ten = sco3ten + UIManager.Instance.shotScore;
                    scorePanels[2].gameObject.transform.GetChild(10).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = sc3ten.ToString();
                    scorePanels[2].gameObject.transform.GetChild(10).GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = sco3ten.ToString();
                    UIManager.Instance.Totalscore = UIManager.Instance.Totalscore + UIManager.Instance.shotRoundScore;

            }
            else
            {

            }
          
            UIManager.Instance.totalScoreTxt.text = "Total Score : " + UIManager.Instance.Totalscore.ToString();
            noOfShotsFired++;

        }

        if (isPracticeMode == true)
        {
            if (noOfShotsFired <= 9)
            {
                    scorePanels[0].SetActive(true);
                    scorePanels[1].SetActive(false);
                    scorePanels[2].SetActive(false);

                    scorePanels[0].gameObject.transform.GetChild(noOfShotsFired).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = (noOfShotsFired + 1).ToString();
                    scorePanels[0].gameObject.transform.GetChild(noOfShotsFired).GetChild(3).gameObject.SetActive(true);
                    scorePanels[0].gameObject.transform.GetChild(noOfShotsFired).GetChild(3).gameObject.transform.Rotate(0, 180, UIManager.Instance.angle);
                    scorePanels[0].gameObject.transform.GetChild(noOfShotsFired).GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = UIManager.Instance.shotRoundScore.ToString();
                    scorePanels[0].gameObject.transform.GetChild(noOfShotsFired).GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text = UIManager.Instance.shotScore.ToString();

                    sc1ten = sc1ten + UIManager.Instance.shotRoundScore;
                    sco1ten = sco1ten + UIManager.Instance.shotScore;
                    scorePanels[0].gameObject.transform.GetChild(10).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = sc1ten.ToString();
                    scorePanels[0].gameObject.transform.GetChild(10).GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = sco1ten.ToString();
                    noOfShotsFired++;
            }
            else
            {
                noOfShotsFired = 0;
                for (int i = 0; i < 10; i++)
                {
                    scorePanels[0].gameObject.transform.GetChild(i).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = ""; ;
                    scorePanels[0].gameObject.transform.GetChild(i).GetChild(3).gameObject.SetActive(false);
                    scorePanels[0].gameObject.transform.GetChild(i).GetChild(3).gameObject.transform.Rotate(0, 180, 0);
                    scorePanels[0].gameObject.transform.GetChild(i).GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = "";
                    scorePanels[0].gameObject.transform.GetChild(i).GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text = "";
                    scorePanels[0].gameObject.transform.GetChild(10).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "";
                    scorePanels[0].gameObject.transform.GetChild(10).GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = "";

                    sc1ten = 0;
                    sco1ten = 0;
                }

                shotFired();
            }
        }

    }
}
