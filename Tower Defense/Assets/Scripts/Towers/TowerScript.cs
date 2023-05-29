using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class TowerScript : MonoBehaviour
{
    [Header("SO")]
    public TurretType turretTypeSO;
    public ProjectilesSO projectilesSO;
    
    [Header("Tower")]
    public float range = 5f;
    public Transform whereSpawnProj;
    public Transform part;
    public bool toShoot = false;
    
    [Header("Target/Enemy")]
    public Transform target;
    public LayerMask lejerMask;
    
    private float shootingSpeed;

    private void Start()
    {
        shootingSpeed = turretTypeSO.shootingSpeed;
        StartCoroutine(ShootAt());
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
            toShoot = true;
            target = enemy.transform;
        }
        else
        {
            toShoot = false;
            target = null;
        }
    }

    private void Update()
    {
        if(target==null)
            return;

        Vector3 dir = target.position - transform.position;
        Quaternion whereLook = Quaternion.LookRotation(dir);
        //Vector3 actualRotation = Quaternion.Lerp(part.rotation,whereLook, Time.deltaTime*10f).eulerAngles;
        Vector3 actualRotation = whereLook.eulerAngles;
        part.rotation = Quaternion.Euler(0f,actualRotation.y-90f,0f);
    }
    private IEnumerator ShootAt()
    {
        while (true)
        {
            if (toShoot)
            {
                GameObject bulletGO = Instantiate(projectilesSO.grayProj[0], whereSpawnProj.position, Quaternion.identity);
                ProjectileMover mover = bulletGO.GetComponent<ProjectileMover>();
                
                if(target!=null)
                {
                    mover.GoTo(target);
                }
            }
            yield return new WaitForSeconds(shootingSpeed);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
