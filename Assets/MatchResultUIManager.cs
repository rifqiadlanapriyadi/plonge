using TMPro;
using UnityEngine;

public class MatchResultUIManager : MonoBehaviour
{
    public GameManager gm;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI winnerText;

    void Start()
    {
        GameManager.OnGameOver += ShowMatchResults;
    }

    void ShowMatchResults()
    {
        int player1Score = gm.GetPlayer1Score();
        int player2Score = gm.GetPlayer2Score();
        scoreText.text = player1Score.ToString() + ":" + player2Score.ToString();
        if (player1Score == player2Score)
            winnerText.text = "It's a draw!";
        else winnerText.text = "Player " + (player1Score > player2Score ? 1 : 2) + " wins!";
    }
}
