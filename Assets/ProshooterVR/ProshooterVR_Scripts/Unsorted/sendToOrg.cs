using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProshooterVR;
public class sendToOrg : MonoBehaviour
{
    private bool hasSpawnedNewGun = false;

    void OnTriggerEnter(Collider collision)
    {
        Debug.Log("" + collision.gameObject.tag);

        if (!hasSpawnedNewGun && collision.gameObject.CompareTag("Weapon") == true)
        {
            // Set the flag to true to prevent multiple spawns
            hasSpawnedNewGun = true;

            // Destroy the current gun
            Destroy(collision.gameObject);
            StartCoroutine(spawn());

        }

        if (!hasSpawnedNewGun && collision.gameObject.CompareTag("RF") == true)
        {
            // Set the flag to true to prevent multiple spawns
            hasSpawnedNewGun = true;

            // Destroy the current gun
            StartCoroutine(spawn());

        }
    } 

    IEnumerator spawn()
    {
        yield return new WaitForSeconds(0.6f);

        if (weaponManager.Instance.isRifleMode == true || weaponManager.Instance.isPistolMode == true)
        { 
            GunGameManeger.Instance.respawnNewWeapon();
            gunRelodeManager.Instance.PalletPoint.SetActive(true);
            
        }else if (weaponManager.Instance.isRapidFireMode == true)
        {
            //RapidFireGunManager.Instance.repos25RFWeapon();
        }

        hasSpawnedNewGun = false;



    }
}
