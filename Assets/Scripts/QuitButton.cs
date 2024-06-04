using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitButton : MonoBehaviour
{
    // OnTriggerEnter is called when another collider enters the trigger collider attached to this GameObject
    void OnTriggerEnter(Collider other)
    {
        // Check if the collider entering the trigger is tagged as "Player"
        if (other.CompareTag("Player"))
        {
            // Call the QuitGame method to quit the application
            QuitGame();
        }
    }

    // Method to quit the application
    void QuitGame()
    {
        // For standalone builds (Windows, Mac, Linux)
#if !UNITY_EDITOR && (UNITY_STANDALONE || UNITY_WEBGL)
            Application.Quit();
#endif

        // For editor testing
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
