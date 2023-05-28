using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clickable : MonoBehaviour
{
    public Camera mainCam;

    public GameObject turretPrefab;

    public LayerMask placeableLayer;
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 200,placeableLayer))
            {
                Transform clickedObject = hit.collider.gameObject.transform;
                Vector3 fixedPos = new Vector3(clickedObject.position.x, clickedObject.position.y + 0.29f,
                    clickedObject.position.z);
                Instantiate(turretPrefab, fixedPos, Quaternion.identity);
            }
        }
    }
}
