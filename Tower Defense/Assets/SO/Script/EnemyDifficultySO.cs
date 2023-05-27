using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "enemySO", menuName = "MySO/Enemies")]
public class EnemyDifficultySO : ScriptableObject
{
    public int type;
    public float dmg;
}