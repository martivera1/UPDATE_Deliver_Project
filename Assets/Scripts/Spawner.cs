using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    //public GameObject myPrefab;
    public GameObject lhsPrefab;
    public GameObject rhsPrefab;
    public GameObject cooperativeArrowPrefab;
    //public Vector3 spawnPosition = new Vector3(75.0f, 0.0f, 25.0f);
    //public Vector3 spawnRotation = new Vector3(100, 0, 0);

    private Vector3 nextSpawnPosition;



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
        new Vector3(65.6f, -1.567744f, 84.7f)
    };
    public Vector3[] spawnRotations = {
        new Vector3(90, 0, 0),
        new Vector3(90, 180, 0),
        new Vector3(90, 270, 0)
    };

    public Vector3[] coopSpawnPositions = {
        new Vector3(50.0f, -1.567f, 70.0f),
        new Vector3(50.0f, 0.0f, 39.0f),
        new Vector3(65.2f,0.0f, 15.8f),
        new Vector3(80.0f, 0.0f, 40.5f),
        new Vector3(20.4f, 0.0f, 40.5f)

    };


    private int currentLeftSpawnIndex = 0;
    private int currentRightSpawnIndex = 0;
    private int currentCoopSpawnIndex = 0;
    private int currentRotationIndex = 0;

    private bool spawnCoopArrowsOnly = false;


    // Event for arrow destruction
    public static event System.Action<string> OnArrowDestroyed;

    // Start is called before the first frame update
    void Start()
    {
        Collision_with_arrow.OnArrowDestroyed += HandleArrowDestroyed;
        ScoreManager.OnCooperativePlayStart += StartCooperativePlay;

        SpawnArrow("LHS");
        SpawnArrow("RHS");
        //SpawnArrow("COOP");

    }


    void OnDestroy()
    {
        // Unsubscribe from the event when this object is destroyed to prevent memory leaks
        Collision_with_arrow.OnArrowDestroyed -= HandleArrowDestroyed;
        ScoreManager.OnCooperativePlayStart -= StartCooperativePlay;
    }


    void SpawnArrow(string side)
    {
        Vector3 spawnPosition;
        GameObject prefab;

        if (spawnCoopArrowsOnly)
        {
            side = "COOP";
        }

        if (side == "LHS")
        {
            //if (currentLeftSpawnIndex >= leftSpawnPositions.Length)
            //{
            //    CheckCoopArrowsCondition();
            //    return;
            //}


            if (currentLeftSpawnIndex >= leftSpawnPositions.Length)
            {
                return;
            }

            spawnPosition = leftSpawnPositions[currentLeftSpawnIndex];
            //currentLeftSpawnIndex = (currentLeftSpawnIndex + 1) % leftSpawnPositions.Length; <-- perq mai s'acabin de generar fletxes
            currentLeftSpawnIndex++;
            prefab = lhsPrefab;
        }
        else if (side == "RHS") // RHS
        {
            //if (currentRightSpawnIndex >= rightSpawnPositions.Length)
            //{
            //    CheckCoopArrowsCondition();
            //    return;
            //}

            if (currentRightSpawnIndex >= rightSpawnPositions.Length)
            {
                return;
            }

            spawnPosition = rightSpawnPositions[currentRightSpawnIndex];
            //currentRightSpawnIndex = (currentRightSpawnIndex + 1) % rightSpawnPositions.Length;
            currentRightSpawnIndex++;
            prefab = rhsPrefab;
        }

        else // COOP
        {
            //if (currentCoopSpawnIndex >= coopSpawnPositions.Length)
            //{
            //    return;
            //}

            spawnPosition = coopSpawnPositions[currentCoopSpawnIndex];
            currentCoopSpawnIndex = (currentCoopSpawnIndex + 1) % coopSpawnPositions.Length ;
            prefab = cooperativeArrowPrefab;
        }

        Vector3 spawnRotation = spawnRotations[currentRotationIndex];
        Quaternion rotation = Quaternion.Euler(spawnRotation);
        Instantiate(prefab, spawnPosition, rotation);
        Debug.Log("Spawned a " + side + " arrow at " + spawnPosition);

        currentRotationIndex = (currentRotationIndex + 1) % spawnRotations.Length;
    }

   

    void StartCooperativePlay()
    {
        Debug.Log("Received cooperative play start event");
        spawnCoopArrowsOnly = true;
        
        SpawnArrow("COOP");
    }



    void HandleArrowDestroyed(string side)
    {

    
        if (side == "LHS" && !spawnCoopArrowsOnly)
        {
                SpawnArrow("LHS");

        }

        else if(side == "RHS" && !spawnCoopArrowsOnly)
        {
            SpawnArrow("RHS");
        }

        else if (spawnCoopArrowsOnly)
        {
            SpawnArrow("COOP");
            return;
        }

        
       
    }

    

}
