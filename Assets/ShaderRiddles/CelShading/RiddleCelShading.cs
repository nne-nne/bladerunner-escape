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
    [SerializeField] private Plug textureDest;

    [SerializeField] private PatternCamera patternCamera;
    [SerializeField] private List<Material> materialPatterns;
    [SerializeField] private MaterialManager materialManager;

    [SerializeField] private float targetLightThreshold = 0.15f;
    [SerializeField] private float targetShadowThreshold = 0.5f;
    [SerializeField] private float tolerance = 0.15f;

    [SerializeField] private ScifiDoor balconyDoor;
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
        return true;
    }

    public void OnPassed()
    {
        if(!passed)
        {
            balconyDoor.Open();
            EventBroadcaster.RiddleFinished(this);
            passed = true;
        }
    }

    public void Prepare()
    {
        SetKnobValue(lightThreshold, 0.9f, "_specularThreshold");
        SetKnobValue(shadowThreshold, 0.1f, "_shadowThreshold");
        materialManager.SetCellPattern();
        EventBroadcaster.PlugDisconnected(operand_dest);
        EventBroadcaster.PlugDisconnected(mainLight_source);
        EventBroadcaster.PlugDisconnected(multiply_dest);
        EventBroadcaster.PlugDisconnected(dotProduct_dest);
        EventBroadcaster.PlugDisconnected(crossProduct_dest);
        EventBroadcaster.PlugDisconnected(textureDest);
        materialManager.SetMaterialsPropertyInt("_LightMul", 0);
        materialManager.SetMaterialsPropertyInt("_LightDot", 0);
        materialManager.SetMaterialsPropertyInt("_LightCross", 0);
        materialManager.SetMaterialsPropertyInt("_NormalOperand", 0);
        materialManager.SetMaterialsPropertyInt("_TimeOperand", 0);
        materialManager.SetMaterialsPropertyInt("_OneOperand", 0);
        materialManager.SetMaterialsPropertyInt("_DottedTexture", 0);
        materialManager.SetMaterialsPropertyInt("_StrippedTexture", 0);
    }

    public void Solve()
    {
        EventBroadcaster.ConnectionMade(mainLight_source, dotProduct_dest);
        EventBroadcaster.ConnectionMade(normal_source, operand_dest);
        SetKnobValue(lightThreshold, targetLightThreshold, "_specularThreshold");
        SetKnobValue(shadowThreshold, targetShadowThreshold, "_shadowThreshold");
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
        if(source == mainLight_source)
        {
            if(dest == multiply_dest)
            {
                materialManager.SetMaterialsPropertyInt("_LightMul", 1);
            }
            else if(dest == dotProduct_dest)
            {
                materialManager.SetMaterialsPropertyInt("_LightDot", 1);
            }
            else if(dest == crossProduct_dest)
            {
                materialManager.SetMaterialsPropertyInt("_LightCross", 1);
            }
            else
            {
                EventBroadcaster.PlugDisconnected(mainLight_source);
            }
        }
        else if(dest == operand_dest)
        {
            if(source == normal_source)
            {
                materialManager.SetMaterialsPropertyInt("_NormalOperand", 1);
            }
            else if(source == one_source)
            {
                materialManager.SetMaterialsPropertyInt("_OneOperand", 1);
            }
            else if(source == time_source)
            {
                materialManager.SetMaterialsPropertyInt("_TimeOperand", 1);
            }
            else
            {
                EventBroadcaster.PlugDisconnected(operand_dest);
            }
        }
        if (IsPassed())
        {
            OnPassed();
        }
    }

    private void OnPlugDisconnected(Plug p)
    {
        if(p == operand_dest)
        {
            materialManager.SetMaterialsPropertyInt("_NormalOperand", 0);
            materialManager.SetMaterialsPropertyInt("_TimeOperand", 0);
            materialManager.SetMaterialsPropertyInt("_OneOperand", 0);
        }
        else if(p == multiply_dest)
        {
            materialManager.RestoreDefaultMaterials();
        }
        else if(p == crossProduct_dest)
        {
            materialManager.RestoreDefaultMaterials();
        }
        else if(p == dotProduct_dest)
        {
            materialManager.RestoreDefaultMaterials();
        }
        if (IsPassed())
        {
            OnPassed();
        }
    }

    private void OnKnobValueChanged(Knob k, float value)
    {
        if (k == lightThreshold)
        {
            SetKnobValue(lightThreshold, lightThreshold.Value, "_specularThreshold");
        }
        else if (k == shadowThreshold)
        {
            SetKnobValue(shadowThreshold, shadowThreshold.Value, "_shadowThreshold");
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
