using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class BuildTower : MonoBehaviour
{
    public LayerMask placeableLayer;
    public Camera mainCam;

    [Header("BuildTurret")] 
    public GameObject turret;
    public LayerMask turretLayer;
    public KeyCode keyCode;
    public UnityEvent OnClicked;
    private TowerListSO towerList;
    private string towerTierTag;

    private void Start()
    {
        OnClicked.AddListener(Clicked);
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(keyCode))
        {
            OnClicked.Invoke();
        }
    }

    private void Clicked()
    {
        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 200,placeableLayer))
        {
            Transform clickedObject = hit.collider.gameObject.transform;
            Vector3 fixedPos = new Vector3(clickedObject.position.x, clickedObject.position.y + 0.29f,
                clickedObject.position.z);
            //Sprawdź jaki tower się znajduje (od najlepszego sprawdzaj w dół) i postaw lepszy jak można


            Collider[] turrets = Physics.OverlapSphere(fixedPos, 0.2f, turretLayer);
            
            if (turrets.Length > 0)
            {
                Debug.Log("coś tu sie znajduje");
            }
            else
            {
                Debug.Log("Postawiono turreta");
                towerTierTag = "Tier1";
                int tierNo = 0;
                PlaceTower(fixedPos,towerList.towerTiers[tierNo],tierNo,towerTierTag);
            }
        }
    }

    #region CheckPlaceable

    private void CheckIfTierOne()
    {
        Debug.Log("Build Tier2");
    }
    private void CheckIfTierTwo()
    {
        Debug.Log("Build Tier3");
    }

    private void PlaceTower(Vector3 towerPos,TowerInfoSO turretInfo, int tierNo,string TowerTag)
    {
        GameObject physicalTurret = Instantiate(turret, towerPos, Quaternion.identity);
        WhatWhere turretComp = physicalTurret.GetComponent<WhatWhere>();
        TowerInfoSO towerInfo = towerList.towerTiers[tierNo];
        physicalTurret.tag = TowerTag;
        

                
        //Mesh - Static
        turretComp.baseF.mesh = towerList.Base;
        turretComp.baseElementF.mesh = towerList.baseElement;
        turretComp.pylonF.mesh = towerList.pylon;
        turretComp.pylonElementF.mesh = towerList.pylonElement;
        //Mesh - Dynamic
        turretComp.turretF.mesh = towerInfo.turret;
        turretComp.gunOneF.mesh = towerInfo.gun;
        //Material
        turretComp.baseR.material = towerInfo.material;
        turretComp.baseElementR.material = towerInfo.material;
        turretComp.pylonR.material = towerInfo.material;
        turretComp.pylonElementR.material = towerInfo.material;
        turretComp.turretR.material = towerInfo.material;
        turretComp.gunOneR.material = towerInfo.material;
        
        turretComp.UpdateGunPos(towerInfo.towerTier);
        physicalTurret.GetComponent<TowerScript>().SaveData(towerInfo);
    }
    
    #endregion
    
    public void InitiateTowerData(TowerListSO towerList)
    {
        this.towerList = towerList;
    }

}
