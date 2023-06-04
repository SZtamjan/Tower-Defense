using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class BuildTower : MonoBehaviour
{
    public LayerMask placeableLayer;
    public Camera mainCam;

    [Header("UI Elements")] 
    public Canvas canvas;
    public GameObject upgradeMenu;
    public GameObject buyMenu;

    [Header("BuildTurret")] 
    public GameObject turret;
    public LayerMask turretLayer;
    public KeyCode LMB;
    public KeyCode RMB;
    private TowerListSO towerList;
    private string[] towerTierTag = new string [4]{"Tier1","Tier2","Tier3","Tier4"};
    
    //Save Data To Upgrade
    private int whatTierIClicked = 0;
    
    //Initialize Data
    private int newTier = 0;
    private GameObject placedTower;
    private Vector3 whereSpawnTower;
    
    public UnityEvent OnClicked;
    public UnityEvent RMBClicked;
    
    //UI
    private UIUpdate uiUpdate;

    private void Start()
    {
        uiUpdate = GameManager.Instance.canvas.GetComponent<UIUpdate>();
        OnClicked.AddListener(Clicked);
        RMBClicked.AddListener(CloseBothMenu);
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(LMB))
        {
            OnClicked.Invoke();
        }

        if (Input.GetKeyDown(RMB))
        {
            RMBClicked.Invoke();
        }
    }

    private void Clicked()
    {
        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 200,placeableLayer))
        {
            Vector2 clickPosition = Input.mousePosition;
            
            //Vector3 worldPosition = Camera.main.ScreenToWorldPoint(clickPosition);
            Vector2 pos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)canvas.transform, clickPosition,
                canvas.worldCamera, out pos);
            
            
            Transform clickedObject = hit.collider.gameObject.transform;
            Vector3 fixedPos = new Vector3(clickedObject.position.x, clickedObject.position.y + 0.29f,
                clickedObject.position.z);
            //Sprawdź jaki tower się znajduje (od najlepszego sprawdzaj w dół) i postaw lepszy jak można


            Collider[] turrets = Physics.OverlapSphere(fixedPos, 0.2f, turretLayer);

            if (turrets.Length > 0)
            {
                Debug.Log("coś tu sie znajduje");
                OpenUpgradeMenu(pos);
                UpgradeBuyDataInitialize(fixedPos,turrets[0].gameObject);
            }
            else
            {
                UpgradeBuyDataInitialize(fixedPos);
                OpenBuyMenu(pos);
            }
        }
    }
    
    private int CheckWhichTier(GameObject unknowTurret)
    {
        int tierNo = 0;
        if (unknowTurret.tag == "Tier4")
        {
            Debug.Log("Max Tier, can't Upgrade");
            tierNo = 4;
        }
        else if (unknowTurret.tag == "Tier3")
        {
            Debug.Log("Checked, it's tier3");
            tierNo = 3;
        }
        else if (unknowTurret.tag == "Tier2")
        {
            Debug.Log("Checked, it's tier2");
            tierNo = 2;
        }
        else if (unknowTurret.tag == "Tier1")
        {
            Debug.Log("Checked, it's tier1");
            tierNo = 1;
        }

        return tierNo;
    }


    #region Menu Management
    
    public void UpgradeTower()
    {
        int fixedNewTier = newTier + 1;
        Debug.Log("Nowy tier " + fixedNewTier);
        Debug.Log("Max tierów " + towerList.towerTiers.Count);
        if (fixedNewTier > towerList.towerTiers.Count)
        {
            StartCoroutine(DisplayText());
        }
        else
        {
            Destroy(placedTower);
            PlaceTower(whereSpawnTower,newTier);
        }
    }

    private IEnumerator DisplayText()
    {
        uiUpdate.warningText.alpha = 1;
        yield return new WaitForSeconds(1f);
        uiUpdate.warningText.alpha = 0;
        yield return null;
    }
    public void BuyTower()
    {
        PlaceTower(whereSpawnTower,0);
        CloseBuyMenu();
    }
    
    private void UpgradeBuyDataInitialize(Vector3 fixedPos)
    {
        whereSpawnTower = fixedPos;
        newTier = whatTierIClicked;
    }

    private void UpgradeBuyDataInitialize(Vector3 fixedPos,GameObject currentTower)
    {
        whereSpawnTower = fixedPos;
        placedTower = currentTower.gameObject;
        whatTierIClicked = CheckWhichTier(currentTower);
        newTier = whatTierIClicked;
    }
    private void OpenUpgradeMenu(Vector2 cursorPos)
    {
        CloseBuyMenu();
        upgradeMenu.transform.position = canvas.transform.TransformPoint(cursorPos);
        upgradeMenu.SetActive(true);
    }
    private void CloseUpgradeMenu()
    {
        upgradeMenu.SetActive(false);
    }

    private void OpenBuyMenu(Vector2 cursorPos)
    {
        CloseUpgradeMenu();
        buyMenu.transform.position = canvas.transform.TransformPoint(cursorPos);
        buyMenu.SetActive(true);
    }

    private void CloseBuyMenu()
    {
        buyMenu.SetActive(false);
    }

    private void CloseBothMenu()
    {
        CloseBuyMenu();
        CloseUpgradeMenu();
    }
    
    #endregion
    
    private void PlaceTower(Vector3 towerPos, int tierNo)
    {
        GameObject physicalTurret = Instantiate(turret, towerPos, Quaternion.identity);
        
        WhatWhere turretComp = physicalTurret.GetComponent<WhatWhere>();
        TowerInfoSO towerInfo = towerList.towerTiers[tierNo];
        physicalTurret.tag = towerTierTag[tierNo];
        UpgradeBuyDataInitialize(physicalTurret.transform.position,physicalTurret);
        
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

    public void InitiateTowerData(TowerListSO towerList)
    {
        this.towerList = towerList;
    }
}
