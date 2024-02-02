using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject finishPanel;
    public GameObject startPanel;

    private void OnStartGame()
    {
        Time.timeScale = 1f;
        startPanel.SetActive(false);
    }

    private void OnPauseGame()
    {
        Time.timeScale = 0f;
        pausePanel.SetActive(true);
    }

    private void OnResumeGame()
    {
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
    }

    private void OnFinishGame()
    {
        Debug.Log("on finished game");
        Time.timeScale = 0f;
        finishPanel.SetActive(true);
    }

    private void Awake()
    {
        Time.timeScale = 0f;
        pausePanel.SetActive(false);
        finishPanel.SetActive(false);
        startPanel.SetActive(true);
    }

    void OnEnable()
    {
        EventBroadcaster.OnGameStarted += OnStartGame;
        EventBroadcaster.OnGamePaused += OnPauseGame;
        EventBroadcaster.OnGameResumed += OnResumeGame;
        EventBroadcaster.OnGameFinished += OnFinishGame;
    }

    void OnDisable()
    {
        EventBroadcaster.OnGameStarted -= OnStartGame;
        EventBroadcaster.OnGamePaused -= OnPauseGame;
        EventBroadcaster.OnGameResumed -= OnResumeGame;
        EventBroadcaster.OnGameFinished -= OnFinishGame;
    }
}
