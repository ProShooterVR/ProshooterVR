using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RapidFireEndSessionManager : MonoBehaviour
{
    public static RapidFireEndSessionManager Instance;
    private void Awake()
    {
        Instance = this;
    }

    public TextMeshPro userNameTxt;
    public TextMeshPro srs1ScoreTxt, srs2ScoreTxt, srs3ScoreTxt, round1ScoreTxt;
    public TextMeshPro srs4ScoreTxt, srs5ScoreTxt, srs6ScoreTxt, round2ScoreTxt;
    public TextMeshPro totalgameScoretxt;
    public TextMeshPro avgScoreTxt, shotsHitTxt, shotsmissTxt, innerTText, timeSpentTxt, pBestScoreTxt;

     GameObject screenCenter, screenEnd;
     GameObject resetShotPos, resetParent;
    public GameObject ScreenobjectToPlace;


    public GameObject[] screens, screentxt;
    public int screenNo;
    public List<GameObject> screenScores;

    public float angle;
    public float shotScore;
    public int shotRoundScore;
    public float shotRoundScoreRifle;
    public string finalScore;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void updateEndSessionShotEndScreen(Vector3 pos, float scoreVal, float direction, int ShotScreenNo)
    {
        if (RapidFireGunManager.Instance.countingScore == true)
        {
            screenNo = ShotScreenNo;
            screenCenter = screens[screenNo].transform.GetChild(0).transform.GetChild(0).transform.gameObject;
            screenEnd = screens[screenNo].transform.GetChild(0).transform.GetChild(1).transform.gameObject;
            resetShotPos = screens[screenNo].transform.GetChild(0).transform.GetChild(2).transform.gameObject;
            resetParent = screens[screenNo].transform.GetChild(0).gameObject;


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

            placedObject.transform.localPosition = new Vector3(-placedObject.transform.localPosition.x / 100, placedObject.transform.localPosition.y / 100, placedObject.transform.localPosition.z);
            screenScores.Add(placedObject);
            placedObject.SetActive(false);
        }
        else
        {
            shotScore = 0;
        }



     }

    public void enableTargetScores()
    {
        for (int i = 0; i < screenScores.Count; i++)
        {
            screenScores[i].SetActive(true);
        }
    }

    public void disableTargetScores()
    {
        for (int i = 0; i < screenScores.Count; i++)
        {
            screenScores[i].SetActive(true);
        }
    }
}
