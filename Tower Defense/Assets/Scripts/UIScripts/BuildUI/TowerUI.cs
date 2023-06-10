using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerUI : MonoBehaviour
{
    public static TowerUI Instance;

    public GameObject buy;
    public GameObject upgrade;
    
    public Transform me;
    private Quaternion rotOffset = Quaternion.Euler(270, 0, 180);

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        Vector3 dir = me.position - transform.position;
        Quaternion whereLook = Quaternion.LookRotation(dir);
        whereLook *= rotOffset;
        transform.rotation = Quaternion.Slerp(transform.rotation, whereLook, Time.deltaTime*20f);
    }
}
