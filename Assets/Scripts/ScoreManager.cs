using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ScoreManager : MonoBehaviour
{
    public static int player1Score = 0;
    public static int player2Score = 0;

    public delegate void ScoreChangedHandler(int playerNumber, int newScore);
    public static event ScoreChangedHandler OnScoreChanged;
    public static event System.Action OnCooperativePlayStart;
    public static event Action<int> OnPlayerWins; // Evento para indicar qué jugador ha ganado


    public static void AddScore(int playerNumber, int points)
    {
        if (playerNumber == 1)
        {
            player1Score += points;
            //Debug.Log("Player 1 Score: " + player1Score);
            OnScoreChanged?.Invoke(1, player1Score);
            if (player1Score >= 1) // Puedes ajustar este valor según lo necesites
            {
                OnPlayerWins?.Invoke(1); // Indicar que el jugador 1 ha ganado
            }

        }
        else if (playerNumber == 2)
        {
            player2Score += points;
            //Debug.Log("Player 2 Score: " + player2Score);
            OnScoreChanged?.Invoke(2, player2Score);
            if (player2Score >= 2000) // Puedes ajustar este valor según lo necesites
            {
                OnPlayerWins?.Invoke(2); // Indicar que el jugador 1 ha ganado
            }
        }
/*
        if (player1Score == 1000 && player2Score == 1000)
        {
            OnCooperativePlayStart?.Invoke();
        }*/
    }
}
