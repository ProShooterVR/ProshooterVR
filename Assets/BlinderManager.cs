using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BNG;
using ProshooterVR;
using TMPro;

public class BlinderManager : MonoBehaviour
{
    private bool isCubeEnabled = true;
    public GameObject Blinder;

    private void Start()
    {
        Blinder.SetActive(false);
    }

    void Update()
    {
        // Toggle the cube on/off when the space key is pressed
        if (InputBridge.Instance.YButtonDown)
        {
            isCubeEnabled = !isCubeEnabled;
            Blinder.SetActive(isCubeEnabled);
        }
    }
}
