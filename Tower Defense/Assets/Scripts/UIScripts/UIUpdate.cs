using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIUpdate : MonoBehaviour
{
    private Canvas canvas;
    public TextMeshProUGUI stageNo;
    public TextMeshProUGUI warningText;
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI cashText;
    public TextMeshProUGUI timeText;
    
    [Header("Menus")]
    
    [Header("Buy Menu")]
    public GameObject buyMenu;
    public TextMeshProUGUI costBuyText;
    
    [Header("Upgrade Menu")]
    public GameObject upgradeMenu;
    public TextMeshProUGUI costOfUpgradeText;
    public TextMeshProUGUI gainText;

    private void Start()
    {
        canvas = GetComponent<Canvas>();
    }
    
    #region Text Update

    public void UpdateUpgradeCostDisplay(string costOfUpgradeText)
    {
        this.costOfUpgradeText.text = "costs: " + costOfUpgradeText;
    }

    public void UpdateUpgradeCostDisplayMAX()
    {
        costOfUpgradeText.text = "MAXED";
    }
    public void UpdateGainDisplay(string costBuyText)
    {
        this.costBuyText.text = "costs: " + costBuyText;
    }
    public void UpdateStageNoOnUI(string stageNo)
    {
        this.stageNo.text = "Stage: " + stageNo;
    }

    public void UpdateCashText(string amount)
    {
        cashText.text = "Cash: " + amount;
    }
    public void UpdateWarningonUI(string warningText)
    {
        this.warningText.text = warningText;
    }

    public void UpdateTimeText(string time)
    {
        if (time == "1")
        {
            timeText.alpha = 0f;
        }
        else
        {
            timeText.text = "X"+time;
            timeText.alpha = 1f;
        }
        
        
    }
    
    #endregion

    public IEnumerator DisplayText()
    {
        warningText.alpha = 1;
        yield return new WaitForSeconds(1f);
        warningText.alpha = 0;
        yield return null;
    }

    #region Buy/Upgrade Menus
    
    public void UpdateHPonUI(string hpText)
    {
        
        this.hpText.text = "Your HP: " + hpText;
    }

    public void OpenBuyMenu(Vector2 cursorPos)
    {
        CloseUpgradeMenu();
        buyMenu.transform.position = canvas.transform.TransformPoint(cursorPos);
        buyMenu.SetActive(true);
    }
    public void CloseBuyMenu()
    {
        buyMenu.SetActive(false);
    }

    public void OpenUpgradeMenu(Vector2 cursorPos)
    {
        CloseBuyMenu();
        upgradeMenu.transform.position = canvas.transform.TransformPoint(cursorPos);
        upgradeMenu.SetActive(true);
    }
    
    public void CloseUpgradeMenu()
    {
        upgradeMenu.SetActive(false);
    }

    public void CloseBothMenus()
    {
        CloseUpgradeMenu();
        CloseBuyMenu();
    }
    
    #endregion
    
}
