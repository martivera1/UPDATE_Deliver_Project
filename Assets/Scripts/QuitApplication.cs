using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitApplication : MonoBehaviour
{
    // This method is called when another collider enters the trigger collider attached to the object this script is attached to
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object colliding with the button is tagged as "Player"
        if (other.CompareTag("Player"))
        {
            // Quit the application
            Application.Quit();

            // This is just for the editor to stop playing the game
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }
}

