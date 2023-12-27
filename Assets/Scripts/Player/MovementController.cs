using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] private Transform spawn;
    private void Move()
    {
        // TODO: implement
        throw new System.NotImplementedException("I'm just a poor boy, I need love and the implementation of Move()");
    }

    private void Jump()
    {
        // TODO: implement
        throw new System.NotImplementedException("I'm just a poor boy, I need love and the implementation of Jump()");
    }

    private void Awake()
    {
        if(spawn != null)
        {
            transform.position = spawn.position;
            transform.rotation = spawn.rotation;
        }
    }
}
