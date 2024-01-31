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
    [SerializeField] private Plug main_light_source;
    [SerializeField] private Knob blend;

    [SerializeField] private PatternCamera patternCamera;
    [SerializeField] private List<Material> materialPatterns;
    [SerializeField] private Material hologramMaterial;

    [SerializeField] private float targetBlendValue = 0.5f;
    [SerializeField] private float tolerance = 0.15f;

    [SerializeField] private GameObject safe;
    [SerializeField] private GameObject unicorn;

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
        if (time_source.connectedPlug != uvOffset_dest) return false;
        if (deltaTime_source.connectedPlug != null) return false;
        if (fresnel_source.connectedPlug != add_dest) return false;
        if (main_light_source.connectedPlug != null) return false;
        if (Mathf.Abs(blend.Value - targetBlendValue) > tolerance) return false;
        return true;
    }

    public void OnPassed()
    {
        if(!passed)
        {
            safe.SetActive(true);
            unicorn.SetActive(true);
            EventBroadcaster.RiddleFinished(this);
            passed = true;
        }
    }

    public void Prepare()
    {
        SetKnobValue(0.0f); 
        hologramMaterial.SetFloat("_Blend", 0.0f);
        safe.SetActive(false);
        unicorn.SetActive(false);
        //materialManager.SetDefaultPattern();

        EventBroadcaster.PlugDisconnected(add_dest);
        EventBroadcaster.PlugDisconnected(uvOffset_dest);
        EventBroadcaster.PlugDisconnected(uvTiling_dest);
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
        EventBroadcaster.PlugDisconnected(main_light_source);
        SetKnobValue(targetBlendValue);
        if (IsPassed())
        {
            OnPassed();
        }
    }

    private void OnConnectionMade(Plug source, Plug dest)
    {
        if(source == fresnel_source && dest == add_dest)
        {
            hologramMaterial.SetInt("_FresnelAdd", 1);
        }
        else if (source == fresnel_source && dest == uvTiling_dest)
        {
            hologramMaterial.SetInt("_FresnelTiling", 1);
        }
        else if (source == fresnel_source && dest == uvOffset_dest)
        {
            hologramMaterial.SetInt("_FresnelOffset", 1);
        }
        else if (source == time_source && dest == add_dest)
        {
            hologramMaterial.SetInt("_TimeAdd", 1);
        }
        else if (source == time_source && dest == uvTiling_dest)
        {
            hologramMaterial.SetInt("_TimeTiling", 1);
        }
        else if (source == time_source && dest == uvOffset_dest)
        {
            hologramMaterial.SetInt("_TimeOffset", 1);
        }
        else if (source == deltaTime_source && dest == add_dest)
        {
            hologramMaterial.SetInt("_DeltaAdd", 1);
        }
        else if (source == deltaTime_source && dest == uvTiling_dest)
        {
            hologramMaterial.SetInt("_DeltaAdd", 1);
        }
        else if (source == deltaTime_source && dest == uvOffset_dest)
        {
            hologramMaterial.SetInt("_DeltaAdd", 1);
        }
        if (IsPassed())
        {
            OnPassed();
        }
    }

    private void OnPlugDisconnected(Plug p)
    {
        if(p == add_dest)
        {
            hologramMaterial.SetInt("_DeltaAdd", 0);
            hologramMaterial.SetInt("_TimeAdd", 0);
            hologramMaterial.SetInt("_FresnelAdd", 0);
        }
        else if(p == uvTiling_dest)
        {
            hologramMaterial.SetInt("_DeltaTiling", 0);
            hologramMaterial.SetInt("_TimeTiling", 0);
            hologramMaterial.SetInt("_FresnelTiling", 0);
        }
        else if(p == uvOffset_dest)
        {
            hologramMaterial.SetInt("_DeltaOffset", 0);
            hologramMaterial.SetInt("_TimeOffset", 0);
            hologramMaterial.SetInt("_FresnelOffset", 0);
        }
        if (IsPassed())
        {
            OnPassed();
        }
    }

    private void OnKnobValueChanged(Knob k, float value)
    {
        if(k == blend)
        {
            hologramMaterial.SetFloat("_Blend", k.Value);
        }
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
