using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerScript : MonoBehaviour
{
    public float range = 5f;
    
    public Transform rot;
    public Transform part;
    private void Update()
    {
        Vector3 dir = rot.position - transform.position;
        Quaternion whereLook = Quaternion.LookRotation(dir);
        Vector3 actualRotation = whereLook.eulerAngles;
        part.rotation = Quaternion.Euler(0f,actualRotation.y+90f,90f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
