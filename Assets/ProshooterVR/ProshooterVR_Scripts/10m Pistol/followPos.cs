using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followPos : MonoBehaviour
{
    //public Transform refPos; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        this.transform.position = GunGameManeger.Instance.touchReloader.transform.position;
        
    }
}
