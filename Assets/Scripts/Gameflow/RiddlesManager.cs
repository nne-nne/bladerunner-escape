using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiddlesManager : MonoBehaviour
{
    [SerializeField] private List<IRiddle> riddlesToSolve;
    public IRiddle CurrentRiddle { get; private set; }

    private bool CheckWinCondition()
    {
        return CurrentRiddle.IsPassed();
    }

    private void NextRiddle(IRiddle newRiddle)
    {
        riddlesToSolve.Remove(CurrentRiddle);
        CurrentRiddle = newRiddle;
    }

    private void OnEnable()
    {
        EventBroadcaster.OnChangedRiddle += NextRiddle;
    }
    private void OnDisable()
    {
        EventBroadcaster.OnChangedRiddle -= NextRiddle;
    }
}
