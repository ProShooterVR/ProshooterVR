using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BNG;

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

                isDown = false;
                isUP = true;
                GunGameManeger.Instance.animator.Rebind();
                GunGameManeger.Instance.animator.Play(GunGameManeger.Instance.clip1);

                GunGameManeger.Instance.isReloaded = false;
                GunGameManeger.Instance.isReloading = true;
                GunGameManeger.Instance.audioSrc.PlayOneShot(GunGameManeger.Instance.pistol[0]);
                GunGameManeger.Instance.touchReloader.SetActive(false);

                GunGameManeger.Instance.pallatePt.SetActive(true);

                UXManagerAirPistol.Instance.UXEvents(2);





                Debug.Log("UP");
            }
            else if (isUP == true)
            {
                if (GunGameManeger.Instance.isPallatPlaced == true)
                {
                    isDown = true;
                    isUP = false;
                    GunGameManeger.Instance.animator.Rebind();
                    GunGameManeger.Instance.animator.Play(GunGameManeger.Instance.clip2);

                    GunGameManeger.Instance.isReloaded = true;
                    GunGameManeger.Instance.isReloading = false;
                    GunGameManeger.Instance.audioSrc.PlayOneShot(GunGameManeger.Instance.pistol[1]);
                    GunGameManeger.Instance.touchReloader.SetActive(false);

                    GunGameManeger.Instance.relodePt.SetActive(false);
                    Debug.Log("UP");
                    UXManagerAirPistol.Instance.UXEvents(5);

                }
            }
        }
    }
}
