using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HMDInfoManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (!XRSettings.isDeviceActive)
        {
            Debug.Log("No headset detected!");
            return;
        }

        if (XRSettings.loadedDeviceName.Contains("Mock"))
        {
            Debug.Log("Using mock!");
        }
        else
        {
            Debug.Log("Headset in use: " + XRSettings.loadedDeviceName);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
