using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiddleTutorial1 : MonoBehaviour, IRiddle
{
    [SerializeField] private Material elevator1Material;
    [SerializeField] private float targetKnobValue;
    [SerializeField] private float tolerance = 0.01f;
    [SerializeField] private PatternCamera patternCamera;
    [SerializeField] private List<Material> materialPatterns;
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
        CheckWinCondition();
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
        Debug.Log("Riddle Tutorial 1 prepared");
    }

    public void OnPassed()
    {
        exit.Open();
        EventBroadcaster.RiddleFinished();
    }

    public bool IsPassed()
    {
        return CheckWinCondition();
    }

    public List<Material> GetMaterialPatterns()
    {
        return materialPatterns;
    }

    public PatternCamera GetPatternCamera()
    {
        return patternCamera;
    }

    private void Awake()
    {
        Prepare();
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
