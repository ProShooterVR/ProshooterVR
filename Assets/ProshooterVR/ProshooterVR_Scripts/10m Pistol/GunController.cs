using System;
using UnityEngine;

public class GunController : MonoBehaviour
{
    private bool hasSpawnedNewGun = false;

    void OnCollisionEnter(Collision collision)
    {

        if (!hasSpawnedNewGun && (String.Equals(collision.gameObject.name,"Floor") == true))
        {
            // Set the flag to true to prevent multiple spawns
            hasSpawnedNewGun = true;

            // Destroy the current gun
            Destroy(gameObject);
            GunGameManeger.Instance.respawnNewWeapon();
            hasSpawnedNewGun = false;
        }

       
    }
}
