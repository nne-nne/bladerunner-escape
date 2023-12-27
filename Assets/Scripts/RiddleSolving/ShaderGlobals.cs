using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderGlobals : MonoBehaviour
{
    [SerializeField] Light mainLight;

    void Start()
    {
        Shader.SetGlobalVector("_MainLightDirection", mainLight.transform.forward);
        Shader.SetGlobalFloat("_MainLightAttenuation", mainLight.range);
        Shader.SetGlobalColor("_MainLightColor", mainLight.color);
    }
}
