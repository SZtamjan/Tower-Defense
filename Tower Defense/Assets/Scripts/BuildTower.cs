using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class BuildTower : MonoBehaviour
{
    public LayerMask placeableLayer;
    public Camera mainCam;
    public GameObject turretPrefab;


    [Header("BuildTurret")] 
    public GameObject turret;
    public KeyCode keyCode;
    public UnityEvent OnClicked;

    private void Start()
    {
        OnClicked.AddListener(Clicked);
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(keyCode))
        {
            OnClicked.Invoke();
        }
    }

    private void Clicked()
    {
        Debug.Log("Clicked");
        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 200,placeableLayer))
        {
            Transform clickedObject = hit.collider.gameObject.transform;
            Vector3 fixedPos = new Vector3(clickedObject.position.x, clickedObject.position.y + 0.29f,
                clickedObject.position.z);
            Instantiate(turret, fixedPos, Quaternion.identity);
        }
    }


}
