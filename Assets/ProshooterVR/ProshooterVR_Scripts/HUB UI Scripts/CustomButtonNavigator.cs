using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomButtonNavigator : MonoBehaviour
{

    public GameObject[] noOfButtons;
    int btnNo;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onButtonClicked(int no)
    {
        for(int i = 0; i < noOfButtons.Length; i++)
        {
            if (i == no)
            {
                noOfButtons[i].GetComponent<buttonBG>().selectedBtn();
            }
            else
            {
                noOfButtons[i].GetComponent<buttonBG>().delectedBtn();
            }
        }
    }

}
