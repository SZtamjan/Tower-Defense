using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCash : MonoBehaviour
{
    public int cash = 100;

    private UIUpdate uiUpdate;
    
    private void Start()
    {
        uiUpdate = GameManager.Instance.canvas.GetComponent<UIUpdate>();
        UpdateCash(0);
    }

    public void UpdateCash(int value)
    {
        cash += value;
        uiUpdate.UpdateCashText(cash.ToString());
    }
}
