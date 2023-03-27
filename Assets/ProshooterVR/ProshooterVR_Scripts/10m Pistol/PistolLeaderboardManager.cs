using Firebase;
using Firebase.Database;
using UnityEngine;
using UnityEditor;

public class PistolLeaderboardManager : MonoBehaviour
{
    DatabaseReference databaseRef;

    void Start()
    {
        FirebaseDatabase.GetInstance("https://proshootervr-d82e9.firebaseio.com");
        databaseRef = FirebaseDatabase.DefaultInstance.RootReference;
        Debug.Log("Database url" + databaseRef.Database.ToString());
    }

    public void SubmitScore(string playerName, int score)
    {
        Score newScore = new Score(playerName, score);
        string json = JsonUtility.ToJson(newScore);
        databaseRef.Child("scores").Push().SetRawJsonValueAsync(json);
    }
}

public class Score
{
    public string playerName;
    public int score;

    public Score(string playerName, int score)
    {
        this.playerName = playerName;
        this.score = score;
    }
}