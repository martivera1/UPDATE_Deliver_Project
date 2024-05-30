using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartManager : MonoBehaviour
{
    public AudioSource instructionAudioSource;  // Reference to the audio source
    public GameObject[] gameElements;  // References to game elements to enable after audio

    private bool gameStarted = false;

    void Start()
    {
        // Ensure game elements are initially disabled
        foreach (var element in gameElements)
        {
            element.SetActive(false);
        }

        // Start playing the instruction audio
        if (instructionAudioSource != null)
        {
            instructionAudioSource.Play();
        }
    }

    void Update()
    {
        // Check if the audio has finished playing and the game hasn't started yet
        if (!instructionAudioSource.isPlaying && !gameStarted)
        {
            // Enable the game elements
            foreach (var element in gameElements)
            {
                element.SetActive(true);
            }

            gameStarted = true;
        }
    }
}

