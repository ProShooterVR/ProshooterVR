using UnityEngine;
using TMPro;
using NovaSamples.Inventory;

public class CubeSpawner : MonoBehaviour
{
    public static CubeSpawner Instance;

    private void Awake()
    {
        Instance = this;
    }

    public GameObject[] cubePrefabs;
    public int maxObjectsToSpawn = 10;
    public float spawnInterval = 1.0f;
    public float destroyHeight = 10.0f;
    public TimerController timerController;

    public float[] speedOptions = { 1.0f, 2.0f, 3.0f };

    private int objectsSpawned = 0;
    private float spawnTimer = 0.0f;


    private bool canSpawn = true;

    void Start()
    {
        //TimerController.Instance.currentTime = TimerController.Instance.totalTime;
    }

    void Update()
    {
        if (TimerController.Instance.currentTime <= 0)
        {
            
            // Game time is over. Disable further spawning and disable all spawned objects.
            DisableSpawningAndObjects();
            return;
        }

        if (objectsSpawned >= maxObjectsToSpawn)
        {
            return;
        }

        //TimerController.Instance.currentTime -= Time.deltaTime;
        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0)
        {
            SpawnObject();
            spawnTimer = spawnInterval;
        }

        ObjectDestroyCheck();
    }

    void DisableSpawningAndObjects()
    {
        canSpawn = false; // Stop further spawning.

        // Disable all spawned objects.
        GameObject[] spawnedObjects = GameObject.FindGameObjectsWithTag("SpawnedObject");
        foreach (var obj in spawnedObjects)
        {
            obj.SetActive(false);
        }
        ArcadeGameUIManager.Instance.envSound.SetActive(false);

        ArcadeGameUIManager.Instance.endGamePopUP.SetActive(true);
        ArcadeGameUIManager.Instance.scorePanel.SetActive(false);
        RayManager.Instance.EnableRey();
        ArcadeGameUIManager.Instance.totalScoreText.text = ArcadeGameManager.instance.totalscore.ToString();
        
    
    }

    void SpawnObject()
    {
        ArcadeGameManager.instance.Hit = true;

        if (!canSpawn)
        {
            return;
        }

        // Randomly select one of the three cube prefabs.
        int randomCubeIndex = Random.Range(0, cubePrefabs.Length);
        GameObject selectedPrefab = cubePrefabs[randomCubeIndex];

        // Instantiate the selected cube prefab.
        GameObject spawnedObject = Instantiate(selectedPrefab, transform.position, Quaternion.identity);
        Rigidbody rb = spawnedObject.GetComponent<Rigidbody>();

        // Randomly select a direction for movement and a random speed.
        int randomDirection = Random.Range(0, 3); // 0: Diagonal Left, 1: Diagonal Right, 2: Y-axis only
        float randomSpeed = speedOptions[Random.Range(0, speedOptions.Length)];
        Vector3 forceDirection = Vector3.up; // Default to Y-axis only

        switch (randomDirection)
        {
            case 0: // Diagonal Left
                forceDirection = new Vector3(-1, 1, 0).normalized;
                break;
            case 1: // Diagonal Right
                forceDirection = new Vector3(1, 1, 0).normalized;
                break;
        }

        rb.AddForce(forceDirection * randomSpeed, ForceMode.VelocityChange);

        objectsSpawned++;
    }

    void ObjectDestroyCheck()
    {
        GameObject[] spawnedObjects = GameObject.FindGameObjectsWithTag("SpawnedObject");

        foreach (var obj in spawnedObjects)
        {
            if (obj.transform.position.y >= destroyHeight)
            {
                Destroy(obj);
                objectsSpawned--;
            }
        }
    }
}
