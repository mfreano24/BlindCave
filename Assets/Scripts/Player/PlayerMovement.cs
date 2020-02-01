using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //COMPONENTS
    Rigidbody2D rb;
	GlobalVars gv;
    //VARS
    Vector2 moveDirection;
    public float playerSpeed;
    public float jumpHeight;

	bool isPlaying = true;

    public float hSpeed = 5f;
    public float jumpPower = 15f;
    private int directionFacing = 1;
    private bool isJumping = false;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
		gv = GameObject.Find("EventSystem").GetComponent<GlobalVars>();
    }
    void Update() {
		if (!isPlaying) {
			return;
		}

		float xInput = Input.GetAxis("Horizontal");
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

	// TODO: Wall jumping is possible and we need to remove it.

	private void DeathToPlayer() {
		if (name == "P1") {
			transform.position = gv.p1ResetPos[gv.level];
			gv.p1Death++;
		} else {
			transform.position = gv.p2ResetPos[gv.level];
			gv.p1Death++;
		}

		// TODO: Update UI

	}

	// Collision with Spikes
	private void OnTriggerEnter2D(Collider2D collision) {
		if (collision.name == "Hazard") {
			DeathToPlayer();
		}
	}


}
