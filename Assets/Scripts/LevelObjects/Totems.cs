using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Totems : MonoBehaviour
{
    [SerializeField]
    GameObject player;
    bool carry;
    

    // Start is called before the first frame update
    //this will need a photonview later
    //as will the other level objects
    void Start()
    {
        carry = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Carry = " + carry); 
        
    }

    private void FixedUpdate()
    {
        
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        //picking up the totem
        if(Input.GetButtonDown("Pickup") && other.CompareTag("Player") && !carry){
            Debug.Log("Picked up!");
            this.transform.position = new Vector3(other.gameObject.transform.position.x, other.gameObject.transform.position.y + 0.25f, 
            other.gameObject.transform.position.z);
            this.transform.parent = other.gameObject.transform;
            carry = true;
        }
        else if(Input.GetButtonDown("Pickup") && carry){
            Debug.Log("Put down!");
            this.transform.position = new Vector3(other.gameObject.transform.position.x + other.gameObject.GetComponent<DebugMovement>().directionFacing, 
                other.gameObject.transform.position.y , 
                other.gameObject.transform.position.z);
            this.transform.parent = null;
            carry = false;
        }
        
    }

    private void OnTriggerExit2D(Collider2D other) {
        /*if(other.CompareTag("Player")){
            this.transform.parent = null;
        }*/
    }
}
