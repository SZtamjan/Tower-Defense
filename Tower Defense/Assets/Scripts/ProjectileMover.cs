using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectileMover : MonoBehaviour
{
    private Transform targetPos;
    public ProjectilesInfoSO projSO;
    public float projSpeed = 10f;
    public int dmg;
    

    private void Start()
    {
        dmg = projSO.dmg;
    }

    public void GoTo(Transform target)
    {
        if(target.position != null)
        {
            targetPos = target;
        }
    }
    
    private void Update()
    {
        if (targetPos != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos.position, projSpeed*Time.deltaTime);
            
            Vector3 dir = targetPos.position - transform.position;
            Quaternion whereLook = Quaternion.LookRotation(dir);
            Vector3 actualRotation = whereLook.eulerAngles;
            transform.rotation = Quaternion.Euler(0f,actualRotation.y-90f,-90f);
            
            if (Vector3.Distance(transform.position,targetPos.position) <= 0.1f)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
