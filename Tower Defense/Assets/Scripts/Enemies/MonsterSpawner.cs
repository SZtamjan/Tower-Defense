using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public static MonsterSpawner instance;
    public GameObject enemyPrefab;

    public void Awake()
    {
        instance = this;
    }
    
    public IEnumerator SpawnEnemiesCor()
    {
        while (true)
        {
            Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(2f);
        }
    }
    
}
