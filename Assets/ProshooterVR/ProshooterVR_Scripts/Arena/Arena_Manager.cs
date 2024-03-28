using BNG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arena_Manager : MonoBehaviour
{
    public static Arena_Manager Instance;

    public GameObject proshooter_menu;

    public Transform airPistolSpawnPoint, airRifleSpawnPoint, rapidFireSpawnPoint, shotgunSpawnPoint;

    public Transform orgPos;
    public GameObject arena_player,playerController;

    public GameObject AirPistolUI;
    private void Awake()
    {
        Instance = this;
    }



    // Start is called before the first frame update
    void Start()
    {
        AirPistolUI.SetActive(false);
        orgPos = arena_player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (InputBridge.Instance.XButtonDown == true || Input.GetKeyDown(KeyCode.L))
        {
            if (proshooter_menu.activeSelf)
            {
                // If menu is active, deactivate it
                proshooter_menu.GetComponent<UIBehaviour>().StartPopInEffect();
            }
            else
            {
                // If menu is inactive, activate it
                proshooter_menu.SetActive(true);
            }
        }
    }

    public void airPistolBtnClick() {

        weaponManager.Instance.isPistolMode = true;
        weaponManager.Instance.isRifleMode = false;
        weaponManager.Instance.isRapidFireMode = false;
    

        Arena_Manager.Instance.arena_player.transform.SetPositionAndRotation(orgPos.localPosition, orgPos.rotation);

        playerController.GetComponent<CharacterController>().enabled = false;
        playerController.GetComponent<LocomotionManager>().enabled = false;

        arena_player.transform.SetPositionAndRotation(airPistolSpawnPoint.position, airPistolSpawnPoint.rotation);

        playerController.GetComponent<CharacterController>().enabled = true;
        playerController.GetComponent<LocomotionManager>().enabled = true;

        AirPistolUI.SetActive(true);


    }
    public void airRifleBtnClick()
    {
        weaponManager.Instance.isPistolMode = false;
        weaponManager.Instance.isRifleMode = true;
        weaponManager.Instance.isRapidFireMode = false;
        Arena_Manager.Instance.arena_player.transform.SetPositionAndRotation(orgPos.localPosition, orgPos.rotation);

        playerController.GetComponent<CharacterController>().enabled = false;
        playerController.GetComponent<LocomotionManager>().enabled = false;

        arena_player.transform.SetPositionAndRotation(airRifleSpawnPoint.position, airRifleSpawnPoint.rotation);

        playerController.GetComponent<CharacterController>().enabled = true;
        playerController.GetComponent<LocomotionManager>().enabled = true;
       
        AirPistolUI.SetActive(false);


    }
    public void rapidFireBtnClick()
    {
        weaponManager.Instance.isPistolMode = false;
        weaponManager.Instance.isRifleMode = false;
        weaponManager.Instance.isRapidFireMode = true;

        Arena_Manager.Instance.arena_player.transform.SetPositionAndRotation(orgPos.localPosition, orgPos.rotation);

        playerController.GetComponent<CharacterController>().enabled = false;
        playerController.GetComponent<LocomotionManager>().enabled = false;

        arena_player.transform.SetPositionAndRotation(rapidFireSpawnPoint.position, rapidFireSpawnPoint.rotation);

        playerController.GetComponent<CharacterController>().enabled = true;
        playerController.GetComponent<LocomotionManager>().enabled = true;

    }
    public void shotgunBtnClick()
    {
        Arena_Manager.Instance.arena_player.transform.SetPositionAndRotation(orgPos.localPosition, orgPos.rotation);

        playerController.GetComponent<CharacterController>().enabled = false;
        playerController.GetComponent<LocomotionManager>().enabled = false;

        arena_player.transform.SetPositionAndRotation(shotgunSpawnPoint.position, shotgunSpawnPoint.rotation);

        playerController.GetComponent<CharacterController>().enabled = true;
        playerController.GetComponent<LocomotionManager>().enabled = true;

    }
}
