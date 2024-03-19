using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arena_AirPistol_ScoreCalculator : MonoBehaviour
{
    public GameObject newobjet;
    float scoreOff;

    public GameObject targetend;
    public GameObject targetcenter, target;
    float Score;


    float targetscoreOff;

    public int scoreMx, DistMx, minScore;




    private void Start()
    {
        targetscoreOff = Vector3.Distance(targetcenter.transform.localPosition, targetend.transform.localPosition) / DistMx;

        Debug.Log("TargetOff :: " + targetscoreOff);
     
    }


    

    void OnCollisionEnter(Collision collision)
    {
        if ((collision.gameObject.name.Contains("BulletHole") == true))
        {

            newobjet = collision.gameObject;
            newobjet.transform.parent = target.transform;


            newobjet.transform.localScale = targetcenter.transform.localScale;
            newobjet.transform.localPosition = new Vector3(newobjet.transform.localPosition.x, newobjet.transform.localPosition.y, 0);


            float newDist = Vector3.Distance(targetcenter.transform.localPosition, newobjet.transform.localPosition);
            Debug.Log("Dist :: " + newDist);


            float Score = ((newDist / targetscoreOff) / scoreMx) - 10.9f;


            Vector3 direction = (Vector2)targetcenter.transform.position - (Vector2)newobjet.transform.position;

            float angle = Vector2.Angle(Vector2.right, direction);
            if (direction.y < 0)
            {
                angle = 360 - angle;
            }


            Debug.Log(" angleeeee :: " + angle);

            Score = -Score;

            Debug.Log("Pre score :: " + Score);

            if (Score < minScore)
            {
                Score = 0;
            }
            Debug.Log("Post score :: " + Score);

            Score = Mathf.Round(Score * 100f) / 100f;

            int finalScore = (int)Score;

            Arena_AirPistol_mananger.Instance.fadeScore.text = finalScore.ToString();
            StartCoroutine(Arena_AirPistol_mananger.Instance.playAnim());

            Arena_AirPistol_mananger.Instance.displayScore(finalScore);


        }
    }
}
