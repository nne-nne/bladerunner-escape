using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField] private ScifiDoor doorLower;
    [SerializeField] private ScifiDoor doorUpper;
    [SerializeField] private float launchTime;
    [SerializeField] private float rideTime;
    [SerializeField] private Vector3 targetPosition;
    [SerializeField] private Transform player;

    private bool isAnimating = false;


    private IEnumerator AnimationCoroutine()
    {
        isAnimating = true;
        doorLower.Close();
        yield return new WaitForSeconds(launchTime);
        yield return new WaitForSeconds(rideTime);
        Transform playerParent = player.parent;
        player.SetParent(this.transform);
        transform.position = targetPosition;
        player.SetParent(playerParent);
        doorUpper.Open();
    }

    public void StartAnimation()
    {
        if (isAnimating) return;

        StartCoroutine(AnimationCoroutine());
    }

    void Awake()
    {
        
    }

    void Update()
    {
        
    }
}
