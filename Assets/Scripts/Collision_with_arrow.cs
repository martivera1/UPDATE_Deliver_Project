using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision_with_arrow : MonoBehaviour
{
    // Define a delegate with a string parameter for side information
    public delegate void ArrowDestroyedHandler(string side);

    // Define an event using the delegate
    public static event ArrowDestroyedHandler OnArrowDestroyed;

    //private HashSet<int> playersCollided = new HashSet<int>();
    private HashSet<GameObject> player1Collided = new HashSet<GameObject>();
    private HashSet<GameObject> player2Collided = new HashSet<GameObject>();



    //==============================================ORIGINAL (NO COOP)==========================
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
    //==============================================ORIGINAL (NO COOP)==========================





    //void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Arrow"))
    //    {
    //        string side = collision.gameObject.GetComponent<ArrowSide>().side;

    //        //if (side == "COOP")
    //        //{
    //        //    //PlayerController playerController = collision.gameObject.GetComponent<PlayerController>(); //original q funciona bé
    //        //    PlayerController playerController = collision.collider.GetComponent<PlayerController>(); //aixo diu el xat perq vagi amb la coop
    //        //    if (playerController != null)
    //        //    {
    //        //        playersCollided.Add(playerController.playerNumber);

    //        //        if (playersCollided.Count == 2)
    //        //        {
    //        //            OnArrowDestroyed?.Invoke("COOP");
    //        //            Destroy(collision.gameObject);
    //        //        }
    //        //    }
    //        //}
    //        //else
    //        //{
    //            OnArrowDestroyed?.Invoke(side);

    //            //PlayerController playerController = GetComponent<playerController>(); //original

    //            PlayerController playerController = collision.collider.GetComponent<PlayerController>(); //chat

    //            if (playerController != null)
    //            {
    //                ScoreManager.AddScore(playerController.playerNumber, 100);
    //            }

    //            Destroy(collision.gameObject);
    //        //}
    //    }
    //}




    //==============================================ORIGINAL (NO COOP)==========================


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



    //==============================================ORIGINAL (NO COOP)==========================



    //void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Arrow"))
    //    {
    //        string side = other.gameObject.GetComponent<ArrowSide>().side;

    //        //if (side == "COOP")
    //        //{
    //        //    PlayerController playerController = GetComponent<PlayerController>(); 
    //        //    //PlayerController playerController = other.gameObject.GetComponent<PlayerController>(); //original
    //        //    //PlayerController playerController = collision.collider.GetComponent<PlayerController>(); xat
    //        //    if (playerController != null)
    //        //    {
    //        //        playersCollided.Add(playerController.playerNumber);

    //        //        if (playersCollided.Count == 2)
    //        //        {
    //        //            OnArrowDestroyed?.Invoke("COOP");
    //        //            Destroy(other.gameObject);
    //        //        }
    //        //    }
    //        //}
    //        //else
    //        //{
    //            OnArrowDestroyed?.Invoke(side);

    //            PlayerController playerController = GetComponent<PlayerController>(); //original (other added)
    //            //PlayerController playerController = collision.collider.GetComponent<PlayerController>(); xat

    //            if (playerController != null)
    //            {
    //                ScoreManager.AddScore(playerController.playerNumber, 100);
    //            }

    //            Destroy(other.gameObject);
    //        //}
    //    }
    //}


    public void PlayerCollidedWithCoopArrow(int playerNumber, GameObject arrow)
    {
        if (playerNumber == 1)
        {
            player1Collided.Add(arrow);
        }
        else if (playerNumber == 2)
        {
            player2Collided.Add(arrow);
        }

        if (player1Collided.Contains(arrow) && player2Collided.Contains(arrow))
        {
            OnArrowDestroyed?.Invoke("COOP");
            Destroy(arrow);
            player1Collided.Remove(arrow);
            player2Collided.Remove(arrow);
        }
    }


    void OnCollisionEnter(Collision collision)
    {
        HandleCollision(collision.gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        HandleCollision(other.gameObject);
    }

    void HandleCollision(GameObject arrow)
    {
        if (arrow.CompareTag("Arrow"))
        {
            string side = arrow.GetComponent<ArrowSide>().side;
            Debug.Log($"Collision with arrow on side: {side}");

            //if (side == "COOP")
            //{
            //    //PlayerController playerController = arrow.GetComponent<PlayerController>();
            //    PlayerController playerController = GetComponent<PlayerController>();
            //    if (playerController != null)
            //    {
            //        playersCollided.Add(playerController.playerNumber);
            //        Debug.Log($"Player {playerController.playerNumber} collided with COOP arrow. Players collided: {playersCollided.Count}");

            //        if (playersCollided.Count == 2)
            //        {
            //            OnArrowDestroyed?.Invoke("COOP");
            //            Destroy(arrow);
            //            playersCollided.Clear(); // Reset the set for the next cooperative arrow
            //            Debug.Log("COOP arrow destroyed by both players.");
            //        }
            //    }
            //}
            //else
            //{
                OnArrowDestroyed?.Invoke(side);

                //PlayerController playerController = GetComponent<PlayerController>();
                PlayerController playerController = GetComponent<PlayerController>();
                
                if (playerController != null)
                {
                    ScoreManager.AddScore(playerController.playerNumber, 100);
                }

                Destroy(arrow);
            //}
        }
    }

    //void OnCollisionExit(Collision collision)
    //{
    //    HandleCollisionExit(collision.gameObject);
    //}

    //void OnTriggerExit(Collider other)
    //{
    //    HandleCollisionExit(other.gameObject);
    //}

    //void HandleCollisionExit(GameObject arrow)
    //{
    //    if (arrow.CompareTag("Arrow"))
    //    {
    //        string side = arrow.GetComponent<ArrowSide>().side;
    //        Debug.Log($"Collision exit with arrow on side: {side}");

    //        //if (side == "COOP")
    //        //{
    //        //    //PlayerController playerController = arrow.GetComponent<PlayerController>();
    //        //    PlayerController playerController = GetComponent<PlayerController>();
    //        //    if (playerController != null)
    //        //    {
    //        //        playersCollided.Remove(playerController.playerNumber);
    //        //        Debug.Log($"Player {playerController.playerNumber} exited collision with COOP arrow. Players collided: {playersCollided.Count}");
    //        //    }
    //        //}
    //    }
    //}




}
