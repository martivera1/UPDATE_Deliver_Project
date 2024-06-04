using UnityEngine;
using UnityEngine.SceneManagement;

using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton: MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object is a finger touch
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player detected, changing scene...");

            // Load the target scene
            SceneManager.LoadScene("Scene");
        }
    }
}
