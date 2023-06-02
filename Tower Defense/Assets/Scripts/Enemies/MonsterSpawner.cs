using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;

public class MonsterSpawner : MonoBehaviour
{
    //Enemy data
    private WaypointsSO waypointsSO;
    private GameObject enemyPrefab;
    private int enemySpeed;
    private int enemyHP;

    //Stage data
    private StageListSO stageList;
    private int stage;

    private float spawnFrequency;
    //Stage data wyliczane
    private int addEnemy;
    private int totalEnemies=0;
    

    
    public void InitiateData(StageListSO stageList,int stage,WaypointsSO waypointsSO)
    {
        this.stageList = stageList;
        this.stage = stage;
        this.waypointsSO = waypointsSO;

        addEnemy = stageList.AddEnemy;
        spawnFrequency = stageList.stageInfoList[0].spawnFrequency;
    }
    
    public void StartStage()
    {
        UpdateEnemyStats();
        StartCoroutine(SpawnEnemiesCor());
    }
    
    public IEnumerator SpawnEnemiesCor()
    {
        totalEnemies += addEnemy;

        for(int i = 0; i < totalEnemies; i++) 
        { 
            GameObject enemyMover = Instantiate(enemyPrefab, transform.position, Quaternion.identity); 
            enemyMover.GetComponent<EnemyMover>().UpdateEnemyDataAndStart(enemyHP,enemySpeed,waypointsSO); 
            yield return new WaitForSeconds(spawnFrequency);
        }

        StartCoroutine(FindEnemiesOnMap());
    }

    private void UpdateEnemyStats()
    {
        int index = stage - 1;
        List<StageInfoSO> enemyInfo = stageList.stageInfoList;
        enemyHP = enemyInfo[index].enemyHP;
        enemySpeed = enemyInfo[index].enemySpeed;
        enemyPrefab = enemyInfo[index].enemyPrefab;
        spawnFrequency = enemyInfo[index].spawnFrequency;
    }

    private IEnumerator FindEnemiesOnMap()
    {
        bool objOnMap = true;
        do
        {
            yield return new WaitForSeconds(0.5f);
            if (GameObject.FindWithTag("Enemy") == null)
            {
                objOnMap = false;
            }
        } while (objOnMap);
        
        //No enemies
        GameManager.Instance.UpdateGameState(GameManager.GameState.CheckAndWait);
        yield return null;
    }
    
    public void UpdateStage()
    {
        stage++;
    }

    public int GetStageNumber()
    {
        return stage;
    }

}
