using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlugLooseEnd : MonoBehaviour
{
    private Hand leftHand;
    private Hand rightHand;
    private Plug plug;

    private Hand GetCloserHand()
    {
        float leftDist= Vector3.Distance(transform.position, leftHand.transform.position);
        float rightDist = Vector3.Distance(transform.position, rightHand.transform.position);
        return leftDist < rightDist ? leftHand : rightHand;
    }

    void Start()
    {
        plug = GetComponent<Plug>();
        leftHand = GameObject.FindGameObjectWithTag("LeftHand").GetComponent<Hand>();
        rightHand = GameObject.FindGameObjectWithTag("RightHand").GetComponent<Hand>();
    }

    public void TakePlug()
    {
        GetCloserHand().plugInHand = plug;
    }
    public void Drop()
    {
        GetCloserHand().plugInHand = null;
    }
}
