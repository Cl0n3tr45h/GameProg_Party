using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CamEventManager : MonoBehaviour
{
    public bool ForZoomIn;

    public UnityEvent ZoomCallIn;
    
    
    public UnityEvent ZoomCallOut;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (ForZoomIn)
        {
            ZoomCallIn.Invoke();
        }
        else
        {
            ZoomCallOut.Invoke();
        }
    }
}
