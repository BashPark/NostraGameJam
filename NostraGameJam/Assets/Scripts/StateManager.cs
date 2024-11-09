using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public static StateManager instance;
    public GameState State;

    // Event to tell other state has changed
    public static event Action<GameState> onGameStateChanged;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

    }

    private void Start()
    {
        updateGameState(GameState.Phase1);
    }

    public void updateGameState(GameState newState)
    {
        State = newState;

        // Handle according to the new current state
        switch (newState)
        {
            case GameState.MainMenu:
                handleMainMenu();
                break;
            case GameState.Phase1:
                handlePhase1();
                break;
            case GameState.Phase2:
                handlePhase2();
                break;
            case GameState.Phase3:
                handlePhase3();
                break;
            case GameState.PauseMenu:
                handlePauseMenu();
                break;
            case GameState.GameOver:
                handleGameOver();
                break;
            default:
                Debug.Log("Out of game States");
                break;

        }

        // Call event to tell everybody who is subscribed that state has changed
        onGameStateChanged?.Invoke(newState);

    }

    private void handleMainMenu()
    {
        
    }

    private void handlePhase1()
    {
      
    }

    private void handlePhase2()
    {
        
    }

    private void handlePhase3()
    {
        
    }

    private void handlePauseMenu()
    {
        
    }

    private void handleGameOver()
    {
        
    }


}

public enum GameState
{
    MainMenu,
    Phase1,
    Phase2,
    Phase3,
    PauseMenu,
    GameOver

}
