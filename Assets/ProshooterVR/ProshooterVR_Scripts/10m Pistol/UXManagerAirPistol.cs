using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UXManagerAirPistol : MonoBehaviour
{
    public static UXManagerAirPistol Instace;

    public bool isUXSeen;

    //highlightes

    public GameObject gunHighlight;
    //Labeles
    public GameObject gunLable;


    // Start is called before the first frame update
    void Start()
    {
        isUXSeen = false;
    }

    void resetUXData()
    {
        gunHighlight.GetComponent<Outline>().enabled = false;
        gunLable.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
