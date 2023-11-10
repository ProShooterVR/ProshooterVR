using UnityEngine;
using TMPro;
using System.Collections;
using BNG;

public class ArcadeGameManager : MonoBehaviour
{
    public static ArcadeGameManager instance;
    public int totalscore;
    public TextMeshPro TotalScoreText;

    public int hitCounter;

    public bool Hit, isReloaded;


    public GameObject x2MultiplierEffect;
    public GameObject x3MultiplierEffect;
    public GameObject x4MultiplierEffect;
    public GameObject x5MultiplierEffect;

    public bool isClipSpawn;
    public GameObject prefabToInstantiate;
    public GameObject spawnClipOrg;

    public GameObject gunObj;
    public GameObject initCLipObj;
    public GameObject targetDeployer;

    public void Start()
    {
        Hit = false;
        isClipSpawn = false;
        hitCounter = 0;
        targetDeployer.SetActive(false);
        gunObj.SetActive(false);
        initCLipObj.SetActive(false);


        x2MultiplierEffect.SetActive(false);
        x3MultiplierEffect.SetActive(false);
        x4MultiplierEffect.SetActive(false);
        x5MultiplierEffect.SetActive(false);
    }
    private void Awake()
    {
        instance = this;
    }
    private void Update()
    {
        if(InputBridge.Instance.BButton == true || Input.GetKeyDown(KeyCode.B)==true)
        {
           Debug.Log("create nee clip");
        }
        
    }

    public void updatescore(int currentScoreValue)
    {
        if(Hit == true)
        {
            if(hitCounter < 5)
            {
                hitCounter++;

                StartCoroutine(playAnim());
            }
            if(hitCounter == 5)
            {
                StartCoroutine(playAnim());
            }
        }

        totalscore = totalscore + (currentScoreValue)*hitCounter;

        TotalScoreText.text = "" + totalscore;
        Debug.Log(totalscore);
    }

    IEnumerator playAnim()
    {
        if (hitCounter == 2)
        {
            x2MultiplierEffect.SetActive(true);
            yield return new WaitForSeconds(1f); 
            x2MultiplierEffect.SetActive(false);

        }
        if (hitCounter == 3)
        {
            x3MultiplierEffect.SetActive(true);
            yield return new WaitForSeconds(1f);
            x3MultiplierEffect.SetActive(false);

        }
        if (hitCounter == 4)
        {
            x4MultiplierEffect.SetActive(true);
            yield return new WaitForSeconds(1f);
            x4MultiplierEffect.SetActive(false);

        }
        if (hitCounter == 5)
        {
            Debug.Log("Multttiiii");
            x5MultiplierEffect.SetActive(true);
            yield return new WaitForSeconds(1f);
            x5MultiplierEffect.SetActive(false);
        }
    }
}
