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
    [SerializeField] private Material patternMaterial;
    [SerializeField] private Plug p1;
    [SerializeField] private Plug p2;
    [SerializeField] private Plug destination;

    private int currentTextureIndex;
    
    private void Awake()
    {
        patternMaterial.SetInt("_IsActive", 0);
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
        if(CheckWinCondition())
        {
            OnPassed();
        }
    }

    private bool CheckWinCondition()
    {
        if (currentTextureIndex == targetTextureIndex)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Prepare()
    {
        currentTextureIndex = 0;
        elevator2.SetTexture("_PatternTexture", patternTextures[0]);
        elevator2.SetFloat("_Overlay", 0f);
        patternMaterial.SetInt("_IsActive", 1);
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
        EventBroadcaster.RiddleFinished(this);
    }

    public bool IsPassed()
    {
        return CheckWinCondition();
    }

    public Material GetPatternMaterial()
    {
        return patternMaterial;
    }

    public PatternCamera GetPatternCamera()
    {
        return patternCamera;
    }

    private void OnConnectionMade(Plug source, Plug destination)
    {
        if(destination == this.destination)
        {
            if(source == p1)
            {
                SetPatternTexture(1);
            }
            else if(source == p2)
            {
                SetPatternTexture(2);
            }
        }
    }

    private void OnEnable()
    {
        EventBroadcaster.OnConnectionMade += OnConnectionMade;
    }

    private void OnDisable()
    {
        EventBroadcaster.OnConnectionMade -= OnConnectionMade;
    }

    public void Solve()
    {
        EventBroadcaster.ConnectionMade(p1, destination);
    }

}
