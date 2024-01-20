using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialManager : MonoBehaviour
{
    public List<Material> celShadingMaterials;
    public List<Overhaul> overhauls;
    public Transform world;

    public void RestoreDefaultMaterials()
    {
        foreach (var o in overhauls)
        {
            o.RestoreDefaultMaterial();
        }
    }

    public void SetCellShading()
    {
        foreach(var o in overhauls)
        {
            o.SetMaterial(celShadingMaterials[o.colorIndex]);
        }
    }

    public void Awake()
    {
        var worldOverhauls = world.GetComponentsInChildren<Overhaul>();
        for(int i = 0; i < worldOverhauls.Length; i++)
        {
            overhauls.Add(worldOverhauls[i]);
            Debug.Log("added overhaul");
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            SetCellShading();
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            RestoreDefaultMaterials();
        }
    }
}
