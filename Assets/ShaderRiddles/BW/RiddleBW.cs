using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiddleBW : MonoBehaviour, IRiddle
{
    [SerializeField] private Plug fullScreenPass_dest;
    [SerializeField] private Plug compare0_dest;
    [SerializeField] private Plug compare1_dest;
    [SerializeField] private Plug compare_source;
    [SerializeField] private Plug xy_source;
    [SerializeField] private Plug x2y2_source;
    [SerializeField] private Knob threshold;

    [SerializeField] private PatternCamera patternCamera;
    [SerializeField] private Material patternMaterial;
    [SerializeField] private Material bwMaterial;

    [SerializeField] private GameObject depthPass;
    [SerializeField] private GameObject bwPass;
    [SerializeField] private float targetThreshold = 0.5f;
    [SerializeField] private float tolerance = 0.15f;

    private bool passed = false;
    public Material GetPatternMaterial()
    {
        return patternMaterial;
    }

    public PatternCamera GetPatternCamera()
    {
        return patternCamera;
    }

    public bool IsPassed()
    {
        if(!((xy_source.connectedPlug == compare0_dest && x2y2_source.connectedPlug == compare1_dest)
            || xy_source.connectedPlug == compare1_dest && x2y2_source.connectedPlug == compare0_dest))
        {
            return false;
        }
        if (compare_source.connectedPlug != fullScreenPass_dest) return false;
        //if (Mathf.Abs(threshold.Value - targetThreshold) > tolerance) return false;
        return true;
    }

    public void OnPassed()
    {
        if(!passed)
        {
            EventBroadcaster.RiddleFinished(this);
            // Game finished
            Debug.Log("Game finished");
            passed = true;
        }
    }

    public void Prepare()
    {
        depthPass.SetActive(false);
        bwPass.SetActive(false);
        //threshold.SetValue(0.3f);
        //Shader.SetGlobalFloat("_Threshold", threshold.Value);
        patternMaterial.SetInt("_IsActive", 1);
    }

    public void Solve()
    {
        EventBroadcaster.ConnectionMade(xy_source, compare0_dest);
        EventBroadcaster.ConnectionMade(x2y2_source, compare1_dest);
        EventBroadcaster.ConnectionMade(compare_source, fullScreenPass_dest);
        SetKnobValue(targetThreshold);
        if (IsPassed())
        {
            OnPassed();
        }
    }

    private void SetKnobValue(float value)
    {
        threshold.SetValue(value);
        bwMaterial.SetFloat("_Threshold", value);
    }

    private void Awake()
    {
        Prepare();
        patternMaterial.SetInt("_IsActive", 0);
    }

    private void OnConnectionMade(Plug source, Plug dest)
    {
        if((source==xy_source || source ==x2y2_source))
        {
            if(dest == fullScreenPass_dest)
            {
                depthPass.SetActive(true);
                bwPass.SetActive(false);
            }
        }
        else if(compare_source.connectedPlug == fullScreenPass_dest)
        {
            if((xy_source.connectedPlug == compare0_dest && x2y2_source.connectedPlug == compare1_dest)||
                xy_source.connectedPlug == compare1_dest && x2y2_source.connectedPlug == compare0_dest)
            {
                depthPass.SetActive(false);
                bwPass.SetActive(true);
            }
        }
        if (IsPassed())
        {
            OnPassed();
        }
    }

    private void OnPlugDisconnected(Plug p)
    {
        if(p== fullScreenPass_dest)
        {
            depthPass.SetActive(false);
            bwPass.SetActive(false);
        }
    }

    private void OnKnobValueChanged(Knob k, float value)
    {
        if(k == threshold)
        {
            bwMaterial.SetFloat("_Threshold", k.Value);
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
