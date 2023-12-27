using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSimple : MonoBehaviour
{
    [SerializeField] private float sensitivity;
    void Update()
    {
        transform.Rotate(new Vector3(-Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"), 0f)*sensitivity*Time.deltaTime, Space.World);
    }
}
