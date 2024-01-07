using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plug : MonoBehaviour, ITakeable
{
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Drop()
    {
        rb.isKinematic = true;
    }

    public void Take()
    {
        rb.isKinematic = false;
    }

    public void Use()
    {
        Debug.Log("Plug is used via manipulator");
    }
}
