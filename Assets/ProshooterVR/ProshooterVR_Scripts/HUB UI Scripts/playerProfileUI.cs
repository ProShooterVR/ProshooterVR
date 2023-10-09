using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerProfileUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
      HUB_UIManager.Instance.update_playerProfileData();
    }

    private void OnEnable()
    {
        HUB_UIManager.Instance.update_playerProfileData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
