using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fetchLeaderboardData : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ArcadeLeaderboardManager.Instance.SaveArcadeGameData(LocalUserDataManager.Instance.metaID, ArcadeGameManager.instance.totalscore.ToString());

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
