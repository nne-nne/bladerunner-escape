using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlugLooseEnd : MonoBehaviour
{
    private Hand hand;
    private Plug plug;

    void Start()
    {
        hand = FindObjectOfType<Hand>();
        plug = GetComponent<Plug>();
    }

    public void TakePlug()
    {
        hand.plugInHand = plug;
    }
    public void Drop()
    {
        hand.plugInHand = null;
    }
}
