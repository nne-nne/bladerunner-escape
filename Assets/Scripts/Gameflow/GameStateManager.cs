using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public GameState currentState { get; private set; }


    #region gamestate_management
    public void StartGame()
    {
        currentState = GameState.AFOOT;
    }
    public void PauseGame()
    {
        currentState = GameState.PAUSED;
    }
    public void ResumeGame()
    {
        currentState = GameState.MAIN_MENU;
    }
    public void FinishGame()
    {
        currentState = GameState.MAIN_MENU;
    }
    #endregion

    private void Update()
    {
        if(currentState == GameState.MAIN_MENU && Input.anyKeyDown)
        {
            EventBroadcaster.GameStarted();
        }
        else if(currentState == GameState.AFOOT && Input.GetKeyDown(KeyCode.P))
        {
            EventBroadcaster.GamePaused();
        }
        else if (currentState == GameState.PAUSED && Input.GetKeyDown(KeyCode.P))
        {
            EventBroadcaster.GameResumed();
        }
    }

    private void OnEnable()
    {
        EventBroadcaster.OnGameStarted += StartGame;
        EventBroadcaster.OnGamePaused += PauseGame;
        EventBroadcaster.OnGameResumed += ResumeGame;
        EventBroadcaster.OnGameFinished += FinishGame;
    }

    private void OnDisable()
    {
        EventBroadcaster.OnGameStarted -= StartGame;
        EventBroadcaster.OnGamePaused -= PauseGame;
        EventBroadcaster.OnGameResumed -= ResumeGame;
        EventBroadcaster.OnGameFinished -= FinishGame;
    }
}
