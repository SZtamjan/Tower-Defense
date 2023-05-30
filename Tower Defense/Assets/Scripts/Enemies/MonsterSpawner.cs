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
    private StageListSO stageList;
    private int stage;//Updatuje tutaj
    private int addEnemy;//SO
    private int totalEnemies=0;//będę sobie liczył
    private int spawnOften;//GM

    //Enemy data
    private int enemySpeed;//SO
    private int enemyHP;//SO
    
    
    
    public void Start()
    {
        //StartCoroutine(SpawnEnemiesCor());
    }

    public IEnumerator SpawnEnemiesCor()
    {
        while (true)
        {
            Debug.Log(totalEnemies);
            totalEnemies += addEnemy; //Tu narazie sie pierdoli i buguje unity bo jest null pewnie
            Debug.Log(totalEnemies + " " + addEnemy);
            
            for(int i = 0; i < totalEnemies; i++)
            {
                GameObject enemyMover = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
                enemyMover.GetComponent<EnemyMover>().UpdateEnemySpeed(enemySpeed);
                
                yield return new WaitForSeconds(spawnOften);
            }

        }
    }

    public void InitiateData(StageListSO stageList,int stage,int spawnOften)
    {
        this.stageList = stageList;
        this.stage = stage;
        this.spawnOften = spawnOften;
    }

    public void UpdateStage()
    {
        stage++; //Jak stage jest 1 to index też 1 a mam od 0
    }
    


}
