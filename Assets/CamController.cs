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
    public float FarFieldOfView = 60;
    
    public bool ZoomIn = false;

    private bool oldState;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<CinemachineFreeLook>();
        oldState = ZoomIn;
    }

    public void DoZoomIn()
    {
        while(cam.m_Lens.FieldOfView > NearFieldOfView)
            cam.m_Lens.FieldOfView -= 2 ;
    }

    public void DoZoomOut()
    {
        while(cam.m_Lens.FieldOfView < FarFieldOfView)
            cam.m_Lens.FieldOfView += 2;
    }

}
