using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerUI : MonoBehaviour
{
    public Transform me;
    private Quaternion rotOffset = Quaternion.Euler(270, 0, 180);
    private void Update()
    {
        Vector3 dir = me.position - transform.position;
        Quaternion whereLook = Quaternion.LookRotation(dir);
        whereLook *= rotOffset;
        transform.rotation = Quaternion.Slerp(transform.rotation, whereLook, Time.deltaTime*20f);
    }
}
