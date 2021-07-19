using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class tutorialTextSroll : MonoBehaviour
{
    private TextMeshProUGUI text;
    private string[] tutorialSlides;
    private int index;

    public activateCanvas StartCanvas;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        tutorialSlides = new string[] 
            {
                
                "Spielregeln:\nStampfe dich am schnellsten zu den Diamanten runter! Die dunklen Steine sind härter und benötigen somit mehr als nur eine Stampfattacke.",
                "Steuerung:\n[Spieler 1]\t\t\t\t\t\t[Spieler 2]\n[A/D]\t\tBewegung links/rechts\t\t[J/L]\n[W]\t\tSpringen\t\t\t\t[I]\n[S]\t\t(Im Sprung) Stampfattacke\t[K]",
                "Tipp:\nAm Zenit deines Sprunges ist die Stampfattacke am Stärksten, aber pass auf! Sobald du den Zenit hinter dir lässt, ist sie nutzlos."
                
            };
        index = 0;
        text.text = tutorialSlides[index];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.J))
        {
            index--;
            ChangeText();
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.L))
        {
            index++;
            ChangeText();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCanvas.ChangeState();
        }
    }

    public void ChangeText()
    {
        if (index < 0)
        {
            index = tutorialSlides.Length - 1;
        }

        if (index > tutorialSlides.Length - 1)
        {
            index = 0;
        }

        text.text = tutorialSlides[index];

    }
}
