using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class pistolPopUPUIManager : MonoBehaviour
{
    public static pistolPopUPUIManager Instance;
    private void Awake()
    {
        Instance = this;
    }

    public TextMeshPro userNameTxt;
    public TextMeshPro srs1ScoreTxt, srs2ScoreTxt, srs3ScoreTxt,gameTotalScoreTxt;
    public TextMeshPro avgScoreTxt, shotsHitTxt, shotsmissTxt, innerTText, timeSpentTxt, pBestScoreTxt;

    public GameObject screen, screenCenter, screenEnd;
    public GameObject resetShotPos, resetParent;
    public GameObject ScreenobjectToPlace;

    public GameObject upperLeft, lowerRight;


    public List<GameObject> screenScores;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateShotEndScreen(Vector3 pos, float scoreVal, float direction)
    {
        GameObject placedObject = Instantiate(ScreenobjectToPlace, screenCenter.transform.position, Quaternion.identity, screen.transform);

        Vector3 scle = screenCenter.transform.localScale;

        placedObject.transform.localScale = screenCenter.transform.localScale;

        placedObject.transform.localPosition = pos;

        float angle = direction - 180;
        Debug.Log(" Aft :: " + angle);

        placedObject.transform.parent = resetParent.transform;
        placedObject.transform.localScale = resetShotPos.transform.localScale;
        placedObject.transform.localRotation = resetShotPos.transform.localRotation;

        placedObject.transform.localPosition = new Vector3(placedObject.transform.localPosition.x, placedObject.transform.localPosition.y, resetShotPos.transform.localPosition.z);

        if (weaponManager.Instance.isRifleMode == true)
        {
            if ((placedObject.transform.localPosition.x + placedObject.transform.localScale.x / 2) < upperLeft.transform.localPosition.x && (placedObject.transform.localPosition.y + placedObject.transform.localScale.y / 2) > upperLeft.transform.localPosition.y && (placedObject.transform.localPosition.x) > lowerRight.transform.localPosition.x + placedObject.transform.localScale.x / 2 && (placedObject.transform.localPosition.y) < lowerRight.transform.localPosition.y + placedObject.transform.localScale.y / 2)
            {
                placedObject.SetActive(true);
            }
            else
            {
                placedObject.SetActive(false);

                // Debug.Log("" + placedObject.transform.localPosition.x + placedObject.transform.localScale.x / 2 + " :: " + upperLeft.transform.localPosition.x);
            }
        }
        if (weaponManager.Instance.isPistolMode == true || weaponManager.Instance.isRifleMode == true)
        {
            if (scoreVal == 0)
            { 
                placedObject.SetActive(false);
            }
            screenScores.Add(placedObject);
            placedObject.SetActive(false);
        }


    }

    public void enableTargetScores()
    {
        for(int i = 0; i < screenScores.Count; i++)
        {
            screenScores[i].SetActive(true);
        }
    }

    public void disableTargetScores()
    {
        for (int i = 0; i < screenScores.Count; i++)
        {
            screenScores[i].SetActive(false);
        }
    }
    public void clearTargetScores()
    {
        disableTargetScores();
       // screenScores.Clear();
    }
}
