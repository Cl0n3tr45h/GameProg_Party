using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.Mathematics;
using UnityEngine;


public class CamController : MonoBehaviour
{

    private CinemachineFreeLook cam;

    public float NearFieldOfView = 30;
    public Transform NearLookAtPoint;
    public float FarFieldOfView = 40;
    public Transform FarLookAtPoint;
    
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<CinemachineFreeLook>();
    }

    public void DoZoomIn()
    {
        cam.m_Lens.FieldOfView = NearFieldOfView;
        cam.m_LookAt = NearLookAtPoint;
    }

    public void DoZoomOut()
    {
        cam.m_Lens.FieldOfView = FarFieldOfView;
        cam.m_LookAt = FarLookAtPoint;
    }

}
