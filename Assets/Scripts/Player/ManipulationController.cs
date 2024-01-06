using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManipulationController : MonoBehaviour
{
    [SerializeField] private float knobSensitivity = 100f;
    public ITakeable LeftHandItem { get; private set; }
    public ITakeable RightHandItem { get; private set; }

    private ITakeable RaycastTakeable()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            ITakeable item = hit.collider.GetComponent<ITakeable>();
            return item;
        }
        return null;
    }

    private void TryTake(bool leftHand)
    {
        // TODO: change this line to use VR controllers
        ITakeable item = RaycastTakeable();

        if(item != null)
        {
            if(leftHand)
            {
                TakeItem(item, true);
            }
            else
            {
                TakeItem(item, false);
            }
        }
    }

    private void TakeItem(ITakeable item, bool leftHand)
    {
        if (leftHand)
        {
            if (LeftHandItem != null)
            {
                LeftHandItem.Drop();
            }
            LeftHandItem = item;
            LeftHandItem.Take();
        }
        else
        {
            if (RightHandItem != null)
            {
                RightHandItem.Drop();
            }
            RightHandItem = item;
            RightHandItem.Take();
        }
    }

    public void Drop(bool leftHand)
    {
        if(leftHand && LeftHandItem!=null)
        {
            LeftHandItem.Drop();
            LeftHandItem = null;
        }
        else if(RightHandItem !=null)
        {
            RightHandItem.Drop();
            RightHandItem = null;
        }
    }

    private void MouseKeyboardControls()
    {
        // mouse and keyboard controls:
        // LPM - left hand take
        // RPM - right hand take
        // left ctrl + mouse button - drop
        // 9 and 0 to rotate right hand knob
        // 1 and 2 to rotate left hand knob
        if (Input.GetMouseButtonDown(0))
        {
            if (Input.GetKey(KeyCode.LeftControl))
            {
                Drop(true);
            }
            else
            {
                TryTake(true);
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            if (Input.GetKey(KeyCode.LeftControl))
            {
                Drop(false);
            }
            else
            {
                TryTake(false);
            }
        }
        if(Input.GetKey(KeyCode.Alpha1) && LeftHandItem is Knob)
        {
            EventBroadcaster.KnobValueChanged(LeftHandItem as Knob, -knobSensitivity * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.Alpha2) && LeftHandItem is Knob)
        {
            EventBroadcaster.KnobValueChanged(LeftHandItem as Knob, knobSensitivity * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.Alpha9) && RightHandItem is Knob)
        {
            EventBroadcaster.KnobValueChanged(RightHandItem as Knob, -knobSensitivity * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.Alpha0) && RightHandItem is Knob)
        {
            EventBroadcaster.KnobValueChanged(RightHandItem as Knob, knobSensitivity * Time.deltaTime);
        }
    }

    private void Update()
    {
        MouseKeyboardControls();
    }
}
