using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "waypointsSO", menuName = "MySO/Waypoints")]
public class WaypointsSO : ScriptableObject
{
    public List<Transform> waypointsList;
}
