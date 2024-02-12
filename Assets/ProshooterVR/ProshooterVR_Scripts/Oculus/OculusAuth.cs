using UnityEngine;
using Oculus.Platform;
using Oculus.Platform.Models;
using ProshooterVR;


public class OculusAuth : MonoBehaviour
{
    private string userId;

    private void Start()
    {
        Core.AsyncInitialize().OnComplete(OnInitializationCallback);
    }

    private void OnInitializationCallback(Message<PlatformInitialize> msg)
    {
        if (msg.IsError)
        {
            Debug.LogErrorFormat("Oculus: Error during initialization. Error Message: {0}",
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
        }
        else
        {
            userId = msg.Data.ID.ToString(); // do not use msg.Data.OculusID;
                                             //Debug.Log("URL=="+msg.Data.SmallImageUrl);

            LocalUserDataManager.Instance.metaID = userId;
            LocalUserDataManager.Instance.meta_username = msg.Data.OculusID;
            LocalUserDataManager.Instance.metauser_profileImage_url = msg.Data.ImageURL;
           

            DBAPIManagerNew.Instance.getProfileData(LocalUserDataManager.Instance.metaID);

            //Debug.Log("ID : " + LocalUserDataManager.Instance.metaID + " | Name : " + LocalUserDataManager.Instance.meta_username+"| URL"+ LocalUserDataManager.Instance.metauser_profileImage_url);
            HUB_UIManager.Instance.userNameTxtMainMenu.text = msg.Data.OculusID;
            DBAPIManagerNew.Instance.Initialise_BackendDAta(); 
            GetUserProof();
        }
    }

    private void GetUserProof()
    {
        Users.GetUserProof().OnComplete(OnUserProofCallback);
    }

    private void OnUserProofCallback(Message<UserProof> msg)
    {
        if (msg.IsError)
        {
            Debug.LogErrorFormat("Oculus: Error getting user proof. Error Message: {0}",
                msg.GetError().Message);
        }
        else
        {
            string oculusNonce = msg.Data.Value;
            
            

            // Authentication can be performed here

        }
    }

    
}