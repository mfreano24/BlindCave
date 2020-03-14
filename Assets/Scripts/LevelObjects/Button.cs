using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            other.gameObject.GetComponent<PlayerMovement>().onButton = true;
            this.gameObject.GetComponent<SpriteRenderer>().color = new Color(0,0,1); //signify change lol
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("Player")){
            other.gameObject.GetComponent<PlayerMovement>().onButton = false;
            this.gameObject.GetComponent<SpriteRenderer>().color = new Color(1,1,1);
        }
    }

}
