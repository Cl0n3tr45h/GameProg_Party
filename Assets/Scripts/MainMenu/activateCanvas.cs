using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR;

public class activateCanvas : MonoBehaviour
{

    public List<GameObject> Canvasses = new List<GameObject>();
    public EventSystem EventSystem;

    public bool IsStarting;

    public GameObject ButtonForWhenIsStartingTrue;
    // Start is called before the first frame update
    void Start()
    {
    }

    public void ChangeState()
    {

        foreach (var canvas in Canvasses)
        {
            canvas.SetActive(!canvas.activeSelf);
        }

        if (IsStarting)
        {
            EventSystem.SetSelectedGameObject(ButtonForWhenIsStartingTrue);
        }
    }
}
