using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    public EnemyDifficultySO enemySO;
    public WaypointsSO waypointSO;

    public float moveSpeed = 5f;

    private void Start()
    {
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
}
