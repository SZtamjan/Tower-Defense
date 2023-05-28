using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDestroyer : MonoBehaviour
{
    public void DestroyEnemy()
    {
        Destroy(gameObject);
    }
}
