using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    

    public GameObject shotParent;

    public float angle;
    public float shotScore;
    public int shotRoundScore;
    public string finalScore;
    public float totalTScore, Totalscore;

    public TextMeshProUGUI totalScoreTxt;


    public GameObject ScreenobjectToPlace;


    public GameObject resetShotPos,resetParent;


    /// <summary>
    /// ----- Screen calc
    /// </summary>

    public GameObject screen, screenCenter, screenEnd;

    float targetscoreOff, screenscoreOff;


    private void Awake()
    {
        Instance = this;
        
    }
    // Start is called before the first frame update
    void Start()
    {
        screenscoreOff = Vector3.Distance(screenCenter.transform.localPosition, screenEnd.transform.localPosition) / 100;
    }

    // Update is called once per frame
    void Update()
    {
        ///float distance = Vector3.Distance(targetCenter.transform.position, end.transform.position);
        
       // float score = (float)((((distance * 10000) / 95.9) - 10.9f)-4.2f);

       // Debug.Log("Score : " +score + " | Dist :"+ (((score + 4.2f) + 10.9) * 95.9) / 10000);
    
    }

    public void backBtnClick()
    {
        SceneManager.LoadSceneAsync("ProShooterVR_Hub");
    }

    public void updateShotScreen(Vector3 pos, float scoreVal, float direction)
    {
        if(scoreVal < 0)
        {
            shotScore = 0;
            shotRoundScore = 0;
        }
        else {
           // shotScore = Mathf.Round(scoreVal * 10f) / 10f;
            shotScore = Mathf.Round(scoreVal * 100f) / 100f; ;

            shotRoundScore = (int)shotScore;
            Debug.Log("shotScore : " + shotScore + "shotRoundScore : " + shotRoundScore);

            if (shotScore < 10.4f)
            {
                finalScore = shotRoundScore.ToString();
                
            }

            if(shotScore >= 10.4f)
            {
                finalScore = "10x";
                shotRoundScore = 10;
            }
            Debug.Log("Final score : "+finalScore);
        }
        


        GameObject placedObject = Instantiate(ScreenobjectToPlace, screenCenter.transform.position, Quaternion.identity, screen.transform);
        placedObject.transform.localScale = screenCenter.transform.localScale;
        placedObject.transform.localPosition = pos;

       

        angle = direction - 180;
        Debug.Log(" Aft :: " + angle);

        placedObject.transform.parent = resetParent.transform;
        placedObject.transform.rotation = resetShotPos.transform.rotation;
        placedObject.transform.localPosition = new Vector3(placedObject.transform.localPosition.x, placedObject.transform.localPosition.y, resetShotPos.transform.localPosition.z);


    }
}
