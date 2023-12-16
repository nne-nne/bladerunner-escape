using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveDistance : MonoBehaviour
{
    public List<Material> dissolveMaterials;
    private Vector3 prevPos;

    void Start()
    {
        prevPos = transform.position;
    }

    void Update()
    {
        if(transform.position != prevPos)
        {
            prevPos = transform.position;
        }
        foreach(Material m in dissolveMaterials)
        {
            m.SetVector("_ActivatorPosition", transform.position);
        }
    }
}
