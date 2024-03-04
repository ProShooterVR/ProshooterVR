using BNG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RFGun_ReloadManager : MonoBehaviour
{

    public static RFGun_ReloadManager Instance;

    public GameObject SlideObject;
    public GameObject gunObj;

    public GameObject magZEffect, gunReloadEffect;
    public GameObject FireAnim;
    public GameObject muzzle;
    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gunObj.GetComponent<Grabbable>().BeingHeld == false)
        {
            RapidFireGunManager.Instance.gunPlatform.GetComponent<BoxCollider>().enabled = true;
            RapidFireGunManager.Instance.gunSpawneffect.SetActive(true);

        }
        if (gunObj.GetComponent<Grabbable>().BeingHeld == true)
        {
            RapidFireGunManager.Instance.gunPlatform.GetComponent<BoxCollider>().enabled = false;
            RapidFireGunManager.Instance.gunSpawneffect.SetActive(false);
            magZEffect.SetActive(true);
            RapidFireGunManager.Instance.SlideObject.SetActive(false);

        }

    }
}
