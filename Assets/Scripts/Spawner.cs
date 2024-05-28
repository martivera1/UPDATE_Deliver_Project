using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    //public GameObject myPrefab;
    public GameObject lhsPrefab;
    public GameObject rhsPrefab;
    public Vector3 spawnPosition = new Vector3(75.0f, 0.0f, 25.0f);
    public Vector3 spawnRotation = new Vector3(100, 0, 0);

    private Vector3 nextSpawnPosition;

    /*    public Vector3[] spawnPositions = {
            new Vector3(55.26f, -1.567744f, 26.0f),
            new Vector3(65.4f, -1.567744f, 35.0f),
            new Vector3(85.0f, -1.567744f, 25.0f),
            new Vector3(85.0f, -1.567744f, 34.8f),
            new Vector3(65.0f,-1.567744f, 54.2f),
            new Vector3(74.4f, -1.567744f, 65.1f),
            new Vector3(65.6f,-1.567744f, 64.3f),
            new Vector3(65.6f,-1.567744f, 84.7f)
        };*/


    public Vector3[] leftSpawnPositions = {
    new Vector3(44.74f, -1.567744f, 26.0f),
    new Vector3(34.6f, -1.567744f, 35.0f),
    new Vector3(15.0f, -1.567744f, 25.0f),
    new Vector3(15.0f, -1.567744f, 34.8f),
    new Vector3(35.0f, -1.567744f, 54.2f),
    new Vector3(25.6f, -1.567744f, 65.1f),
    new Vector3(34.4f, -1.567744f, 64.3f),
    new Vector3(34.4f, -1.567744f, 84.7f)
};

    public Vector3[] rightSpawnPositions = {
        new Vector3(55.26f, -1.567744f, 26.0f),
        new Vector3(65.4f, -1.567744f, 35.0f),
        new Vector3(85.0f, -1.567744f, 25.0f),
        new Vector3(85.0f, -1.567744f, 34.8f),
        new Vector3(65.0f, -1.567744f, 54.2f),
        new Vector3(74.4f, -1.567744f, 65.1f),
        new Vector3(65.6f, -1.567744f, 64.3f),
        new Vector3(65.6f, -1.567744f, 84.7f),
        new Vector3(65.6f, -1.567744f, 94.7f)
    };
    public Vector3[] spawnRotations = {
        new Vector3(90, 0, 0),
        //new Vector3(90, 90, 0),
        new Vector3(90, 180, 0),
        new Vector3(90, 270, 0)
    };

    /*    private int currentSpawnIndex = 0;
        private int currentRotationIndex = 0;*/

    private int currentLeftSpawnIndex = 0;
    private int currentRightSpawnIndex = 0;
    private int currentRotationIndex = 0;

    // Event for arrow destruction
    public static event System.Action<string> OnArrowDestroyed;

    // Start is called before the first frame update
    void Start()
    {
        Collision_with_arrow.OnArrowDestroyed += HandleArrowDestroyed;

        // Initialize the next spawn position
        //nextSpawnPosition = spawnPosition;
        /*SpawnArrow();*/

        SpawnArrow("LHS");
        SpawnArrow("RHS");

    }


    void OnDestroy()
    {
        // Unsubscribe from the event when this object is destroyed to prevent memory leaks
        Collision_with_arrow.OnArrowDestroyed -= HandleArrowDestroyed;
    }


    void SpawnArrow(string side)
    {
        Vector3 spawnPosition;
        GameObject prefab;

        if (side == "LHS")
        {
            spawnPosition = leftSpawnPositions[currentLeftSpawnIndex];
            currentLeftSpawnIndex = (currentLeftSpawnIndex + 1) % leftSpawnPositions.Length;
            prefab = lhsPrefab;
        }
        else // RHS
        {
            spawnPosition = rightSpawnPositions[currentRightSpawnIndex];
            currentRightSpawnIndex = (currentRightSpawnIndex + 1) % rightSpawnPositions.Length;
            prefab = rhsPrefab;
        }

        Vector3 spawnRotation = spawnRotations[currentRotationIndex];
        Quaternion rotation = Quaternion.Euler(spawnRotation);
        Instantiate(prefab, spawnPosition, rotation);

        currentRotationIndex = (currentRotationIndex + 1) % spawnRotations.Length;
    }
    


    void HandleArrowDestroyed(string side)
    {
        // Check if the destroyed arrow was on the LHS
        if (side == "LHS")
        {
            // Spawn a new arrow on the LHS
            SpawnArrow("LHS");
        }
        else
        {
            // Spawn a new arrow on the RHS
            SpawnArrow("RHS");
        }
    }


}
