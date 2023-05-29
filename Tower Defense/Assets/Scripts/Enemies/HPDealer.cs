using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPDealer : MonoBehaviour
{
    public EnemyDifficultySO enemySO;
    private int hp;

    private void Start()
    {
        hp = enemySO.hp;
    }


}
