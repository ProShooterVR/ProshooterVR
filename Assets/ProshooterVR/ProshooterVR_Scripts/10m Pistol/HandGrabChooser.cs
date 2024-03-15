using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandGrabChooser : MonoBehaviour
{

    public GameObject leftHandGrabber, rightHandGrabber;


    // Start is called before the first frame update
    void Start()
    {
        if (LocalUserDataManager.Instance.isRightHand == true)
        {
            leftHandGrabber.SetActive(false);
            rightHandGrabber.SetActive(true);
        }
        else
        {
            leftHandGrabber.SetActive(true);
            rightHandGrabber.SetActive(false);
        }

    }
}
