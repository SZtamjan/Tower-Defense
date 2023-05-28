using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectileMover : MonoBehaviour
{
    private Vector3 targetPos;

    public void GoTo(Transform target)
    {
        if(target.position != null)
        {
            targetPos = target.position;
        }
    }
    
    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, 20f*Time.deltaTime);
        if (Vector3.Distance(transform.position,targetPos) <= 0.1f)
        {
            Destroy(gameObject);
        }
    }


}
