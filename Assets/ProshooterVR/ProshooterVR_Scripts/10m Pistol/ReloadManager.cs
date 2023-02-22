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
            if (PistolGameManeger.Instance.isReloading == true)
            {
                PistolGameManeger.Instance.isReloading = false;
                StartCoroutine(WaitForAnimation());
            }
        }
    }



    private IEnumerator WaitForAnimation()
    {
        PistolGameManeger.Instance.animator.Rebind();
        PistolGameManeger.Instance.animator.Play(PistolGameManeger.Instance.animationName);
        yield return new WaitForSeconds(1.20f);
        PistolGameManeger.Instance.audioSrc.PlayOneShot(PistolGameManeger.Instance.pistol[0]);

        yield return new WaitForSeconds(2.3f);
        PistolGameManeger.Instance.audioSrc.PlayOneShot(PistolGameManeger.Instance.pistol[1]);

        yield return new WaitForSeconds(1f);
        PistolGameManeger.Instance.isReloading = false;
        PistolGameManeger.Instance.isReloaded = true;
        Debug.Log("Animation is complete.");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (string.Compare(other.gameObject.name, "Pistol") == 0)
        {
            PistolGameManeger.Instance.isReloaded = false;
            PistolGameManeger.Instance.isReloading = true;
            Debug.Log("Target entered collision area." + other.gameObject.name);
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (string.Compare(other.gameObject.name, "Pistol") == 0)
        {
            //PistolGameManeger.Instance.isReloaded = true;
            PistolGameManeger.Instance.isReloading = false;
            Debug.Log("Target exited collision area." + other.gameObject.name);
        }
    }
}
