using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class TowerScript : MonoBehaviour
{
    public float range = 5f;
    public Transform target;

    public Transform part;
    public LayerMask lejerMask;

    private void Start()
    {
        InvokeRepeating("UpdateTarget",0f,0.5f);
    }

    void UpdateTarget()
    {
        Vector3 dwa = transform.position; 
        Collider[] enemies = Physics.OverlapSphere(dwa, range, lejerMask);

        float shortestDistance = 100f;
        GameObject enemy = null;
        
        foreach (Collider enemyCO in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemyCO.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                enemy = enemyCO.gameObject;
            }
        }
        if(enemy != null && shortestDistance < range)
        {
            GetComponent<TowerShooting>().toShoot = true;
            target = enemy.transform;
        }
        else
        {
            GetComponent<TowerShooting>().toShoot = false;
            target = null;
        }
    }

    private void Update()
    {
        if(target==null)
            return;
        
        
        Vector3 dir = target.position - transform.position;
        Quaternion whereLook = Quaternion.LookRotation(dir);
        Vector3 actualRotation = whereLook.eulerAngles;
        part.rotation = Quaternion.Euler(0f,actualRotation.y-90f,0f); 
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
