using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BNG;

public class pistolPallet : MonoBehaviour
{
    // Start is called before the first frame update

    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        
            Debug.Log("Done" + other.gameObject.name);
            if ((other.gameObject.name.Contains("AirgunPellet") == true))
            {
                GunGameManeger.Instance.isPallatPlaced = true;
                GunGameManeger.Instance.touchReloader.SetActive(true);

                GunGameManeger.Instance.pallatePt.SetActive(false);
                
                UXManagerAirPistol.Instance.UXEvents(4);
                Debug.Log("working");
                other.gameObject.SetActive(false);
            }
        
    }
}
