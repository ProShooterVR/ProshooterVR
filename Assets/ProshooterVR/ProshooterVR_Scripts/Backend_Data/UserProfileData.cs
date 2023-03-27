using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserProfileData
{
    public string metaUserId;
    public string metaUserName;
    public float timeSpent;
    public int personalAmaBest;
    public int personalSemiProBest;
    public int personalProBest;


    public UserProfileData(string mUserid, string mUserName)
    {
        this.metaUserId = mUserid;
        this.metaUserName = mUserName;
    }

    public UserProfileData(float sessionTime)
    {
        this.timeSpent = sessionTime;
    }

    public UserProfileData(int gPersonalAmaBest, int gPersonalSemiProBest, int gPersonalProBest)
    {
        this.personalAmaBest = gPersonalAmaBest;
        this.personalAmaBest = gPersonalSemiProBest;
        this.personalAmaBest = gPersonalProBest;
    }
    public UserProfileData(string mUserid, string mUserName, int gPersonalAmaBest, int gPersonalSemiProBest, int gPersonalProBest)
    {
        this.metaUserId = mUserid;
        this.metaUserName = mUserName;
        this.personalAmaBest = gPersonalAmaBest;
        this.personalAmaBest = gPersonalSemiProBest;
        this.personalAmaBest = gPersonalProBest;

    }

    public Dictionary<string, object> ToDictionary()
    {
        Dictionary<string, object> result = new Dictionary<string, object>();
        result["MetaUserID"] = metaUserId;
        result["MetaUserName"] = metaUserName;
        result["TotalTimeSpent"] = timeSpent;
        result["UserAmatureBest"] = personalAmaBest;
        result["UserSemiProBest"] = personalSemiProBest;
        result["UserProBest"] = personalProBest;
        return result;
    }
}

