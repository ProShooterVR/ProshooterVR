using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BNG;
using ProshooterVR;
public class RifleManualRelode : MonoBehaviour
{
    // Start is called before the first frame update

   
    bool isUP, isDown;

    void Start()
    {
        isUP = false;
        isDown = true;
        
      
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {

        if (string.Compare(other.gameObject.name, "load") == 0)
        {

            if (isDown == true)
            {
                GunGameManeger.Instance.tempPallet.SetActive(true);
                gunRelodeManager.Instance.animator.Rebind();
                gunRelodeManager.Instance.animator.Play(gunRelodeManager.Instance.clip1);
                isDown = false;
                isUP = true;

                GunGameManeger.Instance.isReloaded = false;
                GunGameManeger.Instance.isReloading = true;
                GunGameManeger.Instance.touchReloader.SetActive(false);

                gunRelodeManager.Instance.PalletPoint.SetActive(true);

                UXManagerAirPistol.Instance.UXEvents(2);





                Debug.Log("UP");
            }
            else if (isUP == true)
            {
                if (GunGameManeger.Instance.isPallatPlaced == true)
                {
                    gunRelodeManager.Instance.animator.Rebind();
                    gunRelodeManager.Instance.animator.Play(gunRelodeManager.Instance.clip2);
                    isDown = true;
                    isUP = false;

                    GunGameManeger.Instance.isReloaded = true;
                    GunGameManeger.Instance.isReloading = false;
                    GunGameManeger.Instance.touchReloader.SetActive(false);

                    gunRelodeManager.Instance.RelodTouch.SetActive(false);
                    Debug.Log("UP");
                    UXManagerAirPistol.Instance.UXEvents(5);

                }
            }
        }
    }
}
