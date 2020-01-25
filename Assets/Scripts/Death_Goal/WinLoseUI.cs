using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinLoseUI : MonoBehaviour
{
    Text winText;
    public GameObject winLose;
    public bool win;
    public bool lose;
    void Start()
    {
        winText = winLose.GetComponent<Text>();
        win = false;
        lose = false;
        winText.text = "";
    }
    void Update()
    {
        if(win){
            winText.text = "You win!\n";
        }
        
        if(lose){
            winText.text = "He's dead!\n";
        }
    }
}
