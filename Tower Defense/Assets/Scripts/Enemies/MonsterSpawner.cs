using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;

public class MonsterSpawner : MonoBehaviour
{

    public GameObject enemyPrefab;
    public GameObject gigaEnemyPrefab;
    public int addEnemy = 3;
    public int enemySpeed = 2;
    public int stage = 0;
    public int spawnOften = 1;
    public int delayBetweenWaves = 5;

    public int chances = 50;
    private Random random = new Random();
    
    public void Start()
    {
        GetStage();
        StartCoroutine(SpawnEnemiesCor());
    }

    
    
    public IEnumerator SpawnEnemiesCor()
    {
        GameObject currecntEnemyPrefab;
        while (true)
        {
            if (stage % 3 == 0)
            {
                enemySpeed++;
            }
            float totalEnemies = addEnemy * stage;
            Debug.Log(addEnemy + " " + stage + " " + totalEnemies);
            for(int i = 0; i < totalEnemies; i++)
            {
                int randomNR = random.Next(0,100);
                if (randomNR < chances)
                {
                    currecntEnemyPrefab = gigaEnemyPrefab;
                }
                else
                {
                    currecntEnemyPrefab = enemyPrefab;
                }
                
                GameObject enemyMover = Instantiate(currecntEnemyPrefab, transform.position, Quaternion.identity);
                enemyMover.GetComponent<EnemyMover>().UpdateEnemySpeed(enemySpeed);
                
                yield return new WaitForSeconds(spawnOften);
            }
            GetStage();
            yield return new WaitForSeconds(delayBetweenWaves);
        }
    }

    public void GetStage()
    {
        stage = GameManager.instance.NextStage();
    }
    
}
