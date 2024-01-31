using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialManager : MonoBehaviour
{
    public List<Material> celShadingMaterials;
    public List<Material> cellPatternMaterials;
    public List<Material> halftonePatternMaterials;
    public List<Overhaul> overhauls;
    public Transform world;

    [SerializeField] private Plug mainLightSource;

    public void RestoreDefaultMaterials()
    {
        foreach (var o in overhauls)
        {
            if(!o.patternOnly)
            {
                o.RestoreDefaultMaterial();
            }
        }
    }

    public void SetCellShading()
    {
        foreach(var o in overhauls)
        {
            if(!o.patternOnly)
            {
                o.SetMaterial(celShadingMaterials[o.colorIndex]);
            }
        }
    }

    public void SetCellPattern()
    {
        foreach (var o in overhauls)
        {
            if (o.patternOnly)
            {
                o.SetMaterial(cellPatternMaterials[o.colorIndex]);
            }
        }
    }

    public void SetHalftonePattern()
    {
        foreach (var o in overhauls)
        {
            if (o.patternOnly)
            {
                o.SetMaterial(halftonePatternMaterials[o.colorIndex]);
            }
        }
    }

    public void SetDefaultPattern()
    {
        foreach (var o in overhauls)
        {
            if (o.patternOnly)
            {
                o.RestoreDefaultMaterial();
            }
        }
    }

    private void OnConnectionMade(Plug source, Plug dest)
    {
        // change to fancy materials on main light source plugged
        if(source == mainLightSource)
        {
            SetCellShading();
        }
    }

    private void OnPlugDisconnected(Plug p)
    {
        // restore default materials on main light source unplugged
        if(p == mainLightSource)
        {
            RestoreDefaultMaterials();
        }
    }

    public void SetMaterialsProperty(string property, float value)
    {
        foreach (var mat in celShadingMaterials)
        {
            mat.SetFloat(property, value);
        }
    }

    public void SetMaterialsPropertyInt(string property, int value)
    {
        foreach (var mat in celShadingMaterials)
        {
            mat.SetInt(property, value);
        }
    }

    private void OnEnable()
    {
        EventBroadcaster.OnConnectionMade += OnConnectionMade;
        EventBroadcaster.OnPlugDisconnected += OnPlugDisconnected;
    }

    public void Awake()
    {
        var worldOverhauls = world.GetComponentsInChildren<Overhaul>();
        for(int i = 0; i < worldOverhauls.Length; i++)
        {
            overhauls.Add(worldOverhauls[i]);
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
