using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderGlobals : MonoBehaviour
{
    [SerializeField] Light mainLight;
    [SerializeField] float bwThreshold;

    void Start()
    {
        Shader.SetGlobalVector("_MainLightDirection", mainLight.transform.forward);
        Shader.SetGlobalFloat("_MainLightAttenuation", mainLight.range);
        Shader.SetGlobalFloat("_Threshold", bwThreshold);
        Shader.SetGlobalColor("_MainLightColor", mainLight.color);
    }

    public void SetBwThreshold(float value)
    {
        Shader.SetGlobalFloat("_Threshold", value);
        bwThreshold = value;
    }

    public void ModifyBwThreshold(float value)
    {
        bwThreshold += value;
        Shader.SetGlobalFloat("_Threshold", bwThreshold);
    }
}
