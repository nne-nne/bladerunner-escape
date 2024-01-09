using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternCamera : MonoBehaviour
{
    [SerializeField] private Material cameraTextureMaterial;

    public Material CameraTextureMaterial { get => cameraTextureMaterial; }
}
