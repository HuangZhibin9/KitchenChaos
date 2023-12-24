using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public enum GameState
    {
        WaittingToStart,
        CountDown,
        GamePlaying,
        GameOver,
    }

    [SerializeField] private GameState gameState;

    [SerializeField] private float waittingToStartTimer = 1f;
    [SerializeField] private float countDownTimer = 3f;
    [SerializeField] private float gamePlayingTimer = 10f;

    //游戏状态改变事件
    public event Action<GameState> GameStateChanged;

    private void Awake()
    {
        gameState = GameState.WaittingToStart;
    }

    private void Update()
    {
        switch (gameState)
        {
            case GameState.WaittingToStart:
                waittingToStartTimer -= Time.deltaTime;
                if (waittingToStartTimer < 0f)
                {
                    gameState = GameState.CountDown;
                    GameStateChanged?.Invoke(gameState);
                }
                break;
            case GameState.CountDown:
                countDownTimer -= Time.deltaTime;
                if (countDownTimer < 0f)
                {
                    gameState = GameState.GamePlaying;
                    GameStateChanged?.Invoke(gameState);
                }
                break;
            case GameState.GamePlaying:
                gamePlayingTimer -= Time.deltaTime;
                if (gamePlayingTimer < 0f)
                {
                    gameState = GameState.GameOver;
                    GameStateChanged?.Invoke(gameState);
                }
                break;
            case GameState.GameOver:
                break;
        }
    }

    public bool IsGamePlaying()
    {
        return gameState == GameState.GamePlaying;
    }

    public float GetCountDownTimer()
    {
        return countDownTimer;
    }
}
