using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerMovement : MonoBehaviourPunCallbacks, IPunObservable
{

    #region IPunObservable Implementation
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info){
        if(stream.IsWriting){
            stream.SendNext(isJumping);
        }
        else{
            this.isJumping = (bool)stream.ReceiveNext();
        }
    }
    #endregion

    //COMPONENTS
    public static GameObject LocalPlayerInstance;
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

    //Photon Configs go in Awake() so that Start() can be called successfully.
    void Awake(){
        if(photonView.IsMine){
            PlayerMovement.LocalPlayerInstance = this.gameObject;
        }
        DontDestroyOnLoad(this.gameObject);

    }
    void Start() {
        this.gameObject.name = "P" + PhotonNetwork.PlayerList.Length.ToString();
        rb = GetComponent<Rigidbody2D>();
		gv = GameObject.Find("EventSystem").GetComponent<GlobalVars>();
        
    }

    void Update() {
        if(!photonView.IsMine && PhotonNetwork.IsConnected){
            return;
        }

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
