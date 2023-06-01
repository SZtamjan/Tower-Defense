using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    private WaypointsSO waypointSO;

    private int moveSpeed = 1;
    private int hp = 5; //Jeszcze nie zrobione przypisywanie hp z listy (hp jest w liście StageList i jest zależne od Stage), to trzeba zrobić dopiero po mechanice stage'y

    public void UpdateEnemyDataAndStart(int enemyHP,int speed,WaypointsSO wP)
    {
        hp = enemyHP;
        waypointSO = wP;
        moveSpeed = speed;
        StartCoroutine(MoveToTarget());
    }
    
    private IEnumerator MoveToTarget()
    {
        foreach (Transform currWaypoint in waypointSO.waypointsList)
        {
            while (Vector3.Distance(transform.position, currWaypoint.position) != 0)
            {
                transform.position =
                    Vector3.MoveTowards(transform.position, currWaypoint.position, moveSpeed * Time.deltaTime);
                yield return null;
            }
        }
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
                Destroy(gameObject);
            }
        }
    }
}
