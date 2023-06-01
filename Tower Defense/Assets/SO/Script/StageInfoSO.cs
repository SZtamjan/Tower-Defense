using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StageNumber", menuName = "MySO/Stage/StageInfo")]
public class StageInfoSO : ScriptableObject
{
    public int enemySpeed;
    public int enemyHP;
    public float spawnFrequency;
    public GameObject enemyPrefab;
}
