using NUnit.Framework;
using System;
using System.Linq;
using Unity.Netcode;
using UnityEngine;

public class GameManager_Network : NetworkBehaviour
{
    static public GameManager_Network Instance;
    public static event Action<GameState> OnGameStateChange;

    public enum GameState {RoundStart, RoundProgress, RoundOver}

    private void Awake()
    {
        if (!IsHost)
            return;

        if (Instance && Instance != this)
        {
            Destroy(this);
        }
        else 
        { 
            Instance = this;
        }
    }

    private void Start()
    {
        UpdateGameState(GameState.RoundStart);
    }

    public void UpdateGameState(GameState newState)
    {
        switch (newState)
        {
            case GameState.RoundStart:
                break;
            case GameState.RoundProgress:
                break;
            case GameState.RoundOver:
                break;
            default:
                break;
        }

        OnGameStateChange?.Invoke(newState);
    }

}