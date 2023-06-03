using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TurretListSO", menuName = "MySO/Turrets/TurretList")]
public class TowerListSO : ScriptableObject
{
    public Mesh Base, baseElement, pylon, pylonElement;
    public List<TowerInfoSO> towerTiers;
}
