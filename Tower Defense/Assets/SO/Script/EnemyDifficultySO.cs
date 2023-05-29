using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyDiffSO", menuName = "MySO/Enemies")]
public class EnemyDifficultySO : ScriptableObject
{
    public float moveSpeed;
    public int hp;
}