using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiddleCelShading : MonoBehaviour, IRiddle
{
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
    [SerializeField] private MaterialManager materialManager;

    [SerializeField] private float targetLightThreshold = 0.15f;
    [SerializeField] private float targetShadowThreshold = 0.5f;
    [SerializeField] private float tolerance = 0.15f;

    [SerializeField] private ScifiDoor balconyDoor;


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
        return true;
    }

    public void OnPassed()
    {
        balconyDoor.Open();
    }

    public void Prepare()
    {
        SetKnobValue(lightThreshold, 0.9f, "_specularThreshold");
        SetKnobValue(shadowThreshold, 0.1f, "_shadowThreshold");
    }

    public void Solve()
    {
        EventBroadcaster.ConnectionMade(mainLight_source, dotProduct_dest);
        EventBroadcaster.ConnectionMade(normal_source, operand_dest);
        SetKnobValue(lightThreshold, targetLightThreshold, "_specularThreshold");
        SetKnobValue(shadowThreshold, targetShadowThreshold, "_shadowThreshold");
    }

    private void SetKnobValue(Knob k, float value, string property)
    {
        k.SetValue(value);
        materialManager.SetMaterialsProperty(property, value);
    }
}
