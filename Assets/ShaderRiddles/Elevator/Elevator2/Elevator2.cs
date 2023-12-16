using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator2 : MonoBehaviour
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
            Debug.Log("Elevator2: Passed");
            return true;
        }
        else
        {
            return false;
        }
    }
}
