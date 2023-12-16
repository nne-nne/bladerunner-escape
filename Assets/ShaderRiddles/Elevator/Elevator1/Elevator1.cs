using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator1 : MonoBehaviour
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
            Debug.Log("Elevator1: Passed");
            return true;
        }
        else
        {
            return false;
        }
    }
}
