using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiddleTutorial2 : MonoBehaviour, IRiddle
{
    [SerializeField] private Material elevator2;
    [SerializeField] private List<Texture2D> patternTextures;
    [SerializeField] private int targetTextureIndex;
    [SerializeField] private Elevator elevator;
    [SerializeField] private PatternCamera patternCamera;
    [SerializeField] private List<Material> materialPatterns;
    [SerializeField] private Plug p1;
    [SerializeField] private Plug p2;
    [SerializeField] private Plug destination;

    private int currentTextureIndex;
    
    private void Awake()
    {
        currentTextureIndex = 0;
        elevator2.SetTexture("_PatternTexture", patternTextures[0]);
        elevator2.SetFloat("_Overlay", 0f);
    }

    public void SetPatternTexture(int option)
    {
        int index = option - 1;
        currentTextureIndex = index;
        if(index >= 0)
        {
            elevator2.SetTexture("_PatternTexture", patternTextures[index]);
            elevator2.SetFloat("_Overlay", 30f);
        }
        else
        {
            elevator2.SetFloat("_Overlay", 0f);
        }
        CheckWinCondition();
    }

    private bool CheckWinCondition()
    {
        if (currentTextureIndex == targetTextureIndex)
        {
            Debug.Log("Riddle Tutorial 2: Passed");
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Prepare()
    {
        
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            EventBroadcaster.ConnectionMade(p1, destination);
        }
        if(Input.GetKeyDown(KeyCode.Alpha4))
        {
            EventBroadcaster.ConnectionMade(p2, destination);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            EventBroadcaster.PlugDisconnected(p1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            EventBroadcaster.PlugDisconnected(p2);
        }
    }

    public void OnPassed()
    {
        elevator.StartAnimation();
        EventBroadcaster.RiddleFinished();
    }

    public bool IsPassed()
    {
        return CheckWinCondition();
    }

    public List<Material> GetMaterialPatterns()
    {
        return materialPatterns;
    }

    public PatternCamera GetPatternCamera()
    {
        return patternCamera;
    }
}
