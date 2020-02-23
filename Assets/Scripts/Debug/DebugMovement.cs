using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugMovement : MonoBehaviour
{
    Rigidbody2D rb;
    int flipped;
    int hSpeed;
    bool isJumping;
    float jumpHeight;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        flipped = 1;
        hSpeed = 5;
        isJumping = false;
        jumpHeight = 6;

        
    }

    void FixedUpdate() {
		float xInput = (flipped) * Input.GetAxis("Horizontal");
        rb.velocity = new Vector3((xInput * hSpeed), rb.velocity.y, 0);

        //if grounded
        if(rb.velocity.y != 0) {
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


    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.name == "Hazard") {
			DeathToPlayer();
		}
    }

    void DeathToPlayer(){
        transform.position = new Vector3(-8, -1.5f, -2);
    }
}
