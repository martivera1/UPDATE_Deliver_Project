using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject lhsPrefab;
    public GameObject rhsPrefab;
    public GameObject cooperativeArrowPrefab;

    public AudioSource audioSource;
    public AudioSource audioSource2;
    public AudioClip arrowDestroyedSound;
    public AudioClip tutorialSound;

    public AudioSource ObjectSong;
    public AudioClip song;

    private Vector3 nextSpawnPosition;



    public Vector3[] leftSpawnPositions = {
    new Vector3(44.74f, -1.567744f, 26.0f),
    new Vector3(34.6f, -1.567744f, 35.0f),
    new Vector3(25.0f, -1.567744f, 24.4f),
    new Vector3(25.0f, -1.567744f, 34.8f),
    new Vector3(35.1f, -1.567744f, 34.8f),
    new Vector3(25.0f, -1.567744f, 44.8f),
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
    new Vector3(64.9f, -1.567744f, 34.8f),  
    new Vector3(74.8f, -1.567744f, 44.8f),  
    new Vector3(65.6f, -1.567744f, 54.3f),  
    new Vector3(65.6f, -1.567744f, 64.3f),  
    new Vector3(65.6f, -1.567744f, 74.3f),  
    new Vector3(65.6f, -1.567744f, 84.7f)   
};


    //public Vector3[] spawnRotations = {
    //    new Vector3(90, 0, 0), //right
    //    new Vector3(90, 180, 0), //left
    //    new Vector3(90, 270, 0) //up
    //};

    public static Vector3[] coopSpawnPositions = {
        new Vector3(55.8f, -1.567f, 75.4f),
        new Vector3(75.0f, 0.0f, 75.4f),
        new Vector3(75.0f, 0.0f, 55.2f),
        new Vector3(75.0f, 0.0f, 34.9f),
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
    new Vector3(15.1f, 0.0f, 15.4f) //last one
    
};

    public Vector3[] right2SpawnPositions = {
    new Vector3(35.3f, 0.0f, 75.4f),
    new Vector3(45.3f, 0.0f, 65.4f),
    new Vector3(55.3f, 0.0f, 55.4f),
    new Vector3(65.3f, 0.0f, 45.4f),
    new Vector3(75.3f, 0.0f, 35.4f),
    new Vector3(85.3f, 0.0f, 25.4f),
    new Vector3(95.3f, 0.0f, 15.4f) //last one
    
};


    private int currentLeftSpawnIndex = 0;
    private int currentRightSpawnIndex = 0;
    public static int currentCoopSpawnIndex = 0;
    //private int currentRotationIndex = 0;

    private bool spawnCoopArrowsOnly = false;
    private bool second_phase = false;
    private bool first_phase = true;

    private Vector3 lastSpawnPosition = Vector3.zero; //for determine the rotation

    // Event for arrow destruction
    public static event System.Action<string> OnArrowDestroyed;

    

    void Start()
    {
        audioSource2.clip = tutorialSound;
        audioSource2.Play();

        StartCoroutine(WaitForAudioToEnd());
    }

    private IEnumerator WaitForAudioToEnd()
    {
        // Esperar hasta que el audio termine de reproducirse
        yield return new WaitWhile(() => audioSource2.isPlaying);


        ObjectSong.clip = song;
        ObjectSong.Play();
        // Una vez que el audio ha terminado, ejecutar el resto del código
        Collision_with_arrow.OnArrowDestroyed += HandleArrowDestroyed;
        ScoreManager.OnCooperativePlayStart += StartCooperativePlay;
        Collision_with_arrow.OnLastCoopArrowDestroyed += EndCooperativePlay;

        if (first_phase)
        {
            SpawnArrow("LHS");
            SpawnArrow("RHS");
        }
    }

    void OnDestroy()
    {
        
        Collision_with_arrow.OnArrowDestroyed -= HandleArrowDestroyed;
        ScoreManager.OnCooperativePlayStart -= StartCooperativePlay;
        Collision_with_arrow.OnLastCoopArrowDestroyed -= EndCooperativePlay;

    }


    void SpawnArrow(string side)
    {
        Vector3 spawnPosition=Vector3.zero;
        GameObject prefab=null;

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
                currentLeftSpawnIndex++;
                prefab = lhsPrefab;
            }

            if (first_phase)
            {
                spawnPosition = leftSpawnPositions[currentLeftSpawnIndex];
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
                currentRightSpawnIndex++;
                prefab = rhsPrefab;
            }

            if (first_phase)
            {
                spawnPosition = rightSpawnPositions[currentRightSpawnIndex];
                currentRightSpawnIndex++;
                prefab = rhsPrefab;
            }
        }

        else // COOP
        {
            
            
            spawnPosition = coopSpawnPositions[currentCoopSpawnIndex];
            currentCoopSpawnIndex++;
            prefab = cooperativeArrowPrefab;


        }

        //Rotations:
        Vector3 rotation = new Vector3(90,0,0);

        if (spawnPosition.x > lastSpawnPosition.x && spawnPosition.z > lastSpawnPosition.z)
        {
            rotation = new Vector3(90, 325, 0);
        }
        else if (spawnPosition.x > lastSpawnPosition.x && spawnPosition.z < lastSpawnPosition.z)
        {
            
            rotation = new Vector3(90, 45, 0);
        }
        else if (spawnPosition.x < lastSpawnPosition.x && spawnPosition.z > lastSpawnPosition.z)
        {
            
            rotation = new Vector3(90, 225, 0);
        }
        else if (spawnPosition.x < lastSpawnPosition.x && spawnPosition.z < lastSpawnPosition.z)
        {
            
            rotation = new Vector3(90, 135, 0);
        }

        else if (spawnPosition.x == lastSpawnPosition.x && spawnPosition.z > lastSpawnPosition.z)
        {
            rotation = new Vector3(90, 270, 0);
        }

        else if (spawnPosition.x == lastSpawnPosition.x && spawnPosition.z < lastSpawnPosition.z)
        {
            rotation = new Vector3(90, 90, 0);
        }

        else if (spawnPosition.x > lastSpawnPosition.x && spawnPosition.z == lastSpawnPosition.z)
        {
            rotation = new Vector3(90, 0, 0);
        }

        else if (spawnPosition.x < lastSpawnPosition.x && spawnPosition.z == lastSpawnPosition.z)
        {
            rotation = new Vector3(90, 180, 0);
        }






        //Vector3 spawnRotation = spawnRotations[currentRotationIndex];
        Quaternion finalrotation = Quaternion.Euler(rotation);
        Instantiate(prefab, spawnPosition, finalrotation);
        Debug.Log("Spawned a " + side + " arrow at " + spawnPosition);
        lastSpawnPosition = spawnPosition;

        //currentRotationIndex = (currentRotationIndex + 1) % spawnRotations.Length;
    }

   

    void StartCooperativePlay()
    {
        Debug.Log("Received cooperative play start event");
        spawnCoopArrowsOnly = true;
        
        SpawnArrow("COOP");
    }

    void EndCooperativePlay()
    {
        spawnCoopArrowsOnly = false;
        second_phase = true;
        currentLeftSpawnIndex = 0;
        currentRightSpawnIndex = 0;
        SpawnArrow("LHS");
        SpawnArrow("RHS");
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

               

            }
        }

        
       
    }

    

}
