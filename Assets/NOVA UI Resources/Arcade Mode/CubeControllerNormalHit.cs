using UnityEngine;

public class CubeControllerNormalHit : MonoBehaviour
{
    public int cubeValue = 40; // This is the variable we want to set to 40.
    public GameObject particles;
    public GameObject CubeObject;

    public GameObject TextAnimation;
    // Start is called before the first frame update
    void Start()
    {

        // You can access cubeValue in your code as needed.
      //  Debug.Log("Cube Value: " + cubeValue);

        TextAnimation.SetActive(false);
    }

    public void OnTriggerEnter(Collider other)
    {
       // Debug.Log(other.gameObject.name);

        if (other.gameObject.name.Contains("Projectile") == true)
        {
            ArcadeGameManager.instance.Hit = true;

            // Enable the Text Animator GameObject
            TextAnimation.SetActive(true);

            // Enable the particle system GameObject
            particles.SetActive(true);

            // Get the ParticleSystem component from the enabled GameObject and play it
            ParticleSystem particleSystem = particles.GetComponent<ParticleSystem>();
            if (particleSystem != null)
            {
                particleSystem.Play();
            }

            Debug.Log(gameObject.name);
            cubeValue += 25;
            ArcadeGameManager.instance.updatescore(cubeValue);

            //Disable Cube Object when Collide
            CubeObject.SetActive(false);
            other.gameObject.SetActive(false);
        }
    }
}
