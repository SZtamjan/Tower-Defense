using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    public int stageGM = 0;

    private void Awake()
    {
        instance = this;
    }

    public int NextStage()
    {
        stageGM++;
        return stageGM;
    }
    
}
