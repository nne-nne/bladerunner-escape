using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public Plug plugInHand;
    public Side side;
}

public enum Side
{
    LEFT,
    RIGHT,
}
