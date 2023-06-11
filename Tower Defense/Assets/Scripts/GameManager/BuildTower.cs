using UnityEngine;
using UnityEngine.Events;
public class BuildTower : MonoBehaviour
{
    public LayerMask placeableLayer;
    public Camera mainCam;

    [Header("UI Elements")] 
    private Canvas canvas;
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
    private int selectedTier = 0;
    private GameObject placedTower;
    private Vector3 whereSpawnTower;
    private int cost;
    
    public UnityEvent OnClicked;
    public UnityEvent RMBClicked;
    
    //UI
    private UIUpdate uiUpdate;
    private PlayerCash playerCash;
    //Tower UI
    private TowerUI uiTower;
    
    private void Start()
    {
        playerCash = GetComponent<PlayerCash>();
        canvas = GameManager.Instance.canvas.GetComponent<Canvas>();
        uiTower = TowerUI.Instance.GetComponent<TowerUI>();
        uiUpdate = GameManager.Instance.canvas.GetComponent<UIUpdate>();
        OnClicked.AddListener(Clicked);
        RMBClicked.AddListener(uiTower.CloseEntireUI);
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

        if (Physics.Raycast(ray, out hit, 200))
        {
            if (((1<< hit.collider.gameObject.layer) & placeableLayer) != 0)
            {
                Transform clickedObject = hit.collider.gameObject.transform;
                Vector3 fixedPos = new Vector3(clickedObject.position.x, clickedObject.position.y + 0.29f,
                    clickedObject.position.z);

                Collider[] turrets = Physics.OverlapSphere(fixedPos, 0.2f, turretLayer);

                if (turrets.Length > 0)
                {
                    Debug.Log("coś tu sie znajduje");
                    UpgradeDataInitialize(fixedPos,turrets[0].gameObject);
                    uiTower.OpenUpgrade(hit.transform.position);
                }
                else
                {
                    BuyDataInitialize(fixedPos);
                    uiTower.OpenBuy(hit.transform.position);
                }
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


    #region Buy/Upgrade Management
    
    private void BuyDataInitialize(Vector3 fixedPos)
    {
        UpdateCost(towerList.towerTiers[0].cost);
        whereSpawnTower = fixedPos;
        selectedTier = whatTierIClicked;
    }
    
    private void UpgradeDataInitialize(Vector3 fixedPos,GameObject currentTower)
    {
        whereSpawnTower = fixedPos;
        placedTower = currentTower.gameObject;
        whatTierIClicked = CheckWhichTier(currentTower);
        Debug.Log("Checked Tier to upgrade from: " + whatTierIClicked);
        if (whatTierIClicked == towerList.towerTiers.Count)
        {
            uiTower.UpdateUpgradeCostDisplayMAX();
        }
        else if (whatTierIClicked < towerList.towerTiers.Count)
        {
            UpdateCost(towerList.towerTiers[whatTierIClicked].cost);
        }
        selectedTier = whatTierIClicked;
    }

    private void UpdateCost(int towerCost)
    {
        cost = towerCost;
        uiTower.UpdateGainDisplay(cost.ToString());
        uiTower.UpdateUpgradeCostDisplay(cost.ToString());
    }
    
    public void UpgradeTower()
    {
        int fixedNewTier = selectedTier + 1;
        if (fixedNewTier > towerList.towerTiers.Count)
        {
            StartCoroutine(uiUpdate.DisplayText());
        }
        else
        {
            int cost = towerList.towerTiers[selectedTier].cost;
            bool canIBuy = playerCash.CheckIfCanBuy(cost);

            if (canIBuy)
            {
                playerCash.UpdateCash(-cost);
                Destroy(placedTower);
                PlaceTower(whereSpawnTower, selectedTier);
            }
        }
    }

    public void BuyTowerNew()
    {
        int cost = towerList.towerTiers[0].cost;
        bool canIBuy = playerCash.CheckIfCanBuy(cost);

        if (canIBuy)
        {
            playerCash.UpdateCash(-cost);
            PlaceTower(whereSpawnTower, 0);
            uiTower.CloseBuyOpenUpgrade();
        }
    }
    #endregion
    
    private void PlaceTower(Vector3 towerPos, int tierNo)
    {
        GameObject physicalTurret = Instantiate(turret, towerPos, Quaternion.identity);
        
        WhatWhere turretComp = physicalTurret.GetComponent<WhatWhere>();
        TowerInfoSO towerInfo = towerList.towerTiers[tierNo];
        physicalTurret.tag = towerTierTag[tierNo];
        UpgradeDataInitialize(physicalTurret.transform.position,physicalTurret);
        
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
