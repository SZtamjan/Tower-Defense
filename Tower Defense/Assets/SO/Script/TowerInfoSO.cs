using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TurretInfoSO", menuName = "MySO/Turret")]
public class TowerInfoSO : ScriptableObject
{
    public int towerTier;
    public Mesh Base, baseElement, pylon, PylonElement, turret, gunOne, gunTwo, gunThree;
}
