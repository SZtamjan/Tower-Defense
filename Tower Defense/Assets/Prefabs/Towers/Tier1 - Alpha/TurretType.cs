using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TurretSO", menuName = "MySO/TurretType")]
public class TurretType : ScriptableObject
{
    public int turretType;
    public float shootingSpeed;

}
