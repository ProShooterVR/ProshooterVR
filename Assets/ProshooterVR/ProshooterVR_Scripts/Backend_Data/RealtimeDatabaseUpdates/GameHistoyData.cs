using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using System;
using System.Threading.Tasks;
using Newtonsoft.Json;


[Serializable]
public class GameHistoyDataPistol
{
    public string meta_id;
    public string meta_username;
    public string game_mode;
    public float zoneA_mult;
    public float zoneB_mult;
    public float zoneC_mult;
    public float diffculty_mult;
    public int[] shots;
    public float avg_score;
    public float no_InTens;
    public float no_shots_target;
    public float no_shots_missed;
    public float personal_best;
    public float sr1Score;
    public float sr2Score;
    public float sr3Score;
    public float total_game_score;
    public string total_timespent;


}

public class GameHistoryData : MonoBehaviour
{
    public static GameHistoryData Instance;

    private void Awake()
    {
        Instance = this;
    }


    
}
