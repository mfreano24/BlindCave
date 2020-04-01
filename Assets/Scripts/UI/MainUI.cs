using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{


    /*
    * 
    *
    *
    */

    [SerializeField]
    GameObject pause;
    [SerializeField]
    GameObject infoText;
    GlobalVars gv;

    int currLv;

    void Start(){
        pause.SetActive(false);
        gv = GameObject.Find("EventSystem").GetComponent<GlobalVars>();
        clearInfoText();
        currLv = 0;
        levelText();
    }
    void Update(){

        if(Input.GetButtonDown("Pause") && pause.activeSelf){
            pause.SetActive(false);
        }
        else if(Input.GetButtonDown("Pause") && !pause.activeSelf){
            pause.SetActive(true);
        }

        if(gv.level != currLv){
            currLv = gv.level;
            levelText();
        }

        
    }


    public IEnumerator drawText(string str){
        Debug.Log("drawText() is running on string " + str);
        Text t = infoText.GetComponent<Text>();
        for(int i = 0; i < str.Length; i++){
            t.text += str[i];
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void clearInfoText(){
        infoText.GetComponent<Text>().text = "";
    }

    public void levelText(){
        clearInfoText();
        Debug.Log("levelText is running...");
        switch(gv.level){
            case 0:
                Debug.Log("First Level");
                StartCoroutine(drawText("1.\nIt's a little hard to see in here... Perhaps we should both stand on those white buttons to move onwards.\n(You can see your friends' cave. Guide them to the button.)"));
                break;
            case 1:
                StartCoroutine(drawText("2.\nGlad that worked. Looks like talking to each other is our best way outta here."));
                break;

            case 2:
                StartCoroutine(drawText("3.\nSomething's moving in here, but I don't think I can see it..."));
                break;

            case 3:
                StartCoroutine(drawText("4.\nMore of those floaty ones."));
                break;
            default:
                Debug.Log("Not implemented");
                break;
        }
    }
}
