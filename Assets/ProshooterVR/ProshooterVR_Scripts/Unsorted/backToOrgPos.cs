using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backToOrgPos : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("" + collision.gameObject.name);
        collision.gameObject.GetComponent<myorgPos>().resetPos();
    }
}
