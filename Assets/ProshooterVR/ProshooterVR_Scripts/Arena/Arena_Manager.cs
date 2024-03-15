using BNG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arena_Manager : MonoBehaviour
{
    public static Arena_Manager Instance;

    public GameObject proshooter_menu;

    public Transform airPistolSpawnPoint, airRifleSpawnPoint, rapidFireSpawnPoint, shotgunSpawnPoint;

    public GameObject arena_player,playerController;


    private void Awake()
    {
        Instance = this;
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (InputBridge.Instance.XButtonDown == true || Input.GetKeyDown("space"))
        {
            proshooter_menu.SetActive(!proshooter_menu.activeSelf);
        }
    }

    public void airPistolBtnClick() {

        playerController.GetComponent<CharacterController>().enabled = false;
        playerController.GetComponent<LocomotionManager>().enabled = false;

        arena_player.transform.SetPositionAndRotation(airPistolSpawnPoint.position, airPistolSpawnPoint.rotation);

        playerController.GetComponent<CharacterController>().enabled = true;
        playerController.GetComponent<LocomotionManager>().enabled = true;

    }
    public void airRifleBtnClick()
    {

        playerController.GetComponent<CharacterController>().enabled = false;
        playerController.GetComponent<LocomotionManager>().enabled = false;

        arena_player.transform.SetPositionAndRotation(airRifleSpawnPoint.position, airRifleSpawnPoint.rotation);

        playerController.GetComponent<CharacterController>().enabled = true;
        playerController.GetComponent<LocomotionManager>().enabled = true;

    }
    public void rapidFireBtnClick()
    {

        playerController.GetComponent<CharacterController>().enabled = false;
        playerController.GetComponent<LocomotionManager>().enabled = false;

        arena_player.transform.SetPositionAndRotation(rapidFireSpawnPoint.position, rapidFireSpawnPoint.rotation);

        playerController.GetComponent<CharacterController>().enabled = true;
        playerController.GetComponent<LocomotionManager>().enabled = true;

    }
    public void shotgunBtnClick()
    {

        playerController.GetComponent<CharacterController>().enabled = false;
        playerController.GetComponent<LocomotionManager>().enabled = false;

        arena_player.transform.SetPositionAndRotation(shotgunSpawnPoint.position, shotgunSpawnPoint.rotation);

        playerController.GetComponent<CharacterController>().enabled = true;
        playerController.GetComponent<LocomotionManager>().enabled = true;

    }
}
