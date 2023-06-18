using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class customBtnMnger : MonoBehaviour
{
    public GameObject b1, b2, b3;
    bool b1c, b2c, b3c;

    public Sprite hover, normal, selected;
    

    // Start is called before the first frame update
    void Start()
    {

        b1c = false;
        b2c = false;

        b3c = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void b1click()
    {
        b1.GetComponent<Image>().sprite = selected;
        b2.GetComponent<Image>().sprite = normal;
        b3.GetComponent<Image>().sprite = normal;

        b1c = true;
        b2c = false;
        b3c = false;

    }
    public void b2click()
    {
        b1.GetComponent<Image>().sprite = normal;
        b2.GetComponent<Image>().sprite = selected;
        b3.GetComponent<Image>().sprite = normal;

        b1c = false;
        b2c = true;
        b3c = false;

    }
    public void b3click()
    {
        b1.GetComponent<Image>().sprite = normal;
        b2.GetComponent<Image>().sprite = normal;
        b3.GetComponent<Image>().sprite = selected;

        b1c = false;
        b2c = false;
        b3c = true;
    }

    public void b1en()
    {
        if (b1c == false)
        {
            b1.GetComponent<Image>().sprite = hover;
        }
    }
    public void b2en()
    {
        if (b2c == false)
        {
            b2.GetComponent<Image>().sprite = hover;
        }

    }
    public void b3en()
    {
        if (b3c == false)
        {
            b3.GetComponent<Image>().sprite = hover;
        }
    }

    public void b1ex()
    {
        if (b1c == false)
        {
            b1.GetComponent<Image>().sprite = normal;
        }
    }
    public void b2ex()
    {
        if (b2c == false)
        {
            b2.GetComponent<Image>().sprite = normal;
        }
    }
    public void b3ex()
    {

        if (b3c == false)
        {
            b3.GetComponent<Image>().sprite = normal;
        }
    }
}
