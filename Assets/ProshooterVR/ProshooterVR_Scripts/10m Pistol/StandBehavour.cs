using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BNG;
public class StandBehavour : MonoBehaviour
{
    public GameObject moveHigh, udHigh, baseG, MoveStand;

    public static StandBehavour Instance;

    private void Awake()
    {
        Instance = this;

    }
    // Start is called before the first frame update
    void Start()
    {
        disableMovement();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void enableMovement()
    {
        udHigh.GetComponent<MeshRenderer>().enabled = true;
        udHigh.GetComponent<Rigidbody>().isKinematic = false;
        udHigh.GetComponent<Grabbable>().enabled = true;

        moveHigh.SetActive(true);
        MoveStand.GetComponent<Rigidbody>().isKinematic = false;
        MoveStand.GetComponent<Grabbable>().enabled = true;

        baseG.SetActive(true);

    }
    public void disableMovement()
    {
        udHigh.GetComponent<MeshRenderer>().enabled = false;
        udHigh.GetComponent<Rigidbody>().isKinematic = true;
        udHigh.GetComponent<Grabbable>().enabled = false;

        moveHigh.SetActive(false);
        MoveStand.GetComponent<Rigidbody>().isKinematic = true;
        MoveStand.GetComponent<Grabbable>().enabled = false;
        baseG.SetActive(false);

    }
}
