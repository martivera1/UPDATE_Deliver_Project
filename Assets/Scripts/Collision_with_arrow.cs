using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision_with_arrow : MonoBehaviour
{
    // Define a delegate with a string parameter for side information
    public delegate void ArrowDestroyedHandler(string side);

    // Define an event using the delegate
    public static event ArrowDestroyedHandler OnArrowDestroyed;

    private HashSet<int> playersCollided = new HashSet<int>();

    /*void OnCollisionEnter(Collision collision)
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
    }*/

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Arrow"))
        {
            string side = collision.gameObject.GetComponent<ArrowSide>().side;

            if (side == "COOP")
            {
                PlayerController playerController = collision.gameObject.GetComponent<PlayerController>(); //original q funciona bé
                //PlayerController playerController = collision.collider.GetComponent<PlayerController>(); chat nou
                if (playerController != null)
                {
                    playersCollided.Add(playerController.playerNumber);

                    if (playersCollided.Count == 2)
                    {
                        OnArrowDestroyed?.Invoke("COOP");
                        Destroy(collision.gameObject);
                    }
                }
            }
            else
            {
                OnArrowDestroyed?.Invoke(side);

                //PlayerController playerController = GetComponent<playerController>(); //original

                PlayerController playerController = collision.collider.GetComponent<PlayerController>(); //chat

                if (playerController != null)
                {
                    ScoreManager.AddScore(playerController.playerNumber, 100);
                }

                Destroy(collision.gameObject);
            }
        }
    }




    /*void OnTriggerEnter(Collider other)
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
    }*/

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Arrow"))
        {
            string side = other.gameObject.GetComponent<ArrowSide>().side;

            if (side == "COOP")
            {
                PlayerController playerController = other.gameObject.GetComponent<PlayerController>(); //original
                //PlayerController playerController = collision.collider.GetComponent<PlayerController>(); xat
                if (playerController != null)
                {
                    playersCollided.Add(playerController.playerNumber);

                    if (playersCollided.Count == 2)
                    {
                        OnArrowDestroyed?.Invoke("COOP");
                        Destroy(other.gameObject);
                    }
                }
            }
            else
            {
                OnArrowDestroyed?.Invoke(side);

                PlayerController playerController = GetComponent<PlayerController>(); //original
                //PlayerController playerController = collision.collider.GetComponent<PlayerController>(); xat

                if (playerController != null)
                {
                    ScoreManager.AddScore(playerController.playerNumber, 100);
                }

                Destroy(other.gameObject);
            }
        }
    }
}
