using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VRDebugManager : MonoBehaviour
{
    public static VRDebugManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        DontDestroyOnLoad(Instance);
    }
    public TextMeshPro DebugLog;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

}
