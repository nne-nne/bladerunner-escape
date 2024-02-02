using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CableDestination : MonoBehaviour
{
    [SerializeField] private Plug destPlug;
    private Hand hand;

    public void Disconect()
    {
        EventBroadcaster.PlugDisconnected(destPlug);
    }

    private void Start()
    {
        hand = FindObjectOfType<Hand>();
    }

    public void Connect()
    {
        if(hand.plugInHand != null)
        {
            EventBroadcaster.ConnectionMade(hand.plugInHand, destPlug);
        }
    }
}
