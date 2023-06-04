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
    public GameObject buyMenu;
    public GameObject upgradeMenu;

    private void Start()
    {
        canvas = GetComponent<Canvas>();
    }
    
    #region Text Update
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
