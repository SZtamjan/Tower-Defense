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
    public void UpdateWarningonUI(string warningText) //Maybe will be used to display different warning texts
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
    
    public IEnumerator DisplayText()
    {
        warningText.alpha = 1;
        yield return new WaitForSeconds(1f);
        warningText.alpha = 0;
        yield return null;
    }
    
    #endregion

    #region Buy/Upgrade Menus
    
    public void UpdateHPonUI(string hpText)
    {
        this.hpText.text = "Your HP: " + hpText;
    }
    #endregion
    
}
