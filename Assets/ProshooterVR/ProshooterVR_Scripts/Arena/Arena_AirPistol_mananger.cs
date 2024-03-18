using BNG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arena_AirPistol_mananger : MonoBehaviour
{
    public static Arena_AirPistol_mananger Instance;


    public GameObject airPistolTarget;

    public Transform[] airPistol_targetPoses;

    public Transform[] airPistol_Playerspawns;

    public Transform[] airPistolHolder_spawns;

    public Transform[] airPistol_Gunspawns;
    public Transform[] scoreAnim_SWP;



    public GameObject righthandedWeapon, righthandedWeaponPro;
    private GameObject dynamicGun;

    public GameObject gunGun_Holder;
    public bool isReloaded;


    public GameObject exitBtn;

    public GameObject animatedScore;
    public enum SkillLevel
    {
        Beginner,
        Intermediate,
        Professional
    }

    public SkillLevel playerSkillLevel;

    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        airPistolTarget.SetActive(false);
        gunGun_Holder.SetActive(false);
        exitBtn.SetActive(false);
        animatedScore.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setAirPistolGameData(int no)
    {

        //Close UI
        Arena_Manager.Instance.AirPistolUI.SetActive(false);

        exitBtn.SetActive(true);


        airPistolTarget.SetActive(true);
        //set the target
        airPistolTarget.transform.SetPositionAndRotation(airPistol_targetPoses[no].position, airPistol_targetPoses[no].rotation);
        
        //spawn the player with no movement
        Arena_Manager.Instance.playerController.GetComponent<CharacterController>().enabled = false;
        Arena_Manager.Instance.playerController.GetComponent<LocomotionManager>().enabled = false;

        Arena_Manager.Instance.arena_player.transform.SetPositionAndRotation(airPistol_Playerspawns[no].position, airPistol_Playerspawns[no].rotation);

        //set score anim pos

        animatedScore.transform.SetPositionAndRotation(scoreAnim_SWP[no].position, scoreAnim_SWP[no].rotation);
        //spawn the Air Pistol 
        respawnNewWeapon(no);
    }

    public void respawnNewWeapon(int pos)
    {
        gunGun_Holder.SetActive(true);

        gunGun_Holder.transform.SetLocalPositionAndRotation(airPistolHolder_spawns[pos].position, airPistolHolder_spawns[pos].rotation);


            if (LocalUserDataManager.Instance.isRightHand == true)
            {
                if(playerSkillLevel == SkillLevel.Professional)
                {
                    dynamicGun = Instantiate(righthandedWeaponPro, airPistol_Gunspawns[pos].position, airPistol_Gunspawns[pos].rotation);
                }
                else
                {
                    dynamicGun = Instantiate(righthandedWeapon, airPistol_Gunspawns[pos].position, airPistol_Gunspawns[pos].rotation);
                }

            }
            else
            {
                //dynamicGun = Instantiate(righthandedWeapon, airPistol_Gunspawns[pos].position, airPistol_Gunspawns[pos].rotation);
            }



        gunGun_Holder.GetComponent<SnapZone>().SetHeldItem(dynamicGun.GetComponent<Grabbable>());



    }

    public void ExitToArena()
    {
        Arena_Manager.Instance.playerController.GetComponent<CharacterController>().enabled = false;
        Arena_Manager.Instance.playerController.GetComponent<LocomotionManager>().enabled = false;

        Arena_Manager.Instance.arena_player.transform.SetPositionAndRotation(Arena_Manager.Instance.airPistolSpawnPoint.position, Arena_Manager.Instance.airPistolSpawnPoint.rotation);

        Arena_Manager.Instance.playerController.GetComponent<CharacterController>().enabled = true;
        Arena_Manager.Instance.playerController.GetComponent<LocomotionManager>().enabled = true;

        exitBtn.SetActive(false);
        Arena_Manager.Instance.AirPistolUI.SetActive(true);


    }

    IEnumerator playAnim()
    {
        yield return new WaitForSeconds(0.2f);
        animatedScore.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        animatedScore.SetActive(false);
        yield return new WaitForSeconds(1f);
        
       
    }
}
