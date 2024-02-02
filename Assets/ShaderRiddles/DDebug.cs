using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DDebug : MonoBehaviour
{
    [SerializeField] private Transform XROrigin;
    [SerializeField] private TMP_Text xrOriginPosText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        xrOriginPosText.text = XROrigin.transform.position.ToString();
    }
}
