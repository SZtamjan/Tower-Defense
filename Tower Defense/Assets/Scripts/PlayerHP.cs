using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    public static PlayerHP Instance;

    private UIUpdate uiUpdate;

    [SerializeField] private int playerHP = 10;

    public void Awake()
    {
        Instance = this;
    }

    public void Start()
    {
        uiUpdate = GameManager.Instance.canvas.GetComponent<UIUpdate>();
        uiUpdate.UpdateHPonUI(playerHP.ToString());
    }

    public void EnemyHitMe()
    {
        playerHP--;
        uiUpdate.UpdateHPonUI(playerHP.ToString());
    }
}
