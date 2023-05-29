using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int stage = 0;
    void Start()
    {
        StartCoroutine(MonsterSpawner.instance.SpawnEnemiesCor());
    }

    public int NextStage()
    {
        stage++;
        return stage;
    }
    
}
