using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(FileHandler))]
public class WinGame_StampfGraben : MonoBehaviour
{
    public TextMeshProUGUI text;
    public Timer Timer;
    private FileHandler fileHandler;
    private activateCanvas activateCanvas;
    
    public PlayerController Player1;
    public float ScaleTime;
    public PlayerController Player2;
    
    // Start is called before the first frame update
    void Start()
    {
        fileHandler = GetComponent<FileHandler>();
        activateCanvas = GetComponent<activateCanvas>();
    }

    private void Update()
    {
        if (!Player1.enabled && Input.GetKeyDown(KeyCode.Space))
        {
            activateCanvas.ChangeState();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerController>() == Player1)
        {
            EndGame(true);
        }
        else if(other.gameObject.GetComponent<PlayerController>() == Player2)
        {
            EndGame(false);
        }
    }

    void EndGame(bool HasPlayer1Won)
    {
        Player1.enabled = false;
        Player2.enabled = false;
        //consider Win/Loose animations
        //Maybe first when there's actual characters tho
        text.text = HasPlayer1Won ? "PLAYER 1 \n WON!!!" : "PLAYER 2 \n WON!!!";
        LeanTween.scale(text.rectTransform, new Vector3(2f, 2f, 2f), ScaleTime);
        string finaltime = Timer.gameObject.GetComponent<TextMeshProUGUI>().text;
        Timer.Stop();
        fileHandler.CalculateHighScore(finaltime);
    }
}
