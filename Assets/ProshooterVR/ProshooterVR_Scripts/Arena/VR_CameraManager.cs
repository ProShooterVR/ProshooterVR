using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VR_CameraManager : MonoBehaviour
{

    // Crating singlton
    public static VR_CameraManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    // Cameras
    public GameObject cam_movement, cam_standing;
    // Start is called before the first frame update
    void Start()
    {
        cam_movement.SetActive(true);
        cam_standing.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void setCameraMovement(bool val)
    {
        if(val == true)
        {
            cam_movement.SetActive(true);
            cam_standing.SetActive(false);
        }
        else
        {
            cam_movement.SetActive(false);
            cam_standing.SetActive(true);
        }
    }
}
