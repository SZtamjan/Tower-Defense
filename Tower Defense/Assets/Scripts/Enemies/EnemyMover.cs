using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    private WaypointsSO waypointSO;

    public int enemyID = 0;
    
    private float time = 1;
    private int moveSpeed = 1;
    private int hp = 5;

    public void UpdateEnemyDataAndStart(int enemyHP,int speed,WaypointsSO wP)
    {
        time = GameManager.Instance.time;
        GameManager.Instance.timeUpdate.AddListener(UpdateTime);
        hp = enemyHP;
        waypointSO = wP;
        moveSpeed = speed;
        StartCoroutine(MoveToTarget());
    }
    private void UpdateTime()
    {
        time = GameManager.Instance.GetTime();
    }
    
    private IEnumerator MoveToTarget()
    {
        foreach (Transform currWaypoint in waypointSO.waypointsList)
        {
            while (Vector3.Distance(transform.position, currWaypoint.position) != 0)
            {
                transform.position =
                    Vector3.MoveTowards(transform.position, currWaypoint.position, time * moveSpeed * Time.deltaTime);
                yield return null;
            }
        }
        PlayerHP.Instance.EnemyHitMe();
        GetComponent<MonsterDestroyer>().DestroyEnemy();
        yield return null;
    }

    private void OnTriggerEnter(Collider other)
    {
        ProjectileMover object1Script = other.gameObject.GetComponent<ProjectileMover>();
        if (object1Script != null)
        {
            hp -= object1Script.dmg;
            if (hp <= 0f)
            {
                GameManager.Instance.playerCash.UpdateCash(10);
                Destroy(gameObject);
            }
        }
    }
}
