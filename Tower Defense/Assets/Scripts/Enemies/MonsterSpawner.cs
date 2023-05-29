using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public static MonsterSpawner instance;
    
    public GameObject enemyPrefab;
    public int enemyAmount = 5;
    public int stage = 0;
    public int spawnOften = 1;

    public void Awake()
    {
        instance = this;
    }

    public IEnumerator SpawnEnemiesCor()
    {
        for(int i = 0; i < enemyAmount; i++)
        {
            Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(spawnOften);
        }

        yield return null;
    }
    
}
