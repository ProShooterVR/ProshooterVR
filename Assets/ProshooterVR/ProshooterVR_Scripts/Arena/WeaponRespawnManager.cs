using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProshooterVR;

public class WeaponRespawnManager : MonoBehaviour
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

        
    }

    IEnumerator spawn()
    {
        yield return new WaitForSeconds(0.6f);

        if (weaponManager.Instance.isPistolMode == true)
        {
            Arena_AirPistol_mananger.Instance.respawnNewWeapon(Arena_AirPistol_mananger.Instance.laneChosen);
        }
        
        hasSpawnedNewGun = false;


    }
}
