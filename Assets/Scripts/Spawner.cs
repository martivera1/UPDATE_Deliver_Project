using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    //public GameObject myPrefab;
    public GameObject lhsPrefab;
    public GameObject rhsPrefab;
    public GameObject cooperativeArrowPrefab;

    public AudioSource audioSource;
    public AudioClip arrowDestroyedSound;

    private Vector3 nextSpawnPosition;



    public Vector3[] leftSpawnPositions = {
    new Vector3(44.74f, -1.567744f, 26.0f),
    new Vector3(34.6f, -1.567744f, 35.0f),
    new Vector3(25.0f, -1.567744f, 24.4f),
    new Vector3(25.0f, -1.567744f, 34.8f),
    new Vector3(35.1f, -1.567744f, 34.9f),
    new Vector3(25.2f, -1.567744f, 44.8f),
    new Vector3(34.4f, -1.567744f, 54.3f),
    new Vector3(34.4f, -1.567744f, 64.3f),
    new Vector3(34.4f, -1.567744f, 74.3f),
    new Vector3(34.4f, -1.567744f, 84.7f)
};



    public Vector3[] rightSpawnPositions = {
    new Vector3(55.26f, -1.567744f, 26.0f), 
    new Vector3(65.4f, -1.567744f, 35.0f),  
    new Vector3(75.0f, -1.567744f, 24.4f),  
    new Vector3(75.0f, -1.567744f, 34.8f),  
    new Vector3(64.9f, -1.567744f, 34.9f),  
    new Vector3(74.8f, -1.567744f, 44.8f),  
    new Vector3(65.6f, -1.567744f, 54.3f),  
    new Vector3(65.6f, -1.567744f, 64.3f),  
    new Vector3(65.6f, -1.567744f, 74.3f),  
    new Vector3(65.6f, -1.567744f, 84.7f)   
};


    public Vector3[] spawnRotations = {
        new Vector3(90, 0, 0),
        new Vector3(90, 180, 0),
        new Vector3(90, 270, 0)
    };

    public Vector3[] coopSpawnPositions = {
        new Vector3(55.8f, -1.567f, 75.4f),
        new Vector3(75.0f, 0.0f, 75.4f),
        new Vector3(75.0f, 0.0f, 55.2f),
        new Vector3(74.5f, 0.0f, 34.9f),
        new Vector3(55.2f, 0.0f, 34.9f),
        new Vector3(35.4f, 0.0f, 35.2f),
        new Vector3(34.5f, -1.567f, 55.4f),
        new Vector3(34.5f, -1.567f, 74.1f),
        new Vector3(55.8f, -1.567f, 75.4f)
    };


    public Vector3[] left2SpawnPositions = {
    new Vector3(75.1f, 0.0f, 75.4f),
    new Vector3(65.1f, 0.0f, 65.4f),
    new Vector3(55.1f, 0.0f, 55.4f),
    new Vector3(45.1f, 0.0f, 45.4f),
    new Vector3(35.1f, 0.0f, 35.4f),
    new Vector3(25.1f, 0.0f, 25.4f),
    new Vector3(15.1f, 0.0f, 15.4f),
    new Vector3(5.1f, 0.0f, 5.4f),
    new Vector3(-4.9f, 0.0f, -4.6f),
    new Vector3(-14.9f, 0.0f, -14.6f)
};

    public Vector3[] right2SpawnPositions = {
    new Vector3(35.3f, 0.0f, 75.4f),
    new Vector3(45.3f, 0.0f, 65.4f),
    new Vector3(55.3f, 0.0f, 55.4f),
    new Vector3(65.3f, 0.0f, 45.4f),
    new Vector3(75.3f, 0.0f, 35.4f),
    new Vector3(85.3f, 0.0f, 25.4f),
    new Vector3(95.3f, 0.0f, 15.4f),
    new Vector3(105.3f, 0.0f, 5.4f),
    new Vector3(115.3f, 0.0f, -4.6f),
    new Vector3(125.3f, 0.0f, -14.6f)
};


    private int currentLeftSpawnIndex = 0;
    private int currentRightSpawnIndex = 0;
    private int currentCoopSpawnIndex = 0;
    private int currentRotationIndex = 0;

    private bool spawnCoopArrowsOnly = false;
    private bool second_phase = false;
    private bool first_phase = true;


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


            if (currentLeftSpawnIndex >= leftSpawnPositions.Length)
            {
                return;
            }



            if (second_phase)
            {
                spawnPosition = left2SpawnPositions[currentLeftSpawnIndex];
                //currentLeftSpawnIndex = (currentLeftSpawnIndex + 1) % leftSpawnPositions.Length; <-- perq mai s'acabin de generar fletxes
                currentLeftSpawnIndex++;
                prefab = lhsPrefab;
            }

            if (first_phase)
            {
                spawnPosition = leftSpawnPositions[currentLeftSpawnIndex];
                //currentLeftSpawnIndex = (currentLeftSpawnIndex + 1) % leftSpawnPositions.Length; <-- perq mai s'acabin de generar fletxes
                currentLeftSpawnIndex++;
                prefab = lhsPrefab;

            }
            
        }
        else if (side == "RHS") // RHS
        {
            if (currentRightSpawnIndex >= rightSpawnPositions.Length)
            {
                return;
            }

            if (second_phase)
            {
                spawnPosition = right2SpawnPositions[currentRightSpawnIndex];
                //currentRightSpawnIndex = (currentRightSpawnIndex + 1) % rightSpawnPositions.Length;
                currentRightSpawnIndex++;
                prefab = rhsPrefab;
            }

            if (first_phase)
            {
                spawnPosition = rightSpawnPositions[currentRightSpawnIndex];
                //currentRightSpawnIndex = (currentRightSpawnIndex + 1) % rightSpawnPositions.Length;
                currentRightSpawnIndex++;
                prefab = rhsPrefab;
            }
        }

        else // COOP
        {
            
            
            spawnPosition = coopSpawnPositions[currentCoopSpawnIndex];
            //currentCoopSpawnIndex = (currentCoopSpawnIndex + 1) % coopSpawnPositions.Length ;
            currentCoopSpawnIndex++;
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

        audioSource.PlayOneShot(arrowDestroyedSound);
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
            if (currentCoopSpawnIndex >= coopSpawnPositions.Length)
            {
                spawnCoopArrowsOnly = false;
                first_phase = false;
                // Reset the indices for LHS and RHS arrows
                currentLeftSpawnIndex = 0;
                currentRightSpawnIndex = 0;

                second_phase = true;

                // Start spawning LHS and RHS arrows again
                SpawnArrow("LHS");
                SpawnArrow("RHS");
            }
        }

        
       
    }

    

}
