using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScoreUIManager : MonoBehaviour
{
    public Text player1ScoreText;
    public Text player2ScoreText;
    //public GameObject myCanvas;


    void Start()
    {
        // Inicializar la puntuación en pantalla
        UpdatePlayer1Score(ScoreManager.player1Score);
        UpdatePlayer2Score(ScoreManager.player2Score);
    }

    void OnEnable()
    {
        ScoreManager.OnScoreChanged += UpdateScore;
    }

    void OnDisable()
    {
        ScoreManager.OnScoreChanged -= UpdateScore;
    }

    void UpdatePlayer1Score(int score)
    {
        player1ScoreText.text = "Player 1 Score: " + score;
    }

    void UpdatePlayer2Score(int score)
    {
        player2ScoreText.text = "Player 2 Score: " + score;
    }

    void UpdateScore(int playerNumber, int newScore)
    {
        if (playerNumber == 1)
        {
            UpdatePlayer1Score(newScore);
        }
        else if (playerNumber == 2)
        {
            UpdatePlayer2Score(newScore);
        }
    }
}
