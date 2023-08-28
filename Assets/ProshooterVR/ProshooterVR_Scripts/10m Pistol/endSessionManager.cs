using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class endSessionManager : MonoBehaviour
{
    public TextMeshPro sr1Score,sr2Score,sr3Score,totalScore;
    public TextMeshPro avgScore, PSbestScore, inTens, shotsOnTrget,shotsMissed;
    public TextMeshPro totalTimeSpent;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnEnable()
    {
        refreshResult();
    }

    private void refreshResult()
    {
        sr1Score.text = GunGameManeger.Instance.series1Score.ToString();
        sr2Score.text = GunGameManeger.Instance.series2Score.ToString();
        sr3Score.text = GunGameManeger.Instance.series3Score.ToString();
        totalScore.text = GunGameManeger.Instance.gameTotalScore.ToString();



    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
