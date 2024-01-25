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
    [SerializeField] private List<Material> materialPatterns;
    [SerializeField] private Material bwMaterial;

    [SerializeField] private GameObject depthPass;
    [SerializeField] private GameObject bwPass;
    [SerializeField] private float targetThreshold = 0.5f;
    [SerializeField] private float tolerance = 0.15f;
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
        if((xy_source.connectedPlug != compare0_dest && x2y2_source.connectedPlug != compare1_dest)
            || xy_source.connectedPlug != compare1_dest && x2y2_source.connectedPlug != compare0_dest)
        {
            return false;
        }
        if (compare_source.connectedPlug != fullScreenPass_dest) return false;
        if (Mathf.Abs(threshold.Value - targetThreshold) > tolerance) return false;
        return true;
    }

    public void OnPassed()
    {
        EventBroadcaster.RiddleFinished();
        // Game finished
    }

    public void Prepare()
    {
        depthPass.SetActive(false);
        bwPass.SetActive(false);
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
    }
}
