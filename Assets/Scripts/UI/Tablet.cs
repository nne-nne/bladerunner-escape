using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tablet : MonoBehaviour
{
    [SerializeField] private GameObject tabletScreen;
    [SerializeField] private List<PatternCamera> patternCameras;
    [SerializeField] private Image tabletImage;
    [SerializeField] private RiddlesManager riddleManager;

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

    public void SetPatternMaterial(Material m)
    {
        tabletImage.material = m;
    }

    //private void OnRiddleFinished(IRiddle prevRiddle)
    //{

    //    if(patternCameras.Count > 0)
    //    {
    //        patternCameras.RemoveAt(0);
    //        if (patternCameras.Count > 0)
    //        {
    //            tabletImage.material = patternCameras[0].CameraTextureMaterial;
    //        }
    //    }
    //}

    private void Start()
    {
        riddleManager = FindObjectOfType<RiddlesManager>();
    }

    //private void OnEnable()
    //{
    //    EventBroadcaster.OnRiddleFinished += OnRiddleFinished;
    //}

    //private void OnDisable()
    //{
    //    EventBroadcaster.OnRiddleFinished -= OnRiddleFinished;
    //}
}
