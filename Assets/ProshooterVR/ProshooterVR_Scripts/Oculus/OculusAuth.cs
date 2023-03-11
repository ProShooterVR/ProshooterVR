using UnityEngine;
using Oculus.Platform;
using Oculus.Platform.Models;



public class OculusAuth : MonoBehaviour
{
    private string userId;

    private void Start()
    {
        VRDebugManager.Instance.AddLog("Oculus Auth Started");
        Core.AsyncInitialize().OnComplete(OnInitializationCallback);
    }

    private void OnInitializationCallback(Message<PlatformInitialize> msg)
    {
        if (msg.IsError)
        {
            Debug.LogErrorFormat("Oculus: Error during initialization. Error Message: {0}",
                msg.GetError().Message);
            VRDebugManager.Instance.AddLog("Oculus: Error during initialization. Error Message: {0}"+
                msg.GetError().Message);
        }
        else
        {
            Entitlements.IsUserEntitledToApplication().OnComplete(OnIsEntitledCallback);
        }
    }

    private void OnIsEntitledCallback(Message msg)
    {
        if (msg.IsError)
        {
            Debug.LogErrorFormat("Oculus: Error verifying the user is entitled to the application. Error Message: {0}",
                msg.GetError().Message);
            VRDebugManager.Instance.AddLog("Oculus: Error verifying the user is entitled to the application. Error Message: {0}" +
                msg.GetError().Message);
        }
        else
        {
            GetLoggedInUser();
        }
    }

    private void GetLoggedInUser()
    {
        Users.GetLoggedInUser().OnComplete(OnLoggedInUserCallback);
    }

    private void OnLoggedInUserCallback(Message<User> msg)
    {
        if (msg.IsError)
        {
            Debug.LogErrorFormat("Oculus: Error getting logged in user. Error Message: {0}",
                msg.GetError().Message);
            VRDebugManager.Instance.AddLog("Oculus: Error getting logged in user. Error Message: {0}" +
                msg.GetError().Message);
        }
        else
        {
            userId = msg.Data.ID.ToString(); // do not use msg.Data.OculusID;
            
            VRDebugManager.Instance.AddLog("User Id By Oculus : "+ userId + " Name :" + msg.Data.OculusID);
            VRDebugManager.Instance.AddLog("call to add data 0k00");
            UserDataManager.Instance.userID = userId;
            UserDataManager.Instance.userName = msg.Data.OculusID;
            VRDebugManager.Instance.AddLog("call to add data 0ksdsddfdsf00");

            GetUserProof();
          
        }
    }

    private void GetUserProof()
    {
        VRDebugManager.Instance.AddLog("call to add data 1");
        Users.GetUserProof().OnComplete(OnUserProofCallback);
    }

    private void OnUserProofCallback(Message<UserProof> msg)
    {
        VRDebugManager.Instance.AddLog("call to add data 2 ");
        if (msg.IsError)
        {
            Debug.LogErrorFormat("Oculus: Error getting user proof. Error Message: {0}",
                msg.GetError().Message);
            VRDebugManager.Instance.AddLog("Oculus: Error getting user proof. Error Message: {0}" +
                msg.GetError().Message);
        }
        else
        {
            string oculusNonce = msg.Data.Value;
            VRDebugManager.Instance.AddLog("call to add data");
            UserDataManager.Instance.saveMeta();
            // Authentication can be performed here
            
        }
    }

    
}