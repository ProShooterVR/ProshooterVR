using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using BNG;
using ProshooterVR;
public class AIrPistolAutoRelode : MonoBehaviour
{


    public Animator animator;
    public string autoReload;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (InputBridge.Instance.AButtonDown == true || Input.GetKeyDown("space"))
        {
            if (Arena_AirPistol_mananger.Instance.isReloaded == false)
            {
                gunAutoReload(); 
            }
        }
    }
    private void gunAutoReload()
    {
        StartCoroutine(reloadGun());
    }         

    IEnumerator reloadGun()
    {
        animator.Rebind();
        animator.Play(autoReload);
        while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
        {
            yield return null;
        }

        // Animation is complete, perform further actions
        Debug.Log("Reload animation complete!");

        Arena_AirPistol_mananger.Instance.isReloaded = true;
    }

}
