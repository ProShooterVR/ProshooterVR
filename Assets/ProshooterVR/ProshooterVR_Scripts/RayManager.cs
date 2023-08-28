using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nova;
using BNG;


namespace NovaSamples.Inventory
{
    public class RayManager : MonoBehaviour
    {
        public static RayManager Instance;
        public GameObject rey;
        private void Awake()
        {
            Instance = this;
        }
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void EnableRey()
        {
            Debug.Log("enables");

            rey.GetComponent<LineRenderer>().enabled = true;
            rey.GetComponent<LeftControllerRaycast>().enabled = true;
        }
        public void DisableRey()
        {
            Debug.Log("Disbaed");
            rey.GetComponent<LineRenderer>().enabled = false;
            rey.GetComponent<LeftControllerRaycast>().enabled = false;

        }

    }
}