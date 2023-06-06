using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class TowerShooting : MonoBehaviour
{
    public TurretType turretTypeSO;
    public ProjectilesSO projectilesSO;
    public Transform whereSpawnProj;
    private Transform target;
    
    public bool toShoot = false;
    private float shootingSpeed;

    private void Start()
    {
        shootingSpeed = turretTypeSO.shootingSpeed;
        StartCoroutine(ShootAt());
    }

    private IEnumerator ShootAt()
    {
        while (true)
        {
            if (toShoot)
            {
                GameObject bulletGO = Instantiate(projectilesSO.grayProj[0], whereSpawnProj.position, Quaternion.identity);
                ProjectileMover mover = bulletGO.GetComponent<ProjectileMover>();
                
                target = GetComponent<TowerScript>().target;
                mover.GoTo(target);
            }
            yield return new WaitForSeconds(shootingSpeed);
        }
    }
}
