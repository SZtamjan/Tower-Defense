using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class TowerScript : MonoBehaviour
{
    [Header("SO")]
    public ProjectilesSO projectilesSO;
    
    [Header("Tower")]
    public float range = 5f;

    public Transform[] whereSpawnProjTab;
    public Transform part;
    public bool toShoot = false;
    
    [Header("Target/Enemy")]
    public Transform target;
    public LayerMask lejerMask;
    
    private float shootingSpeed = 1;
    private int tier;
    //Time
    private float time = 1;
    
    private void Start()
    {
        GameManager.Instance.timeUpdate.AddListener(UpdateTime);
        time = GameManager.Instance.time;
        //shootingSpeed = turretTypeSO.shootingSpeed;
        StartCoroutine(ShootAt());
        InvokeRepeating("UpdateTarget",0f,0.1f);
    }

    private void UpdateTime()
    {
        time = GameManager.Instance.GetTime();
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

    private void SpawnAt(int fromWhereShoot)
    {
        GameObject bulletGO = Instantiate(projectilesSO.grayProj[0], whereSpawnProjTab[fromWhereShoot].position, Quaternion.identity);
        ProjectileMover mover = bulletGO.GetComponent<ProjectileMover>();

        if (target != null)
        {
            mover.GoTo(target);
        }
    }
    private IEnumerator ShootAt()
    {
        int fromWhereShoot=0;
        int fixedTier = tier - 1;
        if(tier == 1)
        {
            while (true)
            {
                if (toShoot)
                {
                    SpawnAt(fromWhereShoot);
                }
                yield return new WaitForSeconds(shootingSpeed);
            }
        }
        if(tier == 2)
        {
            while (true)
            {
                if (toShoot)
                {
                    fromWhereShoot = CheckWhereShot(fromWhereShoot,fixedTier);
                }
                yield return new WaitForSeconds(shootingSpeed);
            }
        }
        if(tier == 3)
        {
            while (true)
            {
                if (toShoot)
                {
                    fromWhereShoot = CheckWhereShot(fromWhereShoot,fixedTier);
                }
                yield return new WaitForSeconds(shootingSpeed);
            }
        }
        if(tier == 4)
        {
            while (true)
            {
                if (toShoot)
                {
                    SpawnAt(3);//Last Element from the table is dedicated for this spining gun
                }
                yield return new WaitForSeconds(shootingSpeed / time);
            }
        }
        
    }

    private int CheckWhereShot(int fromWhereShoot,int fixedTier)
    {
        SpawnAt(fromWhereShoot);
        if (fromWhereShoot < fixedTier)
        {
            fromWhereShoot++;
            return fromWhereShoot;
        }
        else
        {
            fromWhereShoot = 0;
            return fromWhereShoot;
        }
    }
    
    public void SaveData(TowerInfoSO towerInfo)
    {
        shootingSpeed = towerInfo.shootingSpeed;
        tier = towerInfo.towerTier;
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
