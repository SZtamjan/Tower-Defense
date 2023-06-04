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
    private float time = 1;

    private void Start()
    {
        GameManager.Instance.timeUpdate.AddListener(UpdateTime);
        time = GameManager.Instance.time;
        shootingSpeed = turretTypeSO.shootingSpeed;
        StartCoroutine(ShootAt());
    }
    
    private void UpdateTime()
    {
        time = GameManager.Instance.GetTime();
    }

    private IEnumerator ShootAt()
    {
        
        Debug.Log("2");
        while (true)
        {
            if (toShoot)
            {
                GameObject bulletGO = Instantiate(projectilesSO.grayProj[0], whereSpawnProj.position, Quaternion.identity);
                Debug.Log("3");
                ProjectileMover mover = bulletGO.GetComponent<ProjectileMover>();
                
                target = GetComponent<TowerScript>().target;
                mover.GoTo(target);
            }
            yield return new WaitForSeconds(shootingSpeed / time);
        }
    }
}
