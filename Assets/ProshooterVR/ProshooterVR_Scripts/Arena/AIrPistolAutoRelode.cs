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
    public GameObject reloadingAnim;
    public GameObject gunObj;

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

        if (gunObj.GetComponent<Grabbable>().BeingHeld == false)
        {
            Arena_AirPistol_mananger.Instance.gunGun_Holder.SetActive(true);

        }
        if (gunObj.GetComponent<Grabbable>().BeingHeld == true)
        {
            Arena_AirPistol_mananger.Instance.gunGun_Holder.SetActive(false);
        }
    }
    private void gunAutoReload()
    {
        StartCoroutine(reloadGun());
    }         

    IEnumerator reloadGun()
    {
        reloadingAnim.SetActive(true);
        animator.Rebind();
        animator.Play(autoReload);
        while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
        {
            yield return null;
        }

        // Animation is complete, perform further actions
        Debug.Log("Reload animation complete!");
        reloadingAnim.SetActive(false);

        Arena_AirPistol_mananger.Instance.isReloaded = true;
    }

}
