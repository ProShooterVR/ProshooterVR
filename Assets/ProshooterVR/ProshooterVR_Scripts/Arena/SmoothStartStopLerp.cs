using UnityEngine;


namespace ProshooterVR
{
    public class SmoothStartStopLerp : MonoBehaviour
    {



        private float targetZ; // Target Z position in local space
        public float duration = 2.0f; // Duration of the movement in seconds

        private float startZ;
        private float startTime;
        private bool isMoving = false;

        void Start()
        {
            startZ = transform.localPosition.z; // Remember the start Z position in local space
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space)) // Use Space key to start the movement
            {
                //StartMovement(targetZ);
            }

            if (isMoving)
            {
                MoveObject();
            }
        }

        public void StartMovement(float targetZloc)
        {
            startTime = Time.time;
            startZ = transform.localPosition.z;
            isMoving = true;
            targetZ = targetZloc;
        }



        void MoveObject()
        {
            float elapsed = Time.time - startTime;
            float fraction = elapsed / duration;

            if (fraction < 1.0f)
            {
                // Calculate smooth fraction for smooth start and stop
                float smoothFraction = Mathf.SmoothStep(0.0f, 1.0f, fraction);

                // Calculate new Z position based on smooth fraction
                float newZ = Mathf.Lerp(startZ, targetZ, smoothFraction);

                // Update the GameObject's position in local space
                Vector3 newPosition = transform.localPosition;
                newPosition.z = newZ;
                transform.localPosition = newPosition;
            }
            else
            {
                // Ensure the object is exactly at the target position at the end
                Vector3 finalPosition = transform.localPosition;
                finalPosition.z = targetZ;
                transform.localPosition = finalPosition;
                isMoving = false; // Stop the movement
                Arena_AirPistol_mananger.Instance.airPistolTarget.transform.SetPositionAndRotation(Arena_AirPistol_mananger.Instance.airPistol_targetPoses[Arena_AirPistol_mananger.Instance.laneChosen].position, Arena_AirPistol_mananger.Instance.airPistol_targetPoses[Arena_AirPistol_mananger.Instance.laneChosen].rotation);

            }

        }
    }
}