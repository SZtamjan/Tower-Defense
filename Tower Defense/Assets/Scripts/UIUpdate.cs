using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIUpdate : MonoBehaviour
{
    public TextMeshProUGUI stageNo;
    public TextMeshProUGUI warningText;
    public TextMeshProUGUI hpText;

    public void UpdateStageNoOnUI(string stageNo)
    {
        this.stageNo.text = "Stage: " + stageNo;
    }
    
    public void UpdateWarningonUI(string warningText)
    {
        this.warningText.text = warningText;
    }
    
    public void UpdateHPonUI(string hpText)
    {
        
        this.hpText.text = "Your HP: " + hpText;
    }
}
