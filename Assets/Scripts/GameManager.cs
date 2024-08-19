using System;
using UnityEngine;

public enum GameState
{
    MainMenu, InMatch, GameOver
}

public class GameManager : MonoBehaviour
{
    public float matchSeconds;
    float remainingTime;

    int player1Score;
    int player2Score;

    GameState state;

    public GameObject mainMenuUI;
    public GameObject matchUI;
    public GameObject gameOverUI;

    public delegate void MainMenuStart();
    public static event MainMenuStart OnMainMenuStart;
    public delegate void MatchStart();
    public static event MatchStart OnMatchStart;
    public delegate void GameOver();
    public static event GameOver OnGameOver;

    void Start()
    {
        state = GameState.MainMenu;
        if (OnMainMenuStart != null)
            OnMainMenuStart();

        mainMenuUI.SetActive(true);
        matchUI.SetActive(false);
        gameOverUI.SetActive(false);
    }

    private void Update()
    {
        if (state == GameState.InMatch)
        {
            remainingTime -= Time.deltaTime;
            if (remainingTime <= 0) MatchOver();
        }
    }

    public void GoToMainMenu()
    {
        gameOverUI.SetActive(false);
        mainMenuUI.SetActive(true);

        state = GameState.MainMenu;

        OnMainMenuStart();
    }

    public void StartMatch()
    {
        mainMenuUI.SetActive(false);
        matchUI.SetActive(true);

        player1Score = 0;
        player2Score = 0;
        remainingTime = matchSeconds;
        state = GameState.InMatch;

        OnMatchStart();
    }

    private void MatchOver()
    {
        matchUI.SetActive(false);
        gameOverUI.SetActive(true);

        state = GameState.GameOver;

        OnGameOver();
    }

    public GameState GetState()
    {
        return state;
    }

    public float GetRemainingTime()
    {
        return remainingTime;
    }

    public void IncPlayer1Score()
    {
        player1Score++;
    }
    public void IncPlayer2Score()
    {
        player2Score++;
    }
    public void ResetScores()
    {
        player1Score = 0;
        player2Score = 0;
    }

    public int GetPlayer1Score()
    {
        return player1Score;
    }
    public int GetPlayer2Score()
    {
        return player2Score;
    }
}
