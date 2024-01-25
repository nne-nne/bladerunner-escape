using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiddleHologram : MonoBehaviour, IRiddle
{
    [SerializeField] private Plug deltaTime_source;
    [SerializeField] private Plug time_source;
    [SerializeField] private Plug uvTiling_dest;
    [SerializeField] private Plug uvOffset_dest;
    [SerializeField] private Plug add_dest;
    [SerializeField] private Plug fresnel_source;
    [SerializeField] private Knob blend;

    [SerializeField] private PatternCamera patternCamera;
    [SerializeField] private List<Material> materialPatterns;
    [SerializeField] private Material hologramMaterial;

    [SerializeField] private float targetBlendValue = 0.5f;
    [SerializeField] private float tolerance = 0.15f;

    [SerializeField] private GameObject safe;
    [SerializeField] private GameObject unicorn;

    [SerializeField] private MaterialManager materialManager;
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
        if (time_source.connectedPlug != uvOffset_dest) return false;
        if (deltaTime_source.connectedPlug != null) return false;
        if (fresnel_source.connectedPlug != add_dest) return false;
        if (Mathf.Abs(blend.Value - targetBlendValue) > tolerance) return false;
        return true;
    }

    public void OnPassed()
    {
        safe.SetActive(true);
        unicorn.SetActive(true);
        EventBroadcaster.RiddleFinished();
    }

    public void Prepare()
    {
        SetKnobValue(0.0f);
        safe.SetActive(false);
        unicorn.SetActive(false);
        materialManager.SetDefaultPattern();
    }

    private void Awake()
    {
        Prepare();
    }

    private void SetKnobValue(float value)
    {
        blend.SetValue(value);
        hologramMaterial.SetFloat("_Blend", value);
    }

    public void Solve()
    {
        EventBroadcaster.ConnectionMade(time_source, uvOffset_dest);
        EventBroadcaster.ConnectionMade(fresnel_source, add_dest);
        EventBroadcaster.PlugDisconnected(deltaTime_source);
        SetKnobValue(targetBlendValue);
        if (IsPassed())
        {
            OnPassed();
        }
    }
}
