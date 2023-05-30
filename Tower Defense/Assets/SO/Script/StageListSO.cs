using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StageList", menuName = "MySO/Stage/StageList")]
public class StageListSO : ScriptableObject
{
    public int AddEnemy = 3;
    public List<StageInfoSO> stageInfoList;
}
