using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Collision_with_arrow : MonoBehaviour
{
    // Define a delegate with a string parameter for side information
    public delegate void ArrowDestroyedHandler(string side);

    // Define an event using the delegate
    public static event ArrowDestroyedHandler OnArrowDestroyed;

    private HashSet<int> playersCollided = new HashSet<int>();
    private int count_players_colliding;
    public static event Action OnLastCoopArrowDestroyed;
    private static int coopArrowsDestroyed = 0;
    



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

            if (side == "COOP")
            {

                PlayerController playerController = GetComponent<PlayerController>();
                if (playerController != null)
                {
                    
                    count_players_colliding++;
                    Debug.Log($"Player {playerController.playerNumber} collided with COOP arrow. Players collided: {count_players_colliding}");

                    if (count_players_colliding == 2)
                    {
                        OnArrowDestroyed?.Invoke("COOP");
                        Destroy(arrow);
                        playersCollided.Clear(); // Reset the set for the next cooperative arrow
                        count_players_colliding = 0;
                        coopArrowsDestroyed++;
                        Debug.Log("COOP arrow destroyed by both players.");

                        ScoreManager.AddScore(1, 50);
                        ScoreManager.AddScore(2, 50);

                        if (coopArrowsDestroyed >= Spawner.coopSpawnPositions.Length)
                        {
                            OnLastCoopArrowDestroyed?.Invoke();
                        }


                    }
                }
            }
            else
            {
                OnArrowDestroyed?.Invoke(side);

                PlayerController playerController = GetComponent<PlayerController>();
                if (playerController != null)
                {
                    ScoreManager.AddScore(playerController.playerNumber, 100);
                }

                Destroy(arrow);

                

            }
        }
    }


   

    public void PlayerCollidedWithCoopArrow(int playerNumber)
    {
        playersCollided.Add(playerNumber);

        if (playersCollided.Count == 2)
        {
            OnArrowDestroyed?.Invoke("COOP");
            GameObject coopArrow = GameObject.FindWithTag("Arrow");
            if (coopArrow != null)
            {
                Destroy(coopArrow);
            }
        }
    }
    

    void HandleCollisionExit(GameObject arrow)
    {
        if (arrow.CompareTag("Arrow"))
        {
            string side = arrow.GetComponent<ArrowSide>().side;
            Debug.Log($"Collision exit with arrow on side: {side}");

            if (side == "COOP")
            {
                //update: Obtén el PlayerController del objeto que está colisionando con la flecha
                PlayerController playerController = GetComponent<PlayerController>();
                if (playerController != null)
                {
                    playersCollided.Remove(playerController.playerNumber);
                    Debug.Log($"Player {playerController.playerNumber} exited collision with COOP arrow. Players collided: {playersCollided.Count}");
                }
            }
        }
    }




}
