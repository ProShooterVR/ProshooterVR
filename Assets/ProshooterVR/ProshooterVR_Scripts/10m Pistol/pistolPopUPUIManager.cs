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

    public TextMeshProUGUI userNameTxt;
    public TextMeshProUGUI srs1ScoreTxt, srs2ScoreTxt, srs3ScoreTxt,gameTotalScoreTxt;
    public TextMeshProUGUI avgScoreTxt, shotsHitMisTxt, innerTText, timeSpentTxt, pBestScoreTxt;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
