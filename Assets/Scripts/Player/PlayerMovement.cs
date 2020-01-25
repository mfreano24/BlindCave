using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //COMPONENTS
    Rigidbody2D rb;
    //VARS
    Vector2 moveDirection;
    public float playerSpeed;
    public float jumpHeight;
     
    public float hSpeed = 5f;
    public float jumpPower = 15f;
    private int directionFacing = 1;
    private bool isJumping = false;

    void Start(){
        rb = GetComponent<Rigidbody2D>();
    }
    void Update(){
        rb.velocity = new Vector3((Input.GetAxis("Horizontal") * hSpeed), rb.velocity.y, 0);
        //if grounded
        if(rb.velocity.y != 0){
            isJumping = true;
        }
        else if (!isJumping) {  
            //Jump
            if (Input.GetButton("Jump")) {
                isJumping = true;
                rb.velocity = new Vector3(rb.velocity.x, jumpHeight, 0);
            }
        }
        if(rb.velocity.y == 0){
            isJumping = false;
        }
        
    }


    
}
