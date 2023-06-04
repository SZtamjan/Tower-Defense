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
        Debug.Log("Kupiłem coś za: " + value);
        cash += value;
        uiUpdate.UpdateCashText(cash.ToString());
    }

    public bool CheckIfCanBuy(int cost)
    {
        if (cost <= cash)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
