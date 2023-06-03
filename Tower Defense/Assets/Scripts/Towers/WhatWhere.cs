using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhatWhere : MonoBehaviour
{
    [Header("Tier1")] 
    public Vector3 gunPos;
    
    [Header("Tier2")]
    public Vector3 gunTwoPosOne;
    public Vector3 gunTwoPosTwo;
    
    [Header("Tier3")]
    public Vector3 gunThreePosOne;
    public Vector3 gunThreePosTwo;
    public Vector3 gunThreePosThree;

    [Header("Tier4")] 
    public Vector3 gunFourPos;
    
    [Header("Mesh Filters")]
    public MeshFilter baseF;
    public MeshFilter baseElementF;
    public MeshFilter pylonF;
    public MeshFilter pylonElementF;
    public MeshFilter turretF;
    public MeshFilter gunOneF;
    public MeshFilter gunTwoF;
    public MeshFilter gunThreeF;

    [Header("Mesh Renderers")]
    public MeshRenderer baseR;
    public MeshRenderer baseElementR;
    public MeshRenderer pylonR;
    public MeshRenderer pylonElementR;
    public MeshRenderer turretR;
    public MeshRenderer gunOneR;
    public MeshRenderer gunTwoR;
    public MeshRenderer gunThreeR;


    #region UpdateGunPos


    public void UpdateGunPos(int tier)
    {
        Debug.Log(tier);
        if (tier == 1) UpdateGunOne();
        if (tier == 2) UpdateGunTwo();
        if (tier == 3) UpdateGunThree();
        if (tier == 4) UpdateGunFour();
    }

    private void UpdateGunOne()
    {
        gunOneF.gameObject.transform.localPosition = gunPos;
    }    
    private void UpdateGunTwo()
    {
        gunTwoF.mesh = gunOneF.mesh;
        gunTwoR.material = gunOneR.material;
        
        gunOneF.gameObject.transform.localPosition = gunTwoPosOne;
        gunTwoF.gameObject.transform.localPosition = gunTwoPosTwo;
    }    
    private void UpdateGunThree()
    {
        gunTwoF.mesh = gunOneF.mesh;
        gunTwoR.material = gunOneR.material;
        gunThreeF.mesh = gunOneF.mesh;
        gunThreeR.material = gunOneR.material;
        
        gunOneF.gameObject.transform.localPosition = gunThreePosOne;
        gunTwoF.gameObject.transform.localPosition = gunThreePosTwo;
        gunThreeF.gameObject.transform.localPosition = gunThreePosThree;
    }    
    private void UpdateGunFour()
    {
        gunOneF.gameObject.transform.localPosition = gunFourPos;
    }

    #endregion
}
