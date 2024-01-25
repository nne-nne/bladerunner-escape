using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiddlesManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> riddlesToSolve;
    public IRiddle CurrentRiddle { get; private set; }

    private bool CheckWinCondition()
    {
        return CurrentRiddle.IsPassed();
    }

    private void NextRiddle()
    {
        riddlesToSolve.RemoveAt(0);
        if(riddlesToSolve.Count > 0)
        {
            CurrentRiddle = riddlesToSolve[0].GetComponent<IRiddle>();
            CurrentRiddle.Prepare();
        }
        else
        {
            EventBroadcaster.GameFinished();
        }
    }

    private void OnEnable()
    {
        EventBroadcaster.OnRiddleFinished += NextRiddle;
    }
    private void OnDisable()
    {
        EventBroadcaster.OnRiddleFinished -= NextRiddle;
    }

    private void Awake()
    {
        CurrentRiddle = riddlesToSolve[0].GetComponent<IRiddle>();
    }

    private void Update()
    {
        // cheat
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            CurrentRiddle.Solve();
        }
    }
}
