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
    [SerializeField] private Material patternMaterial;

    [SerializeField] private Material dissolve;
    [SerializeField] private GameObject unicorn;
    [SerializeField] private Collider doorCollider;
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
        if (unicornDist_source.connectedPlug != alphaClipThr_dest) return false;
        if (noise_source.connectedPlug != alpha_dest) return false;
        return true;
    }

    public void OnPassed()
    {
        if(!passed)
        {
            doorCollider.enabled = false;
            EventBroadcaster.RiddleFinished(this);
            passed = true;
        }
    }

    public void Prepare()
    {
        doorCollider.enabled = true;
        EventBroadcaster.PlugDisconnected(alpha_dest);
        EventBroadcaster.PlugDisconnected(alphaClipThr_dest);
        dissolve.SetInt("_UnicornAlpha", 0);
        dissolve.SetInt("_UnicornAlphaClip", 0);
        dissolve.SetInt("_NoiseAlpha", 0);
        dissolve.SetInt("_NoiseAlphaClip", 0);
        patternMaterial.SetInt("_IsActive", 1);
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

    private void OnConnectionMade(Plug source, Plug dest)
    {
        if(source == unicornDist_source)
        {
            if(dest == alpha_dest)
            {
                dissolve.SetInt("_UnicornAlpha", 1);
            }
            else if(dest == alphaClipThr_dest)
            {
                dissolve.SetInt("_UnicornAlphaClip", 1);
            }
        }
        else if(source == noise_source)
        {
            if (dest == alpha_dest)
            {
                dissolve.SetInt("_NoiseAlpha", 1);
            }
            else if (dest == alphaClipThr_dest)
            {
                dissolve.SetInt("_NoiseAlphaClip", 1);
            }
        }
        if (IsPassed())
        {
            OnPassed();
        }
    }

    private void OnPlugDisconnected(Plug p)
    {
        if(p == alphaClipThr_dest)
        {
            dissolve.SetInt("_UnicornAlphaClip", 0);
            dissolve.SetInt("_NoiseAlphaClip", 0);
        }
        else if(p == alpha_dest)
        {
            dissolve.SetInt("_UnicornAlpha", 0);
            dissolve.SetInt("_NoiseAlpha", 0);
        }
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

    private void Awake()
    {
        Prepare();
        patternMaterial.SetInt("_IsActive", 0);
    }

    private void Update()
    {
        dissolve.SetVector("_ActivatorPosition", unicorn.transform.position);
    }
}
