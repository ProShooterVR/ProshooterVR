using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BNG;
using UnityEngine.Animations;

public class ReloadManager : MonoBehaviour
{
    public GameObject target;



    private void Update()
    {
        if (InputBridge.Instance.AButton == true)
        {
            if (GunGameManeger.Instance.isReloading == true)
            {
                GunGameManeger.Instance.isReloading = false;
                StartCoroutine(WaitForAnimation());
            }
        }
    }



    private IEnumerator WaitForAnimation()
    {
        GunGameManeger.Instance.animator.Rebind();
        GunGameManeger.Instance.animator.Play(GunGameManeger.Instance.animationName);
        yield return new WaitForSeconds(0.6f);
        GunGameManeger.Instance.audioSrc.PlayOneShot(GunGameManeger.Instance.pistol[0]);

        yield return new WaitForSeconds(1.2f);
        GunGameManeger.Instance.audioSrc.PlayOneShot(GunGameManeger.Instance.pistol[1]);

        yield return new WaitForSeconds(0.5f);
        GunGameManeger.Instance.isReloading = false;
        GunGameManeger.Instance.isReloaded = true;
        Debug.Log("Animation is complete.");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (string.Compare(other.gameObject.name, "Pistol") == 0)
        {
            GunGameManeger.Instance.isReloaded = false;
            GunGameManeger.Instance.isReloading = true;
            Debug.Log("Target entered collision area." + other.gameObject.name);
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (string.Compare(other.gameObject.name, "Pistol") == 0)
        {
            //PistolGameManeger.Instance.isReloaded = true;
            GunGameManeger.Instance.isReloading = false;
            Debug.Log("Target exited collision area." + other.gameObject.name);
        }
    }
}
