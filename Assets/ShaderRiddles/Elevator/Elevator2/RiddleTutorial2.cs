using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiddleTutorial2 : MonoBehaviour, IRiddle
{
    [SerializeField] private Material elevator2;
    [SerializeField] private List<Texture2D> patternTextures;
    [SerializeField] private int targetTextureIndex;
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
            elevator2.SetFloat("_Overlay", 1f);
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
        throw new System.NotImplementedException();
    }

    public void OnPassed()
    {
        throw new System.NotImplementedException();
    }

    public bool IsPassed()
    {
        throw new System.NotImplementedException();
    }

    public List<Material> GetMaterialPatterns()
    {
        throw new System.NotImplementedException();
    }

    public PatternCamera GetPatternCamera()
    {
        throw new System.NotImplementedException();
    }
}
