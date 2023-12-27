using System.Collections.Generic;
using UnityEngine;

public interface IRiddle
{
    public void Prepare();
    public void OnPassed();
    public bool IsPassed();
    public List<Material> GetMaterialPatterns();
    public PatternCamera GetPatternCamera();
}