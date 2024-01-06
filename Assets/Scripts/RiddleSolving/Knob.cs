using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knob : MonoBehaviour, ITakeable
{
    [SerializeField] public float Value { get; private set; }
    [SerializeField] private GameObject activationMarker;

    public void Drop()
    {
        activationMarker.SetActive(false);
    }

    public void Take()
    {
        activationMarker.SetActive(true);
    }

    public void Use()
    {
        Debug.Log("knob should be used via event broadcaster");
    }

    private void Rotate(float value)
    {
        Value = (Value + value) % 1f;
        if(Value < 0)
        {
            Value += 1;
        }
        VisualizeKnobValueChanged(Value * 360);
    }

    private void OnKnobValueChanged(Knob knob, float value)
    {
        if(knob == this)
        {
            Rotate(value);
        }
    }

    private void VisualizeKnobValueChanged(float newValue)
    {
        transform.rotation = Quaternion.Euler(new Vector3(newValue, transform.rotation.y, transform.rotation.z));
    }

    private void Start()
    {
        activationMarker.SetActive(false);
        VisualizeKnobValueChanged(Value);
    }

    private void OnEnable()
    {
        EventBroadcaster.OnKnobValueChanged += OnKnobValueChanged;
    }

    private void OnDisable()
    {
        EventBroadcaster.OnKnobValueChanged -= OnKnobValueChanged;
    }
}
