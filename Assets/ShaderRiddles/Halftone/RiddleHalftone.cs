using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiddleHalftone : MonoBehaviour, IRiddle
{
    [SerializeField] private Knob smoothness;
    [SerializeField] private Knob threshold;
    [SerializeField] private Plug texture_dest;
    [SerializeField] private Plug stripped_source;
    [SerializeField] private Plug dotted_source;

    [SerializeField] private Plug mainLight_source;
    [SerializeField] private Plug time_source;
    [SerializeField] private Plug dotProduct_dest;
    [SerializeField] private Plug crossProduct_dest;
    [SerializeField] private Plug multiply_dest;
    [SerializeField] private Plug normal_source;
    [SerializeField] private Plug one_source;
    [SerializeField] private Plug operand_dest;
    [SerializeField] private Knob lightThreshold;
    [SerializeField] private Knob shadowThreshold;

    [SerializeField] private PatternCamera patternCamera;
    [SerializeField] private List<Material> materialPatterns;

    [SerializeField] private float targetLightThreshold = 0.15f;
    [SerializeField] private float targetShadowThreshold = 0.5f;
    [SerializeField] private float targetSmoothness = 1.0f;
    [SerializeField] private float targetThreshold = 1.0f;
    [SerializeField] private float tolerance = 0.15f;

    [SerializeField] private Safe safe;
    [SerializeField] private MaterialManager materialManager;
    private bool passed = false;

    public List<Material> GetMaterialPatterns()
    {
        return materialPatterns;
    }

    public PatternCamera GetPatternCamera()
    {
        return patternCamera;
    }

    public bool IsPassed()
    {
        if (mainLight_source.connectedPlug != dotProduct_dest) return false;
        if (normal_source.connectedPlug != operand_dest) return false;
        if (Mathf.Abs(lightThreshold.Value - targetLightThreshold) > tolerance) return false;
        if (Mathf.Abs(shadowThreshold.Value - targetShadowThreshold) > tolerance) return false;

        if (dotted_source.connectedPlug != texture_dest) return false;
        if (Mathf.Abs(smoothness.Value - targetSmoothness) > tolerance) return false;
        if (Mathf.Abs(threshold.Value - targetThreshold) > tolerance) return false;

        return true;
    }

    public void OnPassed()
    {
        if(!passed)
        {
            safe.Open();
            EventBroadcaster.RiddleFinished(this);
            passed = true;
        }
    }

    public void Prepare()
    {
        materialManager.SetHalftonePattern();
    }

    public void Solve()
    {
        EventBroadcaster.ConnectionMade(mainLight_source, dotProduct_dest);
        EventBroadcaster.ConnectionMade(normal_source, operand_dest);
        SetKnobValue(lightThreshold, targetLightThreshold, "_specularThreshold");
        SetKnobValue(shadowThreshold, targetShadowThreshold, "_shadowThreshold");
        EventBroadcaster.ConnectionMade(dotted_source, texture_dest);
        SetKnobValue(smoothness, targetSmoothness, "_Smoothness");
        SetKnobValue(threshold, targetThreshold, "_Threshold");
        if (IsPassed())
        {
            OnPassed();
        }
    }

    private void SetKnobValue(Knob k, float value, string property)
    {
        k.SetValue(value);
        materialManager.SetMaterialsProperty(property, value);
    }

    private void OnConnectionMade(Plug source, Plug dest)
    {
        if (IsPassed())
        {
            OnPassed();
        }
    }

    private void OnPlugDisconnected(Plug p)
    {
        if (IsPassed())
        {
            OnPassed();
        }
    }

    private void OnKnobValueChanged(Knob k, float value)
    {
        if (IsPassed())
        {
            OnPassed();
        }
    }

    private void OnEnable()
    {
        EventBroadcaster.OnConnectionMade += OnConnectionMade;
        EventBroadcaster.OnPlugDisconnected += OnPlugDisconnected;
        EventBroadcaster.OnKnobValueChanged += OnKnobValueChanged;
    }

    private void OnDisable()
    {
        EventBroadcaster.OnConnectionMade -= OnConnectionMade;
        EventBroadcaster.OnPlugDisconnected -= OnPlugDisconnected;
        EventBroadcaster.OnKnobValueChanged -= OnKnobValueChanged;
    }
}
