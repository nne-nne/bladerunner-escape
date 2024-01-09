using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tablet : MonoBehaviour
{
    [SerializeField] private GameObject tabletScreen;
    [SerializeField] private List<PatternCamera> patternCameras;
    [SerializeField] private Image tabletImage;

    public void Show()
    {
        tabletScreen.SetActive(true);
    }

    public void Hide()
    {
        tabletScreen.SetActive(false);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            if(tabletScreen.activeInHierarchy)
            {
                Hide();
            }
            else
            {
                Show();
            }
        }
    }

    private void OnRiddleFinished()
    {
        patternCameras.RemoveAt(0);
        if(patternCameras.Count > 0)
        {
            tabletImage.material = patternCameras[0].CameraTextureMaterial;
        }
    }

    private void OnEnable()
    {
        EventBroadcaster.OnRiddleFinished += OnRiddleFinished;
    }

    private void OnDisable()
    {
        EventBroadcaster.OnRiddleFinished -= OnRiddleFinished;
    }
}
