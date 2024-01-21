using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Safe : MonoBehaviour, IOpenable
{
    [SerializeField] private Transform doors;
    [SerializeField] private Vector3 closedRotation;
    [SerializeField] private Vector3 openRotation;
    public void Close()
    {
        throw new System.NotImplementedException();
    }

    public void Open()
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
