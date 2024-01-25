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
