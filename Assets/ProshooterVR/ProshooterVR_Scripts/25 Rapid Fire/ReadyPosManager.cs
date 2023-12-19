using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadyPosManager : MonoBehaviour
{
    bool called;
    
    // Start is called before the first frame update
    void Start()
    {
        called = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if ((other.gameObject.name.Contains("Pistol") == true))
        {
            if (RapidFireGunManager.Instance.seriesStarted == false)
            {
                if (RapidFireGunManager.Instance.SeriesCounter == 0)
                {
                    RapidFireGunManager.Instance.state = RapidFireGunManager.gamestate.round1;
                    RapidFireGunManager.Instance.callGameState();


                }
                else if (RapidFireGunManager.Instance.SeriesCounter == 1)
                {
                    RapidFireGunManager.Instance.state = RapidFireGunManager.gamestate.round2;
                    RapidFireGunManager.Instance.callGameState();


                }
                

                RapidFireGunManager.Instance.foulTimer = true;
                Debug.Log("working");
                RapidFireGunManager.Instance.seriesStarted = true;
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {

        if ((other.gameObject.name.Contains("Pistol") == true))
        {
            RapidFireGunManager.Instance.isReloaded = true;

            if (RapidFireGunManager.Instance.foulTimer == true)
            {
                RapidFireGunManager.Instance.seriesFoul = true;
                InstructionManager.Instance.audioSource.PlayOneShot(InstructionManager.Instance.buzzer);

            }
            Debug.Log("out");
        }

    }
}
