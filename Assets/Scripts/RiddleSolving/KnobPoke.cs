using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnobPoke : MonoBehaviour
{
    [SerializeField] private static float sensitivity = 1.0f;
    [SerializeField] private float rotationStep = 0.1f;
    private Knob knob;
    private Hand leftHand;
    private Hand rightHand;


    private Hand controllingHand;
    private Quaternion prevControlRotation;

    private Hand GetCloserHand()
    {
        float leftDist = Vector3.Distance(transform.position, leftHand.transform.position);
        float rightDist = Vector3.Distance(transform.position, rightHand.transform.position);
        return leftDist < rightDist ? leftHand : rightHand;
    }

    public void Rotate()
    {
        EventBroadcaster.KnobValueChanged(knob, rotationStep);
    }

    public void AssignHand()
    {
        controllingHand = GetCloserHand();
        prevControlRotation = controllingHand.transform.localRotation;
    }

    public void Drop()
    {
        controllingHand = null;
    }

    private void Awake()
    {
        knob = GetComponent<Knob>();
        leftHand = GameObject.FindGameObjectWithTag("LeftHand").GetComponent<Hand>();
        rightHand = GameObject.FindGameObjectWithTag("RightHand").GetComponent<Hand>();
    }

    private float CalculateDeltaRotation()
    {
        Quaternion currentHandRotation = controllingHand.transform.localRotation;
        float deltaRotation = currentHandRotation.eulerAngles.y - prevControlRotation.eulerAngles.y;
        return deltaRotation * sensitivity;
    }

    private void Update()
    {
        if(controllingHand != null)
        {
            float deltaRotation = CalculateDeltaRotation();
            EventBroadcaster.KnobValueChanged(knob, deltaRotation);
            prevControlRotation = controllingHand.transform.localRotation;
        }
    }
}
