using BNG;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Nova;
using ProshooterVR;
using SimpleJSON;

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
    public int noOfShotsAllowed;
    public float[] shotScores;

    public Transform orgPos;

    public GameObject righthandedWeaponBegin, righthandedWeaponIntrm, righthandedWeaponPro;
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


    public GameObject beginBtn, interMBtn, proBtn;

    public GameObject AP_arenaDaily_totalscore_leaderboardParent, AP_arenaOverall_totalscore_leaderboardParent;
    public GameObject AP_arenaDaily_totalshots_leaderboardParent, AP_arenaOverall_totalshots_leaderboardParent;
    public GameObject AP_arenaDaily_tens_leaderboardParent, AP_arenaOverall_tens_leaderboardParent;


    public enum SkillLevel
    {
        Beginner,
        Intermediate,
        Professional
    }

    public SkillLevel playerSkillLevel;
    public string selectedSkillLevel;

    public GameObject startBtn, resetBtn;

    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        noOfShotsAllowed = 10;
        airPistolTarget.SetActive(false);
        gunGun_Holder.SetActive(false);
        exitBtn.SetActive(false);
        animatedScore.SetActive(false);
        orgPos = Arena_Manager.Instance.arena_player.transform;
        resetScores();
        noOfShotsFired = 0;
        resetLanesUI();
        reset_Scores_countDisp();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startGame()
    {
        
        //spawn the Air Pistol 
        respawnNewWeapon(laneChosen);
        noOfShotsFired = 0;

        startBtn = LanesUIDisp[laneChosen].transform.GetChild(0).gameObject;
        resetBtn = LanesUIDisp[laneChosen].transform.GetChild(1).gameObject;

        startBtn.SetActive(false);
        resetBtn.SetActive(true);

        lockButtons();

    }

    
    public void ResetGame()
    {
        //spawn the Air Pistol 
        

        noOfShotsFired = 0;
        shotScores = new float[noOfShotsAllowed];
        gunGun_Holder.SetActive(false);
        gunGun_Holder.GetComponent<SnapZone>().clearHeldItem();


        Destroy(dynamicGun);
        startBtn.SetActive(true);
        resetBtn.SetActive(false);
        resetScores();
        reset_Scores_countDisp();
        unlockButtons();
    }

    

    public void choose5m()
    {
        AirPistolTargets[laneChosen].GetComponent<SmoothStartStopLerp>().StartMovement(beginnerZ);
        playerSkillLevel = SkillLevel.Beginner;
        selectedSkillLevel = "amateur";


    }
    public void choose7m()
    {
        AirPistolTargets[laneChosen].GetComponent<SmoothStartStopLerp>().StartMovement(IntrmZ);
        playerSkillLevel = SkillLevel.Intermediate;
        selectedSkillLevel = "semi_pro";


    }

    public void choose10m()
    {
        AirPistolTargets[laneChosen].GetComponent<SmoothStartStopLerp>().StartMovement(proZ);
        playerSkillLevel = SkillLevel.Professional;
        selectedSkillLevel = "pro";


    }


    public void lockButtons()
    {
        switch (playerSkillLevel)
        {

            case SkillLevel.Beginner:
                beginBtn.GetComponent<Interactable>().enabled = false;
                beginBtn.GetComponent<HubUIButtonManager>().ButtonBorder.SetActive(true);
                proBtn.GetComponent<Interactable>().enabled = false;
                interMBtn.GetComponent<Interactable>().enabled = false;

                break;
            case SkillLevel.Intermediate:
                beginBtn.GetComponent<Interactable>().enabled = false;
                interMBtn.GetComponent<HubUIButtonManager>().ButtonBorder.SetActive(true);
                proBtn.GetComponent<Interactable>().enabled = false;
                interMBtn.GetComponent<Interactable>().enabled = false;
                break;
            case SkillLevel.Professional:
                beginBtn.GetComponent<Interactable>().enabled = false;
                proBtn.GetComponent<HubUIButtonManager>().ButtonBorder.SetActive(true);
                proBtn.GetComponent<Interactable>().enabled = false;
                interMBtn.GetComponent<Interactable>().enabled = false;
                break;
        }
    }

    public void unlockButtons()
    {
        beginBtn.GetComponent<Interactable>().enabled = true;
        proBtn.GetComponent<Interactable>().enabled = true;
        interMBtn.GetComponent<Interactable>().enabled = true;

        beginBtn.GetComponent<HubUIButtonManager>().ButtonBorder.SetActive(false);
        proBtn.GetComponent<HubUIButtonManager>().ButtonBorder.SetActive(false);
        interMBtn.GetComponent<HubUIButtonManager>().ButtonBorder.SetActive(false);


    }

    public void setAirPistolGameData(int no)
    {

        laneChosen = no;
        lanesUIActivator(no);
        beginBtn = LanesUIDisp[laneChosen].transform.GetChild(4).gameObject;
        interMBtn = LanesUIDisp[laneChosen].transform.GetChild(3).gameObject;
        proBtn = LanesUIDisp[laneChosen].transform.GetChild(2).gameObject;


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
        Arena_Manager.Instance.playerController.GetComponent<PlayerTeleport>().enabled = false;



        Arena_Manager.Instance.arena_player.transform.SetPositionAndRotation(airPistol_Playerspawns[no].localPosition, airPistol_Playerspawns[no].rotation);
        Arena_Manager.Instance.playerController.GetComponent<CharacterController>().enabled = false;
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
            for(int j = 0;j < scoreboardUI.transform.GetChild(i).GetChild(1).childCount; j++)
            {
                scoreboardUI.transform.GetChild(i).GetChild(1).GetChild(j).GetComponent<UIBlock2D>().Color = Color.white;
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
                else if(playerSkillLevel == SkillLevel.Intermediate)
                {
                    dynamicGun = Instantiate(righthandedWeaponIntrm, airPistol_Gunspawns[pos].position, airPistol_Gunspawns[pos].rotation);
                }
                else
                {
                    dynamicGun = Instantiate(righthandedWeaponBegin, airPistol_Gunspawns[pos].position, airPistol_Gunspawns[pos].rotation);

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

        Arena_Manager.Instance.arena_player.transform.SetPositionAndRotation(Arena_Manager.Instance.airPistolSpawnPoint.position, Arena_Manager.Instance.arena_player.transform.rotation);

        Arena_Manager.Instance.playerController.GetComponent<CharacterController>().enabled = true;
        Arena_Manager.Instance.playerController.GetComponent<LocomotionManager>().enabled = true;
        Arena_Manager.Instance.playerController.GetComponent<PlayerTeleport>().enabled = true;

        exitBtn.SetActive(false);
        Arena_Manager.Instance.AirPistolUI.SetActive(true);
        resetLanesUI();
        resetScores();
        reset_Scores_countDisp();
        dynamicGun = null;

        resetScores();
        resetLanesUI();
        ResetGame();
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
        shotScores[noOfShotsFired] = score;

        if (noOfShotsFired == 10)
        {
            StartCoroutine(GameComplete());
        }

    }

    IEnumerator GameComplete()
    {

        Areana_backendManager.Instance.SaveGameDataPistol();

        yield return new WaitForSeconds(2f);
        noOfShotsFired = 0;
        reset_Scores_countDisp();
        resetScores();
    }

    public void resetScores()
    {
        shotScores = new float[noOfShotsAllowed];
        playerScore = 0;
        noOfShotsFired = 0;
        for (int i = 0; i < lane_Scores.Length; i++)
        {
            lane_Scores[i].text = "-";
        }
    }
}
