using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendBulletData : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionStay(Collision collision)
    {
        Debug.Log("Continuous collision detected with " + collision.gameObject.name);
       
        //if(String.Compare(collision.gameObject.name,"Target") == 0)
        //{
        //    PistolGameManeger.Instance.isScoreUpdated = true;
        //}
        //else
        //{
        //    PistolGameManeger.Instance.isScoreUpdated = false;
        //}
    }
}
