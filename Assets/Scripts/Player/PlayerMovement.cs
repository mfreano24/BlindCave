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
    public GameObject lightPrefab;
    GameObject light_inst;
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

        /*
        on network, we want the local player's light to be small and to follow them and the nonlocal player's light to be big and light up the whole room.
        */
        if(this.gameObject.name == "P1"){
            this.transform.position = new Vector3(-8.3f, 11f, -1f);

        }

        else if(this.gameObject.name == "P2"){
            this.transform.position = new Vector3(-8.3f, -8f, -1f);
        }


        if(PlayerMovement.LocalPlayerInstance.gameObject.name == "P1"){
            //p1 light
            light_inst = Instantiate(lightPrefab);
            light_inst.gameObject.name = "P1_light";
            light_inst.GetComponent<LightBall>().isPlayerLight(true);
            light_inst.transform.parent = GameObject.Find("P1").transform;
            Light lb =  light_inst.GetComponent<Light>();
            lb.areaSize = new Vector2(2.75f,2.75f);
            lb.color = new Color(242f/255f, 216f/255f, 114f/255f);
            lb.intensity = 1;

            light_inst = Instantiate(lightPrefab);
            light_inst.gameObject.name = "P2_light";
            light_inst.GetComponent<LightBall>().isPlayerLight(false);
            light_inst.transform.parent = GameObject.Find("P2_Camera").transform;
            lb =  light_inst.GetComponent<Light>();
            lb.areaSize = new Vector2(20,20);
            lb.intensity = 5;
        }

        else if(PlayerMovement.LocalPlayerInstance.gameObject.name == "P2"){

        }

        
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
