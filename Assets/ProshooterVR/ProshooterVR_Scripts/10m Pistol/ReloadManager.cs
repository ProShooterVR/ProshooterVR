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
        if (GunGameManeger.Instance.isGamePause == false)
        {
            if (InputBridge.Instance.BButton == true)
            {
                if (GunGameManeger.Instance.isReloading == true)
                {
                    GunGameManeger.Instance.mat.GetComponent<Renderer>().material = GunGameManeger.Instance.blue;

                    GunGameManeger.Instance.isReloading = false;
                    StartCoroutine(WaitForAnimation());
                }
            }
        }
    }



    private IEnumerator WaitForAnimation()
    {
        GunGameManeger.Instance.mat.GetComponent<Renderer>().material = GunGameManeger.Instance.blue;

        yield return new WaitForSeconds(0.6f);

        yield return new WaitForSeconds(1.2f);

        yield return new WaitForSeconds(0.5f);
        GunGameManeger.Instance.isReloading = false;
        GunGameManeger.Instance.isReloaded = true;
        Debug.Log("Animation is complete.");
        GunGameManeger.Instance.mat.GetComponent<Renderer>().material = GunGameManeger.Instance.green;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (string.Compare(other.gameObject.name, "Pistol") == 0)
        {
            GunGameManeger.Instance.mat.GetComponent<Renderer>().material = GunGameManeger.Instance.yellow;

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
            GunGameManeger.Instance.mat.GetComponent<Renderer>().material = GunGameManeger.Instance.black;

        }
    }
}
