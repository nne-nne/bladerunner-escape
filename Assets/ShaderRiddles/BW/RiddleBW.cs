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
