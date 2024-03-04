using BNG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn_pallet : MonoBehaviour
{
    public bool inSide, outSide;

    // Start is called before the first frame update
    void Start()
    {
        inSide = false;
        outSide = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (InputBridge.Instance.LeftTriggerDown == true || InputBridge.Instance.RightTriggerDown == true)
        {
            Debug.Log("Clicked");
            if (inSide == true)
            {
                if (GunGameManeger.Instance.spawnBullet == true)
                {
                    GunGameManeger.Instance.tempPallet.SetActive(false);

                    GameObject pallet = Instantiate(GunGameManeger.Instance.palletSpawn);
                    pallet.transform.parent = GunGameManeger.Instance.palletParent.transform;
                    pallet.transform.position = GunGameManeger.Instance.palletHoldPos.transform.position;
                    UXManagerAirPistol.Instance.UXEvents(3);
                    GunGameManeger.Instance.spawnBullet = false;
                }
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        
            inSide = true;
            outSide = false;
        

            Debug.Log("collided in");
            
    }
    private void OnTriggerExit(Collider other)
    {
        
            inSide = false;
            outSide = true;
        

        Debug.Log("collided out");

    }
}
