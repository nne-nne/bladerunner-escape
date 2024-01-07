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
    private bool isAnimating = false;


    private IEnumerator AnimationCoroutine()
    {
        isAnimating = true;
        float t = 0.0f;
        doorLower.Close();
        yield return new WaitForSeconds(launchTime);
        Vector3 originalPosition = transform.position;
        while( t < rideTime)
        {
            transform.position = Vector3.Lerp(originalPosition, targetPosition, t / rideTime);
            t += Time.deltaTime;
            yield return null;
        }
        doorUpper.Open();
    }

    public void StartAnimation()
    {
        if (isAnimating) return;

        StartCoroutine(AnimationCoroutine());
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
