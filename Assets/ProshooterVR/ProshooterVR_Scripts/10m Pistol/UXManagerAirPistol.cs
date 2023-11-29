using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UXManagerAirPistol : MonoBehaviour
{
    public static UXManagerAirPistol Instance;

    public bool isUXSeen;

    private void Awake()
    {
        Instance = this;
    }
    //highlightes

    public GameObject[] Highlights;
    //Labeles
    public GameObject[] Lables;


    // Start is called before the first frame update
    void Start()
    {
        isUXSeen = false;
        resetUXData();
    }

    void resetUXData()
    {
       for(int i = 0; i< Highlights.Length; i++)
        {
            Highlights[i].GetComponent<Outline>().enabled = false;
        }
        for (int i = 0; i < Lables.Length; i++)
        {
            Lables[i].SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UXEvents(int count)
    {
        if (GunGameManeger.Instance.isUXON == true)
        {
            switch (count)
            {
                case 0:
                    Highlights[0].GetComponent<Outline>().enabled = true;
                    Lables[0].SetActive(true);
                    break;
                case 1:
                    Highlights[0].GetComponent<Outline>().enabled = false;
                    Lables[0].SetActive(false);
                    Lables[1].SetActive(true);
                    break;
                case 2:
                    Lables[1].SetActive(false);
                    Lables[2].SetActive(true);
                    Highlights[1].GetComponent<Outline>().enabled = true;
                    break;
                case 3:
                    Lables[2].SetActive(false);
                    Lables[3].SetActive(true);
                    break;
                case 4:
                    Lables[3].SetActive(false);
                    Lables[4].SetActive(true);
                    break;
                case 5:
                    Lables[4].SetActive(false);
                    Lables[5].SetActive(true);
                    Highlights[2].GetComponent<Outline>().enabled = true;
                    break;
                case 6:
                    Highlights[2].GetComponent<Outline>().enabled = false;
                    Lables[5].SetActive(false);
                    Lables[6].SetActive(true);
                   
                    break;
            }
        }
    }
}
