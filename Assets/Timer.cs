using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private TextMeshProUGUI text;

    private float startTime;
    private float currentTime = 0f;

    private bool isGoing;
    // Start is called before the first frame update
    void Start()
    {
        isGoing = true;
        text = GetComponent<TextMeshProUGUI>();
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(isGoing)
        {
            currentTime = Time.time - startTime;

            string minutes = ((int) currentTime / 60).ToString();
            string seconds = (currentTime % 60).ToString("f2");

            text.text = minutes + ":" + seconds;
        }
    }

    public void Stop()
    {
        isGoing = false;
    }
}
