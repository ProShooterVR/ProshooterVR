using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myorgPos : MonoBehaviour
{
     Vector3 orgPos;
     Quaternion rotation;

    // Start is called before the first frame update
    void Start()
    {
        orgPos = this.transform.position;
        rotation = this.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void resetPos()
    {
        this.transform.position = orgPos;
        this.transform.rotation = rotation;
    }
}
