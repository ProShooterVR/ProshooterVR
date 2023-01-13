using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PistolPhysiocs : MonoBehaviour
{

    public Slider massSlider, dragSlider, AngDragSLider;
    public GameObject gun;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void massChange()
    {
        gun.GetComponent<Rigidbody>().mass = massSlider.value;
    }
    public void dragChange()
    {
        gun.GetComponent<Rigidbody>().drag = dragSlider.value;
    }
    public void angdragChange()
    {
        gun.GetComponent<Rigidbody>().angularDrag = AngDragSLider.value;
    }
}
