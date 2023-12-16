using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public GameState currentState { get; private set; }


    #region gamestate_management
    public void OnPause()
    {
        currentState = GameState.PAUSED;
    }
    public void OnPlay()
    {
        currentState = GameState.AFOOT;
    }
    public void OnReturnToMenu()
    {
        currentState = GameState.MAIN_MENU;
    }
    #endregion
}
