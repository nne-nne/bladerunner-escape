using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManipulationController : MonoBehaviour
{
    public ITakeable LeftHandItem { get; private set; }
    public ITakeable RightHandItem { get; private set; }

    private void TakeItem(ITakeable item, bool leftHand)
    {
        if (leftHand)
        {
            if (LeftHandItem != null)
            {
                LeftHandItem.Drop();
            }
            LeftHandItem = item;
        }
        else
        {
            if (RightHandItem != null)
            {
                RightHandItem.Drop();
            }
            RightHandItem = item;
        }
    }

    public void TakeKnob(bool leftHand, Knob knob)
    {
        TakeItem(knob, leftHand);
    }

    public void TakePlug(bool leftHand, Plug plug)
    {
        TakeItem(plug, leftHand);
    }

    public void Drop(bool leftHand)
    {
        if(leftHand)
        {
            LeftHandItem.Drop();
            LeftHandItem = null;
        }
        else
        {
            RightHandItem.Drop();
            RightHandItem = null;
        }
    }
}
