using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonBG : MonoBehaviour
{
    public GameObject myBg;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void selectedBtn()
    {
        myBg.SetActive(true);
    }
    public void delectedBtn()
    {
        myBg.SetActive(false);

    }
}
