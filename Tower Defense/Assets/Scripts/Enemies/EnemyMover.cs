using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    public EnemyDifficultySO enemySO;
    public WaypointsSO waypointSO;

    public float moveSpeed;
    private int hp;
    
    private void Start()
    {
        hp = enemySO.hp;
        moveSpeed = enemySO.moveSpeed;
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
