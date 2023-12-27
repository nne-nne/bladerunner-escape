using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCharacterController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed;

    private Rigidbody rb;
    private Vector3 prevMousePos;


    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        prevMousePos = Input.mousePosition;
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = transform.forward * speed * Input.GetAxis("Vertical");
        rb.angularVelocity = Vector3.up * rotationSpeed * Input.GetAxis("Horizontal");


        Vector3 mouseDeltaPos = Input.mousePosition - prevMousePos;
        prevMousePos = Input.mousePosition;
    }
}
