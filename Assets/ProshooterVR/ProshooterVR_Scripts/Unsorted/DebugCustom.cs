using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DebugCustom : MonoBehaviour
{
    public static DebugCustom Inst;

    public TextMeshPro debug;

    private void Awake()
    {
        Inst = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
