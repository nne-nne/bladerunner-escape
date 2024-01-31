using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plug : MonoBehaviour, ITakeable
{
    private Rigidbody rb;
    public Plug connectedPlug;

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

    private void OnConnectionMade(Plug from, Plug to)
    {
        if (from == this && to.connectedPlug == null)
        {
            // the following bit of code should be executed by XR plugin socket interactable
            //transform.position = to.transform.position;
            //transform.rotation = to.transform.rotation;
            //rb.isKinematic = true;
            connectedPlug = to;
            to.connectedPlug = from;
        }
    }

    private void OnPlugDisconnected(Plug p)
    {
        if(p == this && rb != null)
        {
            //rb.isKinematic = false;
            if(connectedPlug!=null)
            {
                Plug other = connectedPlug;
                other.connectedPlug = null;
                connectedPlug = null;
                EventBroadcaster.PlugDisconnected(other);
            }
        }
    }

    private void OnEnable()
    {
        EventBroadcaster.OnConnectionMade += OnConnectionMade;
        EventBroadcaster.OnPlugDisconnected += OnPlugDisconnected;
    }

    private void OnDisable()
    {
        EventBroadcaster.OnConnectionMade -= OnConnectionMade;
        EventBroadcaster.OnPlugDisconnected -= OnPlugDisconnected;
    }

    public void Use()
    {
        Debug.Log("Plug is used via manipulator");
    }
}
