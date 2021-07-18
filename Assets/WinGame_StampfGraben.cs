using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WinGame_StampfGraben : MonoBehaviour
{
    public TextMeshProUGUI text;
    public PlayerController Player1;
    public float ScaleTime;
    public PlayerController Player2;
    
    // Start is called before the first frame update
    void Start()
    {
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
    }
}
