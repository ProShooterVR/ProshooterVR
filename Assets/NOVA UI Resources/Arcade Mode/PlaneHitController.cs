using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneHitController : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {  
        if (other.gameObject.name.Contains("Projectile") == true)
        {
            ArcadeGameManager.instance.Hit = false;
            ArcadeGameManager.instance.hitCounter = 0;
            Debug.Log("Collide");
        }
    }
}
