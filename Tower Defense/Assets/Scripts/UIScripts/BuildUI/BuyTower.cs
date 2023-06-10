using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyTower : MonoBehaviour
{
    private void OnMouseDown()
    {
        GameManager.Instance.GetComponent<BuildTower>().BuyTowerNew();
        Debug.Log("clikneoo");
    }
}