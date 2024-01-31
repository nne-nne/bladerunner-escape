using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnobPoke : MonoBehaviour
{
    [SerializeField] private float rotationStep = 0.1f;
    private Knob knob;

    public void Rotate()
    {
        EventBroadcaster.KnobValueChanged(knob, rotationStep);
    }

    private void Awake()
    {
        knob = GetComponent<Knob>();
    }
}
