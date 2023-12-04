using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class missedShotScore : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnCollisionEnter(Collision collision)
    {
        if ((collision.gameObject.name.Contains("BulletHole") == true))
        {
            PistolUIManager.Instance.missedShots();
            collision.gameObject.SetActive(false);
        }
    }
}
