using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DanceBattleManager : MonoBehaviour
{
    public void Player1Wins()
    {
        // Aquí puedes cargar la escena de victoria del jugador 1
        SceneManager.LoadScene("FinalScreen_1_won");
    }

    public void Player2Wins()
    {
        // Aquí puedes cargar la escena de victoria del jugador 2
        SceneManager.LoadScene("Player2WinsScene");
    }
}
