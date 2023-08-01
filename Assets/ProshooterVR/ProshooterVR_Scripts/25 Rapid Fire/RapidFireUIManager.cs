using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RapidFireUIManager : MonoBehaviour
{

    public GameObject shotParent;

    public float angle;
    public float shotScore;
    public int shotRoundScore;
    public float shotRoundScoreRifle;
    public string finalScore;
    
    public GameObject ScreenobjectToPlace;


    GameObject resetShotPos, resetParent, rifleOffObj;
   

    GameObject screenCenter, screenEnd;

    public GameObject[] screens;
    public int screenNo;
    public TextMeshProUGUI sr1Score, sr2Score, sr3Score; // Reference to the UI Text component
    public GameObject[] scoresDisp;
    public string[] instructions;
    public TextMeshProUGUI instructionTxt;


    public static RapidFireUIManager Instance;
    private void Awake()
    {
        Instance = this;

    }


    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void clearScreen()
    {
        for (int i = 0; i < 5; i++)
        {
            screens[i].transform.GetChild(3).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
        }
    }

    public void foulSeries()
    {
        for (int i = 0; i < 5; i++)
        {
            screens[i].transform.GetChild(3).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "0";
            scoresDisp[RapidFireGunManager.Instance.SeriesCounter].transform.GetChild(i).GetComponent<TextMeshProUGUI>().text = shotScore.ToString();

        }
    }
    public void updateShotScreen(Vector3 pos, float scoreVal, float direction,int ShotScreenNo)
    {

        if (RapidFireGunManager.Instance.countingScore == true)
        {
            screenNo = ShotScreenNo;
            screenCenter = screens[screenNo].transform.GetChild(0).transform.gameObject;
            screenEnd = screens[screenNo].transform.GetChild(1).transform.gameObject;
            resetShotPos = screens[screenNo].transform.GetChild(2).transform.gameObject;
            resetParent = screens[screenNo];


            if (scoreVal < 5)
            {
                shotScore = 0;
                shotRoundScore = 0;
                finalScore = "0";
                Debug.Log("Score to record" + shotScore);
            }
            else
            {

                Debug.Log("Score to record" + scoreVal);

                shotScore = Mathf.Round(scoreVal * 100f) / 100f;

                shotScore = Mathf.Floor(shotScore);


                Debug.Log("Final score : " + shotScore);
                screens[screenNo].transform.GetChild(3).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = shotScore.ToString();
            }

            GameObject placedObject = Instantiate(ScreenobjectToPlace, screenCenter.transform.position, Quaternion.identity, screens[screenNo].transform.GetChild(0).transform);

            Vector3 scle = screenCenter.transform.localScale;
            Debug.Log(scle);


            placedObject.transform.localScale = screenCenter.transform.localScale;
            placedObject.transform.localPosition = pos;



            angle = direction - 180;
            //  Debug.Log(" Aft :: " + angle);

            placedObject.transform.parent = resetParent.transform;
            placedObject.transform.localScale = resetShotPos.transform.localScale;
            placedObject.transform.localRotation = resetShotPos.transform.localRotation;

            //placedObject.transform.localPosition = new Vector3(placedObject.transform.localPosition.x, placedObject.transform.localPosition.y, placedObject.transform.localPosition.z);
        }
        else
        {
            shotScore = 0;
        }

        if(RapidFireGunManager.Instance.seriesFoul == true)
        {
            foulSeries();
        }

        if(RapidFireGunManager.Instance.SeriesCounter == 1)
        {
            scoresDisp[0].transform.GetChild(screenNo).GetComponent<TextMeshProUGUI>().text = shotScore.ToString();
        }
        else if (RapidFireGunManager.Instance.SeriesCounter == 2)
        {
            scoresDisp[1].transform.GetChild(screenNo).GetComponent<TextMeshProUGUI>().text = shotScore.ToString();
        }
        else if (RapidFireGunManager.Instance.SeriesCounter == 3)
        {
            scoresDisp[2].transform.GetChild(screenNo).GetComponent<TextMeshProUGUI>().text = shotScore.ToString();
        }


        //screenScores.Add(placedObject);

    }
}
