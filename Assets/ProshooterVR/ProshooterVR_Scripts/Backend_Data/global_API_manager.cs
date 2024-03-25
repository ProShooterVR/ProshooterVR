using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace backend_management
{
    public class global_API_manager : MonoBehaviour
    {
        
        public static global_API_manager Instance;

        private void Awake()
        {
            Instance = this;
        }

        public enum env_param
        {
            prod,
            uat
        }

        public env_param env;

        private string prod_address, uat_address;

        /// <summary>
        /// API Declaration
        /// </summary>
        /// 

        public string createUserAPI = "/api/user/createuser";
        public string fetchUserProfileInfo = "/api/user/getuserinfobymetaid/";
        public string insertgamedata = "/api/user/insertgamedata";

        public string fetchUserSettings = "/api/user/getusersettings/";
        public string updateusersettings = "/api/user/updateusersettings/";

        /// <summary>
        /// Areana API LIST  -------------------------------    START    -------------------------------------------
        /// </summary>

        // ARENA AIR PISTOL API CALLS
        public string insertArenaGameData = "/api/user/insertarenagamedata";

        private string aP_arena_Daily_total_score_leaderboard = "/api/user/arena/daily/leaderboard/totalscore/10";
        private string aP_arena_Overall_total_score_leaderboard = "/api/user/arena/overall/leaderboard/totalscore/10";
        private string aP_arena_Daily_total_shots_leaderboard = "/api/user/arena/daily/leaderboard/totalshots/10";
        private string aP_arena_Overall_total_shots_leaderboard = "/api/user/arena/overall/leaderboard/totalshots/10";
        private string aP_arena_Daily_tenPointers_leaderboard = "/api/user/arena/daily/leaderboard/tenpointers/10";
        private string aP_arena_Overall_tenPointers_leaderboard = "/api/user/arena/overall/leaderboard/tenpointers/10";


        /// <summary>
        /// Areana API LIST  -------------------------------    END    -------------------------------------------
        /// </summary>



        string accessToken = "PROSHOOTERVR @$#PRO#$@";

        public string AP_arena_Daily_total_score_leaderboard { get => aP_arena_Daily_total_score_leaderboard; }
        public string AP_arena_Overall_total_score_leaderboard { get => aP_arena_Overall_total_score_leaderboard; set => aP_arena_Overall_total_score_leaderboard = value; }
        public string AP_arena_Daily_total_shots_leaderboard { get => aP_arena_Daily_total_shots_leaderboard; set => aP_arena_Daily_total_shots_leaderboard = value; }
        public string AP_arena_Overall_total_shots_leaderboard { get => aP_arena_Overall_total_shots_leaderboard; set => aP_arena_Overall_total_shots_leaderboard = value; }
        public string AP_arena_Daily_tenPointers_leaderboard { get => aP_arena_Daily_tenPointers_leaderboard; set => aP_arena_Daily_tenPointers_leaderboard = value; }
        public string AP_arena_Overall_tenPointers_leaderboard { get => aP_arena_Overall_tenPointers_leaderboard; set => aP_arena_Overall_tenPointers_leaderboard = value; }

        private void Start()
        {

           

            
            
        }

        public IEnumerator initAPIcalls()
        {
            prod_address = "http://15.206.116.210";
            uat_address = "http://54.201.154.149";

            if (env == env_param.prod)
            {
                initProd_APIs();
            }
            else if (env == env_param.uat)
            {
                initUAT_APIs();
            }
            // Initialization process, for example:
            yield return new WaitForSeconds(2); // Simulating initialization delay
            Debug.Log("Initialization of UAT APIs completed");
        }

        void initProd_APIs()
        {
            createUserAPI = prod_address + createUserAPI;
            fetchUserProfileInfo = prod_address + fetchUserProfileInfo;
            insertgamedata = prod_address + insertgamedata;
            fetchUserSettings = prod_address + fetchUserSettings;
            updateusersettings = prod_address + updateusersettings;

            insertArenaGameData = prod_address + insertArenaGameData;


            aP_arena_Daily_total_score_leaderboard = prod_address + aP_arena_Daily_total_score_leaderboard;
            aP_arena_Overall_total_score_leaderboard = prod_address + aP_arena_Overall_total_score_leaderboard;
            aP_arena_Daily_total_shots_leaderboard = prod_address + aP_arena_Daily_total_shots_leaderboard;
            aP_arena_Overall_total_shots_leaderboard = prod_address + aP_arena_Overall_total_shots_leaderboard;
            aP_arena_Daily_tenPointers_leaderboard = prod_address + aP_arena_Daily_tenPointers_leaderboard;
            aP_arena_Overall_tenPointers_leaderboard = prod_address + aP_arena_Overall_tenPointers_leaderboard;


        }

        void initUAT_APIs()
        {
            createUserAPI = uat_address + createUserAPI;
            fetchUserProfileInfo = uat_address + fetchUserProfileInfo;
            insertgamedata = uat_address + insertgamedata;
            fetchUserSettings = uat_address + fetchUserSettings;
            updateusersettings = uat_address + updateusersettings;

            insertArenaGameData = uat_address + insertArenaGameData;


            aP_arena_Daily_total_score_leaderboard = uat_address + aP_arena_Daily_total_score_leaderboard;
            aP_arena_Overall_total_score_leaderboard = uat_address + aP_arena_Overall_total_score_leaderboard;
            aP_arena_Daily_total_shots_leaderboard = uat_address + aP_arena_Daily_total_shots_leaderboard;
            aP_arena_Overall_total_shots_leaderboard = uat_address + aP_arena_Overall_total_shots_leaderboard;
            aP_arena_Daily_tenPointers_leaderboard = uat_address + aP_arena_Daily_tenPointers_leaderboard;
            aP_arena_Overall_tenPointers_leaderboard = uat_address + aP_arena_Overall_tenPointers_leaderboard;


        }
    }
}

