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

        if (Input.GetButtonDown("Pickup"))
        {
            Debug.Log("Pickup Pressed");
        }


    }

    private void FixedUpdate()
    {
        
    }

    private void OnTriggerStay2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            Debug.Log("Other tag is player");
        }
        else
        {
            Debug.Log("Other tag is NOT player");
        }

        //picking up the totem
        if(Input.GetButtonDown("Pickup") && !carry){//other.CompareTag("Player")
            Debug.Log("Picked up!");
            this.transform.position = new Vector3(other.gameObject.transform.position.x, other.gameObject.transform.position.y + 0.25f, 
            other.gameObject.transform.position.z);
            this.transform.parent = other.gameObject.transform;
            carry = true;
        }
        else if(Input.GetButtonDown("Pickup") && carry){
            Debug.Log("Put down!");
            Debug.Log("Before drop position (" + transform.position+")");


            if(GetComponentInParent<DebugMovement>() != null)
            {
                Debug.Log("Parent component exists");
                int directionFacing = GetComponentInParent<DebugMovement>().directionFacing;
                Debug.Log("directionFacing: " + directionFacing);
                Debug.Log("Parents transform position: " + transform.parent.position.x + ", " + transform.parent.position.y + ", " + transform.parent.position.z);
                this.transform.position = new Vector3(transform.parent.position.x + directionFacing,
                    transform.parent.position.y,
                    transform.parent.position.z);

            }
            else
            {
                Debug.Log("Parent component null");
            }
            //Debug.Log("Other object direction facing: " + other.gameObject.GetComponent<DebugMovement>().directionFacing);
            //this.transform.position = new Vector3(other.gameObject.transform.position.x + other.gameObject.GetComponent<DebugMovement>().directionFacing, 
            //    other.gameObject.transform.position.y , 
            //    other.gameObject.transform.position.z);
            Debug.Log("After drop position (" + transform.position + ")");
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
