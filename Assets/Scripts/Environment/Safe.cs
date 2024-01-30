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
        doors.transform.localRotation = Quaternion.Euler(closedRotation);
    }

    public void Open()
    {
        doors.transform.localRotation = Quaternion.Euler(openRotation);
    }
}
