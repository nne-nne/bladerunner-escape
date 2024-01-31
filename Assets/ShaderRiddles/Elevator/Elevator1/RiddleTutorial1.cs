using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiddleTutorial1 : MonoBehaviour, IRiddle
{
    [SerializeField] private Material elevator1Material;
    [SerializeField] private float targetKnobValue;
    [SerializeField] private float tolerance = 0.01f;
    [SerializeField] private PatternCamera patternCamera;
    [SerializeField] private Material patternMaterial;
    [SerializeField] private Knob knob;
    [SerializeField] private ScifiDoor exit;
    private float currentKnobValue;

    private void OnKnobValueChanged(Knob k, float value)
    {
        if(k == knob)
        {
            SetKnobValue(knob.Value);
            if (CheckWinCondition())
            {
                OnPassed();
            }
        }
    }

    private void SetKnobValue(float v)
    {
        currentKnobValue = v;
        elevator1Material.SetFloat("_KnobValue", currentKnobValue);
    }

    private bool CheckWinCondition()
    {
        if(Mathf.Abs(currentKnobValue - targetKnobValue) <= tolerance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Prepare()
    {
        currentKnobValue = 0.0f;
        patternMaterial.SetInt("_IsActive", 1);
    }

    public void OnPassed()
    {
        exit.Open();
        EventBroadcaster.RiddleFinished(this);
    }

    public bool IsPassed()
    {
        return CheckWinCondition();
    }
    public void Solve()
    {
        float val = (targetKnobValue-currentKnobValue) % 1f;
        if (val < 0f) val += 1f;
        EventBroadcaster.KnobValueChanged(knob, val);
    }


    public Material GetPatternMaterial()
    {
        return patternMaterial;
    }

    public PatternCamera GetPatternCamera()
    {
        return patternCamera;
    }

    private void Awake()
    {
        Prepare();
        patternMaterial.SetInt("_IsActive", 0);
    }

    private void OnEnable()
    {
        EventBroadcaster.OnKnobValueChanged += OnKnobValueChanged;
    }

    private void OnDisable()
    {
        EventBroadcaster.OnKnobValueChanged -= OnKnobValueChanged;
    }
}
