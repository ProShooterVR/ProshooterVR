using UnityEngine;
using TMPro;
public class targetscore : MonoBehaviour
{
   
    public TextMeshProUGUI debug,scoreTxt;
    public GameObject centerobj, newobjet;
    void OnCollisionEnter(Collision collision)
    {
        newobjet = collision.gameObject;
        //// Get the collision point in local space
        //Vector3 localCollisionPoint = transform.InverseTransformPoint(collision.contacts[0].point);

        //// Get the UV coordinates of the collision point on the cube's texture
        //Vector2 uv = new Vector2(localCollisionPoint.x + 0.5f, localCollisionPoint.y + 0.5f);

        //// Get the position relative to the center of the texture
        //Vector2 center = new Vector2(0.5f, 0.5f);
        //Vector2 relativePosition = uv - center;

        

        float distance = Vector3.Distance(centerobj.transform.position, newobjet.transform.position);

        float score = ((distance * 10000)/60)-10.9f;
        score = Mathf.Abs(score);
        
        // Print the relative position
        Debug.Log("Relative position: " + newobjet.transform.position + " : "+distance+"--"+ score.ToString("F2"));
        scoreTxt.text = score.ToString("F2"); ;
        debug.text = debug.text + "Relative position: " + newobjet.transform.position + "-- "+collision.gameObject.name + " [ Score : "+ score.ToString("F2")+"]\n";
    }
}

