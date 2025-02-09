using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;
    private float spawnPosX = 25f;
    private Vector3 spawnPos;
    private float startDelay = 3;
    private float repeatRate = 3;
    private PlayerController playerControllerScript;

    // Start is called before the first frame update
    void Start()
    {
        spawnPos = new Vector3(spawnPosX, 0, 0);

        // Access public variables and methods from the playerController script
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();

        // Repeatedly call method SpawnObstacle to spawn obstacles in the game world
        InvokeRepeating("SpawnObstacle", startDelay, repeatRate);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SpawnObstacle()
    {
        // Generate a random index for the obstaclePrefabs array and whether to spawn 2 obstacles on top of each other
        int obstacleIndex = Random.Range(0, obstaclePrefabs.Length);
        int spawnMultipleObjects = Random.Range(0, 2);

        // Find the y component of the obstacle to be able to spawn obstacles on top of each other
        float obstacleHeight = obstaclePrefabs[obstacleIndex].GetComponent<BoxCollider>().size.y;
        Vector3 spawnPosSecondObject = new Vector3(spawnPosX, obstacleHeight, 0);

        // Spawn obstacles while game is not over
        if (playerControllerScript.gameOver == false)
        {
            Instantiate(obstaclePrefabs[obstacleIndex], spawnPos, obstaclePrefabs[obstacleIndex].transform.rotation);

            // If spawnMultipleObjects is 1, spawn 2 obstacles on top of each other
            if (spawnMultipleObjects == 1)
            {
                Instantiate(obstaclePrefabs[obstacleIndex], spawnPosSecondObject, obstaclePrefabs[obstacleIndex].transform.rotation);
            }
        }
    }
}
