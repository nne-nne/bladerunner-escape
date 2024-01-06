using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knob : MonoBehaviour, ITakeable
{
    [SerializeField] private float value;
    [SerializeField] private float tolerance;
    [SerializeField] private GameObject activationMarker;

    public void Drop()
    {
        Debug.Log("knob dropped");
        activationMarker.SetActive(false);
    }

    public void Take()
    {
        Debug.Log("knob taken");
        activationMarker.SetActive(true);
    }

    public void Use()
    {
        Debug.Log("knob should be used via event broadcaster");
    }

    private void Rotate(float value)
    {
        this.value = (this.value + value) % 360f;
        VisualizeKnobValueChanged(this.value);
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
        VisualizeKnobValueChanged(value);
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
