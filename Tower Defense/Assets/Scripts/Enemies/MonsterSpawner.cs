using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;

public class MonsterSpawner : MonoBehaviour
{
    //Enemies Prefab
    public GameObject enemyPrefab;
    public GameObject gigaEnemyPrefab;
    
    //Stage data
    private int stage;
    private int addEnemy;//SO
    private int totalEnemies;//będę sobie liczył
    private int spawnOften;

    //Enemy data
    private int enemySpeed;//SO
    private int enemyHP;//SO
    
    
    
    public void Start()
    {
        StartCoroutine(SpawnEnemiesCor());
    }

    public IEnumerator SpawnEnemiesCor()
    {
        while (true)
        {
            for(int i = 0; i < totalEnemies; i++)
            {
                GameObject enemyMover = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
                enemyMover.GetComponent<EnemyMover>().UpdateEnemySpeed(enemySpeed);
                
                yield return new WaitForSeconds(spawnOften);
            }

        }
    }

    public void InitiateData(StageListSO stageList,int stage,int spawnHowOften)
    {
        
    }

    public void UpdateStageNumber()
    {
        stage++;
    }
    


}
