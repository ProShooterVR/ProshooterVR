using Nova;
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
    Color HexToColor(string hex)
    {
        Color color = new Color();
        ColorUtility.TryParseHtmlString(hex, out color);
        return color;
    }
    public void selectedBtn()
    {
        Color targetColor = HexToColor("#FF9F0A");
        myBg.GetComponent<UIBlock2D>().Border.Color = targetColor;
    }
    public void delectedBtn()
    {
        myBg.GetComponent<UIBlock2D>().Border.Color = Color.white;
    }
}
