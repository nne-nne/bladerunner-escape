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
    public List<Material> GetMaterialPatterns()
    {
        throw new System.NotImplementedException();
    }

    public PatternCamera GetPatternCamera()
    {
        throw new System.NotImplementedException();
    }

    public bool IsPassed()
    {
        throw new System.NotImplementedException();
    }

    public void OnPassed()
    {
        throw new System.NotImplementedException();
    }

    public void Prepare()
    {
        throw new System.NotImplementedException();
    }

    public void Solve()
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
