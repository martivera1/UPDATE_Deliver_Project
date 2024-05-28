using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision_with_arrow : MonoBehaviour
{
    // Define a delegate with a string parameter for side information
    public delegate void ArrowDestroyedHandler(string side);

    // Define an event using the delegate
    public static event ArrowDestroyedHandler OnArrowDestroyed;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Arrow"))
        {
            // Extract the side information from the arrow object
            string side = collision.gameObject.GetComponent<ArrowSide>().side;

            
            OnArrowDestroyed?.Invoke(side);

            PlayerController playerController = GetComponent<PlayerController>();
            if (playerController != null)
            {
                ScoreManager.AddScore(playerController.playerNumber, 100);
            }

            // Destroy the arrow
            Destroy(collision.gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Arrow"))
        {
            // Extract the side information from the arrow object, assuming it has a Side component
            string side = other.gameObject.GetComponent<ArrowSide>().side;

            // Invoke the event and pass the side information
            OnArrowDestroyed?.Invoke(side);

            PlayerController playerController = GetComponent<PlayerController>();
            if (playerController != null)
            {
                ScoreManager.AddScore(playerController.playerNumber, 100);
            }

            // Destroy the arrow
            Destroy(other.gameObject);
        }
    }
}
