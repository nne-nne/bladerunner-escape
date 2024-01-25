using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiddleDissolve : MonoBehaviour, IRiddle
{
    [SerializeField] private Plug alpha_dest;
    [SerializeField] private Plug alphaClipThr_dest;
    [SerializeField] private Plug noise_source;
    [SerializeField] private Plug unicornDist_source;

    [SerializeField] private PatternCamera patternCamera;
    [SerializeField] private List<Material> materialPatterns;

    [SerializeField] private Material dissolve;
    [SerializeField] private GameObject unicorn;
    [SerializeField] private Collider doorCollider;
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
        if (unicornDist_source.connectedPlug != alphaClipThr_dest) return false;
        if (noise_source.connectedPlug != alpha_dest) return false;
        return true;
    }

    public void OnPassed()
    {
        doorCollider.enabled = false;
        EventBroadcaster.RiddleFinished();
    }

    public void Prepare()
    {
        doorCollider.enabled = true;
    }
    public void Solve()
    {
        EventBroadcaster.ConnectionMade(noise_source, alpha_dest);
        EventBroadcaster.ConnectionMade(unicornDist_source, alphaClipThr_dest);
        if (IsPassed())
        {
            OnPassed();
        }
    }
}
