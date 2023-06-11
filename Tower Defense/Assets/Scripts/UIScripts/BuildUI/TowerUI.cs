using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Build.Content;
using UnityEngine;

public class TowerUI : MonoBehaviour
{
    public static TowerUI Instance;

    [Header("Buttons")] 
    public Transform parent;
    public GameObject buy;
    public GameObject upgrade;
    public GameObject sell;

    [Header("Texts")] 
    public TextMeshProUGUI sellText;
    public TextMeshProUGUI upgradeText;
    public TextMeshProUGUI buyText;

    [Header("Rotation Towards")]
    public Transform me;
    private Quaternion rotOffset = Quaternion.Euler(270, 0, 180);

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        Vector3 dir = me.position - transform.position;
        Quaternion whereLook = Quaternion.LookRotation(dir);
        whereLook *= rotOffset;
        transform.rotation = Quaternion.Slerp(transform.rotation, whereLook, Time.deltaTime*20f);
    }

    #region ButtonActions

    public void CloseEntireUI()
    {
        CloseBoth();
        CloseSell();
    }
    
    
    public void CloseBoth()
    {
        CloseBuy();
        CloseUpgrade();
    }
    
    public void CloseBuyOpenUpgrade()
    {
        OpenUpgrade();
    }

    public void CloseBuy()
    {
        buy.SetActive(false);
    }

    public void CloseUpgrade()
    {
        upgrade.SetActive(false);
    }

    public void OpenBuy(Vector3 pos)
    {
        CloseEntireUI();
        parent.position = pos;
        buy.SetActive(true);
    }

    public void OpenUpgrade()
    {
        CloseEntireUI();
        upgrade.SetActive(true);
        OpenSell();
    }
    
    public void OpenUpgrade(Vector3 pos)
    {
        parent.position = pos;
        upgrade.SetActive(true);
        OpenSell();
    }

    public void OpenSell()
    {
        sell.SetActive(true);
    }
    
    public void CloseSell()
    {
        sell.SetActive(false);
    }
    
    #endregion

    #region UpdateButtonsText
    
    public void UpdateUpgradeCostDisplay(string costOfUpgradeText)
    {
        upgradeText.text = "Costs: " + costOfUpgradeText;
    }

    public void UpdateUpgradeCostDisplayMAX()
    {
        upgradeText.text = "MAXED";
    }
    public void UpdateGainDisplay(string costBuyText)
    {
        buyText.text = "Costs: " + costBuyText;
    }
    
    #endregion
    
    #region TextUpdate

    public void SellFor(string text)
    {
        sellText.text = "Gain: " + text;
    }

    public void BuyFor(string text)
    {
        buyText.text = "Costs: " + text;
    }
    
    public void UpgradeFor(string text)
    {
        upgradeText.text = "Costs: " + text;
    }
    #endregion
}
