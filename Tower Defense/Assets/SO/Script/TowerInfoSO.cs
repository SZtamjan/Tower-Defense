using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TurretInfoSO", menuName = "MySO/Turrets/Turret")]
public class TowerInfoSO : ScriptableObject
{
    public int towerTier;
    public int cost;
    public float shootingSpeed;
    public Mesh turret, gun;
    public Material material;
}
