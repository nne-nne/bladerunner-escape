using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScifiDoor : MonoBehaviour, IOpenable
{
    [SerializeField] private GameObject upper1;
    [SerializeField] private GameObject lower1;
    [SerializeField] private GameObject upper2;
    [SerializeField] private GameObject lower2;
    [SerializeField] private Vector2 upperBounds;
    [SerializeField] private Vector2 lowerBounds;
    [SerializeField] private float openingTime;
    [SerializeField] private Collider col;
    private bool animating = false;
    private IEnumerator OpenCoroutine()
    {
        animating = true;
        float t = 0;
        while(t < openingTime)
        {
            t += Time.deltaTime;
            float upperHeight = Mathf.Lerp(upperBounds.x, upperBounds.y, t / openingTime);
            float lowerHeight = Mathf.Lerp(lowerBounds.x, lowerBounds.y, t / openingTime);
            upper1.transform.position = new Vector3(upper1.transform.position.x, upperHeight, upper1.transform.position.z);
            upper2.transform.position = new Vector3(upper2.transform.position.x, upperHeight, upper2.transform.position.z);
            lower1.transform.position = new Vector3(lower1.transform.position.x, lowerHeight, lower1.transform.position.z);
            lower2.transform.position = new Vector3(lower2.transform.position.x, lowerHeight, lower2.transform.position.z);
            yield return null;
        }
        upper1.SetActive(false);
        upper2.SetActive(false);
        lower1.SetActive(false);
        lower2.SetActive(false);
        col.enabled = false;
        animating = false;
    }

    private IEnumerator CloseCoroutine()
    {
        animating = true;
        upper1.SetActive(true);
        upper2.SetActive(true);
        lower1.SetActive(true);
        lower2.SetActive(true);
        col.enabled = true;
        float t = 0;
        while (t < openingTime)
        {
            t += Time.deltaTime;
            float upperHeight = Mathf.Lerp(upperBounds.y, upperBounds.x, t / openingTime);
            float lowerHeight = Mathf.Lerp(lowerBounds.y, lowerBounds.x, t / openingTime);
            upper1.transform.position = new Vector3(upper1.transform.position.x, upperHeight, upper1.transform.position.z);
            upper2.transform.position = new Vector3(upper2.transform.position.x, upperHeight, upper2.transform.position.z);
            lower1.transform.position = new Vector3(lower1.transform.position.x, lowerHeight, lower1.transform.position.z);
            lower2.transform.position = new Vector3(lower2.transform.position.x, lowerHeight, lower2.transform.position.z);
            yield return null;
        }
        animating = false;
    }
    public void Close()
    {
        if(!animating)
        {
            StartCoroutine(OpenCoroutine());
        }
    }

    public void Open()
    {
        if (!animating)
        {
            StartCoroutine(CloseCoroutine());
        }
    }
}
