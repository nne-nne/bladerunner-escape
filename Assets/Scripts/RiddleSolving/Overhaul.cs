using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Overhaul : MonoBehaviour
{
    public Material defaultMaterial;
    public int colorIndex;
    private MeshRenderer mr;

    public void SetMaterial(Material m)
    {
        mr.material = m;
    }

    public void RestoreDefaultMaterial()
    {
        mr.material = defaultMaterial;
    }

    void Awake()
    {
        mr = GetComponent<MeshRenderer>();
        defaultMaterial = mr.material;
    }
}
