using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cable : MonoBehaviour
{
    [SerializeField] private GameObject segmentPrefab;
    [SerializeField] private GameObject looseEndPrefab;
    [SerializeField] private int segmentsCount;
    [SerializeField] private Vector3 offset;
    [SerializeField] private GameObject origin;
    [SerializeField] private LineRenderer lr;
    [SerializeField] private List<GameObject> spawnedObjects;

    private Rigidbody cachedBody;
    private Vector3[] positions;

    public void Generate()
    {
        foreach(GameObject g in spawnedObjects)
        {
            DestroyImmediate(g);
        }
        spawnedObjects.Clear();
        for(int i = 0; i < segmentsCount; i++)
        {
            GameObject newSegment = Instantiate(segmentPrefab, origin.transform.position + (i + 1) * offset, Quaternion.identity, transform);
            SpringJoint joint = newSegment.GetComponent<SpringJoint>();
            spawnedObjects.Add(newSegment);
            if(spawnedObjects.Count > 1)
            {
                joint.connectedBody = cachedBody;
            }
            else
            {
                joint.connectedBody = origin.GetComponent<Rigidbody>();
            }
            cachedBody = joint.GetComponent<Rigidbody>();
        }
        GameObject looseEnd = Instantiate(looseEndPrefab, origin.transform.position + (segmentsCount+1.5f) * offset, Quaternion.identity, transform);
        SpringJoint looseJoint = looseEnd.GetComponent<SpringJoint>();
        looseJoint.connectedBody = cachedBody;
        spawnedObjects.Add(looseEnd);
        positions = new Vector3[spawnedObjects.Count + 1];
        lr.positionCount = spawnedObjects.Count + 1;
    }

    void Awake()
    {
        lr.positionCount = spawnedObjects.Count + 1;
    }

    void Update()
    {
        if (positions != null)
        {
            positions[0] = origin.transform.position;
            for (int i = 0; i < spawnedObjects.Count; i++)
            {
                positions[i + 1] = spawnedObjects[i].transform.position;
            }
            lr.SetPositions(positions);
        }
        else
        {
            positions = new Vector3[spawnedObjects.Count + 1];
        }
    }
}
