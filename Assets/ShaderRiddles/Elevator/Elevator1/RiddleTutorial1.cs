using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiddleTutorial1 : MonoBehaviour, IRiddle
{
    [SerializeField] private Material elevator1;
    [SerializeField] private float targetKnobValue;
    [SerializeField] private float tolerance = 0.01f;
    private float currentKnobValue;

    private void Awake()
    {
        currentKnobValue = elevator1.GetFloat("_KnobValue");
    }

    public void SetKnobValue(float v)
    {
        currentKnobValue = v;
        elevator1.SetFloat("_KnobValue", currentKnobValue);
        CheckWinCondition();
    }

    private bool CheckWinCondition()
    {
        if(Mathf.Abs(currentKnobValue - targetKnobValue) <= tolerance )
        {
            Debug.Log("Riddle Tutorial 1: Passed");
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Prepare()
    {
        throw new System.NotImplementedException();
    }

    public void OnPassed()
    {
        throw new System.NotImplementedException();
    }

    public bool IsPassed()
    {
        throw new System.NotImplementedException();
    }

    public List<Material> GetMaterialPatterns()
    {
        throw new System.NotImplementedException();
    }

    public PatternCamera GetPatternCamera()
    {
        throw new System.NotImplementedException();
    }
}
