using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using BNG;

public class PistolManulRelode : MonoBehaviour
{

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
                //GunGameManeger.Instance.audioSrc.PlayOneShot(GunGameManeger.Instance.pistol[0]);
                FmodSetup.Instance.ReloadOpenEvent();
                //ReloadOpen.SetActive(true);
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
                    //GunGameManeger.Instance.audioSrc.PlayOneShot(GunGameManeger.Instance.pistol[1]);
                    FmodSetup.Instance.ReloadCloseEvent();
                    //ReloadClose.SetActive(true);
                    GunGameManeger.Instance.touchReloader.SetActive(false);
                    GunGameManeger.Instance.relodePt.SetActive(false);
                    UXManagerAirPistol.Instance.UXEvents(5);

                    Debug.Log("UP");
                }
            }
        }
    }

}
