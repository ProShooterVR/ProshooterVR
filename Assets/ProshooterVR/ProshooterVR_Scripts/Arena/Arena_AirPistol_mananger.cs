using BNG;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Nova;
using ProshooterVR;
public class Arena_AirPistol_mananger : MonoBehaviour
{
    public static Arena_AirPistol_mananger Instance;


    public GameObject airPistolTarget;

    public Transform[] airPistol_targetPoses;

    public Transform[] airPistol_Playerspawns;

    public Transform[] airPistolHolder_spawns;

    public Transform[] airPistol_Gunspawns;
    public Transform[] scoreAnim_SWP;

    public TextMeshPro[] lane_Scores;

    public GameObject[] scores_countDisp;
    public GameObject[] LanesUIDisp;


    public GameObject[] AirPistolTargets;

    public int noOfShotsFired;

    public Transform orgPos;

    public GameObject righthandedWeapon, righthandedWeaponPro;
    private GameObject dynamicGun;
    public GameObject scoreboardUI;

    public GameObject gunGun_Holder;
    public bool isReloaded;


    public GameObject exitBtn;

    public GameObject animatedScore;

    public TextMeshProUGUI fadeScore;
    public int laneChosen;
    public int playerScore;

    public float beginnerZ, IntrmZ, proZ;

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
        orgPos = Arena_Manager.Instance.arena_player.transform;
        resetScores();
        noOfShotsFired = 0;
        resetLanesUI();
    }

    // Update is called once per frame
    void Update()
    {
        if(noOfShotsFired > 10)
        {
            ResetGame();
        }
    }

    public void startGame()
    {
        //spawn the Air Pistol 
        respawnNewWeapon(laneChosen);
        noOfShotsFired = 0;

        LanesUIDisp[laneChosen].transform.Find("StartBtn").gameObject.SetActive(false);
        LanesUIDisp[laneChosen].transform.Find("ResetBtn").gameObject.SetActive(true);

    }
    public void ResetGame()
    {
        //spawn the Air Pistol 
       // respawnNewWeapon(laneChosen);
       // noOfShitsFired = 0;
       
        LanesUIDisp[laneChosen].transform.Find("StartBtn").gameObject.SetActive(true);
        LanesUIDisp[laneChosen].transform.Find("ResetBtn").gameObject.SetActive(false);
        resetScores();
    }

    public void choose5m()
    {
        AirPistolTargets[laneChosen].GetComponent<SmoothStartStopLerp>().StartMovement(beginnerZ);
        playerSkillLevel = SkillLevel.Beginner;
    }
    public void choose7m()
    {
        AirPistolTargets[laneChosen].GetComponent<SmoothStartStopLerp>().StartMovement(IntrmZ);
        playerSkillLevel = SkillLevel.Intermediate;

    }

    public void choose10m()
    {
        AirPistolTargets[laneChosen].GetComponent<SmoothStartStopLerp>().StartMovement(proZ);
        playerSkillLevel = SkillLevel.Professional;

    }

    public void setAirPistolGameData(int no)
    {

        laneChosen = no;
        lanesUIActivator(no);
        //Close UI
        Arena_Manager.Instance.AirPistolUI.SetActive(false);

        exitBtn.SetActive(true);


        airPistolTarget.SetActive(true);
        //set the target
        airPistolTarget.transform.SetPositionAndRotation(airPistol_targetPoses[no].position, airPistol_targetPoses[no].rotation);

        //spawn the player with no movement
        //spawn the player with no movement
        Arena_Manager.Instance.playerController.GetComponent<CharacterController>().enabled = false;
        Arena_Manager.Instance.playerController.GetComponent<LocomotionManager>().enabled = false;


        Arena_Manager.Instance.arena_player.transform.SetPositionAndRotation(airPistol_Playerspawns[no].localPosition, airPistol_Playerspawns[no].rotation);
        Arena_Manager.Instance.playerController.GetComponent<CharacterController>().enabled = true;
        Arena_Manager.Instance.playerController.GetComponent<LocomotionManager>().enabled = true;
        //set score anim pos

        animatedScore.transform.SetPositionAndRotation(scoreAnim_SWP[no].position, scoreAnim_SWP[no].rotation);
        
    }

    public void lanesUIActivator(int no)
    {
        for(int i = 0; i < LanesUIDisp.Length; i++)
        {
            if(i == no)
            {
                LanesUIDisp[i].SetActive(true);
            }
            else
            {
                LanesUIDisp[i].SetActive(false);
            }
        }
    }

    public void resetLanesUI()
    {
        for (int i = 0; i < LanesUIDisp.Length; i++)
        {
            LanesUIDisp[i].SetActive(false);
        }
    }

    public void reset_Scores_countDisp()
    {
        for(int i = 0; i < scoreboardUI.transform.childCount; i++)
        {
            for(int j = 0;j < scoreboardUI.transform.GetChild(i).childCount; j++)
            {
                scoreboardUI.transform.GetChild(i).GetChild(j).GetComponent<UIBlock2D>().Color = Color.white;
            }
        }
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

        isReloaded = true;

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

        dynamicGun = null;

        resetScores();
        resetLanesUI();
    }

    public IEnumerator playAnim()
    {
       // dynamicGun.transform.FindChild("");
        yield return new WaitForSeconds(0.2f);
        animatedScore.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        animatedScore.SetActive(false);
        yield return new WaitForSeconds(1f);
        
       
    }

    public void displayScore(int score)
    {
        playerScore = playerScore + score;
        lane_Scores[laneChosen].text = playerScore.ToString();

        scores_countDisp[laneChosen].transform.GetChild(noOfShotsFired).GetComponent<UIBlock2D>().Color = Color.green;
        
        noOfShotsFired++;
 

    }

    public void resetScores()
    {
        playerScore = 0;
        noOfShotsFired = 0;
        for (int i = 0; i < lane_Scores.Length; i++)
        {
            lane_Scores[i].text = "-";
        }
    }
}
