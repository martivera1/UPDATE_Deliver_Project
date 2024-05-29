using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneTransition : MonoBehaviour
{
    // This is the name of the scene you want to load
    public string gameSceneName = "Scene";

    // This method is called when another collider enters the trigger collider attached to the object this script is attached to
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object colliding with the button is tagged as "Player"
        if (other.CompareTag("Player"))
        {
            // Load the specified game scene
            SceneManager.LoadScene(gameSceneName);
        }
    }
}
