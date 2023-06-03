using UnityEngine;
using TMPro;
using System;

public class targetscoreRifle : MonoBehaviour
{
   
    public TextMeshProUGUI debug;
    public GameObject newobjet;
    float scoreOff;

    public GameObject targetend;
    public GameObject targetcenter, target;
    float Score;
 
   
    float targetscoreOff;




    private void Start()
    {
        targetscoreOff = Vector3.Distance(targetcenter.transform.localPosition, targetend.transform.localPosition) / 100;
        
        Debug.Log("TargetOff :: " + targetscoreOff);
    }

    void OnCollisionEnter(Collision collision)
    {
         if((collision.gameObject.name.Contains("BulletHole") == true))
         {

            newobjet = collision.gameObject;
            newobjet.transform.parent = target.transform;


            newobjet.transform.localScale = targetcenter.transform.localScale;
            newobjet.transform.localPosition = new Vector3(newobjet.transform.localPosition.x, newobjet.transform.localPosition.y, 0);


            float newDist = Vector3.Distance(targetcenter.transform.localPosition, newobjet.transform.localPosition);
            Debug.Log("Dist :: " + newDist);

             
            float Score = ((newDist / targetscoreOff) / 10) - 10.9f;
            
           
            Vector3 direction = (Vector2)targetcenter.transform.position - (Vector2)newobjet.transform.position;

            float angle = Vector2.Angle(Vector2.right, direction);
            if (direction.y < 0)
            {
                angle = 360 - angle;
            }


            Debug.Log(" angleeeee :: " + angle);

            Score = -Score;
             
            GunGameManeger.Instance.shotFired(newobjet.transform.localPosition, Score, angle);
            

            Debug.Log("score :: " + Score);


            // Calculate the angle of the direction
            

            

        } 
    }
}

