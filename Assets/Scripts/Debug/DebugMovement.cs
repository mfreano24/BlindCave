using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugMovement : MonoBehaviour
{
    //MOVEMENT
    Rigidbody2D rb;
    int flipped;
    int hSpeed;
    bool isJumping;
    float jumpHeight;
    float fallMult = 2.5f;
    float lowMult = 2f;
    private Vector3 parentVelocity;
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
        if(rb.velocity.y < 0){
            //falling condition for smoother jumping
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMult-1) * Time.deltaTime;
        }
        else if(rb.velocity.y > 0 && !Input.GetButton("Jump")){
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowMult-1) * Time.deltaTime;

        }
        
    }


    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.name == "Hazard") {
			DeathToPlayer();
		}
    }

    public void MaintainMomentum()
    {   //check to see if parent is not null
        if((transform.parent != null) && (transform.parent.GetComponent<PlatformMovement>() != null))
        {
            PlatformMovement pVelocity = this.GetComponentInParent<PlatformMovement>();
            Vector3 parentVelocity = pVelocity.velocity;
            //rb.velocity = new Vector3(rb.velocity.x + pVelocity.x, jumpHeight, 0);
            Debug.Log("Platform Velocity: " + pVelocity);
        }
    }

    void DeathToPlayer(){
        transform.position = new Vector3(-8, -1.5f, -2);
    }
}
