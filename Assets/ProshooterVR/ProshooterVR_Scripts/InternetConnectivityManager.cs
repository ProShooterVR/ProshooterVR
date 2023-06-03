using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InternetConnectivityManager : MonoBehaviour
{
    public static InternetConnectivityManager Instance;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        DontDestroyOnLoad(Instance);

    }


    [SerializeField]
    GameObject errorUI;
    bool isAccepted;
    void Start()
    {

        errorUI.SetActive(false);
        isAccepted = false;
       
    }

    // Update is called once per frame
    void Update()
    {
        NetworkReachability reachability = Application.internetReachability;

        if (reachability == NetworkReachability.NotReachable)
        {
            if (isAccepted == false)
            {
                errorUI.SetActive(true);
            }
            Debug.Log("No internet connection available.");
        }
        else if (reachability == NetworkReachability.ReachableViaCarrierDataNetwork)
        {
            errorUI.SetActive(false);
            Debug.Log("Connected via mobile data.");
        }
        else if (reachability == NetworkReachability.ReachableViaLocalAreaNetwork)
        {
            errorUI.SetActive(false);
            Debug.Log("Connected via Wi-Fi or Ethernet.");
        }
    }

    public void onAcceptBtn()
    {
        isAccepted = true;
        errorUI.SetActive(false);
    }
}
