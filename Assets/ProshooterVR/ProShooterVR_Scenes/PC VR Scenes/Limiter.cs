using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Limiter : MonoBehaviour
{
    public GameObject p1, p2, p3, p4;
    public GameObject objT;
    public float xPoint, yPoint;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        xPoint = objT.transform.position.x + objT.transform.localScale.x / 2;
        yPoint = objT.transform.position.y + objT.transform.localScale.y / 2;

        if (xPoint < p1.transform.position.x && yPoint < p1.transform.position.y && objT.transform.position.x > p3.transform.position.x + objT.transform.localScale.x / 2 && objT.transform.position.y > p3.transform.position.y + objT.transform.localScale.y / 2)
        {
            objT.SetActive(true);
        }
        else
        {
            objT.SetActive(false);
        }
        Debug.Log(": : " +objT.transform.position + " : :");
    }
}
