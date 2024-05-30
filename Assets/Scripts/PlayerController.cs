using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int playerNumber;


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Arrow"))
        {
            ArrowSide arrowSide = other.gameObject.GetComponent<ArrowSide>();
            if (arrowSide != null && arrowSide.side == "COOP")
            {
                FindObjectOfType<Collision_with_arrow>().PlayerCollidedWithCoopArrow(playerNumber, other.gameObject);
            }
        }
    }
}
