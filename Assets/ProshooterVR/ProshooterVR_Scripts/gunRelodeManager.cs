using UnityEngine;
using System.Collections;
using BNG;

namespace ProshooterVR
{
    public class gunRelodeManager : MonoBehaviour
    {
        public static gunRelodeManager Instance;
        private void Awake()
        {
            Instance = this;
        }

        public GameObject gunObj;
        public bool isUXCalled;

        public GameObject RelodTouch, PalletPoint;
        public Animator animator;
        public string clip1, clip2;

        private void Start()
        {
            isUXCalled = false;
            RelodTouch.SetActive(true);
            PalletPoint.SetActive(false);
            GunGameManeger.Instance.isWespaonSpawn = true;
        }
        private void Update()
        {
            if (gunObj.GetComponent<Grabbable>().BeingHeld == false)
            {
                GunGameManeger.Instance.gunPlatform.GetComponent<BoxCollider>().enabled = true;
                GunGameManeger.Instance.spwanEffect.SetActive(true);

                if (isUXCalled == true)
                {
                    isUXCalled = false;
                }

            }
            if (gunObj.GetComponent<Grabbable>().BeingHeld == true)
            {
                GunGameManeger.Instance.gunPlatform.GetComponent<BoxCollider>().enabled = false;
                GunGameManeger.Instance.spwanEffect.SetActive(false);


                if (isUXCalled == false)
                {
                    isUXCalled = true;
                }
            }

        }


    }
}